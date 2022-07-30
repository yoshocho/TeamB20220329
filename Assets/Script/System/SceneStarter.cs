using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameUtility.Sound;

namespace GameUtility
{
    public sealed class SceneStarter : MonoBehaviour
    {
        [SerializeField, Header("Scene���J�n�������̏���")]
        UnityEvent _onStart;
        [SerializeField]
        BGMData _bgm;
        void Start()
        {
            SoundManager.PlayBGM(_bgm);
            FadeSystem.FadeIn(() => _onStart?.Invoke());
        }
    }
}