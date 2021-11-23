using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLib.Models.DataDb;
using WebAPI.Models;
using WebAPI.Models.Requests;
using WebAPI.Models.Responses;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all users in DB with pagination and order possibility
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="401">Unauthorized</response>
        [HttpGet("api/[controller]")]
        public async Task<PaginatedListResponse<List<UserResponse>>> GetUsers([FromQuery] PaginatedListRequest request)
        {
            var response = new PaginatedListResponse<List<UserResponse>>();
            try
            {
                Func<RandomUser, Object> orderByFunc = null;

                if (request.SortBy == SortBy.FirstName)
                    orderByFunc = users => users.FirstName;
                else if (request.SortBy == SortBy.LastName)
                    orderByFunc = users => users.LastName;
                else if (request.SortBy == SortBy.DoB)
                    orderByFunc = users => users.DateOfBirthday;

                var users = _context.RandomUsers;
                var sortedUsers =  request.ReverseSort ? users.OrderByDescending(orderByFunc) : users.OrderBy(orderByFunc);
                var paginatedUsers = sortedUsers.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);

                response.Data = new List<UserResponse>();
                foreach (var user in paginatedUsers)
                {
                    response.Data.Add(new UserResponse(user));
                }
                response.Pagination = new Pagination
                {
                    PageSize = request.PageSize,
                    PageIndex = request.PageIndex,
                    TotalItems = users.Count(),
                };
            }
            catch(Exception e)
            {
                response.Status.Code = 1;
                response.Status.Message = e.Message;
            }
            return response;
        }

        /// <summary>
        /// Get one user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="401">Unauthorized</response>
        [HttpGet("api/[controller]/{id}")]
        public async Task<ApiResponse<UserResponse>> GetUser(int id)
        {
            var response = new ApiResponse<UserResponse>();
            try
            {
                var user = await _context.RandomUsers.FindAsync(id);
                if(user == null)
                {
                    response.Status.Code = 1;
                    response.Status.Message = "User not found";
                    return response;
                }
                response.Data = new UserResponse(user);
            }
            catch(Exception e)
            {
                response.Status.Code = 1;
                response.Status.Message = e.Message;
            }
            return response;
        }
    }
}
