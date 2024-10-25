using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowMario : MonoBehaviour
{
    public Transform leader; // Le personnage principal
    public float followDistance = 2.0f; // Distance � laquelle le suiveur suit le leader
    public float moveSpeed = 5.0f; // Vitesse de d�placement du suiveur
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
            return; // Sortir si leader n'est pas assign�
        }

        // Calculer la direction et la distance entre le suiveur et le leader
        Vector3 direction = (leader.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, leader.position);

        // V�rifier si le suiveur doit se d�placer
        if (distance > followDistance)
        {
            // D�placer le suiveur vers le leader avec une vitesse constante
            Vector3 targetPosition = leader.position - direction * followDistance;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            // Mettre � jour les param�tres de l'Animator
            float moveHorizontal = direction.x;
            float moveVertical = direction.z;

            // Si une direction est press�e, mettre � jour la derni�re direction
            if (moveHorizontal != 0 || moveVertical != 0)
            {
                lastMoveHorizontal = moveHorizontal;
                lastMoveVertical = moveVertical;
            }

            // Mettre � jour les valeurs de direction et de vitesse pour l'Animator
            animator.SetFloat("Horizontal", -moveHorizontal);
            animator.SetFloat("Vertical", -moveVertical);
            animator.SetFloat("Speed", moveSpeed);
        }
        else
        {
            // Si le suiveur est � la distance souhait�e, d�finir les param�tres de l'Animator � z�ro
            animator.SetFloat("Speed", 0);

            // Conserver les param�tres de direction pour l'animation idle
            if (lastMoveHorizontal != 0 || lastMoveVertical != 0)
            {
                animator.SetFloat("Horizontal", -lastMoveHorizontal);
                animator.SetFloat("Vertical", -lastMoveVertical);
            }
        }
    }
}