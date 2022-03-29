using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameUtility
{
    public sealed class SceneStarter : MonoBehaviour
    {
        [SerializeField]
        [Header("Scene‚ªŠJŽn‚µ‚½Žž‚Ìˆ—")]
        UnityEvent _onStart;
        void Start()
        {
            FadeSystem.FadeIn(() => _onStart?.Invoke());
        }
    }
}