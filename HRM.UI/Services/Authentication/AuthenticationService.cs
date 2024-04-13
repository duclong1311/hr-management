using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.Core.Repositories;
using Terp.Core.UnitOfWorks;
using Terp.Domain.Exceptions;
using Terp.Domain.Models;
using Terp.Domain.Utils;

namespace Terp.UI.Services
{
    public class AuthenticationService : IAuthenticationSevice
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IRepository<User> userRepository,IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Login(string email, string password)
        {
            User user = await _userRepository.AsQueryable().FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new UserNotFoundException(email);
            }

            if (CryptographyUtil.SHA256Hash(password) != user.Password)
            {
                throw new InvalidPasswordException(email, password);
            }
            return user;
        }

        public async Task<ERegistrationResult> Register(string email,string username,string password,string confirmPassword)
        {
            ERegistrationResult result = ERegistrationResult.Success;
            if(password != confirmPassword)
            {
                result = ERegistrationResult.PasswordDoNotMatch;
            }

            User user = await _userRepository.AsQueryable().FirstOrDefaultAsync(u => u.Email == email);
            if(user != null)
            {
                result = ERegistrationResult.EmailAlreadyExist;
            }

            if(password.Length <6)
            {
                result = ERegistrationResult.InvalidPassword;
            }

            if(result == ERegistrationResult.Success) 
            {
                try
                {
                    User newUser = new User()
                    {
                        Email = email,
                        Name = username,
                        Password = CryptographyUtil.SHA256Hash(password),
                        FingerprintCode = "",
                    };
                    await _unitOfWork.BeginTransactionAsync();
                    await _userRepository.AddAsync(newUser);
                    await _unitOfWork.CommitAsync();
                }
                catch(Exception ex) 
                {
                    await _unitOfWork.RollbackAsync();
                }

            }
            return result;
        }
    }
}
