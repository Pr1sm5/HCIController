using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using ControllerEmulation;

public class Server
{
    private TcpListener _listener;
    private ControllerEmu _controllerEmu = new ControllerEmu();

    public void StartServer()
    {
        _listener = new TcpListener(IPAddress.Any, 12345); // Port 12345
        _listener.Start();

        Console.WriteLine("Server started, waiting for clients...");

        while (true)
        {
            var clientSocket = _listener.AcceptSocket();
            Console.WriteLine("Client connected.");

            // Handle client communication in a separate thread
            var clientThread = new System.Threading.Thread(() => HandleClient(clientSocket));
            clientThread.Start();
        }
    }

    private void HandleClient(Socket clientSocket)
    {
        
        NetworkStream networkStream = new NetworkStream(clientSocket);
        StreamReader reader = new StreamReader(networkStream);
        StreamWriter writer = new StreamWriter(networkStream);
        

        try
        {
            while (true)
            {
                // Read incoming controller data (sent as JSON or raw data)
                string incomingData = reader.ReadLine(); // Read a line of data (you can also use other protocols, such as binary or JSON)

                if (string.IsNullOrEmpty(incomingData))
                    break;

                Console.WriteLine("Received data: " + incomingData);

                // Deserialize the data (e.g., from JSON to ControllerInputData object)
                ControllerInputData data = Newtonsoft.Json.JsonConvert.DeserializeObject<ControllerInputData>(incomingData);

                // Process the data (e.g., simulate controller or update game state)
                Console.WriteLine("Button A: " + data.ButtonA + ", Button B: " + data.ButtonB);
                _controllerEmu.SetControllerInputs(data);

                // Send a response back to the client if needed
                writer.WriteLine("Data received");
                writer.Flush();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            reader.Close();
            writer.Close();
            networkStream.Close();
            clientSocket.Close();
        }
    }
}