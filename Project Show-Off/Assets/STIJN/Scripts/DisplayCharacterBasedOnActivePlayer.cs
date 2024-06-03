using System.Collections.Generic;
using UnityEngine;

public class CharacterObjectManager : MonoBehaviour
{
    [SerializeField] private PlayerSwitch playerSwitch; // Reference to the PlayerSwitch script

    // List of GameObjects to manage based on character index
    [SerializeField] private List<GameObject> objectsToManage;

    private void Start()
    {
        // Ensure the objects are properly set at the start based on the current character
        UpdateObjectState(playerSwitch.currentCharacter);
    }

    private void Update()
    {
        // Check if the active character has changed and update the object states accordingly
        UpdateObjectState(playerSwitch.currentCharacter);
    }

    private void UpdateObjectState(int activeCharacterIndex)
    {
        for (int i = 0; i < objectsToManage.Count; i++)
        {
            // Enable the object if its index matches the active character index, otherwise disable it
            objectsToManage[i].SetActive(i == activeCharacterIndex);
        }
    }
}
