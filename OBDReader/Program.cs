using System;
using System.Collections.Generic;
using System.Linq;
namespace OBDReader
{
    using Obd2;

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter COM: ");
            var com = Console.ReadLine();
            var elm = new Elm327(com, 9600, Protocol.Iso15765V4Can11Bit500Kbaud);

            elm.CommandSent += (s, e) => Console.WriteLine("Command: " + e.Command);
            elm.CommandResultReceived += (s, e) => Console.WriteLine("Result: " + e.Result);

            elm.Connect();

            //elm.SetHeader("7E1");

            var supportedPids = string.Join(", ", elm.SupportedPids);
            Console.WriteLine("Supported PIDs: " + supportedPids);

            var command = "";
            while (command != "exit")
            {
                Console.Write("Enter command: ");
                command = Console.ReadLine();
                if(command == "speed")
                {
                    Console.WriteLine("Speed: " + elm.GetSpeed());
                }
                if(command == "dist")
                {
                    var distanceKm = elm.GetDistanceSinceCodesCleared("7E8", 5);
                    Console.WriteLine("Distance since codes cleared: " + distanceKm + " (" + (distanceKm / 1.609344) + " miles)");
                }
            }

            elm.Disconnect();
        }
    }
}
