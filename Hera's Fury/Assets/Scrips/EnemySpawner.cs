using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; //Drage enemy prefab here 
    [SerializeField] private float spawnRate = 2.0f; // secounds between spawns 

    private bool canSpawn = true; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // start the infinite spawning loop 
        StartCoroutine(SpawnerRoutine());
        
    }
    private IEnumerator SpawnerRoutine() 
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
 
        while (canSpawn)
        {
            yield return wait; // wait for the specified time 
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        //Instantiate the prefab at the spawner  postion with any rotation 
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
