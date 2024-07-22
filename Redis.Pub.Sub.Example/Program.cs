using StackExchange.Redis;

var connection = await ConnectionMultiplexer.ConnectAsync("localhost:1453"); // Redis ile connection qururuq

ISubscriber subscriber = connection.GetSubscriber(); //bir subscriber yaradiriq

while (true)
{
    Console.Write("Mesaj : ");
    string message = Console.ReadLine();
    await subscriber.PublishAsync("mychannel",message);
}