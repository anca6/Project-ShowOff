using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    private void Start()
    {
        int player1Index = GameManager.instance.GetPlayer1CharacterIndex();
        int player2Index = GameManager.instance.GetPlayer2CharacterIndex();

        ActivateSelectedCharacter(player1, player1Index);
        ActivateSelectedCharacter(player2, player2Index);
    }
    private void ActivateSelectedCharacter(GameObject player, int index)
    {
        for (int i = 0; i < player.transform.childCount; i++)
        {
            player.transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }
}
