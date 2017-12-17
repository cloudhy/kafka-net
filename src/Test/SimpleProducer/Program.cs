using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using Dapper;
using Newtonsoft;
using Newtonsoft.Json;

namespace SimpleProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new KafkaOptions(new Uri("http://192.168.2.171:9092"), new Uri("http://192.168.2.172:9092"));
            var router = new BrokerRouter(options);
            var client = new Producer(router);

            List<SampleModel> models = new List<SampleModel>();
            for(int i=0; i<1000;i++)
            {
                SampleModel model = new SampleModel(new Random(100).Next(), new Random(100).Next(), new Random(100).Next());

                models.Add(model);
            }

            string message= JsonConvert.SerializeObject(models);


            client.SendMessageAsync("TestHarness", new[] { new Message(message) }).Wait();

            using (client) { }
        }
    }
}
