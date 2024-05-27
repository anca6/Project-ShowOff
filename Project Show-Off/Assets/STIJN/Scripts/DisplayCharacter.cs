using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIUpdater : MonoBehaviour
{
    [SerializeField] private PlayerSwitch playerSwitch; // Reference to the PlayerSwitch script
    [SerializeField] private List<Image> characterImages; // List of images to show for each character

    private void Start()
    {
        if (playerSwitch == null)
        {
            Debug.LogError("PlayerSwitch reference is not set!");
            return;
        }

        if (characterImages.Count == 0)
        {
            Debug.LogError("Character images are not set!");
            return;
        }

        UpdateCharacterImage(playerSwitch.currentCharacter);
    }

    private void Update()
    {
        UpdateCharacterImage(playerSwitch.currentCharacter);
    }

    private void UpdateCharacterImage(int characterIndex)
    {
        for (int i = 0; i < characterImages.Count; i++)
        {
            characterImages[i].gameObject.SetActive(i == characterIndex);
        }
    }
}
