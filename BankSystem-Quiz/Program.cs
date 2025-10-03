using BankSystem_Quiz.Authentications;
using BankSystem_Quiz.Contracts.Authentications;
using BankSystem_Quiz.Contracts.Repositories;
using BankSystem_Quiz.Contracts.Services;
using BankSystem_Quiz.Contracts.Validators;
using BankSystem_Quiz.DTO;
using BankSystem_Quiz.Infrastructure.DataAccess;
using BankSystem_Quiz.Infrastructure;
using BankSystem_Quiz.Infrastructure.Repositories;
using BankSystem_Quiz.Services;
using BankSystem_Quiz.Validators;
using BankSystem_Quiz.Extensions;
using BankSystem_Quiz.Infrastructure.Local;
using Spectre.Console;

namespace BankSystem_Quiz
{
    internal class Program
    {
        private static readonly AppDbContext _appDbContext = new AppDbContext();
        private static readonly UnitOfWork _unitOfWork = new UnitOfWork(_appDbContext);
        private static readonly IValidator _validate = new Validator();
        private static readonly ICardRepository _cardRepo = new CardRepository(_appDbContext);
        private static readonly ITransactionRepository _transactionRepo = new TransactionRepository(_appDbContext);
        private static readonly IAuthentication _auth = new Authentication(_cardRepo, _validate,_unitOfWork);
        private static readonly ICardService _cardService = new CardService(_cardRepo, _transactionRepo, _unitOfWork);
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                ConsoleHelper.PrintColorful("MAIN PAGE", null, ConsoleColor.DarkCyan);
                Console.WriteLine("\n");
                Console.WriteLine(" 1. Login");

                var choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        LoginPage();
                        break;
                    default:
                        ConsoleHelper.PrintResult(false, "Invalid option! Try again...");
                        break;
                }
            }
        }

        public static void LoginPage()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    ConsoleHelper.PrintColorful("LOGIN PAGE", null, ConsoleColor.DarkCyan);
                    Console.WriteLine("\n");
                    Console.Write("Card number: ");
                    string cardNumber = Console.ReadLine()!;
                    Console.Write("Password: ");
                    string password = Console.ReadLine()!;

                    if (!_auth.Login(cardNumber, password))
                        throw new Exception("Invalid card number or password.");

                    ConsoleHelper.PrintResult(true, "Login was successful!");

                    CardPage();
                    //
                    return;
                }
                catch (Exception e)
                {
                    ConsoleHelper.PrintResult(false, e.Message);
                    Console.WriteLine("Press 0 to go back to Main Menu, or any other key to try again...");
                    var key = Console.ReadKey().KeyChar;

                    if (key == '0')
                        return;
                }
            }
        }

        public static void CardPage()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("CARD PAGE");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine(" 0.Log out");
                    Console.WriteLine(" 1.Transfer Money");
                    Console.WriteLine(" 2.Show Transactions");


                    var choice = int.Parse(Console.ReadLine()!);
                    Console.WriteLine();

                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Clear();

                                Console.Write("Enter destination card number: ");
                                string destinationCardNumber = Console.ReadLine()!;
                                Console.Write("Enter amount: ");
                                float amount = float.Parse(Console.ReadLine()!);
                                if (!_validate.IsValidAmount(amount))
                                {
                                    throw new Exception("amount >0");
                                }

                                var result = _cardService.TransferMoney(LocalStorage.CurrentCard!.CardNumber, destinationCardNumber, amount);
                                if (result)
                                {
                                    ConsoleHelper.PrintResult(result, "Transfer was successful!");
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                var result = _cardService.ShowTransactions(LocalStorage.CurrentCard!.CardNumber);
                                ShowTransactionTable(LocalStorage.CurrentCard!.CardNumber,result);
                                Console.ReadKey();
                                break;
                            }
                        case 0:
                            {
                                Console.Clear();
                                _auth.Logout();
                                return;
                            }
                        default:
                            {
                                ConsoleHelper.PrintResult(false, "Invalid option! Try again...");
                                break;
                            }
                    }
                }
                catch (Exception e)
                {
                    ConsoleHelper.PrintResult(false, e.Message);
                    Console.WriteLine("Press 0 to go back to Main Menu, or press any other key to try again...");
                    var key = Console.ReadKey().KeyChar;

                    if (key == '0')
                        return;
                }
            }
        }


        public static void ShowTransactionTable(string currentCard, List<ShowTransactionDto> transactions)
        {
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Source Card");
            table.AddColumn("Source Name");
            table.AddColumn("Destination Card");
            table.AddColumn("Destination Name");
            table.AddColumn("Amount");
            table.AddColumn("Date");
            table.AddColumn("Success");

            foreach (var t in transactions)
            {
                string rowColor = t.SourceCardNumber == currentCard ? "red" :
                    t.DestinationCardNumber == currentCard ? "green" : "default";

                table.AddRow(
                    $"[{rowColor}]{t.TransactionId}[/]",
                    $"[{rowColor}]{t.SourceCardNumber}[/]",
                    $"[{rowColor}]{t.SourceFullName}[/]",
                    $"[{rowColor}]{t.DestinationCardNumber}[/]",
                    $"[{rowColor}]{t.DestinationFullName}[/]",
                    $"[{rowColor}]{t.Amount}[/]",
                    $"[{rowColor}]{t.TransactionDate:yyyy-MM-dd HH:mm}[/]",
                    $"[{rowColor}]{t.IsSuccessful}[/]"
                );
            }

            AnsiConsole.Write(table);
        }

    }
}
