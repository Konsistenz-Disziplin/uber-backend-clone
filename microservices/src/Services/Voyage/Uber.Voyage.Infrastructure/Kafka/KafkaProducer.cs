using System;
using System.Collections.Generic;
using System.Text;
using Uber.Voyage.Application.Abstractions;

namespace Uber.Voyage.Infrastructure.Kafka;

internal class KafkaProducer : IKafkaProducer

{
    public Task PublishAsync<T>(string topic, string key, T message, CancellationToken ct) where T : class
    {
        throw new NotImplementedException();
    }
}
