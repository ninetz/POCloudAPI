using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POCloudAPI.Entities;
using POCloudAPI.Interfaces;
using System.Security.Cryptography;
using System.Text;
using POCloudAPI.JWTokens;

namespace POCloudAPI.Data
{
    public class APIUserRepository : IAPIUserRepository
    {
        private ITokenService _tokenService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public APIUserRepository(DataContext context, IMapper mapper, ITokenService tokenService)
        {
            _mapper = mapper;
            _context = context;
            _tokenService = tokenService;
        }
        public async Task<IEnumerable<APIUser>> getAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<APIUser>>(users);
        }

        public async Task<APIUser> getUserAsync(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
            return _mapper.Map<APIUser>(user);
        }
        public async Task<string> UpdateToken(APIUser user)
        {
            user.CurrentToken = _tokenService.createToken(user);
            await _context.SaveChangesAsync();
            return user.CurrentToken;
        }
        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> updateUserLoginTime(APIUser apiUser)
        {
            if (apiUser == null) return false;
            apiUser.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddUser(APIUser user) {
            await _context.Users.AddAsync(user);
            return true;
        }
        public async Task<bool> VerifyUserIdentity(string username, string token) {
            APIUser user = await getUserAsync(username);
            // temp errormsg var, need to return error
            var errormsg = "";
            if (user == null) errormsg = ("User " + username + " doesn't exist.");
            if (user.CurrentToken == null) errormsg = ("User " + username + " doesn't have a token present in DB.");
            if (token == null) errormsg = ("User " + username + " has no token present.");
            if (token == user.CurrentToken) return true;
            return false;


        }
        public bool CheckIfPasswordsMatch(byte[] userSalt, byte[] userHash, string inputRawPassword)
        {
            using var hmac = new HMACSHA512(userSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputRawPassword));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != userHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
