using UnityEngine;
using UnityEngine.InputSystem;

public class CustomAction : MonoBehaviour
{
    public InputActionReference shootAction;
    public InputActionReference reloadAction;
    public Arrow arrow;
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float shootForce = 50f;
    private LineRenderer lineRenderer;
    public GameObject stylus;
    public GameObject leftController;
    bool arrowLoaded = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootAction.action.performed += ShootArrow;
        reloadAction.action.performed += ReloadArrow;

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
        float distance = Vector3.Distance(stylus.transform.position, leftController.transform.position);
        Debug.Log("Distance between stylus and left controller: " + distance);

        // Define the minimum and maximum distances
        float minDistance = 0.17f;
        float maxDistance = 0.27f;

        // Define the minimum and maximum shoot forces
        float minShootForce = 50f * 0.1f;
        float maxShootForce = 50f * 1f;

        // Calculate the shoot force based on the distance using linear interpolation
        if (distance <= minDistance)
        {
            shootForce = maxShootForce;
        }
        else if (distance >= maxDistance)
        {
            shootForce = minShootForce;
        }
        else
        {
            float t = (distance - minDistance) / (maxDistance - minDistance);
            shootForce = Mathf.Lerp(maxShootForce, minShootForce, t);
        }

    }
    public void ShootArrow(InputAction.CallbackContext context)
    {
        arrowLoaded = false;
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
        //RespawnArrow();
    }
    private void ReloadArrow(InputAction.CallbackContext context)
    {
        if (!arrowLoaded)
        {
            arrowLoaded = true;
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            arrow = newArrow.GetComponent<Arrow>();
            arrow.transform.SetParent(stylus.transform);
        }
    }

}
