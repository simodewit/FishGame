using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum stateOfUI
{
    inUi,
    boss1Selected,
    boss2Selected,
    boss3Selected,
    isPlaying
}

public class Table : MonoBehaviour
{
    #region variables

    [Tooltip("This enum decides if the ui should be turned on or off at certain events")]
    public stateOfUI state = stateOfUI.inUi;
    [Tooltip("The script that instantiates the player")]
    public PlayerSpawner spawner;
    [Tooltip("Give the data for each state what should be turned on or off")]
    public InfoUI[] info;

    //privates
    private stateOfUI lastState;
    private ContinuousMoveProviderBase moveComponent;
    private ContinuousTurnProviderBase turnComponent;
    private GameObject player;

    #endregion

    #region start and update

    public void Start()
    {
        lastState = state;

        moveComponent = spawner.player.GetComponent<ContinuousMoveProviderBase>();
        turnComponent = spawner.player.GetComponent<ContinuousTurnProviderBase>();
        player = spawner.player;
    }

    public void Update()
    {
        ChangeUIState();
    }

    #endregion

    #region UI

    public void ChangeUIState()
    {
        if (lastState == state)
        {
            return;
        }

        lastState = state;

        foreach (var i in info)
        {
            if (i.state != state)
            {
                continue;
            }

            foreach (GameObject g in i.turnOn)
            {
                g.SetActive(true);
            }

            foreach (GameObject g in i.turnOff)
            {
                g.SetActive(false);
            }

            if (i.shouldTeleport)
            {
                player.transform.position = i.placeToTeleport.position;
            }

            if (!i.canMove)
            {
                moveComponent.enabled = false;
                turnComponent.enabled = false;
            }
            else
            {
                moveComponent.enabled = true;
                turnComponent.enabled = true;
            }

            if (i.bossPrefab != null && i.spawnPlaceBoss != null && i.spawnBoss)
            {
                GameObject boss = Instantiate(i.bossPrefab, i.spawnPlaceBoss.position, i.spawnPlaceBoss.rotation);
                boss.GetComponent<Boss>().attackPlaces = i.bossAttackPlaces;
            }

            return;
        }
    }

    #endregion
}

[Serializable]
public class InfoUI
{
    [Tooltip("The state this is about")]
    public stateOfUI state;
    [Tooltip("The things that should be turned on in this state")]
    public GameObject[] turnOn;
    [Tooltip("The things that should be turned off in this state")]
    public GameObject[] turnOff;
    [Tooltip("Teleport when interacting with this state")]
    public bool shouldTeleport;
    [Tooltip("The place that the player should be teleported to")]
    public Transform placeToTeleport;
    [Tooltip("Decides if the player is able to when entering this state")]
    public bool canMove;
    [Tooltip("Decides if a boss has to be placed when entering this state")]
    public bool spawnBoss;
    [Tooltip("The boss that it has to spawn")]
    public GameObject bossPrefab;
    [Tooltip("The place where the boss should be placed")]
    public Transform spawnPlaceBoss;
    [Tooltip("The attackPlaces of the boss")]
    public Transform[] bossAttackPlaces;
}