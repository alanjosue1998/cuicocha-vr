using UnityEngine;

namespace Gio {
    public class TeleportOrMoveAction : MonoBehaviour
    {
        [Header("Jugador al que afectará este trigger")]
        [SerializeField] private MoveToTarget player;

        [Header("Configuración especial de destino")]
        [SerializeField] private bool useAlternateDestination = false;
        [SerializeField] private Transform alternateDestination;
        [SerializeField] private bool teleportInstantly = false;

        public void ExecuteAction()
        {
            if (player == null) return;

            Transform destination = null;

            if (useAlternateDestination && alternateDestination != null)
            {
                destination = alternateDestination;
                
                if (teleportInstantly)
                {
                    player.transform.position = alternateDestination.position;
                    Debug.Log("✨ Teletransportado a: " + alternateDestination.name);
                }
                else
                {
                    player.MoveTowards(alternateDestination);
                    Debug.Log("🚶 Moviéndose hacia destino alternativo: " + alternateDestination.name);
                }
            }
            else
            {
                destination = transform;
                player.MoveTowards(transform);
                Debug.Log("🚶 Moviéndose hacia este trigger: " + name);
            }

            // Activar zona de audio si el destino tiene una
            if (destination != null)
            {
                AudioZone audioZone = destination.GetComponent<AudioZone>();
                if (audioZone != null)
                {
                    audioZone.ActivateZone();
                }
            }
        }
    }
}
