using System.Windows;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace messengerTCP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Window_Loaded();
        }

        TcpClient client;
        TcpListener server;
        NetworkStream networkStream;
        string ip = "127.0.0.1";

        private void Window_Loaded()
        {
            // tutaj trzeba zmienic numerek portu
            server = new TcpListener(IPAddress.Parse(ip), 54321); // poddalem sie nie chce robic wiecej tcp
            // oby nie bylo juz tego wiecej
            // to nie chce dzialac
            // 2 mi starczy lub tak by zdac na koniec roku bez poprawki :D
            server.Start();
            Task.Run(() => StartS());
        }

        private async Task StartS()
        {
            while(true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                await Task.Run(() => hClient(client));
            }
        }

        private void btnConnect(object sender, RoutedEventArgs e)
        {
            ChatBox.Text = "Connecting...";
            client = new TcpClient(ip, int.Parse(PortNumber.Text));
            networkStream = client.GetStream();
            if(client.Connected)
            {
                ChatBox.Text = "Connected to server";
            }
            Task.Run(() => StartC());
        }

        async Task StartC()
        {
            while(true)
            {
                byte[] data = new byte[256];
                int bytes = await networkStream.ReadAsync(data, 0, data.Length);
                string message = Encoding.ASCII.GetString(data, 0, bytes);
                ChatBox.Text += "A " + message + "\n";
            }
        }

        private async void btnSend(object sender, RoutedEventArgs e)
        {

            string message = ChatInput.Text;
            if(!string.IsNullOrWhiteSpace(message))
            {
                byte[] data = Encoding.ASCII.GetBytes(message);
                await networkStream.WriteAsync(data,0, data.Length);
                ChatBox.Text += "A " + message + "\n";
            }
            ChatBox.Text += string.Empty + "\n";
        }

        private async Task hClient(TcpClient client)
        {
            using (NetworkStream nStream = client.GetStream())
            {
                while(true)
                {
                    byte[] data = new byte[256];
                    int bytes = await nStream.ReadAsync(data, 0, data.Length);
                    string message = Encoding.ASCII.GetString(data, 0, bytes);
                    ChatBox.Text += "A " + message + "\n";
                }
            }
        }

        private void btnDisconnect(object sender, RoutedEventArgs e)
        {
            client.Close();
            ChatBox.Text = "Disconnected from server";
        }
    }
}
