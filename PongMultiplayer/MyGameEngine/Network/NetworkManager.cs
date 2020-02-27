using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using MyEngine.Network.Behaviours;

namespace MyEngine
{
    public static class NetworkManager
    {
        public static IPAddress addres;
        public static int port;
        public static bool isServer = false;
        public static int clientID = -1;

        private static int clientAmount = 0;
        public static int ClientAmount { get { return clientAmount; } set { clientAmount = value; OnClientAmountChange?.Invoke(); } }

        public delegate void ConectionEvent();

        public static ConectionEvent OnConnect;
        public static ConectionEvent OnClientAmountChange;
        public static ConectionEvent OnDiscconect;

        private static TcpListener listener;
        private static Thread searchClient;
        private static Thread reciveData;

        private static TcpClient client;
        public static TcpClient Client { get { return client; } }

        //behaviours
        public static List<BehaviourNetwork> behaviours = new List<BehaviourNetwork>();

        public static void Command(DataNetwork data)
        {
            switch ((BasicCommand)data.obj)
            {
                case BasicCommand.NewConnection:
                    if (isServer)
                    {
                        ClientAmount++;
                        var msg = UtilitiesNetwork.ObjectToByteArray(new DataNetwork(-1, ClientAmount, BasicCommand.NewConnection));
                        Send(msg);
                    }
                    else
                    {
                        ClientAmount = data.senderID;
                        if(clientID == -1)
                            clientID = data.senderID;
                    }
                    break;

                case BasicCommand.Desconection:
                    if (isServer)
                    {
                        ClientAmount--;
                        var msg = UtilitiesNetwork.ObjectToByteArray(new DataNetwork(-1, ClientAmount, BasicCommand.ServerDesconection));
                        Send(msg);
                    }
                    else
                    {
                        ClientAmount = data.senderID;
                    }
                    break;

            }

        } // esto debiese enstar en otra clase

        public static void SuscribeNetworkBehaviour(BehaviourNetwork bn)
        {
            if (!behaviours.Contains(bn))
            {
                behaviours.Add(bn);
            }
        } // esto probablemente tambien

        /// <summary>
        /// Send message.
        /// </summary>
        /// <param name="buffer"></param>
        public static void Send(byte[] buffer)
        {
            if(client == null)
            {
                Console.WriteLine("Client is NULL.");
                return;
            }


            if(client.Connected)
            {
                client.Client.Send(buffer);
            }
            else
            {
                Console.WriteLine("Client is not connected.");
            }
        }

        /// <summary>
        /// Start a new server.
        /// </summary>
        /// <param name="port"></param>
        public static void StartServer(int port)
        {
            NetworkManager.port = port;
            isServer = true;
            
            searchClient = new Thread(SearchClient_Thread);
            searchClient.Start();
           
        }

        /// <summary>
        /// Search a client to the server in async methond.
        /// If nobody connects this method will continue waiting and the TCP client will be NULL.
        /// </summary>
        private static void SearchClient_Thread()
        {
            Console.WriteLine("Start search client...");
            //addres = IPAddress.Any;
            addres = UtilitiesNetwork.GetIPs()[0];
           
            listener = new TcpListener(addres, NetworkManager.port);
            listener.Start();
            try
            {
                client = listener.AcceptTcpClient(); // <- wainting
            }
            catch { }

            Console.WriteLine("End search client...");

            reciveData = new Thread(ReciveData_Thread);
            reciveData.Start();
        }

        /// <summary>
        /// Recive data in async methond.
        /// </summary>
        private static void ReciveData_Thread()
        {
            byte[] buffer = new byte[1024];

            while (client != null && client.Connected)
            {
                try
                {
                    client.Client.Receive(buffer);

                    BinaryFormatter bf = new BinaryFormatter();
                    MemoryStream ms = new MemoryStream(buffer);
                    DataNetwork data = (DataNetwork)bf.Deserialize(ms);
                    
                    if(data.id == -1)
                    {
                        Command(data);
                        continue;
                    }

                    foreach (var behaviour in behaviours)
                    {
                        if (behaviour.networkID == data.id)
                        {
                            behaviour.ReciveData(data);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error ReciveData(): " + e);
                }
            }
        }

        /// <summary>
        /// Connect to an existing server.
        /// </summary>
        public static void ConectToServer()
        {
            client = new TcpClient();
            addres = UtilitiesNetwork.GetIPs()[0];
            IPEndPoint IP_End = new IPEndPoint(addres, port);

            try
            {
                client.Connect(IP_End);

                if (client.Connected)
                {
                    //Envio mensaje de coneccion
                    var msg = UtilitiesNetwork.ObjectToByteArray(new DataNetwork(-1, 0, BasicCommand.NewConnection));
                    Console.WriteLine("[Send message]: conect to server.");
                    client.Client.Send(msg);
                   
                    //Inicio Nuevo thread para recivir informacion
                    reciveData = new Thread(ReciveData_Thread);
                    reciveData.Start();

                    //Llamo al evento de coneccion
                    OnConnect?.Invoke();
                }
            }
            catch 
            {
                Console.WriteLine("ConectToServer() CatchExeption");
            }
        }

        public static void Disconect()
        {
            //Envio mensaje de desconeccion
            var msg = UtilitiesNetwork.ObjectToByteArray(new DataNetwork(-1,clientID,BasicCommand.Desconection));
            Console.WriteLine("[Send message]: disconect.");
            Send(msg);

            //Detengo las posibles corrutinas abiertas
            listener?.Stop();
            reciveData?.Abort();

            //Reinicio todos los valores al base
            addres = null;
            port = -1;
            isServer = false;
            clientID = -1;

            //ClientAmount = 0;
            clientAmount = 0;

            //Llamo al evento de desconeccion
            OnDiscconect?.Invoke();
        }
    }

    public enum BasicCommand
    {
        NewConnection,
        Desconection,
        ServerDesconection,
    }
}
