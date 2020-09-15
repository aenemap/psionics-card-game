using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcanvas : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PixelHeight => " + camera.pixelHeight.ToString());
        Debug.Log("PixelWidth => " + camera.pixelWidth.ToString());
    }

  
}
