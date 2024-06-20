using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int player1CharacterIndex;
    private int player2CharacterIndex;

    public GameObject[] backgrounds;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(instance);

        // Check if there are backgrounds assigned
        if (backgrounds == null || backgrounds.Length == 0)
        {
            Debug.LogError("No backgrounds assigned!");
            return;
        }

        // Select a random index
        int randomIndex = Random.Range(0, backgrounds.Length);

        // Loop through all backgrounds
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (i == randomIndex)
            {
                // Enable the randomly selected background
                backgrounds[i].SetActive(true);
            }
            else
            {
                // Disable all other backgrounds
                backgrounds[i].SetActive(false);
            }
        }
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
}
