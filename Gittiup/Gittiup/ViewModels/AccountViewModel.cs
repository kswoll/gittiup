// <copyright file="AddAccountViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

namespace Gittiup.ViewModels
{
    public class AccountViewModel
    {
        public AccountsViewModel AccountsViewModel { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}