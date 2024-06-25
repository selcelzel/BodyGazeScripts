using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public string sceneName; // Name of the scene you want to switch to

    void Start()
    {
        // Get the IntroTimeline and subscribe to its "IntroFinished" signal
        PlayableDirector director = GetComponent<PlayableDirector>();
        director.stopped += OnIntroFinished;
    }

    public void OnIntroFinished(PlayableDirector director)
    {
        // Load the next scene when the intro timeline finishes
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
