using Metrics;
using NetMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DistributedWorkshop.Seeker
{
    class Program
    {
        static void Main(string[] args)
        {
			string serverPrefix = "tcp://192.168.1.{0}:5559";

			int rangeStart = 23;
			int rangeEnd = 25;

			using(NetMQContext ctx = NetMQContext.Create())
			using(var client = ctx.CreateDealerSocket())
			{
				//client.Options.ReceiveTimeout = TimeSpan.FromMilliseconds(5000);

				for(int i = rangeStart; i <= rangeEnd; i++)
				{
					var server = string.Format(serverPrefix, i);
					Console.WriteLine(server);

					client.Connect(server);
				}

				Console.WriteLine("Connected");

				while (true)
				{
					try
					{
						Console.WriteLine(client.ReceiveString(TimeSpan.FromSeconds(2)));
					}catch{
						Console.WriteLine(".");
					}


				}
			}
        }
    }
}
