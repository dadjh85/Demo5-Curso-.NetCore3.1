using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Repository.UserRepository;
using Service.DtoModels.UserModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.UserService
{
    public class UserService : IUserService
    {
        #region Properties

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapperConfig;

        #endregion

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
            return await _userRepository.GetList()
                                        .ProjectTo<DtoUserGet>(_mapperConfig.ConfigurationProvider)
                                        .ToListAsync();
        }

        public async Task<int?> Add(DtoUserAdd item)
        {
            User userToAdd = _mapperConfig.Map<User>(item);

            User itemAdded = await _userRepository.AddAsync(userToAdd);

            return itemAdded?.Id;
        }

        public async Task<int?> Update(DtoUserUpdate item)
        {
            User itemFound = await _userRepository.FindAsync(item.Id);
            _mapperConfig.Map(item, itemFound);

            return await _userRepository.UpdateAsync(itemFound);
        }

        public async Task<int?> Delete(int id)
        {
            User user = await _userRepository.FindAsync(id);

            return await _userRepository.Delete(user);

        }
    }
}
