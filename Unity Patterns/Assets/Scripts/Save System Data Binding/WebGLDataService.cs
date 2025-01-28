using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WebGLDataService : IDataService
{
    private ISerializer serializer;
    private const string SAVE_KEY_PREFIX = "Save_";

    public WebGLDataService(ISerializer serializer)
    {
        this.serializer = serializer;
    }

    public void Save(GameData data, bool overwrite = true)
    {
        string key = SAVE_KEY_PREFIX + data.Name;

        if (!overwrite && PlayerPrefs.HasKey(key))
        {
            throw new IOException($"Save {data.Name} already exists and cannot be overwritten.");
        }
        string value = serializer.Serialize(data);

        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public GameData Load(string name)
    {
        string key = SAVE_KEY_PREFIX + name;

        if (!PlayerPrefs.HasKey(key))
        {
            throw new FileNotFoundException($"Save {name} not found.");
        }
        return serializer.Deserialize<GameData>(PlayerPrefs.GetString(key));
    }

    public IEnumerable<string> ListSaves()
    {
        throw new System.NotImplementedException();
    }

    public void Delete(string name)
    {
        throw new System.NotImplementedException();
    }

    public void DeleteAll()
    {
        throw new System.NotImplementedException();
    }

}
