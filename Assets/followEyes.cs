using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followEyes : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject eyes;
    public Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = eyes.transform.position + new Vector3(0f, 0.27985f, 0f) + offset;
      
       

    }
}
