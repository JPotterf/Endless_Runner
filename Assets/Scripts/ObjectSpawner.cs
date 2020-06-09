using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private bool spawningObject = false;
    public static ObjectSpawner instance;
    private float totalWeight;
    [SerializeField] private float groundSpawnDistance = 50f;
    public List<Spawnable> enemySpawnables = new List<Spawnable>();
    public List<SpawnSettings> spawnSettings = new List<SpawnSettings>();
    

    [System.Serializable]
    public struct Spawnable
    {
        public string type;
        public float weight;
    }
    
    [System.Serializable]
    public struct SpawnSettings
    {
        public string type;
        public float minWait;
        public float maxWait;
        public int maxObjects;
    }
    

    private void Awake()
    {
        instance = this;
        totalWeight = 0;
        foreach(Spawnable spawnable in enemySpawnables)
        {
            totalWeight += spawnable.weight;
        }
    }

    public void SpawnGround()
    {
        ObjectPooler.instance.SpawnFromPool("ground", new Vector3(0, 0, groundSpawnDistance), Quaternion.identity);
    }


    private IEnumerator SpawnObject(string type, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPooler.instance.SpawnFromPool(type, new Vector3(Random.Range(-4.35f, 4.35f), 0.5f, -10f),Quaternion.identity);
        spawningObject = false;
        GameController.EnemyCount++;
    }

    void Update()
    {
        if(!spawningObject && GameController.EnemyCount < spawnSettings[0].maxObjects)
        {
            spawningObject = true;
            float pick = Random.value * totalWeight;
            int chosenIndex = 0;
            float cumulativeWeight = enemySpawnables[0].weight;

            while(pick > cumulativeWeight && chosenIndex < enemySpawnables.Count - 1)
            {
                chosenIndex++;
                cumulativeWeight += enemySpawnables[chosenIndex].weight;
            }

            //calculates an increasing spawn difficulty & time based on 
            //distance in game, minWait spawn time and maxWait spawn time
            StartCoroutine(SpawnObject(enemySpawnables[chosenIndex].type, Random.Range(spawnSettings[0].minWait / GameController.DifficultyMultiplier, spawnSettings[0].maxWait / GameController.DifficultyMultiplier)));
        }   
    }
}
