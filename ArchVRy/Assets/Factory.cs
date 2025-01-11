using System.Collections;
using UnityEngine;
using TMPro;

public class Factory : MonoBehaviour
{
    GameManager gameManager;
    GameObject orc;
    public Transform[] spawningPoints;
    int enemyCounter = 0;
    public GameObject[] enemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        orc = enemies[0];
        
    }
    IEnumerator SpawnOrc()
    {
        yield return new WaitForSeconds(5);
        Transform spawnPoint = spawningPoints[Random.Range(0, spawningPoints.Length)];
        Instantiate(orc, spawnPoint.position, Quaternion.identity);
        enemyCounter++;
        if (enemyCounter <= gameManager.maxEnemies) StartCoroutine(SpawnOrc());
        else StopCoroutine(SpawnOrc());
    }
    void Update()
    {
        
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnOrc());
    }

}
