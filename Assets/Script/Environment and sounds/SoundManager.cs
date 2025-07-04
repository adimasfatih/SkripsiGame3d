using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip combatMusic;

    [Header("SFX Clips")]
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip Attack3;
    public AudioClip Attack_Skill;
    public AudioClip Atta;

    private void Awake()
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

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayDashSFX() => PlaySFX(dashSFX);
    public void PlaySkillSFX() => PlaySFX(skillSFX);
    public void PlayHitSFX() => PlaySFX(hitSFX);
}
