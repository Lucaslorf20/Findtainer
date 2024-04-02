using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContainerData 
{
    public float[] position = new float[3];
    public string NrContainer;
    public string NrLacre;
    public int CdTipoContainer;

    public ContainerData(Container container)
    {
        position[0] = container.transform.position.x;
        position[1] = container.transform.position.y;
        position[2] = container.transform.position.z;

        NrContainer = container.NrContainer;
        NrLacre = container.NrLacre;
        CdTipoContainer = container.CdTipoContainer;
    }
}
