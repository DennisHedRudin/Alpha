using Domain.Models;

namespace Business.Models;

public class StatusResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public int StatusCode { get; set; }
    public IEnumerable<Status>? Result { get; set; }
}


