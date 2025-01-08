using UnityEngine;

public class Gate : MonoBehaviour
{
    public int health = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (health <= 0)
      {
          Debug.Log("Gate destroyed");
      }  
    }
}
