using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwitch : MonoBehaviour
{
    private Transform characterTransform;
    public int currentCharacter;

    private float lastSwitchTime = 0f;
    private float switchCooldown = 1f;

    [SerializeField] private List<Transform> possibleCharacters;

    [SerializeField] private GameObject playerObj;

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

        if (gamepad.rightShoulder.wasPressedThisFrame)
        {
            CycleCharacter(1);
        }
        else if (gamepad.leftShoulder.wasPressedThisFrame)
        {
            CycleCharacter(-1);
        }
    }

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
        lastSwitchTime = Time.time;
        SwitchCharacter(currentCharacter);
    }

    private void SwitchCharacter(int index)
    {
        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            possibleCharacters[i].gameObject.SetActive(i == index);
        }
    }
}
