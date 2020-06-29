using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileReadWrite 
{
    public static void WriteToBinaryFile<T>(string filePath, T objectToWrite)
    {
        using (Stream stream = File.Open(filePath, FileMode.Create))
        {
            var binaryFormartter = new BinaryFormatter();
            binaryFormartter.Serialize(stream, objectToWrite);
            stream.Close();
        }
    }

    public static T ReadFromBinaryFile<T>(string filePath)
    {
        using (Stream stream = File.Open(filePath, FileMode.Open))
        {
            var binaryFormartter = new BinaryFormatter();
            return (T)binaryFormartter.Deserialize(stream);
        }
    }
}
