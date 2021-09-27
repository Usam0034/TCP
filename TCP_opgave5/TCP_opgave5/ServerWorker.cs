using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Class_for_opg_4_5;

namespace TCP_opgave5
{
    class ServerWorker
    {
        public ServerWorker()
        {
        }

        public static List<Football> FootballPlayers = new List<Football>()
        {
            new Football(1, "Pogba", 600000, 5)
        };

        public void Start()
        {
            TcpListener listener = new TcpListener(7777);
            listener.Start();

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Task.Run(
                    () =>
                    {
                        TcpClient tmpSocket = socket;
                        DoClient(tmpSocket);
                    }
                );
            }


        }

        private void DoClient(TcpClient socket)
        {
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {

                String playerString = sr.ReadLine();
                String playerString2 = sr.ReadLine();

                switch (playerString)
                {
                    case "Hent Alle":
                        string Json = JsonSerializer.Serialize(FootballPlayers);
                        sw.WriteLine(Json);
                        sw.Flush();
                        break;
                    case "Hent":
                        int id = Convert.ToInt32(playerString2);
                        Football FootP = FootballPlayers.Find(P => P.id == id);
                        string Json2 = JsonSerializer.Serialize(FootP);
                        sw.WriteLine(Json2);
                        sw.Flush();
                        break;

                    case "Gem":
                        Football footballPlayer = JsonSerializer.Deserialize<Football>(playerString2);
                        FootballPlayers.Add(footballPlayer);
                        break;

                }
            }
            socket?.Close();
        }
    }
}
