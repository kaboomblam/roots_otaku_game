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

        // Singleton pattern for GameManager
        // ---
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            UpdateGameState(GameState.MainMenu);
        }

        public void UpdateGameState(GameState newState)
        {
            CurrentGameState = newState;

            // TODO: Add logic to handle game state changes

            OnGameStateChange?.Invoke(newState);
        }
    }
}
