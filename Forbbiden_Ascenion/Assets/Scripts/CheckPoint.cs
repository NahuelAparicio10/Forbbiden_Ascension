using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    private PlayerController _player;
    public Transform checkPoint;
    public GameObject canvasDeactivate;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }
    public void FinalCheckPoint()
    {
        AudioManager.Instance.PlayMusic(Enums.Music.FinalMusic);
        SceneManager.LoadScene(4);
    }

    public void SaveCheckPoint()
    {
        _player.currentCheckPoint = checkPoint.position;
        canvasDeactivate.SetActive(false);
    }
}
