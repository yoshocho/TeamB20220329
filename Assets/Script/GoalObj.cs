using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using GameUtility;
using GameUtility.Sound;

public class GoalObj : MonoBehaviour
{
    [SerializeField]
     BGMData _clearBGM;
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

    public void PlayClearBGM()
    {
        //SoundManager.UnPauseBGM();
        SoundManager.PlayBGM(_clearBGM);
    }

    public void GoTitleScene()
    {
        SceneChanger.FadeScene("TitleScene");
    }
}
