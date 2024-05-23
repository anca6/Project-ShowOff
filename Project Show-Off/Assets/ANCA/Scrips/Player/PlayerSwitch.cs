using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwitch : MonoBehaviour
{
    private Transform characterTransform;
    public int currentCharacter;

    private float lastSwitchTime = 0f;
    private float switchCooldown = 1f;

    [SerializeField] private List<Transform> possibleCharacters; //list of the characters the player can turn into

    [SerializeField] private GameObject playerObj;

    //set the first character in the list by default
    private void Start()
    {
        if (characterTransform == null && possibleCharacters.Count >= 1)
        {
            characterTransform = possibleCharacters[0];
            currentCharacter = 0;
            characterTransform.gameObject.SetActive(true);
        }   
    }

    private void Update()
    {
        if (Time.time - lastSwitchTime < switchCooldown)
        {
            return;
        }

        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return;
        }

        //if we press X on the controller
        if (gamepad.buttonWest.wasPressedThisFrame)
        {
            CycleCharacter(1);
        }

        //if we press B on the controller
        else if (gamepad.buttonEast.wasPressedThisFrame)
        {
            CycleCharacter(-1);
        }
    }

    //increasing/decreasing the character cycling increment and setting it to the current character
    private void CycleCharacter(int increment)
    {
        int newCharacter = currentCharacter + increment;
        if (newCharacter < 0)
        {
            newCharacter = possibleCharacters.Count - 1;
        }
        else if (newCharacter >= possibleCharacters.Count)
        {
            newCharacter = 0;
        }

        currentCharacter = newCharacter;
        Debug.Log($"Switched to monster {currentCharacter + 1}");
        lastSwitchTime = Time.time; //countdown restarts
        SwitchCharacter(currentCharacter);
    }

    //set active only the gameobject that we switch to, and set the other ones to inactive
    private void SwitchCharacter(int index)
    {
        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            possibleCharacters[i].gameObject.SetActive(i == index);
        }
    }
}
