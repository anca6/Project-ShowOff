using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameDetection : MonoBehaviour
{
    [SerializeField] private string gameScene;
    public GameObject player1Object;
    public GameObject player2Object;
    public float maxZCoordinate;

    // Start is called before the first frame update
    void Start()
    {
        if (player1Object == null || player2Object == null)
        {
            Debug.LogError("Not all players assigned");
            enabled = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float currentZPositionP1 = player1Object.transform.position.z;
        float currentZPositionP2 = player2Object.transform.position.z;

        if (currentZPositionP1 > maxZCoordinate && currentZPositionP2 > maxZCoordinate)
        {

            EndGame();
        }

    }
        public void EndGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}