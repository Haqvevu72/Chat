using System.Net;
using System.Net.Sockets;
#pragma warning disable
AutoResetEvent autoResetEvent1 = new AutoResetEvent(false);
AutoResetEvent autoResetEvent2 = new AutoResetEvent(false);
var ip = IPAddress.Parse("127.0.0.1");
var port = 27001;


var client = new TcpClient();
client.Connect(ip, port);



var stream = client.GetStream();

var binaryReader = new BinaryReader(stream);
var binaryWriter = new BinaryWriter(stream);

var Mes = String.Empty;

Task.Run(async () =>
{
    
    Console.Write("Your Name: ");
    var name = Console.ReadLine();
    binaryWriter.Write(name);

    while (true)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"To Who , {name}: ");
        Console.ForegroundColor = ConsoleColor.White;

        var ToWho = Console.ReadLine();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"Message: ");
        Console.ForegroundColor = ConsoleColor.White;

        var Message = Console.ReadLine();
        binaryWriter.Write(ToWho + " " + Message);

        Mes = binaryReader.ReadString();
        if (Mes != String.Empty)
        {
            autoResetEvent1.Set();
            autoResetEvent2.WaitOne();
        }
    }
    
});

while (true)
{
    autoResetEvent1.WaitOne();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("\nServer: ");
    Console.ForegroundColor = ConsoleColor.White;

    Console.WriteLine(Mes);
    autoResetEvent2.Set();


}







