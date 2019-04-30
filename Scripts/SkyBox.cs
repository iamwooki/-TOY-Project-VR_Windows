using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{
    Material current;
    void Start()
    {
        current = (Material)Resources.Load("Skybox2_1/Skybox2_1");
        RenderSettings.skybox = current;
    }
}
