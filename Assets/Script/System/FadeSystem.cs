using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameUtility
{
    public sealed class FadeSystem : SingleMonoBehaviour<FadeSystem>
    {
        protected override void ForcedRunSet()
        {
            base.ForcedRunSet();

            var canvasObj = new GameObject("Canvas");
            canvasObj.transform.SetParent(this.transform);

            var canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 10;
            canvasObj.AddComponent<GraphicRaycaster>();
            canvasObj.AddComponent<CanvasScaler>();

            var imageObj = new GameObject("FadeImage");
            imageObj.transform.SetParent(canvasObj.transform);
            var rectTrans = imageObj.AddComponent<RectTransform>();
            rectTrans.anchorMin = Vector3.zero;
            rectTrans.anchorMax = Vector3.one;
            rectTrans.offsetMin = Vector3.zero;
            rectTrans.offsetMax = Vector3.zero;

            Instance._fadeImage = imageObj.AddComponent<Image>();
            Instance._fadeImage.color = Color.black;
            Instance._canvasGroup = imageObj.AddComponent<CanvasGroup>();
            DontDestroyOnLoad(gameObject);
        }

        public const float DefaultFadeTime = 1.0f;

        [SerializeField]
        CanvasGroup _canvasGroup = null;
        [SerializeField]
        Image _fadeImage = null;

        float _startValue = 0.0f;
        float _endValue = 0.0f;
        float _elapedTime = 0.0f;
        float _fadeTime = 0.0f;
        Action _onEndFade;
        bool _isFade = false;

        public static bool IsFade => Instance._isFade;
        private void Update()
        {
            if (!_isFade) return;

            _elapedTime += Time.deltaTime;
            var rate = _elapedTime / _fadeTime;

            _canvasGroup.alpha = Mathf.Lerp(_startValue, _endValue, rate);

            if (rate >= 1.0f)
            {
                _onEndFade?.Invoke();
                _onEndFade = null;
                _isFade = false;
                _canvasGroup.blocksRaycasts = false;
            }
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            if (!_canvasGroup) _canvasGroup = GetComponentInChildren<CanvasGroup>();
            if (!_fadeImage) _fadeImage = GetComponentInChildren<Image>();
            DontDestroyOnLoad(gameObject);
        }

        private void StartFade(float startValue, float endValue, float fadeTime, Action onFadeEnd)
        {
            if (_isFade) return;
            _fadeImage.gameObject.SetActive(true);
            _fadeTime = fadeTime;
            _canvasGroup.alpha = startValue;
            _canvasGroup.blocksRaycasts = true;
            _startValue = startValue;
            _endValue = endValue;
            _elapedTime = 0.0f;
            _onEndFade = onFadeEnd;

            _isFade = true;
        }
        /// <summary>
        /// フェードインする関数
        /// </summary>
        /// <param name="onFadeEnd">フェードが終わった後の処理</param>
        public static void FadeIn(Action onFadeEnd = null)
        {
            Instance.StartFade(1.0f, 0.0f, DefaultFadeTime, onFadeEnd);
        }
        /// <summary>
        /// 指定した時間分フェードインする関数
        /// </summary>
        /// <param name="fadeTime">フェードに掛ける時間</param>
        /// <param name="onFadeEnd">フェードが終わった後の処理</param>
        public static void FadeIn(float fadeTime = DefaultFadeTime, Action onFadeEnd = null)
        {
            Instance.StartFade(1.0f, 0.0f, fadeTime, onFadeEnd);
        }
        /// <summary>
        /// フェードアウトする関数
        /// </summary>
        /// <param name="onFadeEnd">フェードが終わった後の処理</param>
        public static void FadeOut(Action onFadeEnd)
        {
            Instance.StartFade(0.0f, 1.0f, DefaultFadeTime, onFadeEnd);
        }
        /// <summary>
        /// 指定した時間分フェードアウトする関数
        /// </summary>
        /// <param name="fadeTime">フェードに掛ける時間</param>
        /// <param name="onFadeEnd">フェードが終わった後の処理</param>
        public static void FadeOut(float fadeTime = DefaultFadeTime, Action onFadeEnd = null)
        {
            Instance.StartFade(0.0f, 1.0f, fadeTime, onFadeEnd);
        }
    }
}