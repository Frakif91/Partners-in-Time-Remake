using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowMario : MonoBehaviour
{
    public Transform leader; // Le personnage principal
    public float followDistance = 2.0f; // Distance à laquelle le suiveur suit le leader
    public float moveSpeed = 5.0f; // Vitesse de déplacement du suiveur
    public float smoothTime = 0.1f; // Temps de lissage pour le mouvement

    private Vector3 currentVelocity = Vector3.zero; // Vitesse actuelle pour le lissage
    private Animator animator;
    private float lastMoveHorizontal;
    private float lastMoveVertical;

    void Start()
    {
        if (leader == null)
        {
            Debug.LogError("Leader is not assigned in the inspector.");
            return;
        }

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (leader == null)
        {
            return; // Sortir si leader n'est pas assigné
        }

        // Calculer la direction et la distance entre le suiveur et le leader
        Vector3 direction = (leader.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, leader.position);

        // Vérifier si le suiveur doit se déplacer
        if (distance > followDistance)
        {
            // Déplacer le suiveur vers le leader avec une vitesse constante
            Vector3 targetPosition = leader.position - direction * followDistance;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            // Mettre à jour les paramètres de l'Animator
            float moveHorizontal = direction.x;
            float moveVertical = direction.z;

            // Si une direction est pressée, mettre à jour la dernière direction
            if (moveHorizontal != 0 || moveVertical != 0)
            {
                lastMoveHorizontal = moveHorizontal;
                lastMoveVertical = moveVertical;
            }

            // Mettre à jour les valeurs de direction et de vitesse pour l'Animator
            animator.SetFloat("Horizontal", -moveHorizontal);
            animator.SetFloat("Vertical", -moveVertical);
            animator.SetFloat("Speed", moveSpeed);
        }
        else
        {
            // Si le suiveur est à la distance souhaitée, définir les paramètres de l'Animator à zéro
            animator.SetFloat("Speed", 0);

            // Conserver les paramètres de direction pour l'animation idle
            if (lastMoveHorizontal != 0 || lastMoveVertical != 0)
            {
                animator.SetFloat("Horizontal", -lastMoveHorizontal);
                animator.SetFloat("Vertical", -lastMoveVertical);
            }
        }
    }
}