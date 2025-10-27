using UnityEngine;
using System.Collections;

public class MoveToTarget : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Transform target;
    private bool isMoving = false;
    private GameObject lastTargetObject;

    public void MoveTowards(Transform newTarget)
    {
        // Reactivar el trigger anterior
        if (lastTargetObject != null && lastTargetObject != newTarget.gameObject)
        {
            lastTargetObject.SetActive(true);
        }

        target = newTarget;
        lastTargetObject = newTarget.gameObject;

        // 🔥 Desactivar el collider del trigger
        Collider c = lastTargetObject.GetComponent<Collider>();
        if (c != null) c.enabled = false;

        // 🔥 Ocultar el trigger visualmente
        lastTargetObject.SetActive(false);

        isMoving = true;
    }


    void Update()
    {
        if (isMoving && target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            // cuando llega al punto, deja de moverse
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}
