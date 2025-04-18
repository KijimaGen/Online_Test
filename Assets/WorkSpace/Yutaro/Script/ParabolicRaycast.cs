using UnityEngine;

public class ParabolicRaycast : MonoBehaviour
{
    public Transform startPoint;
    public Vector3 initialVelocity;
    public float timeStep = 0.1f;
    public int maxSteps = 100;
    public float gravity = -9.81f;
    public LayerMask hitMask;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SimulateParabola();
        }
    }

    void SimulateParabola()
    {
        Vector3 currentPosition = startPoint.position;
        Vector3 velocity = initialVelocity;

        for (int i = 0; i < maxSteps; i++)
        {
            Vector3 nextVelocity = velocity + Vector3.up * gravity * timeStep;
            Vector3 nextPosition = currentPosition + velocity * timeStep;

            if (Physics.Linecast(currentPosition, nextPosition, out RaycastHit hit, hitMask))
            {
                Debug.Log("Hit: " + hit.collider.name);
                Debug.DrawLine(currentPosition, hit.point, Color.red, 2f);
                break;
            }

            Debug.DrawLine(currentPosition, nextPosition, Color.green, 2f);

            currentPosition = nextPosition;
            velocity = nextVelocity;
        }
    }
}