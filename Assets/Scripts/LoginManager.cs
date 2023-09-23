using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    private const string Login = "admin";
    private const string Senha = "findtainer";

    [SerializeField]
    private TMP_InputField usuarioField = null;
    [SerializeField]
    private TMP_InputField senhaField = null;
    [SerializeField]
    private TMP_Text feedback = null;

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
            Logar();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelected++;
            if (InputSelected > 1) InputSelected = 0;
            SelectInputField();
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

    public void UsernameSelected() => InputSelected = 0;
    public void PasswordSelected() => InputSelected = 1;

    public void Logar()
    {
        string usuario = usuarioField.text;
        string senha = senhaField.text;

        if(usuario == Login && senha == Senha)
        {
            feedback.text = "Login realizado\nCarregando...";
            StartCoroutine(CarregaScene());
        }
        else
        {
            feedback.text = "Usuário ou Senha inválido";
        }
    }

    IEnumerator CarregaScene()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel("Patio");
    }
}
