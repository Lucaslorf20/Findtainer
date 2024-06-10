using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class CriarContainer : MonoBehaviour
{
    public GameObject container;
    public GameObject CadastroContainer;
    [SerializeField]
    private GameObject EscContainer;
    [SerializeField]
    public GameObject UIHandle;
    [SerializeField]
    private GameObject loading;
    [SerializeField]
    private TMP_Text textoBotao;
    [SerializeField]
    private TMP_InputField cliente;
    [SerializeField]
    private TMP_InputField numeroContainer;
    [SerializeField]
    private TMP_InputField armador;
    [SerializeField]
    private TMP_InputField contrato;
    [SerializeField]
    private TMP_InputField tara;
    [SerializeField]
    private TMP_InputField pesobruto;
    [SerializeField]
    private TMP_InputField pesomaximo;
    [SerializeField]
    private TMP_InputField notafiscal;
    [SerializeField]
    private TMP_InputField reserva;
    [SerializeField]
    private TMP_InputField numerolacresif;
    [SerializeField]
    private TMP_InputField temperatura;
    [SerializeField]
    private TMP_InputField mercadoria;
    [SerializeField]
    private TMP_InputField numeroLacre;
    [SerializeField]
    private TMP_Text textoErro;
    [SerializeField]
    private TMPro.TMP_Dropdown tipoContainer;
    [SerializeField]
    private GameObject escFeedback;
    [SerializeField]
    private GameObject escLoading;
    [SerializeField]
    private GameObject textoLogin;
    private TMP_InputField[] inputFields;

    public void OnButtonClick()
    {
        GameObject pc = (GameObject) Instantiate (container, transform.position, Quaternion.identity);
        pc.transform.rotation = Quaternion.Euler(0,0,0);
        pc.transform.position = new Vector3(0,0,0);
    }

    public void Start()
    {
        inputFields = new TMP_InputField[]
        {
            cliente, armador, contrato, numeroContainer, tara, pesobruto, pesomaximo,
            notafiscal, reserva, numeroLacre, numerolacresif, temperatura,
            mercadoria
        };
    }

     public void Update () 
    {
        if (Input.GetKeyDown(KeyCode.C) && !CadastroContainer.active)
        {
            //OnButtonClick();
            CadastroContainer.SetActive(true);
            UIHandle.SetActive(false);
            LimparCampos();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && CadastroContainer.active)
        {
            FecharTela();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !CadastroContainer.active && !EscContainer.active)
        {
            EscContainer.SetActive(true);
            UIHandle.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && EscContainer.active)
        {
            VoltarPatio();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && CadastroContainer.active)
        {
            TMP_InputField currentInputField = null;
            foreach (TMP_InputField inputField in inputFields)
            {
                if (inputField.isFocused)
                {
                    currentInputField = inputField;
                    break;
                }
            }

            if (currentInputField == null)
            {
                SelecionaProximoCampo(inputFields[0]);
            }
            else
            {
                int Indice = System.Array.IndexOf(inputFields, currentInputField);
                
                int nextIndice = (Indice + 1) % inputFields.Length;
                
                SelecionaProximoCampo(inputFields[nextIndice]);
            }
        }
     }

         void SelecionaProximoCampo(TMP_InputField inputField)
    {
        inputField.Select();
        inputField.ActivateInputField();
    }

    public void VoltarPatio()
    {
        EscContainer.SetActive(false);
        UIHandle.SetActive(true);
    }

    public void VoltarLogin()
    {
        LimparCampos();
        textoLogin.SetActive(false);
        escLoading.SetActive(true);
        escFeedback.SetActive(true);
        StartCoroutine(CarregaScene());
    }

    IEnumerator CarregaScene()
    {
        Debug.Log("Voltando ao login...");
        yield return new WaitForSeconds(1.5f);
        Application.LoadLevel("Login");
    }

    public void FecharApp()
    {
        Application.Quit();
    }

    public void CreateContainer()
     {
        textoBotao.text = "";
        loading.SetActive(true);

        bool fecharTela = ValidateContainer();

        loading.SetActive(false);
        textoBotao.text = "Cadastrar";
        if(fecharTela) 
            FecharTela();
     }

     public bool ValidateContainer()
     {
        if(contrato.text == "")
        {
            textoErro.text = "Contrato não pode ser nulo";
            return false;
        }
        if(numeroContainer.text == "")
        {
            textoErro.text = "Número de container não pode ser nulo";
            return false;
        }
        if(numeroContainer.text.Length != 11)
        {
            textoErro.text = "Número de container deve conter 11 dígitos\nExemplo: ZZZU1234567";
            return false;
        }
        if(tipoContainer.value == 0)
        {
            textoErro.text = "Selecione um tipo de container";
            return false;
        }

        if (ContainerExiste())
        {
            textoErro.text = "Já existe um container com o mesmo número.";
            return false;
        }

        float QtTaraAux = tara.text == "" ? 0 : float.Parse(tara.text);
        if(QtTaraAux < 0)
        {
            textoErro.text = "Tara não pode ser negativa";
            return false;
        }

        float QtPesoBrutoAux = pesobruto.text == "" ? 0 : float.Parse(pesobruto.text);
        if(QtPesoBrutoAux < 0)
        {
            textoErro.text = "Peso Bruto não pode ser negativo";
            return false;
        }

        float QtPesoMaximoAux = pesomaximo.text == "" ? 0 : float.Parse(pesomaximo.text);
        if(QtPesoMaximoAux < 0)
        {
            textoErro.text = "Peso Máximo não pode ser negativo";
            return false;
        }

            GameObject pc = (GameObject) Instantiate (container, transform.position, Quaternion.identity);
            pc.transform.rotation = Quaternion.Euler(0,0,0);
            pc.transform.position = new Vector3(0,0,0);
            Container ScriptContainer = pc.GetComponent<Container>();
            ScriptContainer.NrContainer = numeroContainer.text;
            ScriptContainer.NrLacre = numeroLacre.text;
            ScriptContainer.CdTipoContainer = tipoContainer.value; //tipoContainer.options[tipoContainer.value].text;
            ScriptContainer.NmCliente = cliente.text;
            ScriptContainer.NmArmador = armador.text;
            ScriptContainer.NrContrato = contrato.text;
            ScriptContainer.QtTara = QtTaraAux;
            ScriptContainer.QtPesoBruto = pesobruto.text == "" ? 0 : float.Parse(pesobruto.text);
            ScriptContainer.QtPesoMaximo = pesomaximo.text == "" ? 0 : float.Parse(pesomaximo.text);
            ScriptContainer.NrNotafiscal = notafiscal.text;
            ScriptContainer.NrReserva = reserva.text;
            ScriptContainer.NrLacreSIF = numerolacresif.text;
            ScriptContainer.NrLacreArmador = numeroLacre.text;
            ScriptContainer.QtTemperatura = temperatura.text == "" ? 0 : float.Parse(temperatura.text);
            ScriptContainer.DsMercadoria = mercadoria.text;
            Debug.Log(tipoContainer.options[tipoContainer.value].text);

            return true;
     }

         void LimparCampos()
    {
        cliente.text = "";
        numeroContainer.text = "";
        numeroLacre.text = "";
        tipoContainer.value = 0;
        armador.text = "";
        contrato.text = "";
        tara.text = "";
        pesobruto.text = "";
        pesomaximo.text = "";
        notafiscal.text = "";
        reserva.text = "";
        temperatura.text = "";
        mercadoria.text = "";
        textoErro.text = "";
    }

    public void FecharTela()
    {
        CadastroContainer.SetActive(false);
        UIHandle.SetActive(true);
    }

    public void TravarTemperatura()
    {
        if  (tipoContainer.value == 1)
        {
            temperatura.interactable = true;
        }
        else 
        {
            temperatura.text = "";
            temperatura.interactable = false;
        }
    }

    public void ValidarNumeroContainer()
    {
        string nrContainerAux = numeroContainer.text;
        nrContainerAux = nrContainerAux.Replace("-", "").Replace(".", "").Replace("-", "").Replace(" ", "");
        if(nrContainerAux.Length == 11)
        {
            //if(nrContainerAux[10].ToString() != retornaValidadorContainer(nrContainerAux)) // COMENTADO, POIS ESTA DANDO ERRO DE INDICE
            //{
                //textoErro.text = "Número de container inválido";
            //}
            
            //StartCoroutine(ChamarAPIBancoDeDadosContainer()); // COMENTADO, POIS ESTA RETORNANDO ACCESS TOKEN INVALIDO
        }
    }

    public string retornaValidadorContainer(string nrContainerAux)
    {
        var SiglaConteiner = "";

        string Result = "";

        var Soma = 0.0;
        var X = 1;

                while (X < 5)
                {
                    int Valor = nrContainerAux[X - 1] - 'A' + 1;

                    switch (Valor)
                    {
                        case 1:
                            Soma = Soma + ((Mathf.Pow(2, (X - 1))) * (Valor + 9));
                            break;

                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                            Soma = Soma + ((Mathf.Pow(2, (X - 1))) * (Valor + 10));

                            break;

                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 0x10:
                        case 0x11:
                        case 0x12:
                        case 0x13:
                        case 20:
                        case 0x15:
                            Soma = Soma + ((Mathf.Pow(2, (X - 1))) * (Valor + 11));
                            break;

                        default:
                            Soma = Soma + ((Mathf.Pow(2, (X - 1))) * (Valor + 12));
                            break;
                    }
                    X++;
                }

                while (X < 11)
                {
                    var Numeros = int.Parse(SiglaConteiner.Substring(X - 1, 1));
                    Soma = Soma + ((Mathf.Pow(2, (X - 1))) * Numeros);
                    X++;
                }
                var Divisao = Soma / 11;

                var Inteiro = (int)(Divisao);
                var Resto = Soma - (Inteiro * 11);
                if (Resto >= 10)
                {
                    Resto = 0;
                }

                Result = Resto.ToString();
                return Result;
    }

    public IEnumerator ChamarAPIBancoDeDadosContainer()
    {
        string url = "https://app.bic-boxtech.org/oauth/token";
        string login = "lucaspaiva@sateldespachos.com.br";
        string senha = "Zuluq015@";

        UnityWebRequest request = UnityWebRequest.PostWwwForm(url, "");

        string auth = login + ":" + senha;
        string authEncoded = System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(auth));
        string authHeader = "Basic " + authEncoded;
        request.SetRequestHeader("Authorization", authHeader);

/*
        string url = "https://app.bic-boxtech.org/api/v2.0/container/" + numeroContainer;

        string login = "lucaspaiva@sateldespachos.com.br";
        string senha = "Zuluq015@";

        UnityWebRequest request = UnityWebRequest.Get(url);

        string auth = login + ":" + senha;
        string authEncoded = System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(auth));
        string authHeader = "Basic " + authEncoded;
        request.SetRequestHeader("Authorization", authHeader);*/

        yield return request.SendWebRequest();

        // Verifica se houve algum erro durante a requisição
        if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro: " + request.error);
            yield break;
        }
        else
        {
            AccessTokenResponse accessTokenResponse = JsonUtility.FromJson<AccessTokenResponse>(request.downloadHandler.text);
            string accessToken = accessTokenResponse.accessToken;
            Debug.Log(accessToken);

            url = "https://app.bic-boxtech.org/api/v2.0/container/" + numeroContainer;

            UnityWebRequest apiRequest = UnityWebRequest.Get(url);
            apiRequest.SetRequestHeader("Authorization", accessToken);
            yield return apiRequest.SendWebRequest();

            // Verifica se houve algum erro durante a requisição da API protegida
            if (apiRequest.result == UnityWebRequest.Result.ConnectionError || 
                apiRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Erro na requisição da API protegida: " + apiRequest.error);
                yield break;
            }

            // Exibe a resposta da API protegida
            Debug.Log("Resposta da API protegida: " + apiRequest.downloadHandler.text);
        }
    }

    bool ContainerExiste()
    {
        Container[] containers = FindObjectsOfType<Container>();

        foreach (Container container in containers)
        {
            if (numeroContainer.text == container.NrContainer)
                return true;
        }

        return false;
    }
}

[System.Serializable]
public class AccessTokenResponse
{
    public string accessToken;
}

