using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb;
    GameObject player;
    public ParticleSystem chocolateExplosion;
    public int maxHealth;
    int health;
    public GameManager myManager;


    Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform) {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }

        if (health == 0) {
            Die();
        }

        if (myManager.isPlayerDead) {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (player.transform) {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Die();
        }

        if (collision.gameObject.tag == "Bullet") {
            health--;
        }

    }

    void Die() {
        Instantiate(chocolateExplosion, transform.position, transform.rotation);
        myManager.numOfEnemiesDefeated++;
        Destroy(gameObject);
    }
}
