using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    /*public static AudioManager Instance;*/

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    public Sound[] musicSounds;
    public AudioSource musicSource;

    private void Awake()
    {
        Debug.Log("set volume");

        //setting volume when new scene loaded
        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1);
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1);

        sfxVolume = 1;

        SFXVolume(sfxVolume);
        MusicVolume(musicVolume);

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVolume;
        }
        if (musicSlider != null)
        {
            musicSlider.value = musicVolume;
        }

        /*        if (PlayerPrefs.HasKey("sfxVolume"))
                {
                    sfxSlider.value = sfxVolume;
                    SFXVolume(sfxVolume);
                }
                else
                {
                    SFXVolume(sfxSlider.value);
                }

                if (PlayerPrefs.HasKey("musicVolume"))
                {
                    musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
                    MusicVolume(musicSlider.value);
                }
                else
                {
                    MusicVolume(musicSlider.value);
                }*/





        /*        if (Instance == null)
                {
                    Instance = this;
                    //DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }*/

/*        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PLayMusic("MainMenu");
        }*/
    }

    public void PLayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("SoundNotFound");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    
    //set audio source volume
    public void MusicVolume(float volume)
    {
/*        musicSource.volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);*/

        if (volume <= 0.01)
        {
            audioMixer.SetFloat("Music", -80f);
            PlayerPrefs.SetFloat("musicVolume", -80f);
            return;
        }

        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    //set audio mixer volume
    public void SFXVolume(float volume)
    {
        if (volume <= 0.01)
        {
            audioMixer.SetFloat("Volume", -80f);
            PlayerPrefs.SetFloat("sfxVolume", -80f);
            return;
        }

        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
}