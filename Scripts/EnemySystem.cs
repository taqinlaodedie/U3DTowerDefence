using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public Dictionary<string, Enemy> enemyDict = new Dictionary<string, Enemy>();  // Save all the enemies
    Enemy[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = Resources.LoadAll<Enemy>("Enemies");
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemyDict.ContainsKey(enemies[i].name))
                enemyDict.Add(enemies[i].name, enemies[i]);
        }
    }

    private void CreateEnemy(string name, float delay, int count = 1)
    {
        if (GameMain.instance.gameOver == false)
            //Create enemies with time task
            Util.Instance.AddTimeTask(() => Instantiate(
            enemyDict[name], transform.position, transform.rotation),
            delay, count);
    }

    // Click to create enemies
    public void ClickButtonDispatchTroops()
    {
        CreateEnemy("OrcFoot", 1, 5);
        Util.Instance.AddTimeTask(() => CreateEnemy("Orc1", 0.5f, 10), 5);
    }
}
