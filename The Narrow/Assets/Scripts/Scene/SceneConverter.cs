//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class SceneConverter : MonoBehaviour
{
    public bool fadeOut;

    public AudioSource audioSource;

    public TutorialScene scene;

    private void OnDisable()
    {
        if (fadeOut)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    private void OnEnable()
    {
        if (fadeOut)
        {
            StartCoroutine(MusicFadeOutCoroutine());
        }    
    }

    IEnumerator MusicFadeOutCoroutine()
    {
        for (float i = 1f; i >= 0; i -= 0.02f)
        {
            audioSource.volume = i;
            yield return new WaitForSeconds(0.1f);
        }

        scene.StageClear();
    }

    public void ConvertScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
