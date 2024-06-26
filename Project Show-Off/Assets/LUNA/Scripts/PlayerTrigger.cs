using UnityEngine;
using UnityEngine.Events;
// PlayerTrigger class by Luna
public class PlayerTrigger : MonoBehaviour{
    public bool TriggerOnce;
    public string PlayerTag = "Player";
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerLeave;

    public bool Triggered{ get; private set; }

    private void OnTriggerEnter(Collider other){
        if (TriggerOnce && Triggered || !other.gameObject.CompareTag(PlayerTag)) return;
        OnPlayerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other){
        if (TriggerOnce && Triggered || !other.gameObject.CompareTag(PlayerTag)) return;
        Triggered = true;
        OnPlayerLeave.Invoke();
    }
}
