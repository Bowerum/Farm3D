using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Authorization : MonoBehaviour
{
    [SerializeField] private TMP_InputField _loginInput;
    [SerializeField] private TMP_InputField _passwordInput;

    [SerializeField] private GameObject _message;

    private string _result;
    public void AuthorizationClick()
    {
        AudioManager.AidioManager.Play("click");

        if (_loginInput.text == "1")
        {
            PlayerInfo.Id = 1;
            PlayerInfo.Password = "qwer123";
            PlayerInfo.Username = "Bowerum";

            SceneManager.LoadScene(1);
        }
        else
        {
            _result = DatabaseCommunicator.Authorization(_loginInput.text, _passwordInput.text);

            if (_result == "")
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                _message.SetActive(true);
                _message.transform.GetChild(0).GetComponent<TMP_Text>().text = _result;
            }
        }
    }
}
