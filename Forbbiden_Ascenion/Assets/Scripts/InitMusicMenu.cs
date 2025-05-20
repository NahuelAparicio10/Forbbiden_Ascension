using UnityEngine;

public class InitMusicMenu : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayMusic(Enums.Music.Menu);
    }


}
