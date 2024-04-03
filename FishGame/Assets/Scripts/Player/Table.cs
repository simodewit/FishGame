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
    isPlayingBoss1,
    isPlayingBoss2,
    isPlayingBoss3,
}

public class Table : MonoBehaviour
{
    #region variables

    [Tooltip("This enum decides if the ui should be turned on or off at certain events")]
    public stateOfUI state = stateOfUI.inUi;
    [Tooltip("The script that instantiates the player")]
    public PlayerSpawner spawner;
    [Tooltip("The damage model gameObject")]
    public GameObject damageModel;
    [Tooltip("The Environment generator script")]
    public EnvGenerator generator;
    [Tooltip("The total time for the fading")]
    public float fadingTime;
    [Tooltip("Give the data for each state what should be turned on or off")]
    public InfoUI[] info;

    //privates
    private stateOfUI lastState;
    private ContinuousMoveProviderBase moveComponent;
    private ContinuousTurnProviderBase turnComponent;
    private GameObject player;
    private ColliderSystem colliders;

    #endregion

    #region start and update

    public void Start()
    {
        lastState = state;

        moveComponent = spawner.player.GetComponent<ContinuousMoveProviderBase>();
        turnComponent = spawner.player.GetComponent<ContinuousTurnProviderBase>();
        player = spawner.player;
        colliders = damageModel.GetComponent<ColliderSystem>();
    }

    public void Update()
    {
        ChangeUIState();
    }

    #endregion

    #region support functions

    public void TurnOffAndOn(InfoUI index)
    {
        foreach (GameObject currentGameObject in index.turnOn)
        {
            currentGameObject.SetActive(true);
        }

        foreach (GameObject currentGameObject in index.turnOff)
        {
            currentGameObject.SetActive(false);
        }
    }

    #endregion

    #region state change

    public void ChangeUIState()
    {
        if (lastState == state)
        {
            return;
        }

        lastState = state;
        generator.CheckSpawning();

        foreach (var currentInfo in info)
        {
            if (currentInfo.state != state)
            {
                continue;
            }

            MainCode(currentInfo);
        }
    }

    #endregion

    #region main code

    public void MainCode(InfoUI index)
    {
        //turns gameobjects off or on
        TurnOffAndOn(index);

        //runs the code if the player should be teleported
        if (index.shouldTeleport)
        {
            if (index.shouldFade)
            {
                Fading(index);
            }
            else
            {
                player.transform.position = index.placeToTeleport.position;
            }
        }

        //changes if the player can move or not
        if (!index.canMove)
        {
            moveComponent.enabled = false;
            turnComponent.enabled = false;
        }
        else
        {
            moveComponent.enabled = true;
            turnComponent.enabled = true;
        }

        //conditions for the boss spawn code
        if (index.bossPrefab == null)
        {
            return;
        }

        if (index.spawnPlaceBoss == null)
        {
            return;
        }

        if (!index.spawnBoss)
        {
            return;
        }

        //spawns the boss
        GameObject boss = Instantiate(index.bossPrefab, index.spawnPlaceBoss.position, index.spawnPlaceBoss.rotation);
        Boss bossScript = boss.GetComponentInChildren<Boss>();

        //gives the boss its refrences
        bossScript.attackPlaces = index.bossAttackPlaces;
        bossScript.escapePlace = index.spawnPlaceBoss;
        bossScript.table = this;

        //sets other refrences
        colliders.placeToLook = bossScript.transform;
        bossScript.colliderScript = colliders;
        colliders.boss = bossScript;
    }

    #endregion
     
    #region fading

    public IEnumerator Fading(InfoUI index)
    {
        float beginFading = fadingTime * .5f;
        yield return new WaitForSeconds(beginFading);

        player.transform.position = index.placeToTeleport.position;

        float endFading = fadingTime * .5f;
        yield return new WaitForSeconds(endFading);
    }

    #endregion
}

[Serializable]
public class InfoUI
{
    [Tooltip("The state this is about")]
    public stateOfUI state;
    [Tooltip("Should fade in and out")]
    public bool shouldFade;
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