namespace AdminBaker.Shared.Response;

public class BaseResponse
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

public class BaseResponseGeneric<T> : BaseResponse
{
    public T? Data { get; set; }
}

public class PaginationResponse<T> : BaseResponse
{
    public ICollection<T>? Data { get; set; }
    public int TotalPages { get; set; }
}