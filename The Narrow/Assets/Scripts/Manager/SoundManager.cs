//System
using System;
using System.Collections;
using System.Collections.Generic;

//Unity
using UnityEngine;

[System.Serializable]
public class Sound
{
    //Name of sound
    public string name;

    //Clip of Sound
    public AudioClip clip;
}

[DisallowMultipleComponent]
public class SoundManager : MonoBehaviour
{
    //Static Variable : this.gameObject
    static public SoundManager instance;

    //Singletone
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    //Sound Class
    [Header("사운드 클래스 리스트")]
    [SerializeField] public List<Sound> sfxs;

    //Audio Source
    [Header("효과음 오디오 소스 리스트")]
    public List<AudioSource> sfxAudioSources;

    //Playing Sound Name
    [Header("재생 중인 효과음 이름")]
    public string[] playSoundName;

    private void Start()
    {
        playSoundName = new string[sfxs.Count]; 
    }



    //Play SFX Method
    public void PlaySFX (string _name)
    {
        for (int i = 0; i < sfxs.Count; i++)
        {
            if (_name == sfxs[i].name)
            {
                for (int j = 0; j < sfxAudioSources.Count; j++)
                {
                    if (!sfxAudioSources[j].isPlaying)
                    {
                        playSoundName[j] = sfxs[i].name;
                        sfxAudioSources[j].clip = sfxs[i].clip;
                        sfxAudioSources[j].Play();
                        return;
                    }
                }

                //Debug.Log
                Debug.Log("모든 오디오 소스 사용 중");
                return;
            }
        }
        //Debug.Log
        Debug.Log("등록되지 않은 사운드");
    }

    //Stop All SFX Method
    public void StopAllSFX()
    {
        for (int i = 0; i < sfxAudioSources.Count; i++)
        {
            sfxAudioSources[i].Stop();
        }
    }

    //Stop Wanted SFX Method
    public void StopSFX(string _name)
    {
        for (int i = 0; i < sfxAudioSources.Count; i++)
        {
            if (playSoundName[i] == _name)
            {
                sfxAudioSources[i].Stop();
                return;
            }
        }
        //Debug.Log
        Debug.Log("재생 중인 사운드가 없음");
    }

}
