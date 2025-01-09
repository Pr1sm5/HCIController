using ControllerEmulation;
using System;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;

public class Client
{
    private TcpClient _clientSocket;

    public void ConnectToServer()
    {
        _clientSocket = new TcpClient("127.0.0.1", 12345); // Connect to the server at localhost, port 12345
        Console.WriteLine("Connected to server.");

        NetworkStream networkStream = _clientSocket.GetStream();
        StreamWriter writer = new StreamWriter(networkStream);
        StreamReader reader = new StreamReader(networkStream);

        try
        {
            while (true)
            {
                // Collect controller data (you can replace this with actual controller data)
                ControllerInputData data = new ControllerInputData
                {
                    ButtonA = true,  // Example: Button A is pressed
                    LeftThumbX = 30000,  // Example: Left thumbstick X-axis value
                    LeftThumbY = 30000,  // Example: Left thumbstick Y-axis value
                    RightThumbX = 15000,
                    RightThumbY = -10000,
                    RightTrigger = 255 // Example: Right trigger fully pressed
                };

                // Serialize the data to JSON
                string jsonData = JsonConvert.SerializeObject(data);

                // Send the data to the server
                writer.WriteLine(jsonData);
                writer.Flush();

                // Optionally, read a response from the server
                string response = reader.ReadLine();
                Console.WriteLine("Server response: " + response);

                // Simulate delay between sending data (e.g., 50 ms)
                System.Threading.Thread.Sleep(10);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            writer.Close();
            reader.Close();
            networkStream.Close();
            _clientSocket.Close();
        }
    }
}