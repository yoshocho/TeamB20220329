using UnityEngine;

namespace GameUtility
{
    public class GameManager
    {
        private static GameManager s_instance = new GameManager();
        public static GameManager Instance => s_instance ??= new GameManager();
        private GameManager() { }

        public enum GameState
        {
            Title,
            InGame,
            GameClear,
        }

        public void SetGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Title:
                    Debug.Log("Title");

                    break;
                case GameState.InGame:
                    Debug.Log("InGame");

                    break;
                case GameState.GameClear:
                    Debug.Log("GameClear");
                    SceneChanger.FadeScene("TitleScene", 2.0f);
                    break;
                default:
                    break;
            }
        }
    }
}