using UnityEngine;
using System.Collections;

namespace Gio
{
    /// <summary>
    /// Gestor centralizado de música por zonas.
    /// Controla qué música está sonando actualmente y maneja transiciones.
    /// </summary>
    public class AudioZoneManager : MonoBehaviour
    {
        private static AudioZoneManager instance;
        public static AudioZoneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<AudioZoneManager>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject("AudioZoneManager");
                        instance = go.AddComponent<AudioZoneManager>();
                    }
                }
                return instance;
            }
        }

        [Header("Configuración de Audio")]
        [Range(0f, 1f)]
        [Tooltip("Volumen general de la música")]
        [SerializeField] private float volume = 0.5f;

        [Tooltip("Tiempo de fade in/out (segundos)")]
        [SerializeField] private float fadeTime = 1f;

        private AudioSource audioSource;
        private AudioClip currentClip;
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

            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = 0f; // Empezar en 0 para fade in
        }

        /// <summary>
        /// Reproduce la música de una zona específica con fade in
        /// </summary>
        public void PlayZoneMusic(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.LogWarning("⚠️ AudioClip es null, no se puede reproducir");
                return;
            }

            // Si ya está sonando este clip, no hacer nada
            if (currentClip == clip && audioSource.isPlaying)
            {
                Debug.Log($"🎵 Ya está sonando: {clip.name}");
                return;
            }

            // Detener fade anterior si existe
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            currentClip = clip;
            fadeCoroutine = StartCoroutine(FadeToNewClip(clip));
        }

        /// <summary>
        /// Pausa la música actual con fade out (para el menú)
        /// </summary>
        public void StopMusic()
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(FadeOutAndPause());
        }

        /// <summary>
        /// Detiene completamente la música (no se usará normalmente)
        /// </summary>
        public void StopMusicCompletely()
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(FadeOut());
        }

        private IEnumerator FadeToNewClip(AudioClip newClip)
        {
            // Si el clip es el mismo y está pausado, solo reanudar con fade in
            if (currentClip == newClip && audioSource.clip == newClip && !audioSource.isPlaying)
            {
                audioSource.UnPause();
                Debug.Log($"▶️ Reanudando: {newClip.name}");

                // Fade in
                float elapsedResume = 0f;
                while (elapsedResume < fadeTime)
                {
                    elapsedResume += Time.deltaTime;
                    audioSource.volume = Mathf.Lerp(0f, volume, elapsedResume / fadeTime);
                    yield return null;
                }

                audioSource.volume = volume;
                fadeCoroutine = null;
                yield break;
            }

            // Fade out del clip actual
            float startVolume = audioSource.volume;
            float elapsedFadeOut = 0f;

            while (elapsedFadeOut < fadeTime && audioSource.isPlaying)
            {
                elapsedFadeOut += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedFadeOut / fadeTime);
                yield return null;
            }

            // Cambiar al nuevo clip
            audioSource.Stop();
            audioSource.clip = newClip;
            audioSource.Play();

            Debug.Log($"🎵 Reproduciendo: {newClip.name}");

            // Fade in del nuevo clip
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
            // NO cambiar currentClip para que se pueda reanudar

            Debug.Log("⏸️ Música pausada");
            fadeCoroutine = null;
        }

        private IEnumerator FadeOut()
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
            currentClip = null;

            Debug.Log("🔇 Música detenida completamente");
            fadeCoroutine = null;
        }

        /// <summary>
        /// Cambia el volumen general
        /// </summary>
        public void SetVolume(float newVolume)
        {
            volume = Mathf.Clamp01(newVolume);
            if (audioSource != null && fadeCoroutine == null)
            {
                audioSource.volume = volume;
            }
        }

        /// <summary>
        /// Obtiene el volumen actual
        /// </summary>
        public float GetVolume()
        {
            return volume;
        }
    }
}
