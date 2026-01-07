using System;




namespace BANKINGSYSTEMCLI
{
    class Result<T>
    {
        public bool isSuccess {get; set; }
        public T Data {get; set;} 

        public string Message {get; set; }

        public static Result<T> Success(T data, string msg) => new Result<T> { isSuccess = true, Data = data, Message = msg};
        public static Result<T> Failure(string msg) => new Result<T> { isSuccess = false, Message = msg };
    }

    public class UserEntity
    {
        public string Names{get; set;}
        public string Email{get; set;}

        public string Password {get; set;}
    }

    public class UserDto
    {
        public string Names {get; set;}
        public string Email {get; set; }
    }


    class UserService
    {

        private readonly List<UserEntity> _userDB = new List<UserEntity>();
        
        public Result<UserDto> Register(string names, string email, string password)
        {
            if(string.IsNullOrEmpty(email))
            {
                return Result<UserDto>.Failure("Email is required");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new UserEntity {Names = names, Email = email, Password = hashedPassword};
            _userDB.Add(newUser);

            var response = new UserDto {Names = newUser.Names, Email = newUser.Email};

            return Result<UserDto>.Success(response, "User created well");
        }

        
    }
}