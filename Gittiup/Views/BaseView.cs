﻿// <copyright file="BaseView.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Windows.Controls;

namespace Gittiup.Views
{
    public class BaseView<T> : UserControl
        where T : class
    {
        private T viewModel;

        public BaseView()
        {
        }

        public T ViewModel
        {
            get => viewModel;
            set
            {
                if (viewModel != value)
                {
                    viewModel = value;
                    DataContext = value;
                }
            }
        }
    }
}