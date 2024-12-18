using System;
using System.Security.Cryptography.X509Certificates;

namespace BankAccountSystem
{
    class Program
    {
        //Create account class
        public class Account
        {
            public string AccountNumber { get; set; }
            public string AccountName { get; set; }
            public string cellphoneNumber { get; set; }
            public double AccountBalance { get; set; }
            public List<Transaction> Transactions { get; set; }


            public Account(string accNumber, string name, string cellphone, double balance)
            {
                this.AccountNumber = accNumber;
                this.AccountName = name;
                this.cellphoneNumber = cellphone;
                this.AccountBalance = balance;
                this.Transactions = new List<Transaction>();
            }
        }

        // Create list for accounts 

        static List<Account> accounts = new List<Account>();





        //Main Menu

        public static void DisplayMenu()
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Log into Account");
            
        }




        // Function for creating an account, holders details, type or account ( savings, current) 
        public static void CreateAccount()
        {
            Console.Write("What is your name: ");
            string name = Console.ReadLine();

            Console.Write("What is your cellphone number?:  ");
            string cellphoneNumber = Console.ReadLine();

            Console.Write("What is your email address?: ");
            string email = Console.ReadLine();

            Console.Write("Create a 4 digit account number: ");
            string accNumber = Console.ReadLine();

            double initialBalance = 0.0;

            Account newAccount = new Account(accNumber, name,email, initialBalance);

            accounts.Add(newAccount);

            Console.WriteLine("Congratulations!!Your Account has been created.");
            Console.WriteLine("-------------------------------------------");

            Console.Write($"Your account number is:  {accNumber}");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");


            Console.WriteLine("Log in with your Account number.");
            
        }





        // Funcction for loggin in if the provided account number exists/is correct 
        public static void LogIn(out bool isLoggedIn, ref Account loggedInAccount)
        {
            Console.Write("Enter your account number: ");
            string accountNumber = Console.ReadLine();

            bool accountFound = false;

            // Ensure accounts list is checked properly
            foreach (Account account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    accountFound = true;
                    loggedInAccount = account; // Ensure loggedInAccount is set to the correct account
                    break;
                }
            }

            if (accountFound)
            {
                isLoggedIn = true;
                Console.WriteLine($"Welcome to your dashboard {loggedInAccount.AccountName}!");
            }
            else
            {
                isLoggedIn = false; // Ensure it's set to false if no account was found
                loggedInAccount = null; // If not found, ensure loggedInAccount is null
                Console.WriteLine("Account Not Found!");
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");
        }







        // Menu for once the account has been created/ logged into
        public static void AccountMenu(Account loggedInAccount)
        {
            Console.WriteLine($"Welcome, {loggedInAccount.AccountName}!");
            Console.WriteLine("Select an option...");
            Console.WriteLine("1. View Account Balance");
            Console.WriteLine("2. View Transactions");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Widthdraw");
            Console.WriteLine("5. View/download statement of account");
            Console.WriteLine("6. Exit");
        }

        //Function for depositing money and displaying new balance 
        public static void Deposit(Account loggedInAccount)
        {
            Console.WriteLine("Enter the amount you would like to add: ");
            string input = Console.ReadLine();

            double depositeAmount;

            if(double.TryParse(input, out depositeAmount) && depositeAmount > 0)
            {

                loggedInAccount.AccountBalance += depositeAmount;

                Transaction depositTransaction = new Transaction(loggedInAccount.AccountNumber, depositeAmount, "Deposit");
                loggedInAccount.Transactions.Add(depositTransaction);

                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("-------------------------------------------");

                Console.WriteLine($"You are depositing {depositeAmount}");
                Console.WriteLine($"Your new account balance is {loggedInAccount.AccountBalance}");
                

            } else
            {
                Console.WriteLine("That number is invalid! Please try a positive number");
               

            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");

        }




        // Function for depositing money and displaying new balance 
        public static void Withdraw(Account loggedInAccount)
        {
            Console.WriteLine("Enter the withdraw amount: ");
            string input = Console.ReadLine();

            double withdrawAmount;

            if(double.TryParse(input,out withdrawAmount) && withdrawAmount > 0)
            {
                loggedInAccount.AccountBalance -= withdrawAmount;

                Transaction withdrawTransaction = new Transaction(loggedInAccount.AccountNumber, withdrawAmount, "Withdraw");
                loggedInAccount.Transactions.Add(withdrawTransaction);

                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("-------------------------------------------");

                Console.WriteLine($"You are withdrawing {withdrawAmount}");
                Console.WriteLine($"Your new account balance is {loggedInAccount.AccountBalance}");

                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("-------------------------------------------");


            } else
            {
                Console.WriteLine("That number is invalid! Please try a posivive number.");
                Withdraw(loggedInAccount);
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");

        }



        //Function to view the account balance for all accounts 
        public static void ViewAccountBalance(Account loggedInAccount)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Your Account balance is: {loggedInAccount.AccountBalance}");

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");

        }


        // Class to store transactions 
        public class Transaction
        {
            public string AccountNumber { get; set; }
            public double Amount { get; set; }
            public string Type { get; set; }  // e.g., "Deposit" or "Withdrawal"
            public DateTime Date { get; set; }

            public Transaction(string accountNumber, double amount, string type)
            {
                AccountNumber = accountNumber;
                Amount = amount;
                Type = type;
                Date = DateTime.Now;
            }
        }

        // Function to view previous transactions 
        public static void ViewTransactions(Account loggedInAccount)
        {
            if(loggedInAccount.Transactions.Count == 0)
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("No transactions found!");
            } else
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Transaction History");

                foreach (Transaction transaction in loggedInAccount.Transactions)
                {
                    Console.WriteLine($"Transaction Date: {transaction.Date}");
                    Console.WriteLine($"Type: {transaction.Type}");
                    Console.WriteLine($"Amount: {transaction.Amount}");
                    Console.WriteLine($"Account Number: {transaction.AccountNumber}");
                    Console.WriteLine("-------------------------------------------");
                }
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");
        }

