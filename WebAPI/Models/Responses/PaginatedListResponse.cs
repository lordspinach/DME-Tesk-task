using System;
using WebAPI.Models.Responses;

namespace WebAPI.Models.Responses
{
    public class PaginatedListResponse<T> : ApiResponse<T>
    {
        public Pagination Pagination { get; set; }
    }

    public class PaginatedListResponse : ApiResponse
    {
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages 
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
}
