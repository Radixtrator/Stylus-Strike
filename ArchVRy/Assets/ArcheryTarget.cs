using UnityEngine;

public class ArcheryTarget : MonoBehaviour
{
    GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "arrow")
        {
            gameManager.StartGame();
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
