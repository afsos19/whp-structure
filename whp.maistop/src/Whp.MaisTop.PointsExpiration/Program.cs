using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ninject;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.PointsExpiration.Injector;

namespace Whp.MaisTop.PointsExpiration
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

            Console.WriteLine("########## PROCESSANDO EXPIRACAO DE PONTOS ############");

            try
            {
                await punctuation.PointsExpiration();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        
    }
}
