using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private string gameScene;
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private string feedbackMessage;

    [SerializeField] private GameObject[] player1Images;
    [SerializeField] private GameObject[] player2Images;

    private int player1Selection = -1;
    private int player2Selection = -1;

    private void Start()
    {
        UpdateStartButtonState();
        foreach (var image in player1Images)
        {
            image.SetActive(false);
        }
        foreach (var image in player2Images)
        {
            image.SetActive(false);
        }
    }

    public void SelectCharacterP1(int index)
    {
        player1Selection = index;
        UpdateSelectionImages(player1Images, index);
        UpdateStartButtonState();
    }

    public void SelectCharacterP2(int index)
    {
        player2Selection = index;
        UpdateSelectionImages(player2Images, index);
        UpdateStartButtonState();
    }

    private void UpdateSelectionImages(GameObject[] images, int selectedIndex)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(i == selectedIndex);
        }
    }

    private void UpdateStartButtonState()
    {
        if (player1Selection >= 0 && player2Selection >= 0)
        {
            startButton.interactable = true;
            feedbackText.text = "";
        }
        else
        {
            startButton.interactable = false;
            feedbackText.text = feedbackMessage;
        }
    }

    public void StartGame()
    {
        if (player1Selection >= 0 && player2Selection >= 0)
        {
            GameManager.instance.SetPlayerSelection(player1Selection, player2Selection);
            SceneManager.LoadScene(gameScene);
        }
        else
        {
            feedbackText.text = feedbackMessage;
        }
    }
}
