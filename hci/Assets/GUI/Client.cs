using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;
using ControllerEmulation;

public class Client : MonoBehaviour
{
    private Socket _socket;
    private NetworkStream _networkStream;
    private StreamWriter _writer;
    private bool _isRunning;
    private Thread _clientThread;
    private Action<string> _onServerResponse;
    private string ip = "127.0.0.1";
    private int port = 12345;

    private ControllerInputData _data;

    public Client(ControllerInputData data)
    {
        _data = data;
        _data.OnInputChanged += SendData;
        ip = IPData.Instance.ip;
        port = IPData.Instance.port;
    }

    public void StartClient()
    {
        _isRunning = true;
        _clientThread = new Thread(ConnectToServer);
        _clientThread.IsBackground = true;
        _clientThread.Start();
    }

    public void StopClient()
    {
        _isRunning = false;

        if (_socket != null && _socket.Connected)
        {
            _socket.Close();
        }

        _clientThread?.Join(); // Wait for thread to finish
    }

    private void ConnectToServer()
    {
        while (_isRunning) // Retry loop
        {
            try
            {
                Debug.Log("Attempting to connect to server...");
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.Connect(ip, port); // Connect to the server
                Debug.Log("Connected to server.");

                // Get network stream and writer
                _networkStream = new NetworkStream(_socket);
                _writer = new StreamWriter(_networkStream);

                while (_isRunning && _socket.Connected)
                {
                    // Optionally, read a response from the server
                    if (_networkStream.DataAvailable)
                    {
                        string response = new StreamReader(_networkStream).ReadLine();
                        if (!string.IsNullOrEmpty(response))
                        {
                            // Invoke response callback on main thread
                            UnityMainThreadDispatcher.Enqueue(() => 
                            {
                                _onServerResponse?.Invoke(response);
                            });
                        }
                    }

                    // Send data periodically or when changes occur
                    // SendData();
                    Thread.Sleep(100); // Sleep to prevent busy-waiting
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Connection failed: " + ex.Message);
                Thread.Sleep(5000); // Retry after 5 seconds
            }
        }
    }

    private void SendData()
    {
        try
        {
            if (_socket != null && _socket.Connected && _writer != null)
            {
                string jsonData = JsonConvert.SerializeObject(_data);

                // Only write to the existing stream and writer
                _writer.WriteLine(jsonData);
                _writer.Flush();
                jsonData = "";
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error sending data: " + ex.Message);
        }
    }
}
