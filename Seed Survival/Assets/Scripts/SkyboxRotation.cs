using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{

    private Material skybox;
    private float rotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        skybox = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        rotation += Time.deltaTime;
        skybox.SetFloat("_Rotation", rotation);
    }
}
