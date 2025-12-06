using UnityEngine;

namespace Gio
{
    /// <summary>
    /// Componente que define una zona de audio.
    /// Cuando el jugador se teletransporta a este punto, se reproduce el audio asignado.
    /// </summary>
    public class AudioZone : MonoBehaviour
    {
        [Header("Audio de esta Zona")]
        [Tooltip("Audio que se reproducirá cuando el jugador entre a esta zona")]
        [SerializeField] private AudioClip zoneMusic;

        [Header("Configuración")]
        [Tooltip("Activar música automáticamente al iniciar (si el jugador ya está en esta zona)")]
        [SerializeField] private bool playOnStart = false;

        void Start()
        {
            if (playOnStart && zoneMusic != null)
            {
                ActivateZone();
            }
        }

        /// <summary>
        /// Activa la música de esta zona
        /// </summary>
        public void ActivateZone()
        {
            if (zoneMusic != null)
            {
                AudioZoneManager.Instance.PlayZoneMusic(zoneMusic);
                Debug.Log($"🎯 Zona activada: {gameObject.name} → {zoneMusic.name}");
            }
            else
            {
                // Si no hay música asignada, detener cualquier música actual
                AudioZoneManager.Instance.StopMusic();
                Debug.Log($"🔇 Zona sin música: {gameObject.name}");
            }
        }

        /// <summary>
        /// Desactiva la música de esta zona
        /// </summary>
        public void DeactivateZone()
        {
            AudioZoneManager.Instance.StopMusic();
        }
    }
}
