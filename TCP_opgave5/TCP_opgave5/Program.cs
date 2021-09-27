using System;

namespace TCP_opgave5
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerWorker worker = new ServerWorker();
            worker.Start();
        }
    }
}
