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
    //Editor variables
    [SerializeField] float playerSpeed = 5;
    [SerializeField] float health = 5;
    [SerializeField] Transform aimTransform;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject bullet;

    void Start()
    {
        
    }
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
            Instantiate (bullet, transform.position , targetRotation);
            StartCoroutine(ReloadGun());
        }
    }

    IEnumerator ReloadGun(){
        yield return new WaitForSeconds(1);
        gunLoaded = true;
    }

    public void TakeDamage(){
        health--;
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
