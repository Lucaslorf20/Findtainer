using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limitador : MonoBehaviour
{
    private float minX = -52f;
    private float maxX = 52f;
    private float minZ = -52f;
    private float maxZ = 52f;
    private float minY = 0;
    public float maxY = 30f;

    void LateUpdate()
    {
        Vector3 newPosition = transform.position;

        // Limita o movimento da câmera no eixo X
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Limita o movimento da câmera no eixo Y
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Limita o movimento da câmera no eixo Z
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // Atualiza a posição da câmera
        transform.position = newPosition;
    }
}
