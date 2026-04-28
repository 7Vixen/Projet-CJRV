using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource walkSource;
    public AudioSource crowdSource;

    [Header("Music")]
    public AudioClip backgroundMusic;
    public AudioClip victoryMusic;

    [Header("Movement Sounds")]
    public AudioClip walkSound;
    public AudioClip runSound;

    [Header("Combat Sounds")]
    public AudioClip swordDrawSound;
    public AudioClip swordSwingSound;
    public AudioClip punchSound;
    public AudioClip painScreamSound;

    [Header("UI Sounds")]
    public AudioClip buttonClickSound;

    [Header("Ambient Sounds")]
    public AudioClip crowdSound;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume    = 0.3f;
    [Range(0f, 1f)] public float sfxVolume      = 0.9f;
    [Range(0f, 1f)] public float footstepVolume = 0.7f;
    [Range(0f, 1f)] public float crowdVolume    = 0.4f;

    private bool isWalking = false;
    private bool isRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource.volume  = musicVolume;
        sfxSource.volume    = sfxVolume;
        walkSource.volume   = footstepVolume;
        crowdSource.volume  = 0f;

        musicSource.spatialBlend = 0f;
        sfxSource.spatialBlend   = 0f;
        walkSource.spatialBlend  = 0f;
        crowdSource.spatialBlend = 0f;

        PlayBackgroundMusic();
    }

    // ─── MUSIC LOGIC ──────────────────────────────────────────────────────────

    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeOutMenuMusicCoroutine(duration));
    }

    private IEnumerator FadeOutMenuMusicCoroutine(float duration)
    {
        float startVolume = musicSource.volume;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }
        musicSource.Stop();
        musicSource.volume = startVolume; 
    }

    public void PlayVictoryMusic()
    {
        musicSource.Stop();
        musicSource.clip = victoryMusic;
        musicSource.loop = false;
        musicSource.Play();
    }

    // ─── MOVEMENT LOGIC ───────────────────────────────────────────────────────

    public void PlayWalking()
    {
        if (isWalking) return;
        isRunning = false;
        isWalking = true;
        walkSource.clip = walkSound;
        walkSource.loop = true;
        walkSource.Play();
    }

    public void PlayRunning()
    {
        if (isRunning) return;
        isWalking = false;
        isRunning = true;
        walkSource.clip = runSound;
        walkSource.loop = true;
        walkSource.Play();
    }

    public void StopFootsteps()
    {
        isWalking = false;
        isRunning = false;
        walkSource.loop = false;
        walkSource.Stop();
    }

    // ─── COMBAT & UI ──────────────────────────────────────────────────────────

    public void PlaySwordDraw()  => sfxSource.PlayOneShot(swordDrawSound);
    public void PlaySwordSwing() => sfxSource.PlayOneShot(swordSwingSound);
    public void PlayPunch() => sfxSource.PlayOneShot(punchSound);
    public void PlayPainScream() => sfxSource.PlayOneShot(painScreamSound);
    public void PlayButtonClick() => sfxSource.PlayOneShot(buttonClickSound);

    // ─── AMBIENT / CROWD LOGIC (Fixed for AmbientZone.cs) ─────────────────────

    public void FadeInCrowd(float duration = 2f)
    {
        if (!crowdSource.isPlaying)
        {
            crowdSource.clip = crowdSound;
            crowdSource.loop = true;
            crowdSource.volume = 0f;
            crowdSource.Play();
        }
        StartCoroutine(FadeVolume(crowdSource, crowdVolume, duration));
    }

    public void FadeOutCrowd(float duration = 2f)
    {
        StartCoroutine(FadeAndStop(crowdSource, duration));
    }

    private IEnumerator FadeVolume(AudioSource source, float targetVolume, float duration)
    {
        float startVolume = source.volume;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
            yield return null;
        }
        source.volume = targetVolume;
    }

    private IEnumerator FadeAndStop(AudioSource source, float duration)
    {
        yield return FadeVolume(source, 0f, duration);
        source.Stop();
    }
}