using Ninject;
using NLog;
using System;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.HavanProcessing.Extensions;
using Whp.MaisTop.HavanProcessing.Injector;

namespace Whp.MaisTop.HavanProcessing
{
    class Program
    {

        public static Logger logger = LogManager.GetCurrentClassLogger();
    public static void Main(string[] args)
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    public static async Task MainAsync(string[] args)
    {
        var kernel = new StandardKernel();
        kernel.Load<InjectorModule>();
        var havanService = kernel.Get<IHavanService>();
       
        try
        {
                Console.WriteLine("############### PROCESSANDO HIERARQUIA HAVAN ##################");
                await havanService.UpdateHierarchy();
                await havanService.DoProcessesSale();
            }
        catch (Exception ex)
        {

            Console.WriteLine("############### PROCESSAMENTO FINALIZADO COM ERRO ##################");
            logger.Fatal($"Atualização de hierarquia havan - {ex.ToLogString(Environment.StackTrace)}");
            Console.WriteLine(ex.ToLogString(Environment.StackTrace));
        }
        Console.WriteLine("############### PROCESSAMENTO FINALIZADO ##################");

    }


}
}
