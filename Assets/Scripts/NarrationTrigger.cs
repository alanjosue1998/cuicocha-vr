using UnityEngine;

namespace Gio
{
    /// <summary>
    /// Componente que activa una narración específica cuando se mira/activa un punto.
    /// Se agrega a los puntos de interés que tienen narraciones.
    /// </summary>
    public class NarrationTrigger : MonoBehaviour
    {
        [Header("Narración de este Punto")]
        [Tooltip("Audio de narración que se reproducirá al activar este punto")]
        [SerializeField] private AudioClip narrationClip;

        [Header("Configuración")]
        [Tooltip("Pausar narración anterior al activar esta")]
        [SerializeField] private bool pausePreviousNarration = true;

        [Tooltip("Activar automáticamente al mirar (PointerEnter)")]
        [SerializeField] private bool playOnPointerEnter = false;

        [Tooltip("Activar al hacer click (PointerDown)")]
        [SerializeField] private bool playOnPointerDown = true;

        /// <summary>
        /// Activa la narración de este punto
        /// </summary>
        public void PlayNarration()
        {
            if (narrationClip != null)
            {
                if (pausePreviousNarration)
                {
                    // Pausa la narración anterior y reproduce esta
                    NarrationManager.Instance.PlayNarration(narrationClip);
                }
                else
                {
                    // Solo reproduce si no hay otra narración sonando
                    if (!NarrationManager.Instance.IsPlaying())
                    {
                        NarrationManager.Instance.PlayNarration(narrationClip);
                    }
                }
                
                Debug.Log($"🎙️ Activado punto de narración: {gameObject.name}");
            }
            else
            {
                Debug.LogWarning($"⚠️ No hay narración asignada en: {gameObject.name}");
            }
        }

        /// <summary>
        /// Pausa la narración actual
        /// </summary>
        public void PauseNarration()
        {
            NarrationManager.Instance.PauseNarration();
        }

        /// <summary>
        /// Detiene la narración actual
        /// </summary>
        public void StopNarration()
        {
            NarrationManager.Instance.StopNarration();
        }

        // Métodos que se pueden llamar desde TriggerEvent via UnityEvents
        public void OnPointerEnterEvent()
        {
            if (playOnPointerEnter)
            {
                PlayNarration();
            }
        }

        public void OnPointerDownEvent()
        {
            if (playOnPointerDown)
            {
                PlayNarration();
            }
        }
    }
}
