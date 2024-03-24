using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public RectTransform rect;
    public float timeStep;
    public float oneStepAngle;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime >= timeStep)
        {
            Vector3 angle = rect.localEulerAngles;
            angle.z += oneStepAngle;

            rect.localEulerAngles = angle;

            startTime = Time.time;
        }
    }
}
