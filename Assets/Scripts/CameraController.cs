using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(cameraTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator cameraTimer() 
    {
        transform.position = new Vector3(-30, 0, -10);

        yield return new WaitForSeconds(2);

        transform.position = new Vector3(0, 0, -10);

        yield return new WaitForSeconds(10);

        transform.position = new Vector3(30, 0, -10);
    
    }
}
