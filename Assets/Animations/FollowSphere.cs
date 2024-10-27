using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSphere : MonoBehaviour
{
    public Transform sphere; // R�f�rence � la sph�re
    public Camera camera2; // R�f�rence � la Camera2
    public float sizeFactor = 1.0f; // Facteur de taille de la cam�ra par rapport � la sph�re

    void Update()
    {
        if (sphere != null && camera2 != null)
        {
            // Suivre la position de la sph�re
            transform.position = sphere.position;

            // Ajuster la taille de la cam�ra en fonction de la taille de la sph�re
            float sphereSize = sphere.localScale.x; // Supposons que la sph�re est uniform�ment scal�e
            camera2.orthographicSize = sphereSize * sizeFactor; // Utiliser orthographicSize pour une cam�ra orthographique
            // Si la cam�ra est perspective, ajuster le champ de vision (field of view)
            // camera2.fieldOfView = sphereSize * sizeFactor; 
        }
    }
}