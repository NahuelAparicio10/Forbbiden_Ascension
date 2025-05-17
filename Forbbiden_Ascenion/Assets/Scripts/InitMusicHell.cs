using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMusicHell : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMusic(Enums.Music.HellMusic);

    }
}
