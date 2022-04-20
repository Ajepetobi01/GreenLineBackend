namespace GreenLineSystems.Core.ViewModels;


public class General
{
    
}

public class MessageResult<T>
{
    public int Code { get; set; }

    public  string Message { get; set; }

    public T Data { get; set; }
}

public class PaginatedSearchResponse<T>
{
    public T model { get; set; }

    public int offset { get; set; }

    public int limit { get; set; }

    public int totalCount { get; set; }
}