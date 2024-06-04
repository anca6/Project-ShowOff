using UnityEngine;

public class Standingchecker : MonoBehaviour
{
    public GameObject ProgressMeterThisPlayer;
    public GameObject ProgressMeterOtherPlayer;
    public GameObject firstplace;
    public GameObject secondplace;


    void Update()
    {
        if (ProgressMeterThisPlayer == null || ProgressMeterOtherPlayer == null || firstplace == null || secondplace == null)
        {
            Debug.LogError("Not all objects assigned");
            return;
        }

        //If this object is higher than the other, set firstplace active, otherwise set secondplace active
        if (ProgressMeterThisPlayer.transform.position.y > ProgressMeterOtherPlayer.transform.position.y)
        {
            firstplace.SetActive(true);
            secondplace.SetActive(false);
        }            
        else
        {
            secondplace.SetActive(true);
            firstplace.SetActive(false);
        }
    }
}
