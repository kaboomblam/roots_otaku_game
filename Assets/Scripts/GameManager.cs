using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OtakuGameJam.Constants;
using System;

namespace OtakuGameJam
{
    public class GameManager : MonoBehaviour
    {
        public GameState CurrentGameState { get; private set; }

        public static event Action<GameState> OnGameStateChange;

        private GameObject _sceneLevelManager;

        #region Singleton

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _sceneLevelManager = GameObject.FindObjectOfType<LoadingManager>().gameObject;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        #endregion

        private void Start()
        {

        }

        #region Load a Scene

        public void LoadGameScene(SceneIndex index)
        {
            Debug.Log("Loading scene...");
            _sceneLevelManager.GetComponent<LoadingManager>().LoadScene(index);
        }

        #endregion

        #region Game State Management

        public void UpdateGameState(GameState newState)
        {
            CurrentGameState = newState;

            switch (newState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.Game:
                    LoadGameScene(SceneIndex.Game); // TODO: Change to load Game scene
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }

            OnGameStateChange?.Invoke(newState);
        }

        public void StartGame()
        {
            Debug.Log("Starting game...");
            UpdateGameState(GameState.Game);
        }

        public void QuitGame()
        {
            Debug.Log("Quitting game...");
            Application.Quit();
        }

        #endregion
    }
}
