using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class WinScene : MonoBehaviour
{
[Header("Inscribed")]
    public TextMeshProUGUI collectionCompleteText; // Drag your "Collection Complete" text here
    public Button replayButton; // Drag your replay button here
    public float textDisplayDuration = 2f; // Duration to show "Collection Complete"

    void Start()
    {
        collectionCompleteText.gameObject.SetActive(true); // Show text initially
        replayButton.gameObject.SetActive(false); // Hide replay button
        StartCoroutine(DisplayTextAndShowButton());
    }

    IEnumerator DisplayTextAndShowButton()
    {
        yield return new WaitForSeconds(textDisplayDuration); // Wait for specified duration
        collectionCompleteText.gameObject.SetActive(false); // Hide text
        replayButton.gameObject.SetActive(true); // Show replay button
    }

    public void OnReplayButtonClicked()
    {
        SceneManager.LoadScene("Scene_start"); // Replace with your starting scene name
    }
}
