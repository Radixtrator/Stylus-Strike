using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arrow : MonoBehaviour
{
    public bool isFlying = false;
    public CustomAction inputAction;
    public Boolean aiming = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputAction = GameObject.FindGameObjectWithTag("Player").GetComponent<CustomAction>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnColliderEnter(Collider other)
    {
        if (isFlying)
        {
            isFlying = false;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.linearVelocity = Vector3.zero;
                transform.SetParent(other.transform, true);
            }
        }
        Debug.Log("Arrow collided with " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "drawstring")
        {
            Debug.Log("Should now shoot");
            if(aiming)inputAction.ShootArrow();
        }
    }
}
