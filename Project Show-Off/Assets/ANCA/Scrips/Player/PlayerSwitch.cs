using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public Transform characterTransform;
    public List<Transform> possibleCharacters;
    public int currentCharacter;

    private void Start()
    {
        if (characterTransform == null && possibleCharacters.Count >= 1)
        {
            characterTransform = possibleCharacters[0];
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            currentCharacter = 0;
            Debug.Log("switched to 1");
            Switch();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            currentCharacter = 1;
            Debug.Log("switched to 2");
            Switch();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            currentCharacter = 2;
            Debug.Log("switched to 3");
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
