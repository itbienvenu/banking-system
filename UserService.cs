using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace BANKINGSYSTEMCLI
{
    class UserService
    {

        private readonly List<UserEntity> _userDB = new List<UserEntity>();
        private readonly string? secretKey = "super_secret_banking_key_2026_!!super_secret_banking_key_2026_!!";
        

        public string SecretKey
        {
            get { return secretKey; }

        }
        public string GenerateJwtToken(UserDto user, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Email.ToString()),
                    new Claim(ClaimTypes.Name, user.Names.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),

                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
        
        public Result<UserDto> Register(string names, string email, string password)
        {
            if(string.IsNullOrEmpty(email))
            {
                return Result<UserDto>.Failure("Email is required");
            }

            var user = _userDB.FirstOrDefault(u => u.Email == email);

            if(user != null)
            {
                return Result<UserDto>.Failure("User already exists");
            }

            if(string.IsNullOrEmpty(password))
            {
                return Result<UserDto>.Failure("Password is required");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            long userAccountNUmber =  Random.Shared.NextInt64(111111111111L, 999999999999L);

            var newUser = new UserEntity {Names = names, Email = email, Password = hashedPassword, AccountNUmber = userAccountNUmber};
            _userDB.Add(newUser);

            var response = new UserDto {Names = newUser.Names, Email = newUser.Email};

            return Result<UserDto>.Success(response, "User created well");
        }

        public Result<LoginResponse> Login(string email, string password)
        {
            if(string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                return Result<LoginResponse>.Failure("Email or password are required");
            }

            if(string.IsNullOrEmpty(secretKey))
            {
                return Result<LoginResponse>.Failure("Secret key is not configured");
            }

            foreach(UserEntity user in _userDB)
            {
                if(BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    if(user.Email == email)
                    {
                        
                        UserDto userData = new UserDto { Names = user.Names, Email = user.Email };
                        
                        var  token = GenerateJwtToken(userData, secretKey);
                        var response = new LoginResponse { UserData = userData, Token =  token };

                        return Result<LoginResponse>.Success(response, "Login successful");
                    }
                    return Result<LoginResponse>.Failure("Invalid email or password");
                }
            }

            return Result<LoginResponse>.Failure("Invalid email or password");
        }

        public void GetProfileData(string email)
        {
            // logic here
        }


        
    }

}