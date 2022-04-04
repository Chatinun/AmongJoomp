using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    private void Start()
    {
        transition.SetTrigger("Position");
    }

    public void StartButton()
    {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int scene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(scene);
    }

    public void ExitButton()
    {
        Application.Quit();
        PlayerPrefs.DeleteKey("HighScore");
        /*
        StartCoroutine(ExitGame());
        DeleteScore();
        transition.SetTrigger("Start");
        */
    }


}
