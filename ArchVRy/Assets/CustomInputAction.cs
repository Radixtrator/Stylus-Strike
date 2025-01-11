using UnityEngine;
using UnityEngine.InputSystem;

public class CustomAction : MonoBehaviour
{
    public InputActionReference shootAction;
    public Arrow arrow;
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float shootForce = 50f;
    private LineRenderer lineRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootAction.action.performed += ShootArrow;

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void ShootArrow(InputAction.CallbackContext context)
    {
        Debug.Log("Shoot Arrow");
        arrow.isFlying = true;
        Transform parentTransform = arrow.transform.parent;
        if (parentTransform != null)
        {
            arrow.transform.SetParent(null, true);
        }
        Rigidbody rb = arrow.transform.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.linearVelocity = arrowSpawnPoint.forward * shootForce; // Adjust the speed as needed
        }
        RespawnArrow();
    }
    private void RespawnArrow()
    {
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        arrow = newArrow.GetComponent<Arrow>();
        arrow.transform.SetParent(arrowSpawnPoint);
    }

}
