using System;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    private int collectedGems = 0;
    void Start()
    {
        GameManager.Instance.OnCollectGem += GemCollected;
        LoadGems();
        textMeshProUGUI.text = "" + collectedGems;
    }

    private void GemCollected()
    {
        collectedGems++;
        textMeshProUGUI.text = ""+collectedGems;
    }
    private void OnDestroy()
    {
        SaveGems();
    }
    public void SaveGems()
    {
        PlayerPrefs.SetInt("gems", collectedGems);
    }

    public void LoadGems()
    {
        collectedGems = PlayerPrefs.GetInt("gems");
    }

}
