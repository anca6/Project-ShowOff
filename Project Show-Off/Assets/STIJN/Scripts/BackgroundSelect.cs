using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] backgrounds;

    private void Awake()
    {
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
}