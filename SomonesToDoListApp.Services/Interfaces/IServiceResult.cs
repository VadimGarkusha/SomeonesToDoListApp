namespace AndresToDoListApp.Services.Interfaces
{
    public interface IServiceResult<T>
    {
        T Result { get; }
        string ErrorMessage { get; set; }
    }
}
