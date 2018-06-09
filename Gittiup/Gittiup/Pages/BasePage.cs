// <copyright file="BasePage.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using Windows.UI.Xaml.Controls;

namespace Gittiup.Pages
{
    public class BasePage<T> : Page
    {
        private T viewModel;

        public BasePage()
        {
        }

        public T ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;
                DataContext = value;
            }
        }
    }
}