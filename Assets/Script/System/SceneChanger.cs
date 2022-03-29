using UnityEngine.SceneManagement;

namespace GameUtility
{
    public sealed class SceneChanger : SingleMonoBehaviour<SceneChanger>
    {

        protected override void OnAwake()
        {
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 指定したSceneを変える関数
        /// </summary>
        /// <param name="name">Scene名</param>
        public static void SceneChange(string name)
        {
            SceneManager.LoadScene(name);
        }
        /// <summary>
        /// 指定したSceneを変える関数(非同期)
        /// </summary>
        /// <param name="name">Scene名</param>
        public static void SceneChangeAsync(string name)
        {
            SceneManager.LoadSceneAsync(name);
        }
        /// <summary>
        /// フェードしながらSceneを変える
        /// </summary>
        /// <param name="name">Scene名</param>
        public static void FadeScene(string name)
        {
            FadeSystem.FadeOut(() => SceneChange(name));
        }
        /// <summary>
        /// フェードしながらSceneを変える
        /// </summary>
        /// <param name="name">Scene名</param>
        /// <param name="fadeTime">フエード時間</param>
        public static void FadeScene(string name, float fadeTime = 1.0f)
        {
            FadeSystem.FadeOut(fadeTime, () => SceneChange(name));
        }
    }
}