using UnityEngine;
using System.Collections;

public class MoveToTarget : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Transform target;
    private bool isMoving = false;
    private GameObject lastTargetObject; // para recordar el último trigger

    public void MoveTowards(Transform newTarget)
    {
        target = newTarget;
        lastTargetObject = newTarget.gameObject;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving && target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            // cuando llega
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                isMoving = false;
                // Avisamos al trigger que desaparezca
                TriggerDisappear();
            }
        }
    }

    void TriggerDisappear()
    {
        if (lastTargetObject != null)
        {
            lastTargetObject.SetActive(false); // desaparece el trigger actual
        }
    }

    public void ReactivateLastTarget()
    {
        if (lastTargetObject != null)
        {
            lastTargetObject.SetActive(true); // reactiva el último trigger
        }
    }
}
