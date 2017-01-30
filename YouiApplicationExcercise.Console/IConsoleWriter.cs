namespace YouiApplicationExcercise.Console
{
    public interface IConsoleWriter
    {
        void WriteLine(string format, params object[] args);
    }
}
