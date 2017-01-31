namespace YouiApplicationExcercise.Service.Contracts
{
    public interface IConsoleWriter
    {
        void WriteLine(string format, params object[] args);
    }
}
