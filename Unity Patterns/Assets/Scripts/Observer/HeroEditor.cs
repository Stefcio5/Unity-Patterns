using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hero))]
public class HeroEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Hero hero = (Hero)target;
        if (GUILayout.Button("Increase Health"))
        {
            hero.health.Value += 10;
        }

        if (GUILayout.Button("Decrease Health"))
        {
            hero.health.Value -= 10;
        }
        if (GUILayout.Button("Add Debugger"))
        {
            hero.health.AddListener(Debugger.Instance.Debug);
        }
        if (GUILayout.Button("Remove Debugger"))
        {
            hero.health.RemoveListener(Debugger.Instance.Debug);
        }

    }
}
