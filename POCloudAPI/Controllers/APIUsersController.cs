#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POCloudAPI.Data;
using POCloudAPI.Entities;

namespace POCloudAPI.Controllers
{
    public class APIUsersController : BaseAPIController
    {
        private DataContext _context;

        public APIUsersController(DataContext context) {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIUser>>> getAPIUsers() {

            return await _context.Users.ToListAsync(); 
        }
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<ActionResult<APIUser>> getAPIUsersById(long id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
