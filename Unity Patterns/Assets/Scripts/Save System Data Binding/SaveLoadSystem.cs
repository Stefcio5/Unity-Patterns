using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameData
{
    public string Name;
    public string CurrentLevelName;
    public HeroData heroData;
    public UpgradesSaveData upgradeSaveData;
}

public interface ISaveable
{
    string Id { get; set; }
}

public interface IBind<TData> where TData : ISaveable
{
    //Make sure to have a unique id for each saveable object
    string Id { get; set; }
    void Bind(TData data);
}

public class SaveLoadSystem : PersistentSingleton<SaveLoadSystem>
{
    IDataService dataService;
    [SerializeField]
    public GameData gameData;

    protected override void Awake()
    {
        base.Awake();
        //dataService = new FileDataService(new JsonSerializer());
        ISerializer serializer = new JsonSerializer();
        dataService = StorageServiceFactory.Create(serializer);
    }

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu") return;

        Bind<Hero, HeroData>(gameData.heroData);
        Bind<UpgradeManager, UpgradesSaveData>(gameData.upgradeSaveData);
    }

    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
    {
        var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
        if (entity != null)
        {
            if (data == null)
            {
                data = new TData { Id = entity.Id };
            }
            entity.Bind(data);
        }
    }

    void Bind<T, TData>(List<TData> dataList) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
    {
        var entities = FindObjectsByType<T>(FindObjectsSortMode.None);
        foreach (var entity in entities)
        {
            var data = dataList.FirstOrDefault(d => d.Id == entity.Id);
            if (data == null)
            {
                data = new TData { Id = entity.Id };
                dataList.Add(data);
            }
            entity.Bind(data);
        }
    }

    public void NewGame()
    {
        gameData = new GameData
        {
            Name = "Game",
            CurrentLevelName = "SampleScene"
        };
        //TODO: Set up scene
        SceneManager.LoadScene(gameData.CurrentLevelName);
    }

    public void SaveGame() => dataService.Save(gameData);

    public void LoadGame(string gameName)
    {
        gameData = dataService.Load(gameName);
        if (string.IsNullOrWhiteSpace(gameData.CurrentLevelName))
        {
            gameData.CurrentLevelName = "SampleScene";
        }
        SceneManager.LoadScene(gameData.CurrentLevelName);

    }

    public void DeleteGame(string gameName) => dataService.Delete(gameName);

    public void DeleteAll()
    {
        foreach (string gameName in dataService.ListSaves())
        {
            dataService.Delete(gameName);
        }
    }
}