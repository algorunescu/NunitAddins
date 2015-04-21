// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

namespace SampleFixtureExtension
{
    using System;

    using NUnit.Core;
    using NUnit.Core.Extensibility;

    /// <summary>
    /// MockFixtureExtensionBuilder knows how to build
    /// a MockFixtureExtension.
    /// </summary>
    [NUnitAddin(Description = "Wraps an NUnitTestFixture with an additional level of SetUp and TearDown")]
    public class RetryFixtureBuilder : ISuiteBuilder, IAddin
    {
        #region NUnitTestFixtureBuilder Overrides

        // The builder recognizes the types that it can use by the presense
        // of RetryFixtureExtensionAttribute. Note that an attribute does not
        // have to be used. You can use any arbitrary set of rules that can be 
        // implemented using reflection on the type.
        public bool CanBuildFrom(Type type)
        {            
            return Reflect.HasAttribute(type, "SampleFixtureExtension.RetryFixtureAttribute", false);
        }

        public Test BuildFrom(Type type)
        {
            var attr = (RetryFixtureAttribute)Reflect.GetAttribute(type, "SampleFixtureExtension.RetryFixtureAttribute", false);
            return new RetryFixture(type, attr.Count);
        }
        #endregion

        #region IAddin Members
        public bool Install(IExtensionHost host)
        {
            IExtensionPoint suiteBuilders = host.GetExtensionPoint("SuiteBuilders");
            if (suiteBuilders == null)
                return false;

            suiteBuilders.Install(this);
            return true;
        }
        #endregion
    }
}
