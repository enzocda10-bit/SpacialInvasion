using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    [SerializeField] private float musicVolume;
    void Start()
    {
        AudioManager.Instance.PlayMusic(music, musicVolume);
    }
}
