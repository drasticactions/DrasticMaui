// <copyright file="MainPageViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Services;
using DrasticMaui.ViewModels;

namespace DrasticMaui.Sample.ViewModels
{
    /// <summary>
    /// Main page View Model.
    /// </summary>
    public class MainPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        /// <param name="services">IServiceProvider.</param>
        /// <param name="originalPage">Original Page.</param>
        public MainPageViewModel(IServiceProvider services, Page? originalPage = null)
            : base(services, originalPage)
        {
            this.Title = "DrasticMaui";
        }
    }
}
