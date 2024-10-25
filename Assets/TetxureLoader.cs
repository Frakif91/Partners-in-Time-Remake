using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetxureLoader : MonoBehaviour
{
    public Texture2D textureToLoad; // Variable pour la texture à charger (vous pouvez adapter selon vos besoins)

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && textureToLoad != null)
        {
            renderer.material.mainTexture = textureToLoad;
        }
    }
}
