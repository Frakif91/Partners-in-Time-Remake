using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravityForce = 20f;
    public float slopeSpeedReduction = 0.5f;
    public float jumpForce = 10f;
    public float fastFallMultiplier = 2.5f;
    public bool isGrounded = true;
    public AnimatorOverrideController jumpOverrideController;
    public float jumpSpeedMultiplier = 1.5f; // Ajoutez cette ligne pour le multiplicateur de vitesse de saut

    private Rigidbody rb;
    private bool onSlope = false;
    private Animator animator;
    private float lastMoveHorizontal;
    private float lastMoveVertical;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Récupérer les entrées utilisateur
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Inverser les valeurs pour le déplacement
        Vector3 direction = new Vector3(moveHorizontal * -1, 0.0f, moveVertical * -1).normalized;

        // Appliquer la vitesse de déplacement
        rb.velocity = direction * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // Si une direction est pressée, mettre à jour la dernière direction
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            lastMoveHorizontal = moveHorizontal;
            lastMoveVertical = moveVertical;
        }

        // Mettre à jour les paramètres de l'Animator
        animator.SetFloat("Horizontal", moveHorizontal);
        animator.SetFloat("Vertical", moveVertical);
        animator.SetFloat("Speed", direction.magnitude);

        // Si aucune touche n'est pressée, conserver l'animation idle dans la dernière direction
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            if (lastMoveHorizontal != 0 && lastMoveVertical != 0)
            {
                animator.SetFloat("Horizontal", lastMoveHorizontal);
                animator.SetFloat("Vertical", lastMoveVertical);
            }
            else
            {
                animator.SetFloat("Horizontal", lastMoveHorizontal);
                animator.SetFloat("Vertical", lastMoveVertical);
            }
        }

        // Récupérer l'entrée utilisateur pour sauter
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Appliquer la force de saut ajustée en fonction de la vitesse
            float adjustedJumpForce = jumpForce + (rb.velocity.magnitude * jumpSpeedMultiplier);
            rb.AddForce(Vector3.up * adjustedJumpForce, ForceMode.Impulse);

            // Déclencher l'animation de saut
            animator.SetBool("isJumping", true);
            animator.SetTrigger("Jump");

            // Marquer comme étant en l'air
            isGrounded = false;
        }

        // Appliquer une gravité supplémentaire lorsque le personnage descend
        if (rb.velocity.y < 0 && !isGrounded)
        {
            animator.SetBool("isFalling", true);
        }
    }

    void FixedUpdate()
    {
        // Appliquer une force constante vers le bas pour simuler l'attraction par le sol
        rb.AddForce(Vector3.down * gravityForce, ForceMode.Force);

        // Réduction de la vitesse sur pente
        if (onSlope && rb.velocity.y > 0)
        {
            rb.velocity *= slopeSpeedReduction;
        }

        // Appliquer une gravité supplémentaire lorsque le personnage descend
        if (rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.down * gravityForce * (fastFallMultiplier - 1), ForceMode.Force);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) > 0 && Vector3.Angle(contact.normal, Vector3.up) < 90)
            {
                onSlope = true;
                return;
            }
        }
        onSlope = false;
    }

    void OnCollisionExit(Collision collision)
    {
        onSlope = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isLanding", true);

            // Désactiver l'état isLanding après un court délai
            StartCoroutine(ResetLandingState());
        }
    }

    // Coroutine pour réinitialiser l'état d'atterrissage après un court délai
    private IEnumerator ResetLandingState()
    {
        yield return new WaitForSeconds(0.1f); // Ajustez la durée si nécessaire
        animator.SetBool("isLanding", false);
    }

    // Méthode appelée à la fin de l'animation "Land"
    public void OnLandAnimationEnd()
    {
        animator.SetBool("isLanding", false);
        UpdateAnimatorState();
    }

    // Méthode pour mettre à jour l'état de l'animation après l'atterrissage
    void UpdateAnimatorState()
    {
        if (isGrounded)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            if (moveHorizontal != 0 || moveVertical != 0)
            {
                animator.SetFloat("Horizontal", moveHorizontal);
                animator.SetFloat("Vertical", moveVertical);
                animator.SetFloat("Speed", new Vector3(moveHorizontal, 0, moveVertical).magnitude);
            }
            else
            {
                animator.SetFloat("Horizontal", lastMoveHorizontal);
                animator.SetFloat("Vertical", lastMoveVertical);
                animator.SetFloat("Speed", 0);
            }
        }
    }
}