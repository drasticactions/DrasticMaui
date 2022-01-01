// <copyright file="DrasticMauiSampleTrayWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Options;

namespace DrasticMaui.Sample
{
    public class DrasticMauiSampleTrayWindow : DrasticTrayWindow
    {
        public DrasticMauiSampleTrayWindow(DrasticTrayIcon icon, DrasticTrayWindowOptions? options = null)
            : base(icon, options)
        {
        }
    }
}
