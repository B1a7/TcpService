using System.Net.Sockets;
using System.Net;
using System.Text;



    int port = 5001;

    try
    {
        TcpListener listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine($"Server is listening on {port}");

        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            Console.WriteLine($"Connected with client: {client.Client.RemoteEndPoint}");

            _ = HandleClientAsync(client);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error occured: {0}", ex.Message);
    }


static async Task HandleClientAsync(TcpClient client)
{
    try
    {
        NetworkStream stream = client.GetStream();

        while (client.Connected)
        {
            byte[] buffer = new byte[4096];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

            if (bytesRead > 0)
            {
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                Console.WriteLine(receivedMessage);

                // json
                // json
                // 

                // Response
                string responseMessage = "Message received";
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseMessage);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
            else
            {
                break;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error while service the client: {ex.Message}");
    }
    finally
    {
        client.Close();
    }
}