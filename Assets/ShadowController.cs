using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
    public Transform character; // R�f�rence au personnage
    public Vector3 shadowOffset; // D�calage initial de l'ombre

    private CharacterController characterController; // R�f�rence au CharacterController du personnage
    private Vector3 initialShadowPosition; // Position initiale de l'ombre dans le monde

    void Start()
    {
        // Obtenir la r�f�rence au CharacterController du personnage
        characterController = character.GetComponent<CharacterController>();
        // Enregistrer la position initiale de l'ombre dans le monde
        initialShadowPosition = transform.position;
    }

    void Update()
    {
        // Calculer la position de l'ombre en fonction du CharacterController du personnage
        Vector3 newShadowPosition = character.position + shadowOffset;

        // Calculer la hauteur de l'ombre en tenant compte de la pente
        if (Physics.Raycast(character.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            newShadowPosition.y = hit.point.y + initialShadowPosition.y - character.position.y;
        }
        else
        {
            newShadowPosition.y = initialShadowPosition.y; // Fallback si aucun raycast n'est trouv�
        }

        // Mettre � jour la position de l'ombre
        transform.position = newShadowPosition;
    }
}