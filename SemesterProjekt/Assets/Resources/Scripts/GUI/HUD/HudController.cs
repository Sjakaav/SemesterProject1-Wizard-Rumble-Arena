using System;
using UnityEngine;
using UnityEngine.SceneManagement;


// ReSharper disable once CheckNamespace
namespace GUIManagement.HUD
{
    public class HudController : MonoBehaviour
    {
        
        private bool _gamePaused = false;
        private InputController _inputController;
        public GameObject _menu;

        private void Awake()
        {
            _inputController = new InputController();
        }

        private void Start()
        {
            _inputController.UI.PauseGame.performed += _ => DeterminPause();
        }

        private void OnEnable()
        {
            _inputController.Enable();
        }

        private void OnDisable()
        {
            _inputController.Disable();
        }


        public void DeterminPause()
        {
            (_gamePaused ? (Action)ContinueGame : PauseGame)() ;
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            _gamePaused = true;
            _menu.SetActive(true);
        }

        public void ContinueGame()
        {
            // Debug.Log("FUCKING WORK GOD DAMIT");
            Time.timeScale = 1;
            AudioListener.pause = false;
            _gamePaused = false;
            _menu.SetActive(false);
        }

        public void OnExit()
        {
            Time.timeScale = 1;
            StartCoroutine(GUIManager.Instance.LoadLevel(0));
        }


    }
}