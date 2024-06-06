using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] player1Characters;
    [SerializeField] private GameObject[] player2Characters;

    private int selectedCharacterIndexPlayer1 = 0;
    private int selectedCharacterIndexPlayer2 = 0;

    [SerializeField] private Button[] player1Buttons;
    [SerializeField] private Button[] player2Buttons;

    [SerializeField] private string gameSceneName;

    void Start()
    {
        UpdateSelectionUI();
    }

    public void SelectCharacterPlayer1(int index)
    {
        selectedCharacterIndexPlayer1 = index;
        UpdateSelectionUI();
    }

    public void SelectCharacterPlayer2(int index)
    {
        selectedCharacterIndexPlayer2 = index;
        UpdateSelectionUI();
    }

    void UpdateSelectionUI()
    {
        for (int i = 0; i < player1Buttons.Length; i++)
        {
            player1Buttons[i].interactable = i != selectedCharacterIndexPlayer1;
        }

        for (int i = 0; i < player2Buttons.Length; i++)
        {
            player2Buttons[i].interactable = i != selectedCharacterIndexPlayer2;
        }
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("SelectedCharacterPlayer1", selectedCharacterIndexPlayer1);
        PlayerPrefs.SetInt("SelectedCharacterPlayer2", selectedCharacterIndexPlayer2);
        SceneManager.LoadScene(gameSceneName);
    }
}
