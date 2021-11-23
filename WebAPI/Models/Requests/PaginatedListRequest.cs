using System.Runtime.Serialization;

namespace WebAPI.Models.Requests
{
    public class PaginatedListRequest
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
        public bool ReverseSort { get; set; } = false;
        public SortBy? SortBy { get; set; }
    }

    public enum SortBy
    {
        DoB,
        FirstName,
        LastName
    }
}
