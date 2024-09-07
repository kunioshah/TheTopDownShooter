using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer sprRenderer;
     
    void Start()
    {
        Color sprinkleColor = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f) );
        sprRenderer.color = sprinkleColor;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
