// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

namespace SampleFixtureExtension
{
    using System;

    /// <summary>
    /// RetryFixtureExtensionAttribute is used to identify a RetryFixture class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RetryFixtureAttribute : Attribute
    {
        public int Count { get; set; }

        public RetryFixtureAttribute(int count)
        {
            this.Count = count;
        }
    }
}
