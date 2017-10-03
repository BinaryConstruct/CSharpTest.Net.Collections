using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CSharpTest.Net.Serialization;

namespace CSharpTest.Net.Collections.Serialization
{
    /// <summary>
    /// Returns all bytes in the stream, or writes all bytes to the stream
    /// </summary>
    public class BinarySerializer<T> : ISerializer<T>
    {
        IFormatter formatter = new BinaryFormatter();

        void ISerializer<T>.WriteTo(T value, Stream stream)
        {
            formatter.Serialize(stream, value);
        }

        T ISerializer<T>.ReadFrom(Stream stream)
        {
            return (T)formatter.Deserialize(stream);
        }
    }
}
