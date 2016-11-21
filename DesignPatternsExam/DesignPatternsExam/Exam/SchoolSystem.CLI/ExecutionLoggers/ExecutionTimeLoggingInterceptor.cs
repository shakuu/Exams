using System.Diagnostics;

using Ninject.Extensions.Interception;

namespace SchoolSystem.Cli.ExecutionLoggers
{
    public class ExecutionTimeLoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Request.Method.Name;
            var typeName = invocation.Request.Method.DeclaringType.Name;

            var stopwatch = new Stopwatch();

            this.PrintCallingMessage(methodName, typeName);
            stopwatch.Start();
            invocation.Proceed();
            stopwatch.Stop();

            var elapsedTime = stopwatch.ElapsedMilliseconds;
            this.PrintElapsedTimeMessage(methodName, typeName, elapsedTime);
        }

        private void PrintCallingMessage(string methodName, string typeName)
        {
            System.Console.WriteLine($"Calling method {methodName} of type {typeName}...");
        }

        private void PrintElapsedTimeMessage(string methodName, string typeName, long elapsedTime)
        {
            System.Console.WriteLine($"Total execution time for method {methodName} of type {typeName} is {elapsedTime} milliseconds.");
        }
    }
}
