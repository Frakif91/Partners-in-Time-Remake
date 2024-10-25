using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // R�f�rence au joueur
    public Vector3 offset; // D�calage entre la cam�ra et le joueur
    public float smoothSpeed = 0.125f; // Vitesse de lissage du mouvement

    // Ajoutez une variable pour le collider de la zone autoris�e (optionnel si vous ne l'utilisez pas)
    public Collider allowedZoneCollider;

    void LateUpdate()
    {
        if (target != null)
        {
            // Position d�sir�e de la cam�ra
            Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);

            // V�rifier si la cam�ra d�passe les limites de la zone autoris�e (optionnel)
            if (allowedZoneCollider != null && !allowedZoneCollider.bounds.Contains(desiredPosition))
            {
                // Si la position d�sir�e est en dehors de la zone autoris�e, arr�ter la cam�ra
                transform.position = transform.position; // Ou bien transform.position = desiredPosition; pour ne pas bouger
            }
            else
            {
                // Lissage du mouvement de la cam�ra
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

                // Appliquer la nouvelle position liss�e
                transform.position = smoothedPosition;

                // Optionnel : faire en sorte que la cam�ra regarde toujours le joueur
                //transform.LookAt(target); // Vous pouvez commenter cette ligne si vous ne voulez pas que la cam�ra regarde le joueur
            }
        }
    }
}