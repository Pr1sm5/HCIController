using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Start the server in a separate thread
        Thread serverThread = new Thread(() =>
        {
            Server server = new Server();
            server.StartServer();
        });
        serverThread.Start();

        // Give the server a moment to start before running the client
        System.Threading.Thread.Sleep(500); // Sleep for 500ms (adjust if necessary)

        // Start the client
        Client client = new Client();
        client.ConnectToServer();
        
        // Optionally, keep the program running so the server can continue to listen for connections
        Console.ReadLine();
    }
}