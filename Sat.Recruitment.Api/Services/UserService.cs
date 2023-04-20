using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Intefases;
using Sat.Recruitment.Api.Utils;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserSevice
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="user"></param>
        public async Task<MethodResult> CreateUserAsync(User user)
        {
            MethodResult methodResult = ValidateErrors(user);
            if (!methodResult.IsSuccess)
              return methodResult;

            methodResult = IsDuplicatedAsync(user).Result;
            if (!methodResult.IsSuccess)
                return methodResult;

            CalculatePercentage(user);

            await _userRepository.CreateUserAsync(user);

            return methodResult;
         }

        /// <summary>
        /// Validate errors
        /// </summary>
        /// <param name="user"></param>
        public MethodResult ValidateErrors(User user)
        {
            var methodResult = new MethodResult();
            if (String.IsNullOrEmpty(user.Name))
                methodResult.Errors.AppendLine ( "The name is required");
            if (String.IsNullOrEmpty(user.Email))
                methodResult.Errors.AppendLine ("The email is required");
            if (String.IsNullOrEmpty(user.Address))
                methodResult.Errors.AppendLine ("The address is required");
            if (String.IsNullOrEmpty(user.Phone))
                methodResult.Errors.AppendLine  ("The phone is required");

            return methodResult;
        }

        /// <summary>
        /// Calculate the percentage per User type
        /// </summary>
        /// <param name="user"></param>
        public void CalculatePercentage(User user)
        {
            if (user.UserType == UserTypesEmun.Normal.ToString())
            {
                if (user.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = user.Money * percentage;
                    user.Money += gif;
                }
                if (user.Money < 100)
                {
                    if (user.Money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = user.Money * percentage;
                        user.Money = user.Money + gif;
                    }
                }
            }
            if (user.UserType == UserTypesEmun.SuperUser.ToString())
            {
                if (user.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = user.Money * percentage;
                    user.Money += gif;
                }
            }
            if (user.UserType == UserTypesEmun.Premium.ToString())
            {
                if (user.Money > 100)
                {
                    var gif = user.Money * 2;
                    user.Money = user.Money + gif;
                }
            }

        }

        /// <summary>
        /// Check if a user is duplicated
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<MethodResult>IsDuplicatedAsync(User user)
        {
            var result = new MethodResult();
            if (await _userRepository.IsDuplicatedAsync(user))
                result.Errors.AppendLine("The user is duplicated");
            return result;
        }
    }
}
