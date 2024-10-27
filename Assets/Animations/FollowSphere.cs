using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSphere : MonoBehaviour
{
    public Transform sphere; // Référence à la sphère
    public Camera camera2; // Référence à la Camera2
    public float sizeFactor = 1.0f; // Facteur de taille de la caméra par rapport à la sphère

    void Update()
    {
        if (sphere != null && camera2 != null)
        {
            // Suivre la position de la sphère
            transform.position = sphere.position;

            // Ajuster la taille de la caméra en fonction de la taille de la sphère
            float sphereSize = sphere.localScale.x; // Supposons que la sphère est uniformément scalée
            camera2.orthographicSize = sphereSize * sizeFactor; // Utiliser orthographicSize pour une caméra orthographique
            // Si la caméra est perspective, ajuster le champ de vision (field of view)
            // camera2.fieldOfView = sphereSize * sizeFactor; 
        }
    }
}