using UnityEngine;
using System.Collections;

namespace Gio
{
    public class MoveToTarget : MonoBehaviour
    {
        [Header("Movimiento base")]
        public float moveSpeed = 2f;
        public float stopDistance = 0.2f;
        [Tooltip("Mantener altura actual al moverse (evita saltos en Y)")]
        public bool preserveY = true;
        [Header("Reaparición de triggers")]
        [Tooltip("Retraso antes de que reaparezca el punto anterior")]
        public float reappearDelay = 0.75f;

        private bool isMoving = false;
        private Vector3? targetPos = null;
        private GameObject lastTargetObject;

        [Header("Destino alternativo (opcional)")]
        public bool useAlternateDestination = false;
        public Transform alternateDestination;
        public bool teleportToAlternate = false;

        public void MoveTowards(Transform newTarget)
        {
            // Reaparecer el punto anterior con retraso (si es distinto)
            if (lastTargetObject != null && lastTargetObject != newTarget.gameObject)
            {
                StartCoroutine(ReenableAfterDelay(lastTargetObject, reappearDelay));
            }

            lastTargetObject = newTarget.gameObject;

            // Desactivar todos los colliders del trigger y su jerarquía
            SetCollidersEnabled(lastTargetObject, false);

            // Ocultar el trigger visualmente
            lastTargetObject.SetActive(false);

            // Definir punto de destino como posición (no referencia a Transform)
            if (useAlternateDestination && alternateDestination != null)
            {
                if (teleportToAlternate)
                {
                    Vector3 dest = alternateDestination.position;
                    if (preserveY) dest.y = transform.position.y;
                    transform.position = dest;
                    isMoving = false;
                    targetPos = null;
                    return;
                }
                else
                {
                    targetPos = alternateDestination.position;
                }
            }
            else
            {
                targetPos = newTarget.position;
            }

            isMoving = true;
        }

        void Update()
        {
            if (!isMoving || !targetPos.HasValue) return;

            Vector3 dest = targetPos.Value;
            if (preserveY) dest.y = transform.position.y;

            // Avanzar hacia el punto objetivo
            transform.position = Vector3.MoveTowards(
                transform.position,
                dest,
                moveSpeed * Time.deltaTime
            );

            // Al llegar cerca, detener sin "snap" para evitar salto visual
            if (Vector3.Distance(transform.position, dest) <= stopDistance)
            {
                isMoving = false;
                targetPos = null;
            }
        }

        private void SetCollidersEnabled(GameObject go, bool enabled)
        {
            var cols = go.GetComponentsInChildren<Collider>(true);
            for (int i = 0; i < cols.Length; i++) cols[i].enabled = enabled;
        }

        private IEnumerator ReenableAfterDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            if (go == null) yield break;
            // No reactivar si ahora mismo es el objetivo actual
            if (go == lastTargetObject) yield break;
            SetCollidersEnabled(go, true);
            go.SetActive(true);
        }
    }
}
