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
    private AudioSource currentSong;
    private int inUIIndex;
    private int inGameplayIndex;

    #endregion

    #region start and update

    public void Start()
    {
        if (state == MusicState.inUI)
        {
            inUI[inUIIndex].Play();
            currentSong = inUI[inUIIndex];
        }
        else
        {
            inGameplay[inGameplayIndex].Play();
            currentSong = inGameplay[inGameplayIndex];
        }
    }

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
        if (currentSong.isPlaying)
        {
            return;
        }

        inUIIndex += 1;

        if (inUIIndex >= inUI.Length)
        {
            inUIIndex = 0;
        }

        inUI[inUIIndex].Play();
        currentSong = inUI[inUIIndex];
    }

    public void InGameplay()
    {
        if (currentSong.isPlaying)
        {
            return;
        }

        inGameplayIndex += 1;

        if (inGameplayIndex >= inGameplay.Length)
        {
            inGameplayIndex = 0;
        }

        inGameplay[inGameplayIndex].Play();
        currentSong = inGameplay[inGameplayIndex];
    }

    #endregion
}
