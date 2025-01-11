using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AudioSource hornSound;
    Gate gate;
    Factory factory;
    public int maxEnemies = 50;
    public TMP_Text gateHealth;
    bool gameStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gate = GameObject.FindGameObjectWithTag("Gate").GetComponent<Gate>();
        factory = GameObject.FindGameObjectWithTag("spawner").GetComponent<Factory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gate.health >= 0) gateHealth.text = "Gate Health: " + gate.health;
        else gateHealth.text = "Game Over, shoot target to restart";

    }
    public void StartGame()
    {
        if (!gameStarted)
        {
            factory.StartSpawning();
            gameStarted = true;
            hornSound.Play();
        }
    }
    public void restartGame()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        gate.health = 50;
        factory.StartSpawning();
    }
}
