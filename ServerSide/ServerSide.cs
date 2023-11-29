using System.Net;
using System.Net.Sockets;

#pragma warning disable
Dictionary<string,EndPoint> Clients = new Dictionary<string,EndPoint>();
List<TcpClient> TCPclients = new List<TcpClient>();

var ip = IPAddress.Parse("127.0.0.1");
var port = 27001;

var listener = new TcpListener(ip, port);


listener.Start(2);


while (true)
{
    Task.Run(() =>
    {
        bool isFirstTime = true;

        var client = listener.AcceptTcpClient();
        TCPclients.Add(client); 
        var stream = client.GetStream();

        var binaryReader = new BinaryReader(stream);
        var binaryWriter = new BinaryWriter(stream);

        var clientName = String.Empty;
        if (isFirstTime)
        {
            clientName = binaryReader.ReadString();
            Console.WriteLine($"{clientName} - {client.Client.RemoteEndPoint}Connected.... ");

            Clients.Add(clientName, client.Client.RemoteEndPoint);

            isFirstTime = false;
        }

        while (true)
        {
            var Info = binaryReader.ReadString();
            string[] array = Info.Split(' ',2);
            var ToWho = array[0];
            var ClientMessage = array[1];

            Console.WriteLine(ToWho);
            Console.WriteLine(ClientMessage);

            bool IsFound = false;
            for (int i = 0; i < Clients.Count; i++)
            {
                if (ToWho == Clients.ElementAt(i).Key)
                {
                    var receiver_EndPoint = Clients.ElementAt(i).Value;
                    Console.WriteLine($"{Clients.ElementAt(i).Key} - {Clients.ElementAt(i).Value}");
                    var receiver = TCPclients.FirstOrDefault(r => r.Client.RemoteEndPoint == receiver_EndPoint);

                    var receiver_Stream = receiver.GetStream();

                    var receiverWriter = new BinaryWriter(receiver_Stream);
                    receiverWriter.Write(ClientMessage);
                    IsFound= true;
                    break;
                }
            }
            if (IsFound == false) { binaryWriter.Write("Not Found !"); }
            
        }

    });
}


