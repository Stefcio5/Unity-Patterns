using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeroData : ISaveable
{
    [SerializeField]
    public string Id { get; set; } = "Hero";
    public int Health;
    public int damage;
}
public class Hero : MonoBehaviour, IBind<HeroData>
{
    [SerializeField]
    public string Id { get; set; } = "Hero";
    public Observer<int> health = new Observer<int>(100);
    public int damage;

    [SerializeField] HeroData data;

    public void Bind(HeroData data)
    {
        this.data = data;
        this.data.Id = Id;
        health.Value = data.Health;
        damage = data.damage;
    }
    void Start()
    {
        health.Invoke();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            health.Value += 10;
        }

        data.Health = health.Value;
        damage = health.Value * 2;
        data.damage = damage;
    }
}
