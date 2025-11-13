using System.Collections.Generic;

namespace BankingSystemCLI
{
    // A new class to manage all User-related operations
    public class UserService 
    {
        // This list is declared at the class level and stores all users permanently.
        // The 'static' keyword means there is only one list, shared by the whole program.
        private static List<User> _users = new List<User>();

        public void RegisterUser(string firstName, string lastName, string email, string password)
        {
            try
            {
                // Create a new User object
                var newUser = new User(firstName, lastName, email, password);
                
                // Add the user to the permanent list
                _users.Add(newUser); 
                
                Console.WriteLine($"\nSuccessfully registered user: {firstName} {lastName}");
            }
            catch (Exception ex)
            {
                // Better to log the original exception (ex) or use a more specific catch
                Console.WriteLine($"\nFailed to create a new User: {ex.Message}");
            }
        }
        
        public List<User> GetAllUsers()
        {
            return _users;
        }
    }
}