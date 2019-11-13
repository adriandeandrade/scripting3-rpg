using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerDebug : MonoBehaviour
{
    // Inspector Fields
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject enemyPrefab;

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
}
