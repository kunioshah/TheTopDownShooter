using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update

    public float turnSpeed;
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 direction; 
    Vector2 movementVector; 
    float angle;

    public GameObject bullet;
    public float shootForce;
    public Transform shootLocation;

    public ParticleSystem iceCreamExplosion;
    public int maxHealth;
    int health;
    public GameManager myManager;


    void Start()
    {
        health = maxHealth;
        myManager.health = health;
        myManager.isPlayerDead = false;
        myManager.numOfEnemiesDefeated = 0;
    }

    void Update()
    {
        float rightInput = (Input.GetAxis("Horizontal"))*(moveSpeed)*UnityEngine.Time.deltaTime;
        float upInput = (Input.GetAxis("Vertical"))*(moveSpeed)*UnityEngine.Time.deltaTime;
        movementVector = new Vector2(rightInput, upInput).normalized;

       if (Input.GetMouseButtonDown(0)) {
            Shoot();
       }
 
       if (health == 0) {
           Die();
       }
         
    }

    void FixedUpdate() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 angleDirection = direction - rb.position;
        angle = Mathf.Atan2(angleDirection.y, angleDirection.x) * Mathf.Rad2Deg - 90f;

        // this worked but it slowed down rotation massively idk why?? ill revisit it later its fine for now
        // if ((mouseX!=0) || (mouseY!=0)) { 
            rb.rotation = angle;
        // }

        rb.velocity = new Vector2(movementVector.x * moveSpeed, movementVector.y * moveSpeed);
    }

    void Shoot() {
        GameObject newBullet = Instantiate(bullet, shootLocation.position, shootLocation.rotation);
        newBullet.GetComponent<Rigidbody2D>().AddForce(shootLocation.up * shootForce, ForceMode2D.Impulse);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "Enemy") {
            TakeDamage();
         }
    }

    void TakeDamage() {
        health--;
        myManager.health--;
    }

    void Die() {
        Instantiate(iceCreamExplosion, transform.position, transform.rotation);
        myManager.isPlayerDead = true;
        Destroy(gameObject);
    }

}
