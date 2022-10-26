namespace Vives.Services.Model;

public class ServiceResult
{
    public IList<ServiceMessage> Messages { get; set; } = new List<ServiceMessage>();
}

public class ServiceResult<T> : ServiceResult
{
    public ServiceResult(T? data = default(T))
    {
        Data = data;
    }
    public T? Data { get; set; }
}