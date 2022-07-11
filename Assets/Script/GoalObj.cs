using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using GameUtility;

public class GoalObj : MonoBehaviour
{
     PlayableDirector _director;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        _director.Play();
        GameManager.Instance.SetGameState(GameManager.GameState.GameClear);
    }

    private void Start()
    {
        _director = GetComponent<PlayableDirector>();
    }
}
