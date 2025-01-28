using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public float gold;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gold += 10;
        }
    }
}