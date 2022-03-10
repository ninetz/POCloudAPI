using AutoMapper;
using POCloudAPI.Interfaces;
using POCloudAPI.JWTokens;

namespace POCloudAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
     
        public UnitOfWork(DataContext context, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public IAPIUserRepository APIUserRepository => new APIUserRepository(_context, _mapper, _tokenService);

        public async Task<bool> PushChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            _context.ChangeTracker.DetectChanges();
            var changes = _context.ChangeTracker.HasChanges();

            return changes;
        }

    }
}
