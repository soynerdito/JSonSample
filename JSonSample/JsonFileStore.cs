using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JSonSample
{
    public class ConfigFileStruct
    {
        public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();
    }
    public class JsonFileStore : SSH.Stores.IStore
    {
        private ConfigFileStruct _settings = new ConfigFileStruct();

        protected string FileName => Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".possh"), "hosts.json");
        public IDictionary<string, string> GetKeys()
        {
            LoadFromDisk();
            return _settings.Keys;
        }

        public bool SetKey(string host, string fingerprint)
        {
            _settings.Keys.Remove(host);
            _settings.Keys.Add(host, fingerprint);
            WriteToDisk();
            return true;
        }

        public void LoadFromDisk()
        {
            var jsonString = File.ReadAllText(FileName);
            _settings = JsonSerializer.Deserialize<ConfigFileStruct>(jsonString);
        }
      
        private void WriteToDisk()
        {
            var jsonString = JsonSerializer
                .Serialize<ConfigFileStruct>(_settings, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            Directory.CreateDirectory(Path.GetDirectoryName(FileName));
            File.WriteAllText(FileName, jsonString);
        }
    }
}
