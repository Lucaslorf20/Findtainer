using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadContainer : MonoBehaviour
{
    public static List<Container> containers = new List<Container>();
    [SerializeField] public Container containerPrefab;
    public const string filename = "/container2";
    public const string filenameCount = "/container2_count";

    public void Awake()
    {
        LoadContainer();
    }

    public static void SaveContainer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + filename;
        string countPath = Application.persistentDataPath + filenameCount;

        FileStream countStream = new FileStream(countPath, FileMode.Create);
        formatter.Serialize(countStream, containers.Count);
        countStream.Close();

        for (int i = 0; i < containers.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            ContainerData containerData = new ContainerData(containers[i]);

            formatter.Serialize(stream, containerData);
            Debug.Log("Salvou");
            stream.Close();
        }
    }

    public void LoadContainer()
    {
        string countPath = Application.persistentDataPath + filenameCount;

        if (File.Exists(countPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + filename;
            FileStream countStream = new FileStream(countPath, FileMode.Open);
            int containerCount = (int)formatter.Deserialize(countStream);
            Debug.Log("Num conteiner: " + containerCount);
            countStream.Close();

            for (int i = 0; i < containerCount; i++)
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                ContainerData containerData = formatter.Deserialize(stream) as ContainerData;
                stream.Close();

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
        else
        {
            Debug.Log("Nenhum contêiner registrado.");
        }
    }
}
