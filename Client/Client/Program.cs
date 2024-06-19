
using System;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;

class Client
{
    static async Task Main(string[] args)
    {
        try
        {
            TcpClient client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 8888); // Подключение к серверу на localhost и порту 8888

            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            Console.WriteLine("Введите ваше имя:");
            string userName = Console.ReadLine();
            await writer.WriteLineAsync(userName);
            await writer.FlushAsync();

            // Отправка и получение сообщений
            while (true)
            {
                string? message = Console.ReadLine();
                await writer.WriteLineAsync(message);
                await writer.FlushAsync();

                string? response = await reader.ReadLineAsync();
                Console.WriteLine(response);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}