using System;

namespace EventSourcing.Utility
{
    public interface ISerializer
    {
        string Serialize(object command, string[] exclusions);
        T Deserialize<T>(string value, Type type);
        string Serialize<T>(T value);
    }
}