        //List to store transactions 

        //  static List<Transactions> transactions = new List<Transactions>();



        //Function to view statement, with transactions, account details and balances 
        public static void ViewStatement(Account loggedInAccount)
        {
            // Show account details
            Console.WriteLine($"Account Statement for {loggedInAccount.AccountName}");
            Console.WriteLine($"Account Number: {loggedInAccount.AccountNumber}");
            Console.WriteLine($"Current Balance: {loggedInAccount.AccountBalance}");
            Console.WriteLine("\nTransaction History:");

            // Loop through and display transactions
            if (loggedInAccount.Transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
            }
            else
            {
                foreach (Transaction transaction in loggedInAccount.Transactions)
                {
                    Console.WriteLine($"Date: {transaction.Date}, Type: {transaction.Type}, Amount: {transaction.Amount}");
                }
            }

            Console.WriteLine("\nEnd of Statement");
            
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");
        }



        //Function to download statement as pdf 
        public static void DownloadStatement(Account loggedInAccount)
        {
            // Define the file path where the statement will be saved
            string filePath = $"{loggedInAccount.AccountNumber}_Statement.txt";

            // Create or overwrite the file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"Account Statement for {loggedInAccount.AccountName}");
                writer.WriteLine($"Account Number: {loggedInAccount.AccountNumber}");
                writer.WriteLine($"Current Balance: {loggedInAccount.AccountBalance}");
                writer.WriteLine("\nTransaction History:");

                // Check if there are any transactions
                if (loggedInAccount.Transactions.Count == 0)
                {
                    writer.WriteLine("No transactions found.");
                }
                else
                {
                    foreach (Transaction transaction in loggedInAccount.Transactions)
                    {
                        writer.WriteLine($"Date: {transaction.Date}, Type: {transaction.Type}, Amount: {transaction.Amount}");
                    }
                }

                writer.WriteLine("\nEnd of Statement");
            }

            Console.WriteLine($"Statement downloaded successfully as {filePath}");
            
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");
        }

        public static void Exit()
        {
            DisplayMenu();
        }




        static void Main(String[] args)
        {

            Console.WriteLine("Welcome to Console Bank!!!");
            Console.WriteLine("---------------------------");

            bool isLoggedIn = false;
            Account loggedInAccount = null;

            while (true)
            {
                if (!isLoggedIn)
                {
                    DisplayMenu();
                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1: // Create Account
                            CreateAccount();
                            break;
                        case 2: // Log into Account
                            LogIn(out isLoggedIn, ref loggedInAccount);
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    // Once logged in, show the Account Menu
                    AccountMenu(loggedInAccount);
                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1: // View Account Balance
                            ViewAccountBalance(loggedInAccount);
                            break;
                        case 2: // View Transactions
                            ViewTransactions(loggedInAccount);
                            break;
                        case 3: // Deposit
                            Deposit(loggedInAccount);
                            break;
                        case 4: // Withdraw
                            Withdraw(loggedInAccount);
                            break;
                        case 5: // View Statement
                            ViewStatement(loggedInAccount);
                            break;
                        case 6: // Download Statement
                            DownloadStatement(loggedInAccount);
                            break;
                        case 7: // Exit (Log out)
                            isLoggedIn = false;
                            loggedInAccount = null;
                            Console.WriteLine("You have been logged out.");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }
        }
    }
}