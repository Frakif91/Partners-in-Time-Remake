using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string targetSceneName; // Nom de la scène cible
    public Vector3 targetPosition; // Position de téléportation dans la scène cible

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController.instance.Teleport(targetSceneName, targetPosition);
        }
    }
}