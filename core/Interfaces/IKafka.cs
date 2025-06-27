using System;

namespace core.Interfaces;

public interface IKafka
{
    string ConsumeMessages(int bookmark, int offset);
    Task<bool> ProduceMessage(string message);
}