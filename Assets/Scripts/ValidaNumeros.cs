using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValidaNumeros : MonoBehaviour
{
    private TMP_InputField Campo;
    void Start()
    {
        Campo = GetComponent<TMP_InputField>();
        Campo.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(string newValue)
    {
        Campo.text = FormatarTexto(newValue);
    }

    string FormatarTexto(string text)
    {
        string textoFormatado = "";

        // Adicionar letras da entrada, limitando-se a 4 caracteres
        for (int i = 0; i < Mathf.Min(text.Length, 4); i++)
        {
            if (char.IsLetter(text[i]))
            {
                textoFormatado += text[i];
            }
        }

        // Adicionar números da entrada, limitando-se a 7 caracteres
        for (int i = 4; i < Mathf.Min(text.Length, 11); i++)
        {
            if (char.IsDigit(text[i]))
            {
                textoFormatado += text[i];
            }
        }

        return textoFormatado.ToUpper();
    }
}
