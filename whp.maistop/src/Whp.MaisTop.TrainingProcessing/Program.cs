using Ninject;
using System;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.FilesProcessing.Injector;

namespace Whp.MaisTop.TrainingProcessing
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
            var trainingService = kernel.Get<ITrainingService>();

            Console.WriteLine("########## PROCESSANDO TREINAMENTOS ############");

            try
            {
                await trainingService.ProcessesAcademyTraining();

                await trainingService.ProcessesAcademyTrainingManagers();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

           
        }
    }
}
