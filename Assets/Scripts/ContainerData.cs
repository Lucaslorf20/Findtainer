using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ContainerData 
{
    public float[] position = new float[3];
    public float[] rotation = new float[3];
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

    public ContainerData(Container container)
    {
        position[0] = container.transform.position.x;
        position[1] = container.transform.position.y;
        position[2] = container.transform.position.z;

        rotation[0] = container.transform.localEulerAngles.x;
        rotation[1] = container.transform.localEulerAngles.y;
        rotation[2] = container.transform.localEulerAngles.z;

        NrContainer = container.NrContainer;
        NrLacre = container.NrLacre;
        CdTipoContainer = container.CdTipoContainer;
        NmCliente = container.NmCliente;
        NmArmador = container.NmArmador;
        NrContrato = container.NrContrato;
        QtTara = container.QtTara;
        QtPesoBruto = container.QtPesoBruto;
        QtPesoMaximo = container.QtPesoMaximo;
        NrNotafiscal = container.NrNotafiscal;
        NrReserva = container.NrReserva;
        NrLacreSIF = container.NrLacreSIF;
        NrLacreArmador = container.NrLacreArmador;
        QtTemperatura = container.QtTemperatura;
        DsMercadoria = container.DsMercadoria;
    }
}
