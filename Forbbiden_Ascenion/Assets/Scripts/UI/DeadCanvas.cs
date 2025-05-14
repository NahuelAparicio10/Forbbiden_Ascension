using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeadCanvas : MonoBehaviour
{
    public List<string> deadSentences = new List<string>();

    public TextMeshProUGUI deadSentenceText;
    public TextMeshProUGUI timesJumpedText;
    public TextMeshProUGUI timesDashedText;
    public TextMeshProUGUI timesTouchedCloudText;

    public void PlayAgain()
    {
        GameManager.Instance.InvokePlayAgain();
        gameObject.SetActive(false);
    }

    public void MainMenu() => SceneManager.LoadScene(0);

    private void OnEnable()
    {
        deadSentenceText.text = GetRandomSentence();
        timesJumpedText.text = "You have jumped: " + GameManager.Instance.timesJumped + " times.";
        timesDashedText.text = "You have dashed: " + GameManager.Instance.timesDashed + " times.";
        timesTouchedCloudText.text = "You have been in over: " + GameManager.Instance.timesTouchedCloud + " clouds.";
        GameManager.Instance.PauseGame();

    }

    private void OnDisable()
    {
        GameManager.Instance.PauseGame();
    }



    private string GetRandomSentence() => deadSentences[Random.Range(0, deadSentences.Count)];
}
