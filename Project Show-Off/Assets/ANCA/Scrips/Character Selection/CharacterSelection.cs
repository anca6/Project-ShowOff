using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private string gameScene;

    private int player1Selection = 0;
    private int player2Selection = 0;

    public void SelectCharacterP1(int index)
    {
        player1Selection = index;
    }
    public void SelectCharacterP2(int index)
    {
        player2Selection = index;
    }
    public void StartGame()
    {
        GameManager.instance.SetPlayerSelection(player1Selection, player2Selection);
        SceneManager.LoadScene(gameScene);
    }
}
