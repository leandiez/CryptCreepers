using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] float enemySpeed = 1;

    Transform playerPosition;
    private void Start() {
        playerPosition = FindObjectOfType<PlayerController>().transform;
    }
    private void Update() {
        Vector2 playerDirection = playerPosition.position - transform.position;
        transform.position += (Vector3)playerDirection.normalized * Time.deltaTime * enemySpeed;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        other.GetComponent<PlayerController>().TakeDamage();
    }

    public void TakeDamage(){
        health--;
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
