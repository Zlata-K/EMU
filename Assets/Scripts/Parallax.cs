 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform camera;
    public Vector3 lastCamPos;
    private float multiplier = 0.7f; 
    
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").transform;
        lastCamPos = camera.position;

        if (gameObject.name == "Sky")
        {
            transform.position = new Vector3(camera.position.x, camera.position.y, 396.356f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = camera.position - lastCamPos;
        if (this.gameObject.name == "Clouds")
        {
            transform.position += delta * multiplier;
        }
        else
        {
            transform.position += delta;
        }
        lastCamPos = camera.position;
    }
}
