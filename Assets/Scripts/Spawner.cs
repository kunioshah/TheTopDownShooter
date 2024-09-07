using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject chocolateEnemy;
    public GameObject whiteChocEnemy;
    public GameObject player;

    public float spawnTime;
    public float wcSpawnChance;
    public GameManager myManager;

    public TMP_Text healthText;
    public TMP_Text scoreText;



    // Start is called before the first frame update
    void Start()
    {
         healthText.text = ("Health: " + myManager.health);
         StartCoroutine(spawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if (myManager.isPlayerDead) {
            StartCoroutine(spawnNewPlayer());
            myManager.isPlayerDead = false;
        }

        healthText.text = ("Health: " + myManager.health);
        scoreText.text = ("Enemies Defeated: " + myManager.numOfEnemiesDefeated);

    }

    private IEnumerator spawnEnemies() {
        
        yield return new WaitForSeconds(spawnTime * (30f/(myManager.numOfEnemiesDefeated + 30)));

        float randNum = (Random.Range(0f,4f));

        if (randNum < 1) {
            spawnEnemyNow(-11f, 11f, 6f, 6f);
        } else if (randNum < 2) {
            spawnEnemyNow(-11f, 11f, -6f, -6f);
        } else if (randNum < 3) {
            spawnEnemyNow(11f, 11f, -6f, 6f);
        } else if (randNum <= 4) {
            spawnEnemyNow(-11f, -11f, -6f, 6f);
        }

        StartCoroutine(spawnEnemies());
    }

    void spawnEnemyNow(float x1, float x2, float y1, float y2) {
        if (Random.Range(0f, 1f) <= (wcSpawnChance/100f))    {
            Instantiate(whiteChocEnemy, new Vector3(Random.Range(x1, x2), Random.Range(y1, y2), 0), Quaternion.identity);
         }
        else {
            Instantiate(chocolateEnemy, new Vector3(Random.Range(x1, x2), Random.Range(y1, y2), 0), Quaternion.identity);
        }
    }

    private IEnumerator spawnNewPlayer() {
        myManager.numOfEnemiesDefeated = 0;
        yield return new WaitForSeconds(3f);
        myManager.shouldEnemiesDie = false;
        Instantiate(player);
    }


}
