using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RDG;

public class GameOver : MonoBehaviour
{
    [SerializeField] private MobileShake mobileShake;

    void Awake()
    {
        StartCoroutine(LavaHoundsWhenItDiesGuards());
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
