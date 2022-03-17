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
using POCloudAPI.Interfaces;

namespace POCloudAPI.Controllers
{
    public class APIUsersController : BaseAPIController
    {
        private IUnitOfWork _UnitOfWork;

        public APIUsersController(IUnitOfWork UnitOfWork) {
            this._UnitOfWork = UnitOfWork;

        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> getAPIUsers() {
            return Ok(await _UnitOfWork.APIUserRepository.getAllUsersAsync());
        }

        [HttpGet("{username}")]
        //[Authorize]
        public async Task<ActionResult<MemberDTO>> getAPIUserByUsername(String username)
        {
            return Ok(await _UnitOfWork.APIUserRepository.getUserAsync(username));
        }
    }
}
