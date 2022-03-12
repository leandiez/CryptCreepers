using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            //Hace un poder
            Destroy(gameObject, 0.1f);
        }
    }

    public abstract void RunPowerUp();
}
