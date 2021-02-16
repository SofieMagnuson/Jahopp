using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialManager : MonoBehaviour
{

    public GameObject[] popUps;
    private int popUpIndex;

    void Update()

    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);

            }

            if (popUpIndex == 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    popUpIndex++;
                }

                else if (popUpIndex == 1)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        popUpIndex++;

                    }

                    else if (popUpIndex == 2)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                }

            }


        }
    }
}
