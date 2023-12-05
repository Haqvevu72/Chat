using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;
#pragma warning disable

var ip = IPAddress.Parse("127.0.0.1");
var port = 27001;


var client = new TcpClient();
client.Connect(ip, port);



var stream = client.GetStream();

var binaryReader = new BinaryReader(stream);
var binaryWriter = new BinaryWriter(stream);

var name_2 = "";
Task.Run(async () =>
{
    
    Console.Write("Your Name: ");
    var name = Console.ReadLine();
    name_2 = name;
    var FromWho = name;
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
        binaryWriter.Write(FromWho + " " + ToWho + " " + Message);
    }
    
});

while (true)
{
    var Mes = binaryReader.ReadString();
    if (String.IsNullOrEmpty(Mes) == false)
    {
        string[] array = Mes.Split(' ', 2);
        var FromWho = array[0];
        var Message = array[1];
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"\n{FromWho}: ");
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine(Message);

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"To Who , {name_2}: ");
        Console.ForegroundColor = ConsoleColor.White;
    }

}







