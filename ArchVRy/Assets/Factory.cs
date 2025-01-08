using System.Collections;
using UnityEngine;

public class Factory : MonoBehaviour
{
    GameObject orc;
    public Transform[] spawningPoints;
    int enemyCounter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        orc = Resources.Load<GameObject>("Orc");
        Instantiate(orc, this.transform.position, Quaternion.identity);
        enemyCounter++;
        StartCoroutine(SpawnOrc());
    }
    IEnumerator SpawnOrc()
    {
        yield return new WaitForSeconds(5);
        Transform spawnPoint = spawningPoints[Random.Range(0, spawningPoints.Length)];
        Instantiate(orc, spawnPoint.position, Quaternion.identity);
        enemyCounter++;
        StartCoroutine(SpawnOrc());
    }
    void Update()
    {

    }
}
