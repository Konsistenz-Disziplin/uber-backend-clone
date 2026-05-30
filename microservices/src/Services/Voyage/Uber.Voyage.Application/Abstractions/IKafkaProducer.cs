using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Voyage.Application.Abstractions;

public interface IKafkaProducer
{
    Task PublishAsync<T>(string topic, string key, T message, CancellationToken ct) where T : class;
}
