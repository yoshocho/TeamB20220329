using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GoalObj : MonoBehaviour
{
     PlayableDirector _director;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _director.Play();
    }

    private void Start()
    {
        _director = GetComponent<PlayableDirector>();
    }
}
