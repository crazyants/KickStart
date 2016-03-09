﻿using System;
using KickStart.Logging;
using Microsoft.Practices.Unity;

namespace KickStart.Unity
{
    public class UnityStarter : IKickStarter
    {
        private static readonly ILogger _logger = Logger.CreateLogger<UnityStarter>();
        private readonly UnityOptions _options;

        public UnityStarter(UnityOptions options)
        {
            _options = options;
        }

        public void Run(Context context)
        {
            var modules = context.GetInstancesAssignableFrom<IUnityRegistration>();

            var container = new UnityContainer();

            foreach (var module in modules)
            {
                _logger.Trace()
                    .Message("Register Unity Module: {0}", module)
                    .Write();

                module.Register(container);
            }

            if (_options.InitializeContainer != null)
                _options.InitializeContainer(container);

            var adaptor = new UnityAdaptor(container);
            context.SetContainer(adaptor);
        }
    }
}