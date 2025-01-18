using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : PersistentSingleton<Debugger>
{
    public void Debug(int value)
    {
        UnityEngine.Debug.Log($"Value changed to {value}");
    }
}
