using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarContainer : MonoBehaviour
{
    public GameObject container;

    public void OnButtonClick()
    {
        GameObject pc = (GameObject) Instantiate (container, transform.position, Quaternion.identity);
        pc.transform.rotation = Quaternion.Euler(270,0,0);
        pc.transform.position = new Vector3(0,0,0);
    }

         public void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnButtonClick();
        }
     }
}
