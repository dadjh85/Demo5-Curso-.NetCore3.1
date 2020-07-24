using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Repository.UserRepository;
using Service.DtoModels.UserModel;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapperConfig;

        public UserService(IUserRepository userRepository, IMapper mapperConfig)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapperConfig = mapperConfig ?? throw new ArgumentNullException(nameof(mapperConfig));
        }


        public async Task<DtoUserGet> Get(int id)
        {
            DtoUserGet result = await _userRepository.Get(id)
                                                     .ProjectTo<DtoUserGet>(_mapperConfig.ConfigurationProvider)
                                                     .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<DtoUserGet>> GetList()
        {
            List<DtoUserGet> result = await _userRepository.GetList()
                                                           .ProjectTo<DtoUserGet>(_mapperConfig.ConfigurationProvider)
                                                           .ToListAsync();

            return result;
        }

        public async Task<int?> AddAsync(DtoUserAdd item)
        {
            User itemToAdd = _mapperConfig.Map<User>(item);

            User itemAdded = await _userRepository.AddAsync(itemToAdd);

            return itemAdded?.Id;
        }

        public async Task<int?> UpdateAsync(DtoUserUpdate item)
        {
            User itemFound = await _userRepository.FindAsync(item.Id);
            _mapperConfig.Map(item, itemFound);

            return await _userRepository.UpdateAsync(itemFound);
        }

        public async Task<int?> Delete(int id)
        {
            User itemFound = await _userRepository.FindAsync(id);
            return await _userRepository.Delete(itemFound);
        }
    }
}
