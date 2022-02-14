#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POCloudAPI.Data;
using POCloudAPI.DTO;
using POCloudAPI.Entities;

namespace POCloudAPI.Controllers
{
    public class APIUsersController : BaseAPIController
    {
        private DataContext _context;
        private readonly IMapper mapper;

        public APIUsersController(DataContext context, IMapper mapper) {
            _context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> getAPIUsers() {
            var users = await _context.Users.ToListAsync();
            return Ok(mapper.Map<IEnumerable<MemberDTO>>(users));
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> getAPIUsersByUsername(String username)
        {
            var user = await _context.Users.SingleAsync(x => x.Username == username);
            return mapper.Map<MemberDTO>(user);
        }
    }
}
