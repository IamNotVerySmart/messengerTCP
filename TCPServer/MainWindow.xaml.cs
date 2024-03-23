using System.Windows;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using System;

namespace TCPServer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			
		}

		TcpListener server;
		TcpClient client;

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			

		}

		public async void StartConnection(object sender, RoutedEventArgs e)
		{
			btnStart.IsEnabled = false;
			server = new TcpListener(IPAddress.Parse(HostAddress.Text), int.Parse(PortNumber.Text));
			server.Start();
			infoBox.Text += "Server started\n";
			client = await server.AcceptTcpClientAsync();
            Thread receiveThread = new Thread(() => ReceiveData(client));
            if (client.Connected)
			{
				infoBox.Text += $"{client.ToString} connected\n";
                
                receiveThread.Start();
            }
			btnStop.IsEnabled = true;
		}

		private void StopConnection(object sender, RoutedEventArgs e)
		{
			btnStop.IsEnabled = false;
			server.Stop();
			client.Close();
			infoBox.Text += "Server stopped\n";
			btnStart.IsEnabled = true;
		}
        private void ReceiveData(TcpClient client)
        {
            try
            {
                NetworkStream networkStream = client.GetStream();
                byte[] receiveBuffer = new byte[256];

                while (true)
                {
                    int bytesRead = networkStream.Read(receiveBuffer, 0, receiveBuffer.Length);
                    string receivedMessage = Encoding.ASCII.GetString(receiveBuffer, 0, bytesRead);
                    infoBox.Text += "Client 2: " + receivedMessage;
                }
            }
            catch (Exception ex)
            {
                infoBox.Text += $"Error receiving data: {ex.Message}";
            }
        }
    }
}
