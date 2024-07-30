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
    [Header("���� Ŭ���� ����Ʈ")]
    [SerializeField] public List<Sound> sfxs;

    //Audio Source
    [Header("ȿ���� ����� �ҽ� ����Ʈ")]
    public List<AudioSource> sfxAudioSources;

    //Playing Sound Name
    [Header("��� ���� ȿ���� �̸�")]
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
                Debug.Log("��� ����� �ҽ� ��� ��");
                return;
            }
        }
        //Debug.Log
        Debug.Log("��ϵ��� ���� ����");
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
        Debug.Log("��� ���� ���尡 ����");
    }

}
