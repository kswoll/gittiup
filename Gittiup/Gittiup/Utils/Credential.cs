// <copyright file="Credential.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

namespace Gittiup.Utils
{
    public class Credential
    {
        public CredentialType CredentialType { get; }
        public string ApplicationName { get; }
        public string UserName { get; }
        public string Password { get; }

        public Credential(CredentialType credentialType, string applicationName, string userName, string password)
        {
            ApplicationName = applicationName;
            UserName = userName;
            Password = password;
            CredentialType = credentialType;
        }

        public override string ToString()
        {
            return string.Format("CredentialType: {0}, ApplicationName: {1}, UserName: {2}, Password: {3}", CredentialType, ApplicationName, UserName, Password);
        }
    }
}