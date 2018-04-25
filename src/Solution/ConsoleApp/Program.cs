using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // .NET Core must register provider
            System.Text.Encoding.RegisterProvider(
                System.Text.CodePagesEncodingProvider.Instance
                );

            // windows default use UTF-16 process unicode
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            // windows default use UTF-16 process unicode
            Console.InputEncoding = System.Text.Encoding.Unicode;

            Console.Write("User ID: ");

            var userId = Console.ReadLine();

            Console.Write("Password: ");

            var password = Console.ReadLine();

            try
            {
                using (var client = new HinetSms.Client(
                    userId,
                    password
                    ))
                {
                    Console.WriteLine("Connected");

                    client.Authenticate();

                    Console.WriteLine("Authenticated");

                    Console.Write("Phone Number: ");

                    var phoneNumber = Console.ReadLine();

                    Console.Write("Text Message: ");

                    var content = Console.ReadLine();

                    var messageId = client.SendTextMessage(phoneNumber, content);

                    Console.WriteLine($"Message ID: {messageId}");

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception : {ex}");
            }

            Console.WriteLine("Press any key to end.");

            Console.ReadKey();
        }
    }
}
