using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    private Transform characterTransform;
    private int currentCharacter;

    private float lastSwitchTime = 0f;
    private float switchCooldown = 1f;

    [SerializeField] private List<Transform> possibleCharacters;

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
            int newCharacter = currentCharacter;

            switch (true)
            {
                case true when Input.GetKeyUp(KeyCode.Alpha1):
                    newCharacter = 0; break;
                case true when Input.GetKeyUp(KeyCode.Alpha2):
                    newCharacter = 1; break;
                case true when Input.GetKeyUp(KeyCode.Alpha3):
                    newCharacter = 2; break;
            }

            if (newCharacter != currentCharacter)
            {
                currentCharacter = newCharacter;
                Debug.Log($"switched to {currentCharacter + 1}");
                lastSwitchTime = Time.time;
                Switch();
            }
    }

    private void Switch()
    {
        characterTransform = possibleCharacters[currentCharacter];
        characterTransform.gameObject.SetActive(true);
        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if (possibleCharacters[i] != characterTransform)
            {
                possibleCharacters[i].gameObject.SetActive(false);
            }
        }
    }

}
