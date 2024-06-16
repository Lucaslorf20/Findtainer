using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Battlehub.Utils;
using Battlehub.RTHandles;
using Battlehub.RTCommon;

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
    void Awake()
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
        NumeroContainerUI.transform.GetChild(0).gameObject.SetActive(true);
        TMP_Text textMesh = NumeroContainerUI.GetComponentInChildren<TMP_Text>();
        textMesh.text = NrContainer;

        CriarContainer.selectedContainer = gameObject;
        CriarContainer.recadastro = true;
    }

    public void OcultarContainer()
    {  
        if(NumeroContainerUI.active)
        {
            TMP_Text textMesh = NumeroContainerUI.GetComponentInChildren<TMP_Text>();
            textMesh.text = "";
            NumeroContainerUI.transform.GetChild(0).gameObject.SetActive(false);

            CriarContainer.selectedContainer = null;
            CriarContainer.recadastro = false;
	    Debug.Log("verifica");
        }
    }

    public void HighlightContainer()
    {
        IRuntimeSelection m_selection;
	m_selection = IOC.Resolve<IRTE>().Selection;
        m_selection.Select(gameObject, new Object[] { gameObject });
        ExibirContainer();
    }
}