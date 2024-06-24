using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

    private bool player1Confirmed = false; //bool for player1 selection confirmation
    private bool player2Confirmed = false; //bool for player2 selection confirmation

    private Gamepad gamepad1;
    private Gamepad gamepad2;

    private void Start()
    {
        if (Gamepad.all.Count > 0)
        {
            gamepad1 = Gamepad.all.Count > 0 ? Gamepad.all[0] : null;
            gamepad2 = Gamepad.all.Count > 1 ? Gamepad.all[1] : null;
        }

        if (player1Images != null && player1HoverImages != null && player1CharacterImages != null)
        {
            UpdateSelectionImages(player1Images, player1Selection, player1HoverImages, player1CharacterImages);
        }
        if (player2Images != null && player2HoverImages != null && player2CharacterImages != null)
        {
            UpdateSelectionImages(player2Images, player2Selection, player2HoverImages, player2CharacterImages);
        }
        UpdateStartButtonState();

        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void Update()
    {
        HandlePlayer1Input();
        HandlePlayer2Input();
        CheckGamepadStart();
    }

    private void HandlePlayer1Input()
    {
        if (gamepad1 != null)
        {
            if (gamepad1.dpad.up.wasPressedThisFrame)
            {
                MoveSelectionUp(ref player1Selection, player1Images.Length);
                player1Confirmed = false;
                UpdateStartButtonState();
            }
            else if (gamepad1.dpad.down.wasPressedThisFrame)
            {
                MoveSelectionDown(ref player1Selection, player1Images.Length);
                player1Confirmed = false;
                UpdateStartButtonState();
            }
            else if (gamepad1.buttonNorth.wasPressedThisFrame) //ability button to select the character
            {
                SelectCharacterP1(player1Selection);
            }
        }
    }

    private void HandlePlayer2Input()
    {
        if (gamepad2 != null)
        {
            if (gamepad2.dpad.up.wasPressedThisFrame)
            {
                MoveSelectionUp(ref player2Selection, player2Images.Length);
                player2Confirmed = false;
                UpdateStartButtonState();
            }
            else if (gamepad2.dpad.down.wasPressedThisFrame)
            {
                MoveSelectionDown(ref player2Selection, player2Images.Length);
                player2Confirmed = false;
                UpdateStartButtonState();
            }
            else if (gamepad2.buttonNorth.wasPressedThisFrame) //ability button to select the character
            {
                SelectCharacterP2(player2Selection);
            }
        }
    }

    private void MoveSelectionUp(ref int selection, int maxIndex)
    {
        selection--;
        if (selection < 0)
        {
            selection = maxIndex - 1; // Make it Loop back to the last option
        }
        UpdateAllSelectionImages();
    }

    private void MoveSelectionDown(ref int selection, int maxIndex)
    {
        selection++;
        if (selection >= maxIndex)
        {
            selection = 0; // Make it Loop back to the first option
        }
        UpdateAllSelectionImages();
    }

    private void UpdateAllSelectionImages()
    {
        if (player1Images != null && player1HoverImages != null && player1CharacterImages != null)
        {
            UpdateSelectionImages(player1Images, player1Selection, player1HoverImages, player1CharacterImages);
        }
        if (player2Images != null && player2HoverImages != null && player2CharacterImages != null)
        {
            UpdateSelectionImages(player2Images, player2Selection, player2HoverImages, player2CharacterImages);
        }
    }

    private void UpdateSelectionImages(GameObject[] images, int selectedIndex, GameObject[] hoverImages, GameObject[] characterImages)
    {
        if (images == null || hoverImages == null || characterImages == null) return;

        for (int i = 0; i < images.Length; i++)
        {
            if (images[i] != null)
                images[i].SetActive(i == selectedIndex);
            if (hoverImages[i] != null)
                hoverImages[i].SetActive(i == selectedIndex);
            if (characterImages[i] != null)
                characterImages[i].SetActive(i == selectedIndex);
        }
    }

    private void UpdateStartButtonState()
    {
        if (startButton != null)
        {
            if (player1Confirmed && player2Confirmed)
            {
                startButton.interactable = true;
                if (feedbackText != null)
                {
                    feedbackText.text = "";
                }
            }
            else
            {
                startButton.interactable = false;
                if (feedbackText != null)
                {
                    feedbackText.text = feedbackMessage;
                }
            }
        }
    }

    public void SelectCharacterP1(int index)
    {
        player1Selection = index;
        player1Confirmed = true;
        if (player1Images != null && player1HoverImages != null && player1CharacterImages != null)
        {
            UpdateSelectionImages(player1Images, player1Selection, player1HoverImages, player1CharacterImages);
        }
        UpdateStartButtonState();

        if (EventSystem.current != null && startButton != null)
        {
            EventSystem.current.SetSelectedGameObject(startButton.gameObject);
        }

        if (HighlightStartbuttonImage != null)
        {
            HighlightStartbuttonImage.SetActive(true);
        }
    }

    public void SelectCharacterP2(int index)
    {
        player2Selection = index;
        player2Confirmed = true;
        if (player2Images != null && player2HoverImages != null && player2CharacterImages != null)
        {
            UpdateSelectionImages(player2Images, player2Selection, player2HoverImages, player2CharacterImages);
        }
        UpdateStartButtonState();

        if (EventSystem.current != null && startButton != null)
        {
            EventSystem.current.SetSelectedGameObject(startButton.gameObject);
        }

        if (HighlightStartbuttonImage != null)
        {
            HighlightStartbuttonImage.SetActive(true);
        }
    }

    private void OnStartButtonClicked()
    {
        if (player1Confirmed && player2Confirmed)
        {
            StartGame();
        }
        else if (feedbackText != null)
        {
            feedbackText.text = feedbackMessage;
        }
    }

    private void CheckGamepadStart()
    {
        if ((gamepad1 != null && gamepad1.startButton.wasPressedThisFrame) ||
            (gamepad2 != null && gamepad2.startButton.wasPressedThisFrame))
        {
            if (player1Confirmed && player2Confirmed)
            {
                StartGame();
            }
            else if (feedbackText != null)
            {
                feedbackText.text = feedbackMessage;
            }
        }
    }

    public void StartGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SetPlayerSelection(player1Selection, player2Selection);
        }
        SceneManager.LoadScene(gameScene);
    }
}
