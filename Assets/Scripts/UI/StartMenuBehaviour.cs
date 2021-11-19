using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuBehaviour : MonoBehaviour
{

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
}
