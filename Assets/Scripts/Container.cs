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
    public bool containerExcluded = false;
    public System.DateTime inputDateTime;


    private GameObject NumeroContainerUI;

    // Start is called before the first frame update
    void Start()
    {
        NumeroContainerUI = GameObject.FindWithTag("NrContainerUI");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnApplicationQuit()
    {
        SavingContainer();
    }

    public void SavingContainer()
    {
        SaveLoadContainer.containers.Add(this);
        Debug.Log("adicionou");
        SaveLoadContainer.SaveContainer();
    }

    private void OnDestroy()
    {
        if (containerExcluded)
        {
            SaveLoadContainer.containers.Remove(this);
            SaveLoadContainer.RemoveContainer(NrContainer);
        }
    }

    public void ExibirContainer()
    {
	Rigidbody rigidbody = GetComponent<Rigidbody>();
	rigidbody.useGravity = false;	

        NumeroContainerUI.transform.GetChild(0).gameObject.SetActive(true);
        TMP_Text textMesh = NumeroContainerUI.GetComponentInChildren<TMP_Text>();
        textMesh.text = NrContainer;

        SaidaContainer.selectedContainer.Add(gameObject);
    }

    public void OcultarContainer()
    {
	Rigidbody rigidbody = GetComponent<Rigidbody>();
	rigidbody.useGravity = true;

        TMP_Text textMesh = NumeroContainerUI.GetComponentInChildren<TMP_Text>();
        textMesh.text = "";
        NumeroContainerUI.transform.GetChild(0).gameObject.SetActive(false);

        SaidaContainer.selectedContainer.Remove(gameObject);
    }
}