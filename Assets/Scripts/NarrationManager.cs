using UnityEngine;
using System.Collections;

namespace Gio
{
    /// <summary>
    /// Gestor de narraciones/audios puntuales.
    /// Maneja la reproducción de audios que se activan al mirar/activar puntos específicos.
    /// Independiente del sistema de música de fondo.
    /// </summary>
    public class NarrationManager : MonoBehaviour
    {
        private static NarrationManager instance;
        public static NarrationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<NarrationManager>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject("NarrationManager");
                        instance = go.AddComponent<NarrationManager>();
                    }
                }
                return instance;
            }
        }

        [Header("Configuración de Narración")]
        [Range(0f, 1f)]
        [Tooltip("Volumen de las narraciones")]
        [SerializeField] private float volume = 0.7f;

        [Tooltip("Tiempo de fade in/out (segundos)")]
        [SerializeField] private float fadeTime = 0.5f;

        private AudioSource audioSource;
        private AudioClip currentNarration;
        private Coroutine fadeCoroutine;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeAudioSource();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void InitializeAudioSource()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.loop = false; // Las narraciones no hacen loop
            audioSource.playOnAwake = false;
            audioSource.volume = 0f;
        }

        /// <summary>
        /// Reproduce una narración específica
        /// </summary>
        public void PlayNarration(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.LogWarning("⚠️ Narración AudioClip es null");
                return;
            }

            // Si ya está sonando esta narración, no hacer nada
            if (currentNarration == clip && audioSource.isPlaying)
            {
                Debug.Log($"🎙️ Ya está sonando esta narración: {clip.name}");
                return;
            }

            // Detener fade anterior si existe
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            currentNarration = clip;
            fadeCoroutine = StartCoroutine(FadeToNewNarration(clip));
        }

        /// <summary>
        /// Pausa la narración actual
        /// </summary>
        public void PauseNarration()
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(FadeOutAndPause());
        }

        /// <summary>
        /// Detiene completamente la narración
        /// </summary>
        public void StopNarration()
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(FadeOutAndStop());
        }

        private IEnumerator FadeToNewNarration(AudioClip newClip)
        {
            // Fade out de la narración actual si está sonando
            if (audioSource.isPlaying)
            {
                float startVolume = audioSource.volume;
                float elapsedFadeOut = 0f;

                while (elapsedFadeOut < fadeTime)
                {
                    elapsedFadeOut += Time.deltaTime;
                    audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedFadeOut / fadeTime);
                    yield return null;
                }
            }

            // Cambiar a la nueva narración
            audioSource.Stop();
            audioSource.clip = newClip;
            audioSource.Play();

            Debug.Log($"🎙️ Reproduciendo narración: {newClip.name}");

            // Fade in de la nueva narración
            float elapsedFadeIn = 0f;
            while (elapsedFadeIn < fadeTime)
            {
                elapsedFadeIn += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(0f, volume, elapsedFadeIn / fadeTime);
                yield return null;
            }

            audioSource.volume = volume;
            fadeCoroutine = null;
        }

        private IEnumerator FadeOutAndPause()
        {
            float startVolume = audioSource.volume;
            float elapsed = 0f;

            while (elapsed < fadeTime)
            {
                elapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeTime);
                yield return null;
            }

            audioSource.Pause();
            audioSource.volume = 0f;

            Debug.Log("⏸️ Narración pausada");
            fadeCoroutine = null;
        }

        private IEnumerator FadeOutAndStop()
        {
            float startVolume = audioSource.volume;
            float elapsed = 0f;

            while (elapsed < fadeTime)
            {
                elapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeTime);
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = 0f;
            currentNarration = null;

            Debug.Log("🔇 Narración detenida");
            fadeCoroutine = null;
        }

        /// <summary>
        /// Verifica si hay una narración reproduciéndose
        /// </summary>
        public bool IsPlaying()
        {
            return audioSource != null && audioSource.isPlaying;
        }

        /// <summary>
        /// Cambia el volumen de las narraciones
        /// </summary>
        public void SetVolume(float newVolume)
        {
            volume = Mathf.Clamp01(newVolume);
            if (audioSource != null && fadeCoroutine == null)
            {
                audioSource.volume = volume;
            }
        }
    }
}
