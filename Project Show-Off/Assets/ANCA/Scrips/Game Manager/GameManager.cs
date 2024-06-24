using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int player1CharacterIndex;
    private int player2CharacterIndex;

    [SerializeField] private MonoBehaviour disableScript;

    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(instance);  
    }

    public void SetPlayerSelection(int p1index,  int p2index)
    {
        player1CharacterIndex = p1index;
        player2CharacterIndex = p2index;
    }

    public int GetPlayer1CharacterIndex()
    {
        return player1CharacterIndex;
    }

    public int GetPlayer2CharacterIndex()
    {
        return player2CharacterIndex; 
    }

    public void StartGame()
    {
        if (disableScript != null)
        {
            disableScript.enabled = false;
        }
    }

    public void ResumeCharSelect()
    {
        disableScript.enabled = true;
    }
}
