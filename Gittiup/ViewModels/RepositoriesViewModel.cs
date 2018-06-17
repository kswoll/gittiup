// <copyright file="RepositoriesViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using Gittiup.Models;
using Gittiup.Utils;

namespace Gittiup.ViewModels
{
    public class RepositoriesViewModel
    {
        public ObservableCollection<RepositoryModel> Repositories { get; set; } = new ObservableCollection<RepositoryModel>();

        public RepositoriesViewModel()
        {
            using (var db = new GittiupDb())
            {
                Repositories.AddRange(db.Repositories.FindAll());
            }
        }

        public void SaveRepository(RepositoryModel repository)
        {
            bool isNew = repository.Id == 0;

            using (var db = new GittiupDb())
            {
                db.Repositories.Upsert(repository);
            }

            if (isNew)
            {
                Repositories.Add(repository);
            }
        }
    }
}