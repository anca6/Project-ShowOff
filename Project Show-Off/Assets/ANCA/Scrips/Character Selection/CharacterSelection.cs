using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    [SerializeField] private GameObject[] player1HoverImages;
    [SerializeField] private GameObject[] player2HoverImages;

    [SerializeField] private GameObject[] player1CharacterImages;
    [SerializeField] private GameObject[] player2CharacterImages;

    [SerializeField] private GameObject HighlightStartbuttonImage;

    private int player1Selection = 0; // Default to the first option for Player 1
    private int player2Selection = 0; // Default to the first option for Player 2

    private void Start()
    {
        UpdateSelectionImages(player1Images, player1Selection, player1HoverImages, player1CharacterImages);
        UpdateSelectionImages(player2Images, player2Selection, player2HoverImages, player2CharacterImages);
        UpdateStartButtonState();
    }

    private void Update()
    {
        HandlePlayer1Input();
        HandlePlayer2Input();
    }

    private void HandlePlayer1Input()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveSelectionUp(ref player1Selection, player1Images.Length);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveSelectionDown(ref player1Selection, player1Images.Length);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SelectCharacterP1(player1Selection);
        }
    }

    private void HandlePlayer2Input()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelectionUp(ref player2Selection, player2Images.Length);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelectionDown(ref player2Selection, player2Images.Length);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectCharacterP2(player2Selection);
        }
    }


    private void MoveSelectionUp(ref int selection, int maxIndex)
    {
        selection--;
        if (selection < 0)
        {
            selection = maxIndex - 1; // Make it Loop back to the last option
        }
        UpdateSelectionImages(player1Images, player1Selection, player1HoverImages, player1CharacterImages);
        UpdateSelectionImages(player2Images, player2Selection, player2HoverImages, player1CharacterImages);
    }

    private void MoveSelectionDown(ref int selection, int maxIndex)
    {
        selection++;
        if (selection >= maxIndex)
        {
            selection = 0; // Make it Loop back to the first option
        }
        UpdateSelectionImages(player1Images, player1Selection, player1HoverImages, player1CharacterImages);
        UpdateSelectionImages(player2Images, player2Selection, player2HoverImages, player2CharacterImages);
    }

    private void UpdateSelectionImages(GameObject[] images, int selectedIndex, GameObject[] hoverImages, GameObject[] characterImages)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(i == selectedIndex);
            hoverImages[i].SetActive(i == selectedIndex);
            characterImages[i].SetActive(i == selectedIndex);
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

    public void SelectCharacterP1(int index)
    {
        player1Selection = index;
        UpdateSelectionImages(player1Images, player1Selection, player1HoverImages, player1CharacterImages);
        UpdateStartButtonState();

        // Set the startButton as the selected game object
        EventSystem.current.SetSelectedGameObject(startButton.gameObject);

        // Activate the selection indicator image
        HighlightStartbuttonImage.SetActive(true);
    }

    public void SelectCharacterP2(int index)
    {
        player2Selection = index;
        UpdateSelectionImages(player2Images, player2Selection, player2HoverImages, player2CharacterImages);
        UpdateStartButtonState();

        // Set the startButton as the selected game object
        EventSystem.current.SetSelectedGameObject(startButton.gameObject);

        // Activate the selection indicator image
        HighlightStartbuttonImage.SetActive(true);
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
