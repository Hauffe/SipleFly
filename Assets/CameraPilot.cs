using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPilot : MonoBehaviour
{
    public float vector3Fw = 1.8f;
    public float vector3Up = 0.9f;
    public float bias = 0.96f;
    public float CamPos = 1.0f;
    public float CamLookAt = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 moveCamTo = transform.position - transform.forward * vector3Fw + Vector3.up * vector3Up;

        Camera.main.transform.position = Camera.main.transform.position * bias + 
                                         moveCamTo * (CamPos-bias);

        Camera.main.transform.LookAt(transform.position + transform.forward * CamLookAt);
    }
}
