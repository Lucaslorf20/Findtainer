using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadContainer : MonoBehaviour
{
    public static List<Container> containers;
    [SerializeField] public Container containerPrefab;

    private void Awake()
    {
        containers = new List<Container>();
        LoadContainers();
    }

    public static void SaveContainer()
    {
        string absPath = Application.persistentDataPath + "/containers";
        string containerFilePath = absPath + "/container_";

        Directory.CreateDirectory(absPath);

        BinaryFormatter formatter = new BinaryFormatter();

        for (int i = 0; i < containers.Count; i++)
        {
            FileStream stream = new FileStream(containerFilePath + $"{containers[i].NrContainer}.bin", FileMode.Create);
            ContainerData containerData = new ContainerData(containers[i]);

            formatter.Serialize(stream, containerData);
            Debug.Log("Salvou");
            stream.Close();
        }
    }

    public static List<ContainerData> ReadContainers()
    {
        List<ContainerData> containerDataList = new List<ContainerData>();
        string absPath = Application.persistentDataPath + "/containers";
        Directory.CreateDirectory(absPath);

        string[] containerFilePathList = Directory.GetFiles(absPath);

        if (containerFilePathList.Length > 0)
        {
            Debug.Log("Num conteiner: " + containerFilePathList.Length);
            BinaryFormatter formatter = new BinaryFormatter();

            foreach (string containerFilePath in containerFilePathList)
            {
                if (File.Exists(containerFilePath))
                {
                    FileStream stream = new FileStream(containerFilePath, FileMode.Open);
                    ContainerData containerData = formatter.Deserialize(stream) as ContainerData;
                    containerDataList.Add(containerData);
                    stream.Close();
                }
                else
                {
                    Debug.Log($"{containerFilePath} não encontrado.");
                }
            }
        }
        else
        {
            Debug.Log("Nenhum contêiner registrado.");
        }

        return containerDataList;
    }

    public void LoadContainers()
    {
        List<ContainerData> containerDataList = ReadContainers();

        foreach (ContainerData containerData in containerDataList)
        {
            Vector3 containerPosition = new Vector3(containerData.position[0], containerData.position[1], containerData.position[2]);
            Vector3 containerRotation = new Vector3(containerData.rotation[0], containerData.rotation[1], containerData.rotation[2]);
            Container container = Instantiate(containerPrefab, containerPosition, Quaternion.Euler(containerRotation));
            container.CdTipoContainer = containerData.CdTipoContainer;
            container.NrContainer = containerData.NrContainer;
            container.NrLacre = containerData.NrLacre;
            container.NmCliente = containerData.NmCliente;
            container.NmArmador = containerData.NmArmador;
            container.NrContrato = containerData.NrContrato;
            container.QtTara = containerData.QtTara;
            container.QtPesoBruto = containerData.QtPesoBruto;
            container.QtPesoMaximo = containerData.QtPesoMaximo;
            container.NrNotafiscal = containerData.NrNotafiscal;
            container.NrReserva = containerData.NrReserva;
            container.NrLacreSIF = containerData.NrLacreSIF;
            container.NrLacreArmador = containerData.NrLacreArmador;
            container.QtTemperatura = containerData.QtTemperatura;
            container.DsMercadoria = containerData.DsMercadoria;
            Debug.Log("Instanciou.");
        }
    }

    public static void RemoveContainer(string NrContainer)
    {
        List<ContainerData> containerDataList = ReadContainers();

        foreach(ContainerData containerData in containerDataList)
        {
            if (containerData.NrContainer == NrContainer)
            {
                string absPath = Application.persistentDataPath + "/containers";
                string absContainerFilePath = absPath + $"/container_{NrContainer}.bin";

                File.Delete(absContainerFilePath);
                Debug.Log("Dado do contêiner excluído: " + NrContainer);
            }
        }

    }
}
