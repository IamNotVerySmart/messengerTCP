using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class TcpClientApp
{
    private TcpClient _client;

    public async Task ConnectAsync(string serverIp, int port)
    {
        _client = new TcpClient();
        await _client.ConnectAsync(serverIp, port);
    }

    public async Task SendMessageAsync(string message)
    {
        if (_client.Connected)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            await _client.GetStream().WriteAsync(data, 0, data.Length);
        }
    }
}

