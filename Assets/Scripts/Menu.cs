using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RDG;

public class Menu : MonoBehaviour
{
    [SerializeField] private MobileShake mobileShake;
    [SerializeField] AudioSource speechAudioSource;
    [SerializeField] AudioSource numberAudioSource;

    private int sceneIndex = 0;
    [SerializeField] private int maxSceneIndex;

    [SerializeField] private List<AudioClip> numberVoiceLine;

    private bool canScroll = true;

    private int mainCount = 0;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //tutorial recording
        speechAudioSource.Play();

        mainCount = PlayerPrefs.GetInt("MainCount",0);
        PlayerPrefs.SetInt("MainCount", mainCount++);

        Debug.Log("MainCount = " + mainCount);

        if (mainCount != 0)
        {
            StartCoroutine(CancelAudio());
        }
    }

    private IEnumerator CancelAudio()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        speechAudioSource.Stop();
    }

/*    private IEnumerator HapticTester()
    {
        yield return new WaitForSeconds(2f);

        //single
        Vibration.VibratePredefined(0);

        yield return new WaitForSeconds(2f);

        //double
        Vibration.VibratePredefined(1);

        yield return new WaitForSeconds(2f);

        //single
        Vibration.VibratePredefined(2);

        yield return new WaitForSeconds(2f);

        //
        Vibration.VibratePredefined(3);

    }*/

    void Update()
    {
        if (mobileShake.magnitude > 3)
        {
            Play();
        }

        if (Input.GetMouseButtonDown(0) && canScroll)
        {
            StartCoroutine(LavaHoundsWhenItDiesGuards());
        }

    }

    private IEnumerator LavaHoundsWhenItDiesGuards()
    {
        canScroll = false;

        float pos = Input.mousePosition.y;

        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));

        float newPos = Input.mousePosition.y;

        if (Mathf.Abs(pos-newPos) > Screen.height/16)
        {
            //Vibration.Vibrate(100, 100);

            if (newPos - pos > 0)
            {
                //scroll up
                if(sceneIndex == maxSceneIndex)
                {
                    sceneIndex = 1;
                }
                else
                {
                    sceneIndex++;
                }
            }
            else
            {
                //scroll down
                if (sceneIndex == 1)
                {
                    sceneIndex = maxSceneIndex;
                }
                else
                {
                    sceneIndex--;
                }
            }

            for(int i = 0; i < sceneIndex; i++)
            {
                Vibration.VibratePredefined(0);
                yield return new WaitForSeconds(0.1f);
            }

            speechAudioSource.PlayOneShot(numberVoiceLine[sceneIndex-1], 1f);

            Debug.Log("SceneIndex: " + sceneIndex);
        }

        canScroll = true;
    }

    private void Play()
    {
        SceneManager.LoadScene(sceneIndex);
    }

}