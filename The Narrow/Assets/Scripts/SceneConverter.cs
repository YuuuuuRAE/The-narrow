//System
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class SceneConverter : MonoBehaviour
{
    public void ConvertScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
