using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlehub.Utils;
using Battlehub.RTCommon;
using System;

public class SaidaContainer : MonoBehaviour
{
    public static List<GameObject> selectedContainer = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickSaidaContainer()
    {
        Debug.Log("Qtd de contêiner selecionado:  " + selectedContainer.Count);
        for (int i = 0; i < selectedContainer.Count; i++)
        {
            selectedContainer[i].GetComponent<Container>().containerExcluded = true;
            Destroy(selectedContainer[i]);
        }
        
    }
}
