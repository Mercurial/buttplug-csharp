﻿using Buttplug.Core;
using Buttplug.Messages;
using NLog;
using NLog.Config;
using NLog.Targets;
using Xunit;

namespace ButtplugTest.Core
{
    internal class TestService : ButtplugService
    {
        public TestService() : base("Test Service", 100)
        {
            // Build ourselves an NLog manager just so we can see what's going on.
            DebuggerTarget dt = new DebuggerTarget();
            LogManager.Configuration = new LoggingConfiguration();
            LogManager.Configuration.AddTarget("debugger", dt);
            LogManager.Configuration.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, dt));
            LogManager.Configuration = LogManager.Configuration;

            // Send RequestServerInfo message now, to save us having to do it in every test.
            var t = SendMessage(new RequestServerInfo("TestClient"));
            t.Wait();
            Assert.True(t.Result is ServerInfo);
        }
    }
}