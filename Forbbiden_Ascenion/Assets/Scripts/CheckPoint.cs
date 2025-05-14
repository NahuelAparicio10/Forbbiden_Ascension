using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private PlayerController _player;
    public Transform checkPoint;
    public GameObject canvasDeactivate;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    public void SaveCheckPoint()
    {
        _player.currentCheckPoint = checkPoint.position;
        canvasDeactivate.SetActive(false);
    }
}
