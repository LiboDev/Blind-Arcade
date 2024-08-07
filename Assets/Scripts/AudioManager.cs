using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer audioMixer;
    /*    [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider musicSlider;*/

    private float volume;

    public Sound[] musicSounds;
    public AudioSource musicSource;

    private void Awake()
    {
        Instance = this;

        Debug.Log("set volume");

        //setting volume when new scene loaded
        volume = PlayerPrefs.GetFloat("volume", 1);

        /*SetVolume(volume);*/
    }

    //set audio mixer volume
    public void SetVolume(float volume)
    {
        if (volume <= Mathf.Pow(10, -80))
        {
            audioMixer.SetFloat("Volume", -80f);
            PlayerPrefs.SetFloat("volume", -80f);
            return;
        }

        if(volume >= 20){
            volume = 20;
        }

        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);

        Debug.Log(volume);
    }
}