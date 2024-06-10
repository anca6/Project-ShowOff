using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;

    void Update()
    {
        //Put item logic here when implemented

        //Temp testing code below
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnableItem1();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableItem2();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EnableItem3();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DisableAll();
        }
    }

    void EnableItem1()
    {
        item1.SetActive(true);
        item2.SetActive(false);
        item3.SetActive(false);
    }

    void EnableItem2()
    {
        item1.SetActive(false);
        item2.SetActive(true);
        item3.SetActive(false);
    }

    void EnableItem3()
    {
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(true);
    }
    void DisableAll()
    {
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);
    }

}
