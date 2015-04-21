namespace SampleFixtureExtension
{
    using System;

    using NUnit.Core;
    using NUnit.Core.Extensibility;

    [NUnitAddin(Type = ExtensionType.Core,
                     Name = "Reporting Addin",
                     Description = "Writes aditional information to the console.")]
    public class NunitConsoleReporter : IAddin, EventListener
    {
        public void RunStarted(string name, int testCount)
        {
            Console.WriteLine("Running asembly '{0}' containing {1} tests", name, testCount);
        }

        public void RunFinished(TestResult result)
        {
        }

        public void RunFinished(Exception exception)
        {
        }

        public void TestStarted(TestName testName)
        {
            Console.WriteLine("\nRunning test '{0}'", testName.Name);
        }

        public void TestFinished(TestResult result)
        {
            switch (result.ResultState)
            {
                    case ResultState.Success:
                        Console.WriteLine("Result: {0}\n", result.ResultState);
                        break;
                    case ResultState.Failure:
                        Console.WriteLine("Result: {0}\n{1}\n", result.ResultState, result.Message);
                        break;
                    case ResultState.Error:
                        Console.WriteLine("Result: {0} ({1})\n{2}\n", result.ResultState, result.Message, result.StackTrace);                        
                        break;
            }
        }

        public void SuiteStarted(TestName testName)
        {
            Console.WriteLine("Running suite '{0}'", testName.FullName);
        }

        public void SuiteFinished(TestResult result)
        {
        }

        public void UnhandledException(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }

        public void TestOutput(TestOutput testOutput)
        {
        }

        public bool Install(IExtensionHost host)
        {
            IExtensionPoint listeners = host.GetExtensionPoint("EventListeners");
            if (listeners == null)
                return false;

            listeners.Install(this);
            return true;
        }
    }
}
