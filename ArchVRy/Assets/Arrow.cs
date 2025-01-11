using UnityEngine;
using UnityEngine.InputSystem;

public class Arrow : MonoBehaviour
{
    public bool isFlying = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void OnCollisionEnter(Collision collision)
    {
        if(isFlying)
        {
            isFlying = false;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                transform.SetParent(collision.transform, true);
            }
        }
        Debug.Log("Arrow collided with " + collision.gameObject.name);
    }
   
}
