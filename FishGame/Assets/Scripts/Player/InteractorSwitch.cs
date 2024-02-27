using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorSwitch : MonoBehaviour
{
    public GameObject rayInteractor;
    public GameObject directInteractor;

    public void Switch()
    {
        if (rayInteractor.activeInHierarchy)
        {
            rayInteractor.SetActive(false);
            directInteractor.SetActive(true);
        }
        else
        {
            directInteractor.SetActive(false);
            rayInteractor.SetActive(true);
        }
    }
}
