using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerTimeline : MonoBehaviour
{
    public PlayableDirector timeline; // Assigne la timeline dans l'inspecteur
    public Camera camera1; // Assigne la Camera 1 dans l'inspecteur
    public Camera camera2; // Assigne la Camera 2 dans l'inspecteur

    private LayerMask originalCullingMask1;
    private LayerMask originalCullingMask2;

    private void Start()
    {
        // Sauvegarde les masques de rendu originaux
        originalCullingMask1 = camera1.cullingMask;
        originalCullingMask2 = camera2.cullingMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assure-toi que le personnage a le tag "Player"
        {
            timeline.Play();
            ConfigureCamerasForTimelineStart();
            timeline.stopped += OnTimelineStopped;
        }
    }

    private void ConfigureCamerasForTimelineStart()
    {
        // S'assure que Camera1 voit la map au début
        camera1.cullingMask = originalCullingMask1;

        // S'assure que Camera2 voit toujours la map et le personnage
        camera2.cullingMask |= LayerMask.GetMask("Map");
        camera2.cullingMask |= LayerMask.GetMask("Characters");
        camera2.cullingMask |= LayerMask.GetMask("Mob");
    }

    public void HideMapFromCamera1()
    {
        // Exclut la map de Camera1
        camera1.cullingMask &= ~LayerMask.GetMask("Map");
        camera1.cullingMask |= LayerMask.GetMask("Characters");
        camera1.cullingMask |= LayerMask.GetMask("Mob");
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        // Réinitialise les masques de rendu des caméras
        camera1.cullingMask = originalCullingMask1;
        camera2.cullingMask = originalCullingMask2;

        // Désabonne de l'événement
        timeline.stopped -= OnTimelineStopped;
    }
}