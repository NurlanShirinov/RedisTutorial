using StackExchange.Redis;

var connection = await ConnectionMultiplexer.ConnectAsync("localhost:1453"); // Redis ile connection qururuq

ISubscriber subscriber = connection.GetSubscriber(); //bir subscriber yaradiriq

await subscriber.SubscribeAsync("mychannel.*", (channel, message) =>
{
    Console.WriteLine(message);
});

Console.Read();