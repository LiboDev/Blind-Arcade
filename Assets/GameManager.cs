using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RDG;

public class GameOver : MonoBehaviour
{
    [SerializeField] private MobileShake mobileShake;
    [SerializeField] AudioSource speechAudioSource;
    private int playCount = 0;

    void Awake()
    {
        StartCoroutine(LavaHoundsWhenItDiesGuards());

        //tutorial recording
        speechAudioSource.Play();

        playCount = PlayerPrefs.GetInt("PlayCount",0);
        PlayerPrefs.SetInt("PlayCount", playCount++);

        Debug.Log("PlayCount = " + playCount);

        if (playCount != 0)
        {
            StartCoroutine(CancelAudio());
        }
    }

    private IEnumerator CancelAudio()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        speechAudioSource.Stop();
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
