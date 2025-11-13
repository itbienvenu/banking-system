
namespace BankingSystemCLI
{

    public class User
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        private string _userPassword { get; set; }
        public User(string firstName, string lastName, string email, string password)
        {

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this._userPassword = password;
        }
        public bool VerifyPassword(string attemptedPassword)
        {
            return attemptedPassword == this._userPassword;
        }

        // public void registerUser(String firstName, String lastName, String email, String password)

        // {

        //     var users = new List<User>();
        //     try
        //     {
        //         var newUser = new User(firstName, lastName, email, password);
        //         users.Add(newUser);
        //         Console.WriteLine($"Created new user {firstName}");
        //     }
        //     catch (Exception)
        //     {
        //         throw new Exception("Faild to create a new User");
        //     }

        // }
    }


    class Program
    {
        public static void Main(string[] args)
        {
            UserService service = new UserService();
            Console.WriteLine(@"Hello this are Options to use this System
        1. Creating a new User
        2. Get All Users
        3. Exiting the progrm");

            bool isRunning = true;

            while (isRunning)

            {
                Console.WriteLine("Enter Your Choice here: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Creating an account");
                        Console.WriteLine("");

                        Console.Write("First name: ");
                        String firstName = Console.ReadLine();

                        Console.Write("Second Name: ");
                        String lastName = Console.ReadLine();

                        Console.Write("Email: ");
                        String email = Console.ReadLine();

                        Console.Write("Password: ");
                        String password = Console.ReadLine();

                        service.RegisterUser(firstName, lastName, email, password);
                        break;
                    case "2":
                    var users = service.GetAllUsers();
                    foreach (var user in users)
                    {
                        Console.WriteLine(user);
                    }
                    break;
                    case "3":
                        Console.WriteLine("Exiting the Program .............");
                        isRunning = false;
                        break;

                }
            }
        }
    }

}