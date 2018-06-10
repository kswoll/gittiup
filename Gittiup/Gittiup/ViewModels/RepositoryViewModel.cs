// <copyright file="RepositoryViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using Gittiup.Database;

namespace Gittiup.ViewModels
{
    public class RepositoryViewModel
    {
        public Repository Repository { get; set; }
        public ObservableCollection<Repository> Repositories { get; set; }

        public void Delete()
        {
            using (var db = new GittiupDb())
            {
                db.Repositories.Delete(Repository.Id);
            }

            Repositories.Remove(Repository);
        }
    }
}