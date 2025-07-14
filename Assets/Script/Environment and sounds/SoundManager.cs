using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEngine.Rendering.HighDefinition.CameraSettings;
using UnityEngine.SceneManagement;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioMixer audioMixer;

   
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public int lastCombatIndex = -1;


    [Header("Music Clips")]
    public AudioClip mainMenuMusic;

    [Header("Combat Music Playlist")]
    public AudioClip[] combatMusicClips;

    [Header("Player Hurt Sounds")]
    public AudioClip[] playerHurtClips;

    [Header("Enemy Hurt Sounds")]
    public AudioClip[] enemyHurtClips;

    [Header("Pocong Attack sounds")]
    public AudioClip[] PocongAttackClips;

    [Header("Footstep")]
    public AudioClip[] FootstepClips;

    [Header("SFX Clips")]
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip Attack3;
    public AudioClip AttackLong;
    public AudioClip Attack_Skill;
    public AudioClip Explosive;
    public AudioClip Attack_Kunti;
    public AudioClip Dodge;


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

    private void Start()
    {
        SetupOnLoad();
    }

    void Update()
    {
        if (!musicSource.isPlaying && combatMusicClips.Length > 0 && SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayNextCombatTrack();
        }
    }

    private void PlayNextCombatTrack()
    {
        if (combatMusicClips.Length == 0) return;

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, combatMusicClips.Length);
        } while (randomIndex == lastCombatIndex && combatMusicClips.Length > 1);

        lastCombatIndex = randomIndex;
        PlayMusic(combatMusicClips[randomIndex], 1f, false);
    }


    public void SetupOnLoad()
    {
        // Sliders
        OptionCanvas.Instance.BGMSlider.onValueChanged.AddListener((_) => SetMusicVolume());
        OptionCanvas.Instance.SFXSlider.onValueChanged.AddListener((_) => SetSFXVolume());
        OptionCanvas.Instance.MasterSlider.onValueChanged.AddListener((_) => SetMasterVolume());

        // Toggles
        OptionCanvas.Instance.BGMToggle.onValueChanged.AddListener(MuteBGM);
        OptionCanvas.Instance.SFXToggle.onValueChanged.AddListener(MuteSFX);
        OptionCanvas.Instance.MasterToggle.onValueChanged.AddListener(MuteMaster);
    }


    public void PlayMusic(AudioClip clip, float volume = 1f, bool loop = true) 
    {
        if (musicSource.clip == clip && musicSource.isPlaying)
        {
            musicSource.loop = loop; //Ensures loop behavior is correctly applied
            return;
        }

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.PlayOneShot(clip, volume );
    }

    public void PlayMainMenuMusic() => PlayMusic(mainMenuMusic, 0.4f, true);
    public void PlayCombatMusic() => PlayNextCombatTrack();
    public void PlayAttack1SFX() => PlaySFX(Attack1,1f);

    public void PlayDodgeSFX() => PlaySFX(Dodge, 0.1f);
    public void PlayAttack2SFX() => PlaySFX(Attack2, 1f);
    public void PlayAttack3SFX() => PlaySFX(Attack3, 1f);

    public void PlayAttack_LongFX() => PlaySFX(AttackLong, 1f);
    public void PlayAttack_SkillFX() => PlaySFX(Attack_Skill, 1f);
    public void PlayExplosiveFX() => PlaySFX(Explosive, 0.5f);

    public void PlayAttackKunti() => PlaySFX(Attack_Kunti, 0.1f);

    public void PlayRandomHurt(bool isPlayer, float volume = 1f)
    {
        AudioClip[] clips = isPlayer ? playerHurtClips : enemyHurtClips;
        if (clips.Length == 0) return;

        int randomIndex = Random.Range(0, clips.Length);
        sfxSource.PlayOneShot(clips[randomIndex], volume );
    }

    public void PlayPocongAttack(float volume =1f)
    {

        AudioClip[] clips = PocongAttackClips;
        int randomIndex = Random.Range(0, clips.Length);
        sfxSource.PlayOneShot(clips[randomIndex], volume);
    }

    public void PlayFootstep(float volume = 0.4f)
    {
        AudioClip[] clips = FootstepClips;
        int randomIndex = Random.Range(0, clips.Length);
        sfxSource.PlayOneShot(clips[randomIndex], volume);
    }
    public void PlayPlayerFootstep() => PlayFootstep(0.4f);

    public void SetMusicVolume ()
    {
        float value = OptionCanvas.Instance.BGMSlider.value;
        audioMixer.SetFloat("VolumeBGM", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    }

    public void SetSFXVolume()
    {
        float value = OptionCanvas.Instance.SFXSlider.value;
        audioMixer.SetFloat("VolumeSFX", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    }

    public void SetMasterVolume()
    {
        float value = OptionCanvas.Instance.MasterSlider.value;
        audioMixer.SetFloat("VolumeMaster", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    }

    public void MuteBGM(bool isOn)
    {
        if (isOn)
        {
            // Mute
            audioMixer.SetFloat("VolumeBGM", -80f);
        }
        else
        {
            SetMusicVolume();
        }
    }

    public void MuteSFX(bool isOn)
    {   
        if (isOn)
        {
            // Mute
            audioMixer.SetFloat("VolumeSFX", -80f);
        }
        else
        {
            SetSFXVolume();
        }
    }

    public void MuteMaster(bool isOn)
    {
        if (isOn)
        {
            // Mute
            audioMixer.SetFloat("VolumeMaster", -80f);
        }
        else
        {
            SetMasterVolume();
        }
    }

}
