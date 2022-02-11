#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POCloudAPI.Data;
using POCloudAPI.Entities;

namespace POCloudAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIUsersController : ControllerBase
    {
        private DataContext _context;

        public APIUsersController(DataContext context) {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIUser>>> getAPIUsers() {

            return await _context.Users.ToListAsync(); 
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<APIUser>> getAPIUsersById(long id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
