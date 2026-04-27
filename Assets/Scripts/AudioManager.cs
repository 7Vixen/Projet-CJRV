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
    [Range(0f, 1f)] public float musicVolume    = 0.3f; // low so other sounds cut through
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
    // Apply volumes
    musicSource.volume  = musicVolume;
    sfxSource.volume    = sfxVolume;
    walkSource.volume   = footstepVolume;
    crowdSource.volume  = 0f; // starts silent, fades in when entering zone

    // Force all sounds to 2D
    musicSource.spatialBlend = 0f;
    sfxSource.spatialBlend   = 0f;
    walkSource.spatialBlend  = 0f;
    crowdSource.spatialBlend = 0f;

    PlayBackgroundMusic();
}

    // ─── MUSIC ────────────────────────────────────────────────────────────────

    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayVictoryMusic()
    {
        musicSource.Stop();
        musicSource.clip = victoryMusic;
        musicSource.loop = false;
        musicSource.Play();
    }

// ─── MOVEMENT ─────────────────────────────────────────────────────────────

public void PlayWalking()
{
    if (isWalking) return; // already walking, do nothing

    isRunning = false;
    isWalking = true;

    walkSource.clip = walkSound;
    walkSource.loop = true;  // ← make sure this is true
    walkSource.Play();
}

public void PlayRunning()
{
    if (isRunning) return; // already running, do nothing

    isWalking = false;
    isRunning = true;

    walkSource.clip = runSound;
    walkSource.loop = true;  // ← make sure this is true
    walkSource.Play();
}

public void StopFootsteps()
{
    isWalking = false;
    isRunning = false;
    walkSource.loop = false;
    walkSource.Stop();
}

    // ─── COMBAT ───────────────────────────────────────────────────────────────

    public void PlaySwordDraw()  => sfxSource.PlayOneShot(swordDrawSound);
    public void PlaySwordSwing() => sfxSource.PlayOneShot(swordSwingSound);
    public void PlayPunch()      => sfxSource.PlayOneShot(punchSound);
    public void PlayPainScream() => sfxSource.PlayOneShot(painScreamSound);

    // ─── UI ───────────────────────────────────────────────────────────────────

    public void PlayButtonClick() => sfxSource.PlayOneShot(buttonClickSound);

    // ─── CROWD / AMBIENT ──────────────────────────────────────────────────────

    public void FadeInCrowd(float duration = 2f)
    {
        if (!crowdSource.isPlaying)
        {
            crowdSource.clip = crowdSound;
            crowdSource.loop = true;
            crowdSource.volume = 0f;
            crowdSource.Play();
        }
        StopAllCoroutines();
        StartCoroutine(FadeVolume(crowdSource, crowdVolume, duration));
    }

    public void FadeOutCrowd(float duration = 2f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeAndStop(crowdSource, duration));
    }

    // ─── HELPERS ──────────────────────────────────────────────────────────────

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