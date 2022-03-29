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
        /// �w�肵��Scene��ς���֐�
        /// </summary>
        /// <param name="name">Scene��</param>
        public static void SceneChange(string name)
        {
            SceneManager.LoadScene(name);
        }
        /// <summary>
        /// �w�肵��Scene��ς���֐�(�񓯊�)
        /// </summary>
        /// <param name="name">Scene��</param>
        public static void SceneChangeAsync(string name)
        {
            SceneManager.LoadSceneAsync(name);
        }
        /// <summary>
        /// �t�F�[�h���Ȃ���Scene��ς���
        /// </summary>
        /// <param name="name">Scene��</param>
        public static void FadeScene(string name)
        {
            FadeSystem.FadeOut(() => SceneChange(name));
        }
        /// <summary>
        /// �t�F�[�h���Ȃ���Scene��ς���
        /// </summary>
        /// <param name="name">Scene��</param>
        /// <param name="fadeTime">�t�G�[�h����</param>
        public static void FadeScene(string name, float fadeTime = 1.0f)
        {
            FadeSystem.FadeOut(fadeTime, () => SceneChange(name));
        }
    }
}