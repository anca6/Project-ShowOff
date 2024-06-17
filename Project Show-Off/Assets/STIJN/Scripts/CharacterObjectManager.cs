using System.Collections.Generic;
using UnityEngine;

public class CharacterObjectManager : MonoBehaviour
{
    // List of GameObjects to manage based on character index
    [SerializeField] private List<GameObject> objectsToManage;
    [SerializeField] private int playerNumber;

    private void Start()
    {
        // Ensure the objects are properly set at the start based on the current character
        UpdateObjectState(GetActiveCharacterIndex());
    }

    private void Update()
    {
        // Check if the active character has changed and update the object states accordingly
        UpdateObjectState(GetActiveCharacterIndex());
    }

    private int GetActiveCharacterIndex()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is null. there is no 'GameManager'.");
            return -1;
        }

        if (playerNumber == 1)
        {
            return GameManager.instance.GetPlayer1CharacterIndex();
        }
        else if (playerNumber == 2)
        {
            return GameManager.instance.GetPlayer2CharacterIndex();
        }
        return -1;
    }

    private void UpdateObjectState(int activeCharacterIndex)
    {
        if (activeCharacterIndex == -1)
        {
            //Debug.LogError("Invalid character index.");
            return;
        }

        for (int i = 0; i < objectsToManage.Count; i++)
        {
            // Enable the object if its index matches the active character index, otherwise disable it
            objectsToManage[i].SetActive(i == activeCharacterIndex);
        }
    }
}
