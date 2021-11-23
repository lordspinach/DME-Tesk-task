using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLib.Models.DataDb;
using WebAPI.Models.Responses;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class FilesController : Controller
    {
        private readonly ApplicationContext _context;
        public FilesController(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return picture by userId and fileType
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        [HttpGet("api/[controller]/picture")]
        public async Task<FileResult> GetPicture([FromQuery] int userId, [FromQuery] FileType fileType)
        {
            var dbFile = _context.Files
                .Where(f => f.RandomUser.Id == userId && f.FileType == fileType)
                .FirstOrDefault();

            FileStreamResult fileStream = null;
            if (dbFile != null)
            {
                var stream = new MemoryStream(dbFile.Data);
                fileStream = File(stream, "image/jpeg", dbFile.Name);
            }
            return fileStream;
        }
    }
}
