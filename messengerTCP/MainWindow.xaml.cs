using System.Windows;
using System.Net.Sockets;
using System.Text;
using System.Net;

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


        }

        TcpClient client;
        string ip = "127.0.0.1";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnConnect(object sender, RoutedEventArgs e)
        {
            ChatBox.Text = "Connecting to server...";
            client = new TcpClient(ip, int.Parse(PortNumber.Text));
            if(client.Connected)
            {
                ChatBox.Text = "Connected to server";
            }
        }

        private void btnSend(object sender, RoutedEventArgs e)
        {

            NetworkStream networkStream = client.GetStream();
            byte[] sendBuffer = Encoding.ASCII.GetBytes(ChatInput.Text);
            networkStream.Write(sendBuffer, 0, sendBuffer.Length);
        }

        private void btnDisconnect(object sender, RoutedEventArgs e)
        {
            client.Close();
            ChatBox.Text = "Disconnected from server";
        }
    }
}

/*
class Client1
    {
        static void Main()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);

            foreach (IPAddress ipAddress in ipEntry.AddressList)
            {
                Console.WriteLine($"IP Address: {ipAddress}");
            }

            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
            listener.Start();

            Console.WriteLine("Client 1 is waiting for a connection...");

            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client 1 connected to Client 2");

            Thread receiveThread = new Thread(() => ReceiveData(client));
            receiveThread.Start();

            while (true)
            {
                string message = Console.ReadLine();
                SendData(client, message);
            }
        }

        static void ReceiveData(TcpClient client)
        {
            try
            {
                NetworkStream networkStream = client.GetStream();
                byte[] receiveBuffer = new byte[256];

                while (true)
                {
                    int bytesRead = networkStream.Read(receiveBuffer, 0, receiveBuffer.Length);
                    string receivedMessage = Encoding.ASCII.GetString(receiveBuffer, 0, bytesRead);
                    Console.WriteLine("Client 2: " + receivedMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving data: {ex.Message}");
            }
        }

        static void SendData(TcpClient client, string message)
        {
            try
            {
                NetworkStream networkStream = client.GetStream();
                byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
                networkStream.Write(sendBuffer, 0, sendBuffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending data: {ex.Message}");
            }
        }
    }
 */