using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POCloudAPI.DTO;
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

        public async Task<IEnumerable<APIFileDTOSimple>> GetAllUserAPIFilesAsync(APIUser user)
        {
            
            var files = await _context.Files.Where(x => user == x.User).ToListAsync();

            return _mapper.Map<IEnumerable<APIFileDTOSimple>>(files);
        }

        public async Task<APIFile> GetAPIFileAsync(APIFileDownloadDTO ApiFileDTO,APIUser user)
        {
           
            return await _context.Files.FirstOrDefaultAsync(x => ApiFileDTO.FileName == x.FullNameOfFile && x.User == user);
        }
    }
}
