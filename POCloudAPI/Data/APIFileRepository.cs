using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POCloudAPI.Entities;
using POCloudAPI.Interfaces;

namespace POCloudAPI.Data
{
    public class APIFileRepository : IAPIFileRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public APIFileRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> AddAPIFileAsync(APIFile file)
        {
            await _context.Files.AddAsync(file);
            return true;
        }

        public async Task<APIFile> GetAPIFileAsync(string filename)
        {
            return await _context.Files.SingleOrDefaultAsync(x => filename == x.FullNameOfFile);
        }
    }
}
