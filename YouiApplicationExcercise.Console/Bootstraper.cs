using Ninject;
using YouiApplicationExcercise.Model;
using YouiApplicationExcercise.Service;
using YouiApplicationExcercise.Service.Contracts;

namespace YouiApplicationExcercise.Console
{
    public class Bootstraper : IBootstraper
    {
        public void Start(IKernel kernel)
        {
            kernel.Bind<ICustomerService>().To<CustomerService>();
            kernel.Bind<IFileParser>().To<FileParser>();
            kernel.Bind<IStringToModelParser<Customer>>().To<CustomerParser>();
            kernel.Bind<IFileSystem>().To<FileSystem>();
        }
    }
}
