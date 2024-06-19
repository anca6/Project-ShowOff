
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound : MonoBehaviour
{

    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player" && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
