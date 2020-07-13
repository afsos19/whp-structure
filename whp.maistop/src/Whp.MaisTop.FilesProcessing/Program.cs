using Ninject;
using NLog;
using System;
using System.Configuration;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.FilesProcessing.Extensions;
using Whp.MaisTop.FilesProcessing.Injector;

namespace Whp.MaisTop.FilesProcessing
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
            var hierarchyFileService = kernel.Get<IHierarchyProcessesService>();
            var saleFileService = kernel.Get<ISaleProcessesService>();

            try
            {
                Console.WriteLine("############### PROCESSANDO VALIDACAO DE ESTRUTURA DE HIERARQUIA ##################");

                await hierarchyFileService.ValidateImportedStructure(ConfigurationManager.AppSettings.Get("pathFromHierarchy").ToString());

                hierarchyFileService.Dispose();

                logger.Info("Processamento de validacao de estrutura de hierarquia finalizado");

                Console.WriteLine("############### PROCESSANDO ARQUIVO DE HIERARQUIA ##################");

                await hierarchyFileService.ProcessesHierarchyFile();

                hierarchyFileService.Dispose();

                logger.Info("Processamento hierarquia finalizado");

                Console.WriteLine("############### PROCESSANDO VALIDACAO DE ESTRUTURA DE VENDA ##################");

                await saleFileService.ValidateImportedStructure(ConfigurationManager.AppSettings.Get("pathFromSale").ToString());

                logger.Info("Processamento de validacao de estrutua de arquivo de venda finalizado");

                Console.WriteLine("############### PROCESSANDO VENDAS ##################");

                await saleFileService.ProcessesSaleFile();

                logger.Info("Processamento de venda finalizado");

                Console.WriteLine("############### PROCESSANDO PONTOS ##################");

                logger.Info("Processamento de pontuacao de vendas");

                await saleFileService.ProcessesPunctuation();

                logger.Info("Processamento de pontuacao de vendas finalizado");

            }
            catch (Exception ex)
            {

                Console.WriteLine("############### PROCESSAMENTO FINALIZADO COM ERRO ##################");
                logger.Fatal("Processamento de arquivos finalizado com erro - "+ex.ToLogString(Environment.StackTrace));
                Console.WriteLine(ex.ToLogString(Environment.StackTrace));
            }
            Console.WriteLine("############### PROCESSAMENTO FINALIZADO ##################");
            
        }

        
    }
}
