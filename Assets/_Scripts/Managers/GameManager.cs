using System;
using PVZ.Sound;
using UnityEngine;

namespace PVZ.Managers
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private bool isGameAlive = false;

        [SerializeField] public GameObject cardUIPanel = null;

        public event Action OnGameStart;
        void Start()
        {
            // cardUIPanel.SetActive(false);
        }

        public void GameStart()
        {
            isGameAlive = true;
            cardUIPanel.SetActive(true);
            Debug.Log("游戏开始");
            OnGameStart?.Invoke();
            SoundPlayer.Instance.PlayBGM(GlobalPath.BGM1);
            SoundPlayer.Instance.PlaySound(GlobalPath.S_awooga);
            Time.timeScale = 2;





        }

        // public void GameOver()
        // {
        //     isGameAlive = true;
        //     cardUIPanel.SetActive(true);
        //     Debug.Log("游戏开始");
        //     OnGameStart?.Invoke();

        // }
    }
}


