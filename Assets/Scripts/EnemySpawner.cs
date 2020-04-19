using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject enemyPrefab;

    Enemy lastSpawned;
    int level;

    int[] unitCounter;
    int noSpawned;
    int noKilled;

    [SerializeField]
    UpgradeManager upgradeManager;

    [SerializeField]
    Text nextWaveText;

    
    void Start()
    {
        level = -1;
        InitLevels();
    }

    void InitLevels()
    {
        unitCounter = new int[10];
        for (int i = 0; i < 10; i++)
        {
            unitCounter[i] = Mathf.CeilToInt(0.5f*i*i + 2*i +1);
        }
    }

    // Update is called once per frame
    float time;
    float spawnTime;
    void Update()
    {
        if(level < 0)
        {
            return;
        }
        if (noSpawned >= unitCounter[level])
        {
            Debug.Log("Finished Spawning!");
            return;
        }

        time += Time.deltaTime;
        if(time >= spawnTime)
        {
            SpawnEnemy();
            time -= spawnTime;
            spawnTime = Random.Range(0.5f, 3f - level*0.2f);
            noSpawned++;
        }
    }

    void SpawnEnemy()
    {
        GameObject go = Instantiate(enemyPrefab, transform.position, Quaternion.identity, this.transform);

        go.GetComponent<Enemy>().enemyAhead = lastSpawned;
        go.GetComponent<Enemy>().speed = 1.5f + level * 0.4f;

        lastSpawned = go.GetComponent<Enemy>();

    }

    public void PlayLevel()
    {
        
        level++;

        if (level >= unitCounter.Length)
        {
            upgradeManager.Win();
            return;
        }
        lastSpawned = null;
        spawnTime = 1f;
        noSpawned = 0;
        noKilled = 0;

        nextWaveText.text = level < unitCounter.Length - 1 ? "Enemies next day: " + unitCounter[level + 1].ToString() : "Final level!";
        
    }

    public void OnUnitDeath(float zPos)
    {
        upgradeManager.EnemyKilled(zPos);
        noKilled++;
        if(noKilled >= unitCounter[level])
        {
            if (level >= unitCounter.Length -1)
            {
                upgradeManager.Win();
                return;
            }
            upgradeManager.OnLevelFinish();
        }
        Debug.Log("NO killed: "  + noKilled);
    }
}
