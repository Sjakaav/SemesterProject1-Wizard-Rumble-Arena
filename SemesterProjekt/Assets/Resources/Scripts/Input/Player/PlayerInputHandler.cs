using System;
using System.Linq;
using GUIManagement.HUD;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;
using static UnityEngine.InputSystem.InputAction;

// ReSharper disable once CheckNamespace
namespace Player
{
    public class PlayerInputHandler : SingletonMono<PlayerInputHandler>
    {
        private PlayerInput _input;
        private MoverBehaviour _moverBehaviour;
        private PlayerController _playerController;
        private HudController _hudController;


        [SerializeField] private GameObject playerPrefab;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();


            var movers = FindObjectsOfType<MoverBehaviour>();
            var playerControllers = FindObjectsOfType<PlayerController>();


            var index = _input.playerIndex;

            _moverBehaviour = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
            _playerController = playerControllers.FirstOrDefault(m => m.GetPlayerIndex() == index);
            _hudController = GetComponent<HudController>();
        }


        public void OnMove(CallbackContext context)
        {
            if (!(_moverBehaviour is null)) _moverBehaviour.SetInputVector(context.ReadValue<Vector2>());
        }

        public void OnJump(CallbackContext context)
        {
            if (!(_moverBehaviour is null)) _moverBehaviour.Jump(context);
        }

        public void OnShoot(CallbackContext context)
        {
            if (context.performed)
            {
                if (!(_playerController is null)) _playerController.Shoot();
            }
        }

        public void OnBlock(CallbackContext context)
        {
            if (context.performed)
            {
                if (!(_playerController is null)) _playerController.Block();
            }
        }
    }
}