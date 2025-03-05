using Confluent.Kafka;

namespace officeapi.Services
{
   public interface IKafkaProducer
    {
        public Task CreateMessage(string msg, string mytopic = "my-topic");
    }
    public class KafkaProducer: IKafkaProducer
    {
        public KafkaProducer()
        {
            string zookeeperdir = @"C:\kafka\kafka\bin\windows";
            string cmdzookeeper = "/C zookeeper-server-start.bat ../../config/zookeeper.properties";
            string cmdkafka = "/C kafka-server-start.bat ../../config/server.properties";
           
        }
        public async Task CreateMessage(string msg,string mytopic= "my-topic")
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = "my-app",
                BrokerAddressFamily = BrokerAddressFamily.V4
                
            };
            using
            var producer = new ProducerBuilder<Null,  string>(config).Build();
            var message = new Message<Null, string>
            {
                Value = msg,
            };
            try
            {
                var deliveryReport = await producer.ProduceAsync(mytopic, message);
              //  Console.WriteLine(mytopic + " : Message delivered");
            }
            catch (Exception  e)
            {
               // Console.WriteLine("Errorrrrrrrrrrrrrrrrrrrrrrrrrrr\n" + e.StackTrace);
            }
            
            
        }
       
    }
}
