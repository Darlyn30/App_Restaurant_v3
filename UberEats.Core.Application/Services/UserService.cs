
using System.Net.NetworkInformation;
using Microsoft.Extensions.Configuration;
using UberEats.Core.Application.Helpers;
using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Application.ViewModels.User;
using UberEats.Core.Domain.Entities;
using UberEats.Core.Domain.Settings;
using UberEats.Infrastructure.Shared.Services;

namespace UberEats.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel vm)
        {
            User user = new();
            user.IsActive = false;
            user.Name = vm.Name;
            user.Email = vm.Email;
            user.Role = vm.Role;
            user.PasswordHash = PasswordEncryptation.ComputeSha256Hash(vm.Password);

            await _userRepository.AddAsync(user);

            SaveUserViewModel userVm = new();

            userVm.Id = user.Id;
            userVm.Password = user.PasswordHash;
            userVm.Email = user.Email;
            userVm.Role = user.Role;
            userVm.isActive = user.IsActive;
            return userVm;
        }

        public async Task delete(int id)
        {
            var entity = await _userRepository.GetByIdAsync(id);
            
            await _userRepository.DeleteAsync(entity);
        }

        public Task<IEnumerable<SaveUserViewModel>> GetAllViewModel()
        {
            throw new NotImplementedException();
        }

        public Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserViewModel>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<UserViewModel> Login(LoginViewModel loginVm)
        {
            UserViewModel userVm = new();
            User user = await _userRepository.LoginAsync(loginVm);

            if (user == null)
                return null;

            userVm.Id = user.Id;
            userVm.Email = user.Email;
            userVm.Name = user.Name;
            userVm.Role = user.Role;

            return userVm;
        }

        public async Task Update(SaveUserViewModel vm)
        {
            User user = await _userRepository.GetByIdAsync(vm.Id);
            user.Id = vm.Id;
            user.Name = vm.Name;
            user.Email = vm.Email;
            user.PasswordHash = PasswordEncryptation.ComputeSha256Hash(vm.Password);
            user.Role = vm.Role;

            await _userRepository.UpdateAsync(user);
        }
    }
}
