using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPointing : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject cam;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        
    }

    // Update is called once per frame
    void Update()
    {
     
        Quaternion cameraRotation = cam.transform.rotation;

       
        Quaternion xRotation = Quaternion.Euler(cameraRotation.eulerAngles.x, cameraRotation.eulerAngles.y, 0);
       

    
        gameObject.transform.rotation = xRotation;
    }

}
