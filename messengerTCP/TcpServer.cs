using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class TcpServer
{
    private TcpListener _listener;

    public async Task StartAsync(int port)
    {
        _listener = new TcpListener(IPAddress.Any, port);
        _listener.Start();

        while (true)
        {
            TcpClient client = await _listener.AcceptTcpClientAsync();
            HandleClientAsync(client);
        }
    }

    private async void HandleClientAsync(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            // Przetwarzaj wiadomość, na przykład wyświetl ją w interfejsie użytkownika
        }
    }
}
