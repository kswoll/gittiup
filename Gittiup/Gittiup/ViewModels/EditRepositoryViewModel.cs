// <copyright file="RepositoryViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using Gittiup.Database;

namespace Gittiup.ViewModels
{
    public class EditRepositoryViewModel
    {
        public Repository Repository { get; set; }
        public ObservableCollection<Repository> Repositories { get; set; }

        public void Save()
        {
            bool isNew = Repository.Id == 0;

            using (var db = new GittiupDb())
            {
                db.Repositories.Upsert(Repository);
            }

            if (isNew)
            {
                Repositories.Add(Repository);
            }
        }
    }
}