namespace YouiApplicationExcercise.Service.Contracts
{
    public interface IStringToModelParser<T>
    {
        T Parse(string input, char delimiter = ',');
    }
}
