using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RDG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MobileShake mobileShake;
    [SerializeField] AudioSource preAudioSource;
    [SerializeField] AudioSource postAudioSource;

    void Awake()
    {
        //freeze until tutorial is over
        Time.timeScale = 0f;

        int playCount = 0;

        //pre game tutorial recording

        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        playCount = PlayerPrefs.GetInt("PlayCount" + buildIndex, 0);
        PlayerPrefs.SetInt("PlayCount" + buildIndex, playCount++);

        Debug.Log("PlayCount_" + buildIndex + " = " + playCount);

        if (playCount != 0)
        {
            StartCoroutine(CancelAudio());
        }
        else
        {
            StartCoroutine(PlayAudio());
        }
    }

    public IEnumerator GameOver()
    {
        StartCoroutine(LavaHoundsWhenItDiesGuards());

        yield return new WaitForSeconds(5f);
        postAudioSource.Play();
        //int playCount = 0;
        //post game instructions recording


        /*        playCount = PlayerPrefs.GetInt("PlayCount", 0);
                PlayerPrefs.SetInt("PlayCount", playCount++);

                Debug.Log("PlayCount = " + playCount);

                if (playCount != 0)
                {
                    StartCoroutine(CancelAudio(postAudioSource));
                }*/

        yield return null;
    }

    private IEnumerator CancelAudio()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)||preAudioSource.isPlaying == false);
        preAudioSource.Stop();
        Time.timeScale = 1.0f;
    }

    private IEnumerator PlayAudio()
    {
        yield return new WaitUntil(() => preAudioSource.isPlaying == false);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        Time.timeScale = 1.0f;
    }

    private IEnumerator LavaHoundsWhenItDiesGuards()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            //reload scene
            if (mobileShake.shake == true)
            {
                int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(sceneIndex);
            }

            float holdTime = 0f;

            //if pressing screen for 3 seconds go to main menu
            while (Input.GetMouseButton(0))
            {
                yield return new WaitForSeconds(1f);
                Vibration.VibratePredefined(0);

                holdTime += 1f;

                if (holdTime >= 3)
                {
                    SceneManager.LoadScene(0);
                }
            }

            yield return null;
        }
    }
}
