using UnityEngine;

public class InitMusicSky : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMusic(Enums.Music.SkyMusic);

    }

}
