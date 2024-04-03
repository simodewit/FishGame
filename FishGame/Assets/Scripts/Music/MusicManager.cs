using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MusicState
{
    inUI,
    inGameplay
}

public class MusicManager : MonoBehaviour
{
    #region variables

    [Header("Main info")]
    [Tooltip("The fist state that it will be in when starting the gameplay")]
    public MusicState state;
    [Tooltip("All the songs played when in UI")]
    public AudioSource[] inUI;
    [Tooltip("All the songs played when in Gameplay")]
    public AudioSource[] inGameplay;

    //privates
    private MusicState lastState;

    #endregion

    #region update

    public void Update()
    {
        CheckStates();
    }

    #endregion

    #region supports

    public void ResetSongs()
    {
        foreach (var song in inUI)
        {
            song.Stop();
        }

        foreach (var song in inGameplay)
        {
            song.Stop();
        }
    }

    #endregion

    #region main manager

    public void CheckStates()
    {
        if (state != lastState)
        {
            lastState = state;
            ResetSongs();
        }

        if (state == MusicState.inUI)
        {
            InUI();
        }
        else
        {
            InGameplay();
        }
    }

    public void InUI()
    {

    }

    public void InGameplay()
    {

    }

    #endregion
}
