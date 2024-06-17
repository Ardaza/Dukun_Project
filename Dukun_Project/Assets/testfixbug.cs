using UnityEngine;

public class testfixbug : MonoBehaviour
{
    public float interactionRadius = 2f;
    public LayerMask interactionLayer;
    public LayerMask obstacleLayer;

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRadius, interactionLayer);
            foreach (var hitCollider in hitColliders)
            {
                Vector3 directionToTarget = hitCollider.transform.position - transform.position;
                if (!Physics.Raycast(transform.position, directionToTarget, directionToTarget.magnitude, obstacleLayer))
                {
                    Debug.Log("Interacted with " + hitCollider.name);
                    // Add interaction code here
                }
                else
                {
                    Debug.Log("Blocked by obstacle");
                }
            }
        }
    }
}
