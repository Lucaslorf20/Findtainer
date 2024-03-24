using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    /*
     public Transform target;
     public float distance = 20.0f;
     public float zoomSpd = 2.0f;
 
     public float xSpeed = 240.0f;
     public float ySpeed = 123.0f;
 
     public int yMinLimit = -723;
     public int yMaxLimit = 877;
 
     private float x = 22.0f;
     private float y = 33.0f;
 
     public void Start () {
        
         x = 22f;
         y = 33f;
 
         // Make the rigid body not change rotation
         if (GetComponent<Rigidbody>())
             GetComponent<Rigidbody>().freezeRotation = true;
     }
 
     public void LateUpdate () {
         if (target) {
             x -= Input.GetAxis("Horizontal") * xSpeed * 0.02f;
             y += Input.GetAxis("Vertical") * ySpeed * 0.02f;
             
             y = ClampAngle(y, yMinLimit, yMaxLimit);
             
         distance -= Input.GetAxis("Fire1") *zoomSpd* 0.02f;
             distance += Input.GetAxis("Fire2") *zoomSpd* 0.02f;
             
             Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
             Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
             
             transform.rotation = rotation;
             transform.position = position;
         }
     }
 
     public static float ClampAngle (float angle, float min, float max) {
         if (angle < -360.0f)
             angle += 360.0f;
         if (angle > 360.0f)
             angle -= 360.0f;
         return Mathf.Clamp (angle, min, max);
     }*/

    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;

    private void Update()
    {
        // Movimento da câmera com as teclas WASD
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Posição Y nunca negativa, isso é, nunca irá passar do chão
        transform.position = new Vector3(transform.position.x, Mathf.Max(transform.position.y, 0f), transform.position.z);

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Rotação apenas nos eixos X e Y
            transform.Rotate(Vector3.up, mouseX * rotationSpeed);
            transform.Rotate(Vector3.left, mouseY * rotationSpeed);

            // Mantém o eixo Z constante
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
        }
    }
}
