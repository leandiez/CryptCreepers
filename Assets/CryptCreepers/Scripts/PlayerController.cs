using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Privates variables
    float h,v;
    Vector3 moveDirection;
    Vector2 facingDirection;
    bool gunLoaded = true;
    bool bigShotEnabled = false;
    bool isInvulnerable = false;
    //Editor variables
    [SerializeField] float playerSpeed = 5;
    [SerializeField] float health = 5;
    [SerializeField] Transform aimTransform;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject bullet;
    [Range (1.0f,10.0f)] [SerializeField] float fireRate = 1.0f;
    

    void Update()
    {
        //Player movement
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        moveDirection.x = h;
        moveDirection.y = v;
        transform.position += moveDirection * Time.deltaTime * playerSpeed; //Movedirection es [-1,1] segun los ejes, deltaTime es un normalizador del tiempo entre frames, playerSpeed define la velocidad maxima y aceleracion.

        //Player aim movement
        facingDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position; //Get the world position of mouse and make it independent of parent position
        aimTransform.position = transform.position + (Vector3) facingDirection.normalized; //Normalize the vector of facingDirection to put a limit betwieen 0 and 1, and adds the transform again to recover relativity.

        //Shot
        if(Input.GetMouseButton(0) && gunLoaded){
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg; //Get the angle in degree, needed to make rotate the bullet before instantiate
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward); //Get the rotation object to asign to the instantiation
            GameObject bigShot = Instantiate(bullet, transform.position , targetRotation);
            if(bigShotEnabled){
                bigShot.GetComponent<BulletController>().isBigShot = true;
            }
            
            StartCoroutine(ReloadGun());
        }
    }

    IEnumerator ReloadGun(){
        yield return new WaitForSeconds(1/fireRate);
        gunLoaded = true;
    }

    IEnumerator InvulnerabilityTime(){
        isInvulnerable = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(3);
        GetComponent<SpriteRenderer>().color = Color.white;
        isInvulnerable = false;
    }

    public void TakeDamage(){
        if(!isInvulnerable){
            health--;
            StartCoroutine(InvulnerabilityTime());
        }
        if(health <= 0){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PowerUp")){
            switch(other.GetComponent<PowerUpVariants>().powerUpType){
                case(PowerUpVariants.PowerUpType.FireRateIncrease):
                    fireRate++;
                    Destroy(other.gameObject,0.1f);
                    break;
                case(PowerUpVariants.PowerUpType.BigShot):
                    bigShotEnabled = true;
                    Destroy(other.gameObject,0.1f);
                    break;
            }
        }
    }
}
