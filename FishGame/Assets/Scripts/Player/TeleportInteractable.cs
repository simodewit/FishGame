using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportInteractable : MonoBehaviour
{
    [Tooltip("The gameObject of the player")]
    public GameObject player;
    [Tooltip("The transform of the place you are going to play")]
    public Transform playPosition;
    [Tooltip("The panel of ui you see when teleporting from place to place")]
    public GameObject panel;
    [Tooltip("The model of this interactable")]
    public GameObject model;
    [Tooltip("The speed at wich the fading goes")]
    public float fadingSpeed;

    public void StartTeleport()
    {
        player.transform.position = playPosition.position;
        model.SetActive(false);
    }
}
