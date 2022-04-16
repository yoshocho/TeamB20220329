using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUtility
{
    namespace Sound
    {
        /// <summary>
        /// BGMを鳴らす際のクラス(newして使う)
        /// </summary>
        [System.Serializable]
        public class BGMData
        {
            [SerializeField]
            public AudioClip Clip;
            [SerializeField, Range(0, 2.0f)]
            public float Volume = 1.0f;
        }

        /// <summary>
        /// SEを鳴らす際のクラス(newして使う)
        /// </summary>
        [System.Serializable]
        public class SEData
        {
            [SerializeField]
            public AudioClip Clip;
            [SerializeField, Range(0, 2.0f)]
            public float Volume = 1.0f;
        }
        public class SoundManager : SingleMonoBehaviour<SoundManager>
        {

            AudioSource _audioSource;

            protected override void OnAwake()
            {
                _audioSource = GetComponent<AudioSource>();
            }

            protected override void ForcedRunSet()
            {
                base.ForcedRunSet();
                _audioSource = gameObject.AddComponent<AudioSource>();
            }

            public static void PlayBGM(BGMData data)
            {
                if (!Instance._audioSource || !data.Clip) return;

                Instance._audioSource.clip = data.Clip;
                Instance._audioSource.volume = data.Volume;
                Instance._audioSource.Play();
            }
            public static void PauseBGM()
            {
                if (!Instance._audioSource) return;
                Instance._audioSource.Pause();
            }
            public static void UnPauseBGM()
            {
                if (!Instance._audioSource) return;
                Instance._audioSource.UnPause();
            }
            public static void PlaySE(SEData data)
            {
                if (!Instance._audioSource || !data.Clip) return;
                Instance._audioSource.PlayOneShot(data.Clip, data.Volume);
            }
        }
    }
}