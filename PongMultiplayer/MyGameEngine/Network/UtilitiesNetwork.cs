using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    public static class UtilitiesNetwork
    {
        public static object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            ms.Write(arrBytes, 0, arrBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);

            object obj = (object)bf.Deserialize(ms);

            return obj;
        }

        public static Byte[] ObjectToByteArray(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        public static IPAddress[] GetIPs()
        {
            IPAddress[] localsIp = Dns.GetHostAddresses(Dns.GetHostName());
            List<IPAddress> toReturn = new List<IPAddress>();

            for (int i = 0; i < localsIp.Length; i++)
            {
                if (localsIp[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    toReturn.Add(localsIp[i]);
                }
            }
            return toReturn.ToArray();
        }
    }
}
