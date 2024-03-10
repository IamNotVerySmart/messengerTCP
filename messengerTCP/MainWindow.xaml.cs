using System.Windows;

namespace messengerTCP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpClientApp tcpClientApp;

        public MainWindow()
        {
            InitializeComponent();

            //string a = Dns.GetHostName();

            //value1.Content = a;
            //value2.Content = Dns.GetHostEntry(a);

            tcpClientApp = new TcpClientApp();
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