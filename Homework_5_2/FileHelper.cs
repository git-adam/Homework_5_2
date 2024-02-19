using Newtonsoft.Json;
using System.IO;

namespace Homework_5_2
{
    public class FileHelper<T> where T : new()
    {

        private string _filePath;
        public FileHelper(string filePath)
        {
            this._filePath = filePath;
        }
        public void SerializeToFile(T employees)
        {
            var serializer = new JsonSerializer();
            using (StreamWriter streamWriter = new StreamWriter(_filePath))
            {
                serializer.Serialize(streamWriter, employees);
                streamWriter.Close();
            }

            //var json = JsonConvert.SerializeObject(employees);//serializacja
            //File.WriteAllText($"{_filePath}6XXX", json);//zapis do pliku
        }

        public T DeserializeFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return new T();
            }

            var serializer = new JsonSerializer();

            using (StreamReader streamReader = new StreamReader(_filePath))
            using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
            {
                var employees = serializer.Deserialize<T>(jsonTextReader);
                jsonTextReader.Close();
                streamReader.Close();
                return employees;
            }

            //if (!File.Exists($"{_filePath}6XXX"))
            //{
            //    return new T();
            //}

            //var json = File.ReadAllText($"{_filePath}6XXX");//odczyt z pliku
            //return JsonConvert.DeserializeObject<T>(json);//deserializacja

        }
    }
}
