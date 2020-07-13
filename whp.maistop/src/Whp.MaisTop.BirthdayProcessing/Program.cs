using Ninject;
using System;
using System.Threading.Tasks;
using Whp.MaisTop.BirthdayProcessing.Injector;
using Whp.MaisTop.Business.Interfaces;

namespace Whp.MaisTop.BirthdayProcessing
{
    class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        public static async Task MainAsync(string[] args)
        {

            var kernel = new StandardKernel();
            kernel.Load<InjectorModule>();
            var punctuation = kernel.Get<IPunctuationService>();

            Console.WriteLine("########## PROCESSANDO ANIVERSARIANTES ############");

            try
            {
                await punctuation.BirthdayProcessing();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
