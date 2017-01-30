using Ninject;

namespace YouiApplicationExcercise.Console
{
    public interface IBootstraper
    {
        void Start(IKernel kernel);
    }
}