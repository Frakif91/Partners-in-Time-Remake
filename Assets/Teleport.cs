using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string targetSceneName; // Nom de la sc�ne cible
    public Vector3 targetPosition; // Position de t�l�portation dans la sc�ne cible

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController.instance.Teleport(targetSceneName, targetPosition);
        }
    }
}