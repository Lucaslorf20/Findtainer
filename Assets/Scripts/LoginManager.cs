using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoginManager : MonoBehaviour
{
    private const string Login = "admin";
    private const string Senha = "findtainer";

    bool estaLogando = true;

    [Header("Login")]
    [SerializeField]
    private GameObject telaLogin;
    [SerializeField]
    private TMP_InputField usuarioField = null;
    [SerializeField]
    private TMP_InputField senhaField = null;
    [SerializeField]
    private TMP_Text feedback = null;
    [SerializeField]
    private TMP_Text textoBotao = null;
    [SerializeField]
    private GameObject loading = null;

    [Header("Cadastro")]
    [SerializeField]
    private GameObject telaCadastro;
    [SerializeField]
    private TMP_InputField emailField;
    [SerializeField]
    private TMP_InputField cadSenhaField;
    [SerializeField]
    private TMP_InputField cadSenhaConfirmField;
    [SerializeField]
    private TMP_Text cadFeedback;

    private int InputSelected = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectInputField();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (telaLogin.activeSelf)
                Logar();
            else
                Cadastrar();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelected++;
            if (telaLogin.activeSelf)
            {
                if (InputSelected > 1) InputSelected = 0;
                SelectInputField();
            }
            else
            {
                if (InputSelected > 2) InputSelected = 0;
                CadSelectInputField();
            }
        }
    }

    void SelectInputField()
    {
        switch (InputSelected)
        {
            case 0: usuarioField.Select();
            break;
            case 1: senhaField.Select();
            break;
        }
    }

    void CadSelectInputField()
    {
        switch (InputSelected)
        {
            case 0:
                emailField.Select();
                break;
            case 1:
                cadSenhaField.Select();
                break;
            case 2:
                cadSenhaConfirmField.Select();
                break;
        }
    }

    public void UsernameSelected() => InputSelected = 0;
    public void PasswordSelected() => InputSelected = 1;

    public void EmailSelected() => InputSelected = 0;
    public void CadSenhaSelected() => InputSelected = 1;
    public void CadSenhaConfirmSelected() => InputSelected = 2;


    public void Logar()
    {
        textoBotao.text = "";
        loading.SetActive(true);

        string usuario = usuarioField.text;
        string senha = senhaField.text;
        string path = Application.persistentDataPath + $"/{usuario}/data.bin";

        if(usuario == Login && senha == Senha)
        {
            feedback.text = "Login realizado";
            StartCoroutine(CarregaScene());
        }
        else if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            UserData userData = formatter.Deserialize(stream) as UserData;
            stream.Close();

            if (senha == userData.Password)
            {
                feedback.text = "Login realizado";
                StartCoroutine(CarregaScene());
            }
            else
            {
                feedback.text = "Usuário ou Senha inválido";
                loading.SetActive(false);
                textoBotao.text = "Entrar";
            }
        }
        else
        {
            feedback.text = "Usuário ou Senha inválido";
            loading.SetActive(false);
            textoBotao.text = "Entrar";
        }

    }

    public void Cadastrar()
    {
        string email = emailField.text;
        string senha = cadSenhaField.text;
        string senhaConfirm = cadSenhaConfirmField.text;

        if (!validEmail(email))
        {
            cadFeedback.text = "Digite um email válido.";
        }
        else if (senha != senhaConfirm)
        {
            cadFeedback.text = "As senhas não coincidem.";
        }
        else if(!strongPassword(senha))
        {
            cadFeedback.text = "Digite uma senha com pelo menos 8 caracteres, pelo menos 1 letra maiúscula, pelo menos 1 letra minúscula e pelo menos um caractere especial.";
        }
        else
        {
            string absPath = Application.persistentDataPath + $"/{email}";
            string path = absPath + "/data.bin";
            Directory.CreateDirectory(absPath);

            if (File.Exists(path))
            {
                cadFeedback.text = "Usuário já cadastrado.";
            }
            else
            {
                UserData userData = new UserData(email, senha);

                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Create);
                formatter.Serialize(stream, userData);
                Debug.Log("Usuário cadastrado com sucesso.");
                stream.Close();
                cadFeedback.text = "Usuário cadastrado com sucesso.";

                emailField.text = "";
                cadSenhaField.text = "";
                cadSenhaConfirmField.text = "";

                InputSelected = 0;
                CadSelectInputField();
            }

        }
    }

    public void MudarTelas()
    {
        estaLogando = !estaLogando;

        telaLogin.SetActive(estaLogando);
        telaCadastro.SetActive(!estaLogando);
        InputSelected = 0;
        if (estaLogando)
        {
            usuarioField.text = "";
            senhaField.text = "";
            feedback.text = "";
            SelectInputField();
        }
        else
        {
            emailField.text = "";
            cadSenhaField.text = "";
            cadSenhaConfirmField.text = "";
            cadFeedback.text = "";
            CadSelectInputField();
        }

        

    }

    private bool validEmail (string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(email);
    }

    private bool strongPassword(string password)
    {
        if (password.Length < 8)
            return false;

        bool hasUpperCase = false;
        bool hasLowerCase = false;
        bool hasDigit = false;
        bool hasSpecialChar = false;

        foreach(char c in password)
        {
            if (char.IsUpper(c))
                hasUpperCase = true;
            if (char.IsLower(c))
                hasLowerCase = true;
            if (char.IsDigit(c))
                hasDigit = true;
            if (!char.IsLetterOrDigit(c))
                hasSpecialChar = true;
        }

        return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
    }

    IEnumerator CarregaScene()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel("Patio");
        //loading.SetActive(false);
        //textoBotao.text = "Entrar";
    }
}

[System.Serializable]
public class UserData
{
    private string email;
    private string password;

    public UserData(string email, string password)
    {
        this.email = email;
        this.password = password;
    }

    public string Email
    {
        get { return email; }
    }

    public string Password
    {
        get { return password;  }
    }
}
