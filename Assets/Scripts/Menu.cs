using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RDG;

public class Menu : MonoBehaviour
{
    [SerializeField] private MobileShake mobileShake;

    private int sceneIndex = 0;
    [SerializeField] private int maxSceneIndex;

    private bool canScroll = true;

    private void Start()
    {


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

        float pos = Input.mousePosition.x;

        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));

        float newPos = Input.mousePosition.x;

        if (Mathf.Abs(pos-newPos) > Screen.width/8)
        {
            Vibration.Vibrate(100, 100);

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
                yield return new WaitForSeconds(0.05f);
            }

            Debug.Log("SceneIndex: " + sceneIndex);
        }

        canScroll = true;
    }

    private void Play()
    {
        SceneManager.LoadScene(sceneIndex);
    }

}