using Ninject;
using System;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.PasswordExpiration.Injector;

namespace Whp.MaisTop.PasswordExpiration
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
            var userService = kernel.Get<IUserService>();

            Console.WriteLine("########## PROCESSANDO EXPIRACAO DE PASSWORD ############");

            try
            {
                await userService.PasswordExpiration();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
