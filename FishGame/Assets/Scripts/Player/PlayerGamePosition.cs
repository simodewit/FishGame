using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerGamePosition : MonoBehaviour
{
    [Tooltip("The script that spawned the player")]
    public PlayerSpawner Spawner;
    [Tooltip("The model of the object you have to collide with to start the lerping")]
    public GameObject collideObject;
    [Tooltip("The speed the player lerps with to the position")]
    public float lerpSpeed = 3;
    [Tooltip("The minimum distance to the final point before stopping with lerp")]
    public float maxLerp = .1f;
    [Tooltip("The boss that you will be fighting")]
    public GameObject boss;

    private GameObject player;
    private bool beginLerp;
    private Vector3 startPos;

    public void Start()
    {
        //player = Spawner.player;
    }

    public void FixedUpdate()
    {
        ChangePosition();
    }

    public void EnableLerp()
    {
        player.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
        player.GetComponent<ActionBasedContinuousTurnProvider>().enabled = false;

        startPos = player.transform.position;
        beginLerp = true;
        collideObject.SetActive(false);
    }

    public void ChangePosition()
    {
        if (!beginLerp)
        {
            return;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance > maxLerp)
        {
            player.transform.position = Vector3.Lerp(startPos, transform.position, lerpSpeed);
        }
        else
        {
            print("completed lerp");
            beginLerp = false;
            boss.SetActive(true);
        }
    }
}
