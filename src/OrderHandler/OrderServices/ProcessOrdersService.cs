using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using OrderDomain;
using OrderServices.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderServices
{
    public class ProcessOrdersService : BackgroundService
    {
        private readonly ConsumerConfig consumerConfig;
        private readonly ProducerConfig producerConfig;

        public ProcessOrdersService(ConsumerConfig consumerConfig, ProducerConfig producerConfig)
        {
            this.producerConfig = producerConfig;
            this.consumerConfig = consumerConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("OrderProcessing Service Started");

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumerHelper = new ConsumerWrapper(consumerConfig, "orderrequests");
                string orderRequest = consumerHelper.ReadMessage();

                //Deserialize
                OrderRequest order = JsonConvert.DeserializeObject<OrderRequest>(orderRequest);

                //TODO:: Process Order
                Console.WriteLine($"Info: OrderHandler => Processing the order for {order.ProductName}");
                order.Status = OrderStatus.Completed;

                //Write to ReadyToShip Queue
                var producerWrapper = new ProducerWrapper(producerConfig, "readytoship");
                await producerWrapper.WriteMessage(JsonConvert.SerializeObject(order));
            }
        }
    }
}
