using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [Range (1.0f,10.0f)][SerializeField] float spawnRate;
    [SerializeField] GameObject[] enemyPrefab;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies(){
        while(GameManager.instance.gameIsRunning){
            yield return new WaitForSeconds(1/spawnRate);
            float random = Random.Range(0.0f,1.0f);
            if(random < GameManager.instance.gameDifficult * 0.1f){
                Instantiate(enemyPrefab[0]);
            }else{
                Instantiate(enemyPrefab[1]);
            }
        }
    }
}
