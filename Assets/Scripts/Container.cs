using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Container : MonoBehaviour
{
    public string NrContainer;
    public string NrLacre;
    public int CdTipoContainer;
    public string NmCliente;
    public string NmArmador;
    public string NrContrato;
    public float QtTara;
    public float QtPesoBruto;
    public float QtPesoMaximo;
    public string NrNotafiscal;
    public string NrReserva;
    public string NrLacreSIF;
    public string NrLacreArmador;
    public float QtTemperatura;
    public string DsMercadoria;
    public System.DateTime inputDateTime;


    private GameObject NumeroContainerUI;

    // Start is called before the first frame update
    void Start()
    {
        NumeroContainerUI = GameObject.FindWithTag("NrContainerUI");
        //inputDateTime = System.DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnApplicationQuit()
    {
        SaveLoadContainer.containers.Add(this);
        Debug.Log("adicionou");
        SaveLoadContainer.SaveContainer();
    }

    public void ExibirContainer()
    {
        NumeroContainerUI.transform.GetChild(0).gameObject.SetActive(true);
        TMP_Text textMesh = NumeroContainerUI.GetComponentInChildren<TMP_Text>();
        textMesh.text = NrContainer;
    }

    public void OcultarContainer()
    {
        TMP_Text textMesh = NumeroContainerUI.GetComponentInChildren<TMP_Text>();
        textMesh.text = "";
        NumeroContainerUI.transform.GetChild(0).gameObject.SetActive(false);
    }
}