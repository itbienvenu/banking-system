using System;
using System.Linq;



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

        public long AccountNUmber {get; set;}

        public string Password {get; set;}
    }

    public class UserDto
    {
        public string Names {get; set;}
        public string Email {get; set; }
    }

    public class LoginResponse
    {
        public string Token {get; set;}

        public UserDto UserData {get; set; }
    }

    
    // Testing the API

    public class Program
    {
        public static void Main(string[] args)
        {
            var userService = new UserService();
            bool isRunnig = true;


            while(isRunnig)
            {
                Console.WriteLine("Choose from the options here");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");

                int option = int.Parse(Console.ReadLine());
                switch(option)
                {
                    case 1:
                        Console.WriteLine("Enter your names");
                        string names = Console.ReadLine();

                        Console.WriteLine("Enter your email");
                        string email = Console.ReadLine();  

                        Console.WriteLine("Enter your password");
                        string password = Console.ReadLine();

                        var result = userService.Register(names, email, password);
                        Console.WriteLine(result.Message);
                        break;
                    case 2:
                        Console.WriteLine("Enter your email");
                        string loginEmail = Console.ReadLine();

                        Console.WriteLine("Passowrd");
                        string loginPassword = Console.ReadLine();

                        var res = userService.Login(loginEmail, loginPassword);
                        Console.WriteLine(res.Message, res.Data.Token);
                        break;
                    case 3:
                        isRunnig = false;
                        break;

                }

            }
        }
    }
}



