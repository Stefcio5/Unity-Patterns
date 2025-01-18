using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public Observer<int> health = new Observer<int>(100);

    void Start()
    {
        health.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            health.Value += 10;
        }
    }
}
