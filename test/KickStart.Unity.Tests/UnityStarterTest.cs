﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using KickStart.Logging;
using Microsoft.Practices.Unity;
using Test.Core;
using Xunit;
using Xunit.Abstractions;

namespace KickStart.Unity.Tests
{
    public class UnityStarterTest
    {
        public UnityStarterTest(ITestOutputHelper output)
        {
            var writer = new DelegateLogWriter(d => output.WriteLine(d.ToString()));
            Logger.RegisterWriter(writer);
        }

        [Fact]
        public void UseUnity()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<UserUnityRegistration>()
                .UseUnity()
            );

            Kick.Container.Should().NotBeNull();
            Kick.Container.Should().BeOfType<UnityAdaptor>();
            Kick.Container.As<IUnityContainer>().Should().BeOfType<UnityContainer>();

            var repo = Kick.Container.Resolve<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();
        }

        [Fact]
        public void UseUnityInitialize()
        {
            Kick.Start(config => config
                .IncludeAssemblyFor<UserUnityRegistration>()
                .UseUnity(c => c
                    .Container(b => b.RegisterType<Employee>())
                )
            );

            Kick.Container.Should().NotBeNull();
            Kick.Container.Should().BeOfType<UnityAdaptor>();
            Kick.Container.As<IUnityContainer>().Should().BeOfType<UnityContainer>();
            
            var repo = Kick.Container.Resolve<IUserRepository>();
            repo.Should().NotBeNull();
            repo.Should().BeOfType<UserRepository>();

            var employee = Kick.Container.Resolve<Employee>();
            employee.Should().NotBeNull();
        }

    }
}
