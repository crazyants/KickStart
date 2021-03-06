﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace KickStart.SimpleInjector
{
    /// <summary>
    /// SimpleInjector options class
    /// </summary>
    public class SimpleInjectorOptions
    {
        /// <summary>
        /// Gets or sets the initialize container delegate.
        /// </summary>
        /// <value>
        /// The initialize container delegate.
        /// </value>
        public Action<Container> InitializeContainer { get; set; }
    }
}
