// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

namespace SampleFixtureExtension
{
    using System;
    using System.Reflection;

    using NUnit.Core;
    using NUnit.Framework;

    /// <summary>
    /// RetryFixture extends NUnitTestFixture and adds a custom setup
    /// before running TestFixtureSetUp and after running TestFixtureTearDown.
    /// Because it inherits from NUnitTestFixture, a lot of work is done for it.
    /// </summary>
    public class RetryFixture : NUnitTestFixture
    {
        private readonly int count;

        public RetryFixture(Type fixtureType, int count)
            : base(fixtureType)
        {
            this.count = count;
            // NOTE: Since we are inheriting from NUnitTestFixture we don't 
            // have to do anything if we don't want to. All the attributes
            // that are normally used with an NUnitTestFixture will be
            // recognized.
            //
            // Just to have something to do, we override DoOneTimeSetUp and 
            // DoOneTimeTearDown below to do some special processing before 
            // and after the normal TestFixtureSetUp and TestFixtureTearDown.
            // In this example, we simply display a message.
            this.Fixture = Reflect.Construct(fixtureType);

            // Locate our test methods and add them to the suite using
            // the Add method of TestSuite. Note that we don't do a simple
            // Tests.Add, because that wouldn't set the parent of the tests.
            foreach (MethodInfo method in fixtureType.GetMethods(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (method.GetCustomAttributes(typeof(TestAttribute), false).Length > 0)
                {
                    this.Add(new NUnitTestMethod(method));
                }
            }
        }

        public override TestResult Run(EventListener listener, ITestFilter filter)
        {
            var runs = 0;

            var result = new TestResult(TestName);

            while (runs < this.count + 1)
            {
                runs++;
                result = base.Run(listener, filter);

                if (result.IsSuccess)
                {
                    return result;
                }
            }

            return result;
        }
    }
}
