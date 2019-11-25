using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerDebug : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject enemyPrefab;

    bool started = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while(true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(8f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !started)
        {
            StartCoroutine(SpawnEnemiesRoutine());
            started = true;
        }
    }
}
