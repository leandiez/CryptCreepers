using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject checkpointPrefab;
    [SerializeField] int spawnTime = 5;
    [SerializeField] float spawnRadius = 5;

    private void Start() {
        StartCoroutine(SpawnCheckpointRoutine());
    }

    IEnumerator SpawnCheckpointRoutine(){
        while(true){
            yield return new WaitForSeconds(spawnTime);
            Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;
            Instantiate(checkpointPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
