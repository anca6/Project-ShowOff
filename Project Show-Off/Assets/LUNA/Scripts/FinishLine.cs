using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class FinishLine : MonoBehaviour{
    public string PlayerTag = "Player";
    
    private void OnTriggerEnter(Collider other){
        if (!other.gameObject.CompareTag(PlayerTag)) return;
    }
}
