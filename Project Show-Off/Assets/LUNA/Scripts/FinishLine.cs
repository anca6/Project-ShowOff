using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(BoxCollider))]
public class FinishLine : MonoBehaviour{
    public string PlayerTag = "Player";
    public List<GameObject> Players;
    public string EndingSceneName;
    
    private void OnTriggerEnter(Collider other){
        if (!other.gameObject.CompareTag(PlayerTag)) return;
        foreach (GameObject player in Players){
            if (other.gameObject != player) continue;
            FinishGame(player);
            break;
        }
    }

    public void FinishGame(GameObject winningPlayer){
        if (GameManager.instance == null) throw new Exception("GameManager not initialised!");
        GameManager.instance.VictorName = winningPlayer.name;
        Debug.Log($"{GameManager.instance.VictorName} wins!");
        SceneManager.LoadScene(EndingSceneName);
    }
}
