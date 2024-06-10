using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                Debug.LogError("No GameManager!");
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
}
