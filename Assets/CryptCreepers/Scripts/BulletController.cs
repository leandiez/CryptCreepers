using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5;
    int health = 3;
    public bool isBigShot = false;

    private void Start() {
        Destroy(gameObject,5);
    }
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            other.GetComponent<EnemyController>().TakeDamage();
            if(!isBigShot){
                Destroy(gameObject);
            }
            health --;
            if(health <= 0){
                Destroy(gameObject);
            }
            
        }
    }
    


    
}
