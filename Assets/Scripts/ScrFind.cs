using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Battlehub.Utils;
using Battlehub.RTHandles;
using Battlehub.RTCommon;

public class ScrFind : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField searchInput;
    [SerializeField]
    private TMP_Dropdown searchResult;
    [SerializeField]
    private TMP_Text feedback;
    [SerializeField]
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        searchInput.text = "";
        searchInput.Select();
        feedback.text = "";
        searchResult.ClearOptions();
        searchResult.AddOptions(NrContainers());
    }

    List<string> NrContainers()
    {
        List<string> nrContainer = new List<string>();
        
        nrContainer.Add("Container");

        Container[] containers = FindObjectsOfType<Container>();

        foreach (Container container in containers)
        {
            nrContainer.Add(container.NrContainer);
        }

        return nrContainer;
    }

    public void OnTextChanged()
    {
        feedback.text = "";
        searchInput.text = searchInput.text.ToUpper();

        if (searchInput.text.Length >= 3)
        {
            searchResult.ClearOptions();
            List<string> searchResults = new List<string>(); 
            searchResults.Add("Container");

            foreach (string nrContainer in NrContainers())
            {
                if(nrContainer.StartsWith(searchInput.text) && nrContainer != "Container")
                {
                    searchResults.Add(nrContainer);
                    
                }
            }

            searchResult.AddOptions(searchResults);
        }

        if(searchInput.text.Length == 0)
        {
            searchResult.ClearOptions();
            
            searchResult.AddOptions(NrContainers());
        }
    }

    public void setText()
    {
        feedback.text = "";
        string textoDropdown = searchResult.options[searchResult.value].text;
        if (textoDropdown != "Container")
        {
            searchInput.text = textoDropdown;
        }
    }

    public void DoReSearch()
    {
        if (searchInput.text.Length == 0)
        {
            feedback.text = "Digite o número de algum container";
        }
        else if (ContainerExiste())
        {
            gameObject.SetActive(false);
            CriarContainer criarContainer = FindObjectOfType<CriarContainer>();
	    criarContainer.UIHandle.SetActive(true);

            Container[] containers = FindObjectsOfType<Container>();
            GameObject containerGameObject = null;

            foreach (Container container in containers)
            {
                if (searchInput.text == container.NrContainer)
                {
                    containerGameObject = container.gameObject;
		    Debug.Log("entrou");
                    break;
                }
            }

            containerGameObject.GetComponent<Container>().HighlightContainer();

            cam.transform.LookAt(containerGameObject.transform);
        }
        else
        {
            feedback.text = "Container não encontrado";
        }
    }

    bool ContainerExiste()
    {
        Container[] containers = FindObjectsOfType<Container>();

        foreach (Container container in containers)
        {
            if (searchInput.text == container.NrContainer)
                return true;
        }

        return false;
    }

}
