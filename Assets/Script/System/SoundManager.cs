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
            [SerializeField]
            public float Volume = 1.0f;
            [SerializeField]
            public int StartLoop;
            [SerializeField]
            public int LoopLength;
        }

        /// <summary>
        /// SEを鳴らす際のクラス(newして使う)
        /// </summary>
        [System.Serializable]
        public class SEData
        {
            [SerializeField]
            public AudioClip Clip;
            [SerializeField]
            public float Volume = 1.0f;
        }
        public class SoundManager : SingleMonoBehaviour<SoundManager>
        {

            AudioSource _audioSource;

            int _loopStart = 0;
            int _loopLength = 0;

            protected override void OnAwake()
            {
                _audioSource = GetComponent<AudioSource>();
            }

            protected override void ForcedRunSet()
            {
                base.ForcedRunSet();
                _audioSource = gameObject.AddComponent<AudioSource>();
                //_audioSource.loop = true;
            }

            private void Update()
            {
                if (!_audioSource.isPlaying) _audioSource.timeSamples = 0;

                if (_audioSource.timeSamples > _loopLength + _loopStart)
                {
                    _audioSource.timeSamples = _loopStart + (_audioSource.timeSamples - _loopStart - 1) % _loopLength;
                }
            }

            public static void PlayBGM(BGMData data)
            {
                if (!Instance._audioSource || !data.Clip) return;

                Instance._audioSource.clip = data.Clip;
                Instance._audioSource.volume = data.Volume;
                //
                Instance._loopLength = data.LoopLength;
                Instance._loopStart = data.StartLoop;
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