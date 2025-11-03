using UnityEngine;
using System.Collections;

namespace Gio
{
    public class MoveToTarget : MonoBehaviour
    {
        [Header("Movimiento base")]
        public float moveSpeed = 2f;
        private Transform target;
        private bool isMoving = false;
        private GameObject lastTargetObject;

        [Header("Destino alternativo (opcional)")]
        public bool useAlternateDestination = false;
        public Transform alternateDestination;
        public bool teleportToAlternate = false;

        public void MoveTowards(Transform newTarget)
        {
            // Reactivar el trigger anterior si existe
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

            // ✅ Si tiene destino alternativo, usamos esa ruta
            if (useAlternateDestination && alternateDestination != null)
            {
                if (teleportToAlternate)
                {
                    // Teletransportar instantáneamente
                    transform.position = alternateDestination.position;
                    Debug.Log("✨ Teletransportado a punto alternativo: " + alternateDestination.name);
                    isMoving = false;
                    return;
                }
                else
                {
                    // Movimiento normal hacia el punto alternativo
                    target = alternateDestination;
                    Debug.Log("🚶 Moviéndose hacia punto alternativo: " + alternateDestination.name);
                }
            }

            isMoving = true;
        }

        void Update()
        {
            if (isMoving && target != null)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target.position,
                    moveSpeed * Time.deltaTime
                );

                // cuando llega al punto, deja de moverse
                if (Vector3.Distance(transform.position, target.position) < 0.1f)
                {
                    isMoving = false;
                }
            }
        }
    }
}
