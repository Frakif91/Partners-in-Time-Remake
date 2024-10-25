using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Référence au joueur
    public Vector3 offset; // Décalage entre la caméra et le joueur
    public float smoothSpeed = 0.125f; // Vitesse de lissage du mouvement

    // Ajoutez une variable pour le collider de la zone autorisée (optionnel si vous ne l'utilisez pas)
    public Collider allowedZoneCollider;

    void LateUpdate()
    {
        if (target != null)
        {
            // Position désirée de la caméra
            Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);

            // Vérifier si la caméra dépasse les limites de la zone autorisée (optionnel)
            if (allowedZoneCollider != null && !allowedZoneCollider.bounds.Contains(desiredPosition))
            {
                // Si la position désirée est en dehors de la zone autorisée, arrêter la caméra
                transform.position = transform.position; // Ou bien transform.position = desiredPosition; pour ne pas bouger
            }
            else
            {
                // Lissage du mouvement de la caméra
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

                // Appliquer la nouvelle position lissée
                transform.position = smoothedPosition;

                // Optionnel : faire en sorte que la caméra regarde toujours le joueur
                //transform.LookAt(target); // Vous pouvez commenter cette ligne si vous ne voulez pas que la caméra regarde le joueur
            }
        }
    }
}