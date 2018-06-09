// <copyright file="CredentialManager.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Gittiup.Utils
{
    /// <summary>
    /// CredentialManager, wraps the Windows CredMan/CredVault APIs (e.g. CredReadW/CredWriteW)
    /// </summary>
    public class CredentialManager
    {
        private static readonly string ApplicationName = "PlanGrid";
        private static readonly string ProxyName = "PlanGridProxy";

        public Credential GetCredentials()
        {
            var cred = ReadCredential(ApplicationName) ?? new Credential(CredentialType.Generic, ApplicationName, null, null);
            return cred;
        }

        public Credential GetProxyCredentials()
        {
            var cred = ReadCredential(ProxyName) ?? new Credential(CredentialType.Generic, ProxyName, null, null);
            return cred;
        }

        public void SetCredentials(string userName, string token)
        {
            WriteCredential(ApplicationName, userName?.ToLower(), token);
        }

        public void SetProxyCredentials(string username, string password)
        {
            WriteCredential(ProxyName, username, password);
        }

        private static Credential ReadCredential(string applicationName)
        {
            bool read = CredRead(applicationName, CredentialType.Generic, 0, out IntPtr nCredPtr);

            if (read)
            {
                using (CriticalCredentialHandle critCred = new CriticalCredentialHandle(nCredPtr))
                {
                    CREDENTIAL cred = critCred.GetCredential();

                    return ReadCredential(cred);
                }
            }

            return null;
        }

        private static Credential ReadCredential(CREDENTIAL credential)
        {
            string applicationName = Marshal.PtrToStringUni(credential.TargetName);
            string userName = Marshal.PtrToStringUni(credential.UserName);
            string secret = null;

            if (credential.CredentialBlob != IntPtr.Zero)
            {
                secret = Marshal.PtrToStringUni(credential.CredentialBlob, (int)credential.CredentialBlobSize / 2);
            }

            return new Credential(credential.Type, applicationName, userName, secret);
        }

        private static void WriteCredential(string applicationName, string userName, string secret)
        {
            byte[] byteArray = secret == null ? null : Encoding.Unicode.GetBytes(secret);

            if (Environment.OSVersion.Version < new Version(6, 1) /* Windows 7 */)
            {
                // XP and Vista: 512;
                if (byteArray != null && byteArray.Length > 512)
                {
                    throw new ArgumentOutOfRangeException(nameof(secret), "The secret message has exceeded 512 bytes.");
                }
            }
            else
            {
                // 7 and above: 5*512
                if (byteArray != null && byteArray.Length > 512 * 5)
                {
                    throw new ArgumentOutOfRangeException(nameof(secret), "The secret message has exceeded 2560 bytes.");
                }
            }

            CREDENTIAL credential = new CREDENTIAL();
            credential.AttributeCount = 0;
            credential.Attributes = IntPtr.Zero;
            credential.Comment = IntPtr.Zero;
            credential.TargetAlias = IntPtr.Zero;
            credential.Type = CredentialType.Generic;
            credential.Persist = (uint)CredentialPersistence.LocalMachine;
            credential.CredentialBlobSize = (uint)(byteArray?.Length ?? 0);
            credential.TargetName = Marshal.StringToCoTaskMemUni(applicationName);
            credential.CredentialBlob = Marshal.StringToCoTaskMemUni(secret);
            credential.UserName = Marshal.StringToCoTaskMemUni(userName ?? Environment.UserName);

            bool written = CredWrite(ref credential, 0);
            Marshal.FreeCoTaskMem(credential.TargetName);
            Marshal.FreeCoTaskMem(credential.CredentialBlob);
            Marshal.FreeCoTaskMem(credential.UserName);

            if (!written)
            {
                int lastError = Marshal.GetLastWin32Error();
                throw new Win32Exception(lastError, string.Format("CredWrite failed with the error code {0}.", lastError));
            }
        }

        [DllImport("Advapi32.dll", EntryPoint = "CredReadW", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool CredRead(string target, CredentialType type, int reservedFlag, out IntPtr credentialPtr);

        [DllImport("Advapi32.dll", EntryPoint = "CredWriteW", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool CredWrite([In] ref CREDENTIAL userCredential, [In] UInt32 flags);

        [DllImport("Advapi32.dll", EntryPoint = "CredFree", SetLastError = true)]
        static extern bool CredFree([In] IntPtr cred);

        private enum CredentialPersistence : uint
        {
            Session = 1,
            LocalMachine,
            Enterprise
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CREDENTIAL
        {
            public uint Flags;
            public CredentialType Type;
            public IntPtr TargetName;
            public IntPtr Comment;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
            public uint CredentialBlobSize;
            public IntPtr CredentialBlob;
            public uint Persist;
            public uint AttributeCount;
            public IntPtr Attributes;
            public IntPtr TargetAlias;
            public IntPtr UserName;
        }

        sealed class CriticalCredentialHandle : CriticalHandleZeroOrMinusOneIsInvalid
        {
            public CriticalCredentialHandle(IntPtr preexistingHandle)
            {
                SetHandle(preexistingHandle);
            }

            public CREDENTIAL GetCredential()
            {
                if (!IsInvalid)
                {
                    CREDENTIAL credential = (CREDENTIAL)Marshal.PtrToStructure(handle, typeof(CREDENTIAL));
                    return credential;
                }

                throw new InvalidOperationException("Invalid CriticalHandle!");
            }

            protected override bool ReleaseHandle()
            {
                if (!IsInvalid)
                {
                    CredFree(handle);
                    SetHandleAsInvalid();
                    return true;
                }

                return false;
            }
        }
    }
}