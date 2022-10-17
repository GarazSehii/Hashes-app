# HASH Application
>by Garaz Serhii

## Software requirements:

- .NET 6.0
- SQL Server Express LocalDB (default setup)
- [RabbitMQ](https://www.rabbitmq.com/download.html) (default setup)
- [Angular2](https://angular.io/)
- [Node.js](http://nodejs.org)
## Run Application

1.Run the bat-file: 
```sh
start.bat
```

2.Go to one of links below:



| Page | Link |
| ------ | ------ |
| UI | https://localhost:44494/ |
| Swagger| https://localhost:7058/swagger |
| RabbitMQ Managment| http://localhost:15672/#/queues |



## How we can be improved
1) Perfomance can be slightly improved by using cache, like: https://github.com/ZiggyCreatures/FusionCache
2) Interaction with RabbitMQ can be optimized through Mastransit pattern (https://github.com/MassTransit/Sample-Direct), so full-fledged consumers can be implemented which will be initiated by notification in the Rabbit queue, and not as a worker.








