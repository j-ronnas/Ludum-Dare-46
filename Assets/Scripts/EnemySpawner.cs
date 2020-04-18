using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject enemyPrefab;

    Enemy lastSpawned;
    void Start()
    {
        
    }

    // Update is called once per frame
    float time;
    float spawnTime = 3f;
    void Update()
    {
        time += Time.deltaTime;
        if(time >= spawnTime)
        {
            SpawnEnemy();
            time -= spawnTime;
        }
    }

    void SpawnEnemy()
    {
        GameObject go = Instantiate(enemyPrefab, transform.position, Quaternion.identity, this.transform);

        go.GetComponent<Enemy>().enemyAhead = lastSpawned;

        lastSpawned = go.GetComponent<Enemy>();

    }
}
