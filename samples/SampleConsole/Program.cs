using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TableStorage.UnsupportedTypes.SampleConsole.Configuration;

namespace TableStorage.UnsupportedTypes.SampleConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var providerConfigurator = new ServiceProviderConfigurator();
                var serviceProvider = await providerConfigurator.ConfigureTheWorldAsync();

                using (var applicationScope = serviceProvider.CreateScope())
                {
                    var presenter = applicationScope
                        .ServiceProvider
                        .GetRequiredService<Presenter>();

                    await presenter.RunTheShowAsync();
                }
            }
            catch (Exception e)
            {
                DisplayException(e);
            }
            finally
            {
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }

        private static void DisplayException(Exception e)
        {
            if (e == null) return;

            if (_innerExceptionCount > 0)
            {
                Console.WriteLine("\tInner exception {0}:", _innerExceptionCount);
                Console.WriteLine();
            }

            Console.WriteLine("Exception: {0}", e.GetType());
            Console.WriteLine("Message: {0}", e.Message);
            Console.WriteLine("StackTrace:");
            Console.WriteLine(e.StackTrace);
            Console.WriteLine();

            _innerExceptionCount++;

            DisplayException(e.InnerException);
        }

        private static int _innerExceptionCount = 0;
    }
}
