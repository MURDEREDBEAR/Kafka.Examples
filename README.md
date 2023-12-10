# Kafka.Examples
_✨ The test code used on .NET OpenDay✨_

### Requirements
1. __Docker__: https://docs.docker.com/get-docker/
2. __.NET 6 SDK__: https://dotnet.microsoft.com/en-us/download/dotnet/6.0

### Optional software

1. __KafkaMagic__ - kind of topic viewer, included in repository inside KafkaMagic.zip (Windows x64 version) - version for other platforms, and newer Windows versions are available: https://www.kafkamagic.com/download/
2.2. Unpack included .zip file, and start `KafkaMagic.exe` - GUI should be available on the link printed inside the console.
2. __Conduktor__ - another software for an example to look inside cluster and view data in the topics: https://www.conduktor.io/download/
3. __Postman__ - application to call Producer API (attached swagger should be enough but that depends on the user what prefers): https://www.postman.com/downloads/

### Packages used in solution

| Plugin | Type | README |
| ------ | ------ | ------ |
| Zookeeper | Docker image | https://hub.docker.com/r/confluentinc/cp-zookeeper |
| Apache Kafka | Docker image | https://hub.docker.com/r/confluentinc/cp-kafka |
| Confluent.Kafka | Nuget package | https://www.nuget.org/packages/Confluent.Kafka/ |
| Swashbuckle.AspNetCore | Nuget package | https://www.nuget.org/packages/Swashbuckle.AspNetCore |

## How to use

> __Note:__ Make sure you have started `Docker Desktop` app - the docker engine.

Run the command from the main repository folder:
```sh
docker-compose up
```
that would trigger defined in _docker-compose.yml_ file defined two containers:
- `broker-demo`, local port `9092`
- `zookeeper-demo`, local port `2181`

Open `Kafka.Demo.sln` from `/Apps` folder.
Build both projects, the Consumer and the Producer `.cproj` in your favorite IDE - `Rider`, `Visual Studio`, it doesn't matter but would give better experience.
1. __Producer__ should start on http://localhost:5000, and has defined the endpoint `/Message` with `MessageDto` that includes properties:
1.1. _Value_ - the message value, it can be something, the input type is a _string_
1.2. _SpecifiedTopicName_ - optional field that determines the topic name different than the default `kafka.example`
> Note: _Producers_ Swagger URL is default and it's available using the link: https://localhost:5001/swagger/index.html

2. __Cosumer__ has defined `appsettings.json`:
2.1. Topic - topic name, default is: `kafka.example`
2.2. Host - broker hostname, default is: `localhost:9092`
2.3. GroupId - consumer group id, default is: `consumerGroupA`
2.4. IsCommitEnabled - determines the consumer behavior to send feedback to the Kafka cluster when a message has been consumed, default is: false

> Note: On _Consumer_ you can get an error: `Consumer error: Broker: Unknown topic or partition`, which is normal, when you send the first message using _Producer_ that should create expected _Topic_ and _Consumer_ should receive your first message.

## Helpful links
The online sources used and/or mentioned to create the presentation:
- https://kafka.apache.org
- https://docs.confluent.io/clients-confluent-kafka-dotnet/current/overview.html
- https://aws.amazon.com/msk/
- https://dzone.com/articles/real-world-examples-and-use-cases-for-apache-kafka
- https://www.red-gate.com/simple-talk/development/dotnet-development/using-apache-kafka-with-net/
- https://github.com/confluentinc/confluent-kafka-dotnet
- https://www.codemag.com/Article/2201061/Working-with-Apache-Kafka-in-ASP.NET-6-Core
- https://github.com/confluentinc/confluent-kafka-dotnet/ 
- https://thecloudblog.net/post/building-reliable-kafka-producers-and-consumers-in-net/ 
- https://www.kafkamagic.com 
- https://www.conduktor.io/kafka/ 
- https://www.uber.com/en-PL/blog/kafka-async-queuing-with-consumer-proxy/ 
- https://engineering.linkedin.com/blog/2019/apache-kafka-trillion-messages 
- https://www.confluent.io/blog/how-kafka-is-used-by-netflix/
- https://www.confluent.io/kafka-summit-nyc17/every-message-counts-kafka-foundation-highly-reliable-logging-airbnb/ 

**Cheers and happy coding!**
