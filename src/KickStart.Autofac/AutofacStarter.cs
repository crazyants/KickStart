﻿using System;
using Autofac;
using KickStart.Logging;

namespace KickStart.Autofac
{
    /// <summary>
    /// 
    /// </summary>
    public class AutofacStarter : IKickStarter
    {
        private static readonly ILogger _logger = Logger.CreateLogger<AutofacStarter>();
        private AutofacOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacStarter"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AutofacStarter(AutofacOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Runs the application KickStart extension with specified <paramref name="context" />.
        /// </summary>
        /// <param name="context">The KickStart <see cref="Context" /> containing assemblies to scan.</param>
        public void Run(Context context)
        {
            var modules = context.GetInstancesAssignableFrom<Module>();

            var builder = new ContainerBuilder();

            foreach (var module in modules)
            {
                _logger.Trace()
                    .Message("Register Autofac Module: {0}", module)
                    .Write();

                builder.RegisterModule(module);
            }

            if (_options.InitializeBuilder != null)
                _options.InitializeBuilder(builder);

            _logger.Trace()
                .Message("Create Autofac Container...")
                .Write();

            var container = builder.Build(_options.BuildOptions);

            if (_options.InitializeContainer != null)
                _options.InitializeContainer(container);

            var adaptor = new AutofacAdaptor(container);
            context.SetContainer(adaptor);
        }
    }
}