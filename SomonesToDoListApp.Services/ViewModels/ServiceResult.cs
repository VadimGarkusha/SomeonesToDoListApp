using AndresToDoListApp.Services.Interfaces;

namespace AndresToDoListApp.Services.ViewModels
{
    internal class ServiceResult<T> : IServiceResult<T>
    {
        public ServiceResult(T result)
        {
            Result = result;
        }
    
        public T Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
