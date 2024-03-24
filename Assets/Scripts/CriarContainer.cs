using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CriarContainer : MonoBehaviour
{
    public GameObject container;
    public GameObject CadastroContainer;
    [SerializeField]
    private GameObject loading;
    [SerializeField]
    private TMP_Text textoBotao;
    [SerializeField]
    private TMP_InputField numeroContainer;
    [SerializeField]
    private TMP_InputField numeroLacre;
    [SerializeField]
    private TMPro.TMP_Dropdown tipoContainer;

    public void OnButtonClick()
    {
        GameObject pc = (GameObject) Instantiate (container, transform.position, Quaternion.identity);
        pc.transform.rotation = Quaternion.Euler(0,0,0);
        pc.transform.position = new Vector3(0,0,0);
    }

     public void Update () 
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //OnButtonClick();
            CadastroContainer.SetActive(true);
        }
     }

     public void CreateContainer()
     {
        textoBotao.text = "";
        loading.SetActive(true);

        bool fecharTela = ValidateContainer();

        loading.SetActive(false);
        textoBotao.text = "Cadastrar";
        if(fecharTela) 
            CadastroContainer.SetActive(false);
     }

     public bool ValidateContainer()
     {
        if(numeroContainer.text != "" && numeroLacre.text != "" && tipoContainer.value != 0)
        {
            GameObject pc = (GameObject) Instantiate (container, transform.position, Quaternion.identity);
            pc.transform.rotation = Quaternion.Euler(0,0,0);
            pc.transform.position = new Vector3(0,0,0);
            Container ScriptContainer = pc.GetComponent<Container>();
            ScriptContainer.NrContainer = numeroContainer.text;
            ScriptContainer.NrLacre = numeroLacre.text;
            ScriptContainer.CdTipoContainer = tipoContainer.value; //tipoContainer.options[tipoContainer.value].text;
            Debug.Log(tipoContainer.options[tipoContainer.value].text);

            return true;
        }
        else
            return false;
     }
}
