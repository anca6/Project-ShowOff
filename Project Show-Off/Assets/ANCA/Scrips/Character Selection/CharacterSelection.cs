using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private string gameScene;

    private int player1Selection = 0;
    private int player2Selection = 0;

    private void SelectCharacterP1(int index)
    {
        player1Selection = index;
    }
    private void SelectCharacterP2(int index)
    {
        player2Selection = index;
    }
    private void StartGame()
    {
        GameManager.instance.SetPlayerSelection(player1Selection, player2Selection);
        SceneManager.LoadScene(gameScene);
    }
}
