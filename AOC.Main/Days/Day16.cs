using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day16 : IDay
    {
        public const int Day = 16;

        public int PacketCount = 0;

        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var packets = HexToBinary(input.Split('\n', '\r').Where(s => !string.IsNullOrWhiteSpace(s)).First());
            var version = Convert.ToInt32(packets[0..3], 2);
            var id = Convert.ToInt32(packets[3..6], 2);


            ReadPacket(packets, 0);

            return new Results(PacketCount, 0);
        }

        public Packet ReadPacket(string packets, int startingPoint, int length = 0)
        {
            var packet = new Packet();
            packet.Version = Convert.ToInt32(packets[startingPoint..(startingPoint+3)], 2);
            packet.Type = Convert.ToInt32(packets[(startingPoint+3)..(startingPoint + 6)], 2);
            packet.Position = startingPoint + 6;

            if (packet.Type == 4) // Literal value
            {
                var dataString = new StringBuilder();
                var done = false;

                while (!done)
                {
                    var dataheader = packets[packet.Position];
                    var data = packets[(packet.Position + 1)..(packet.Position + 5)];
                    dataString.Append(data);
                    packet.Position += 5;
                    if (dataheader != '1')
                    {
                        packet.Value = Convert.ToInt64(dataString.ToString(), 2);
                        done = true;
                    }
                }
            }
            else
            {
                var lengthType = packets[packet.Position];
                var subPackets = new List<Packet>();
                if (lengthType == '0')
                {
                    var subpacketLength = Convert.ToInt32(packets[(packet.Position + 1)..(packet.Position + 16)], 2);
                    packet.Position += 16;
                    var done = false;
                    var goal = packet.Position + subpacketLength;
                    while (!done)
                    {
                        var resultPacket = ReadPacket(packets, packet.Position);
                        packet.Position = resultPacket.Position;
                        subPackets.Add(resultPacket);
                        if (packet.Position == goal)
                        {
                            done = true;
                        }
                    }
                }
                if (lengthType == '1')
                {
                    var subpacketCount = Convert.ToInt32(packets[(packet.Position + 1)..(packet.Position + 12)], 2);
                    packet.Position += 12;

                    for (int i = 0; i < subpacketCount; i++)
                    {
                        var resultPacket = ReadPacket(packets, packet.Position);
                        packet.Position = resultPacket.Position;
                        subPackets.Add(resultPacket);
                    }
                }

                if (packet.Type == 0)
                {
                    packet.Value = subPackets.Sum(s => s.Value);
                }
                if (packet.Type == 1)
                {
                    var returnValue = 1L;
                    foreach (var p in subPackets)
                    {
                        returnValue *= p.Value;
                    }
                    packet.Value = returnValue;
                }
                if (packet.Type == 2)
                {
                    packet.Value = subPackets.Min(s => s.Value);
                }
                if (packet.Type == 3)
                {
                    packet.Value = subPackets.Max(s => s.Value);
                }
                if (packet.Type == 5)
                {
                    packet.Value = subPackets.FirstOrDefault().Value > subPackets.LastOrDefault().Value ? 1 : 0;
                }
                if (packet.Type == 6)
                {
                    packet.Value = subPackets.FirstOrDefault().Value < subPackets.LastOrDefault().Value ? 1 : 0;
                }
                if (packet.Type == 7)
                {
                    packet.Value = subPackets.FirstOrDefault().Value == subPackets.LastOrDefault().Value ? 1 : 0;
                }
            }
            // Last one is the answer
            Console.WriteLine(packet.Value);
            PacketCount += packet.Version;
            return packet;
        }

        public string HexToBinary(string hex)
        {
            var stringbuidler = new StringBuilder();

            foreach (char c in hex)
            {
                var toAdd = string.Empty;
                switch (c)
                {
                    case '0':
                        toAdd = "0000";
                        break;
                    case '1':
                        toAdd = "0001";
                        break;
                    case '2':
                        toAdd = "0010";
                        break;
                    case '3':
                        toAdd = "0011";
                        break;
                    case '4':
                        toAdd = "0100";
                        break;
                    case '5':
                        toAdd = "0101";
                        break;
                    case '6':
                        toAdd = "0110";
                        break;
                    case '7':
                        toAdd = "0111";
                        break;
                    case '8':
                        toAdd = "1000";
                        break;
                    case '9':
                        toAdd = "1001";
                        break;
                    case 'A':
                        toAdd = "1010";
                        break;
                    case 'B':
                        toAdd = "1011";
                        break;
                    case 'C':
                        toAdd = "1100";
                        break;
                    case 'D':
                        toAdd = "1101";
                        break;
                    case 'E':
                        toAdd = "1110";
                        break;
                    case 'F':
                        toAdd = "1111";
                        break;
                    default:
                        break;

                }

                stringbuidler.Append(toAdd);
            }

            return stringbuidler.ToString();
        }

        public class Packet
        {
            public long Value { get; set; }
            public int Type { get; set; }
            public int Position { get; set; }
            public int Version { get; set; }
        }
    }
}

