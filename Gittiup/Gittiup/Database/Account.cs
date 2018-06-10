// <copyright file="Account.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace Gittiup.Database
{
    public class Account : INotifyPropertyChanged
    {
        private string name;
        public int Id { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public string UserName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}