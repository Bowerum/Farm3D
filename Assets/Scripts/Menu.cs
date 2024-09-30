using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _mess;
    [SerializeField] private GameObject _but;

    private bool _menuOpen = false;

    private PlayerInputActions _inputActions;
    private void Start()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.OpenMenu.performed += OpenMenu;
    }

    private void OpenMenu(InputAction.CallbackContext obj)
    {
        _mess.SetActive(false);
        _but.SetActive(false);

        if (!_menuOpen)
        {
            _menuOpen = true;
            _menu.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            _menuOpen = false;
            _menu.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
