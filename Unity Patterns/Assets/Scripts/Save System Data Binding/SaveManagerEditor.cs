using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveLoadSystem))]
public class SaveManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SaveLoadSystem saveLoadSystem = (SaveLoadSystem)target;
        string gameName = saveLoadSystem.gameData.Name;
        if (GUILayout.Button("New Game"))
        {
            saveLoadSystem.NewGame();
        }
        if (GUILayout.Button("Save Game"))
        {
            saveLoadSystem.SaveGame();
        }
        if (GUILayout.Button("Load Game"))
        {
            saveLoadSystem.LoadGame(gameName);
        }
        if (GUILayout.Button("Delete Game"))
        {
            saveLoadSystem.DeleteGame(gameName);
        }
        if (GUILayout.Button(gameName))
        {
            saveLoadSystem.DeleteAll();
        }
    }
}