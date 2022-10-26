namespace Vives.Services.Model.Extensions;

public static class ServiceMessageExtensions
{
    public static ServiceResult<T> JsonNull<T>(this ServiceResult<T> serviceResult)
    {
        serviceResult.AddJsonNull();
        return serviceResult;
    }
    
    public static ServiceResult JsonNull(this ServiceResult serviceResult)
    {
        serviceResult.AddJsonNull();
        return serviceResult;
    }

    public static ServiceResult<T> NotFound<T>(this ServiceResult<T> serviceResult, string entityName)
    {
        serviceResult.AddNotFound(entityName);
        return serviceResult;
    }
    
    public static ServiceResult NotFound(this ServiceResult serviceResult, string entityName)
    {
        serviceResult.AddNotFound(entityName);
        return serviceResult;
    }

    private static void AddJsonNull(this ServiceResult serviceResult)
    {
        serviceResult.Messages.Add(new ServiceMessage
        {
            Code = "JsonNullError",
            Message = "Impossible to parse Json.",
            Type = ServiceMessageType.Error
        });
    }

    private static void AddNotFound(this ServiceResult serviceResult, string entityName)
    {
        serviceResult.Messages.Add(new ServiceMessage
        {
            Code = "NotFound",
            Message = $"Unable to find {entityName}.",
            Type = ServiceMessageType.Error
        });
    }
}