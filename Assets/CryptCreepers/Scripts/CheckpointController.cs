using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] int addedTime = 5;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            GameManager.instance.time += addedTime;
            Destroy(gameObject, 0.1f);
        }
    }
}
