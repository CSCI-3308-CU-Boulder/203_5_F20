using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using System.Diagnostics;

public class UnityClient
{
    private ClientWebSocket ws = new ClientWebSocket();
    private Uri address;
    private UTF8Encoding encoder; //this helps with conversion of C# strings to byte arrays
    private const UInt64 MAXSIZE = 1024 * 1024; //set max receiving size to 1 MB

    public BlockingCollection<ArraySegment<byte>> outQueue { get; } //queue of data to send to server
    public ConcurrentQueue<String> inQueue { get; }

    private Thread outThread { get; set; }
    public Thread inThread { get; set; }

    //constructor
    public UnityClient(string serverAddress)
    {
        ws = new ClientWebSocket();
        address = new Uri(serverAddress);
        encoder = new UTF8Encoding();
        outQueue = new BlockingCollection<ArraySegment<byte>>();
        inQueue = new ConcurrentQueue<String>();

        outThread = new Thread(MessageOut);
        inThread = new Thread(MessageIn);
        outThread.Start();
        inThread.Start();
    }

    //method to connect to server
    public async Task Connect()
    {
        UnityEngine.Debug.Log("Attempting to connect to " + address);
        await ws.ConnectAsync(address, CancellationToken.None);

        //while the server is in the process of connecting
        while (ws.State == WebSocketState.Connecting)
        {
            UnityEngine.Debug.Log("Waiting to connect...");
            Task.Delay(50).Wait();
        }
        UnityEngine.Debug.Log("Connect status: " + ws.State);
    }

    public void SendMessage(string message)
    {
        byte[] buffer = encoder.GetBytes(message);
        var addSend = new ArraySegment<byte>(buffer);
        outQueue.Add(addSend);
        // SendMessage();
    }

    //thread for sending messages
    private async void MessageOut()
    {
        while (ws.State != WebSocketState.Open)
        {
            Task.Delay(50).Wait(); //wait to send the message until the connection is established
        }
        ArraySegment<byte> message;
        while (!outQueue.IsCompleted)
        {
            message = outQueue.Take();
            //Debug.Log("Dequeued this message to send: " + message);
            await ws.SendAsync(message, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

    private async Task<string> ReceiveMessage()
    {
        byte[] buffer = new byte[4 * 1024]; //4 KB
        var ms = new MemoryStream();
        ArraySegment<byte> arrBuffer = new ArraySegment<byte>(buffer);
        WebSocketReceiveResult result = null;

        //wait to connect
        while (ws.State != WebSocketState.Open)
        {
            Task.Delay(50).Wait();
        }

        if (ws.State == WebSocketState.Open)
        {
            do
            {
                result = await ws.ReceiveAsync(arrBuffer, CancellationToken.None);
                ms.Write(arrBuffer.Array, arrBuffer.Offset, result.Count);
                if ((UInt64)(result.Count) > MAXSIZE)
                {
                    Console.Error.WriteLine("Warning: message from server is larger than 1 MB. This is too large!");
                    return "error";
                }
            } while (!result.EndOfMessage); //use a do-while so we can execute await ReceiveAsync the first time
            ms.Seek(0, SeekOrigin.Begin); //set the position to the beginning of the stream

            if (result.MessageType == WebSocketMessageType.Text)
            {
                string message = "";
                var reader = new StreamReader(ms, encoder);
                message = reader.ReadToEnd();
                return message;
            }
        }
        return "Not connected";
    }

    //thread for receiving messages
    public async void MessageIn()
    {
        int i = 0;
        UnityEngine.Debug.Log(i);
        i++;
        string message;
        message = await ReceiveMessage();
        UnityEngine.Debug.Log("other function: " + message);
        if (message != null && message.Length > 0)
        {
            //check for type 1 message

            inQueue.Enqueue(message);
        }
        else
        {
            Task.Delay(50).Wait();
        }
    }
}