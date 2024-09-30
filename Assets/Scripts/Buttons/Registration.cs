using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Mail;
using System.Net.Mime;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

public class Registration : MonoBehaviour
{
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _loginInput;
    [SerializeField] private TMP_InputField _passwordInput;

    [SerializeField] private GameObject _message;
    [SerializeField] private GameObject _registrationUI;
    [SerializeField] private GameObject _codeUI;


    public static int Code = 0;

    public static bool Exist;

    public static bool UncorrectEmail = false;

    public static bool OpenCode = false;
    public void Update()
    {
        if(OpenCode)
        {
            _codeUI.SetActive(true);
            _registrationUI.SetActive(false);
        }
    }
    public void SendEmail()
    {
        AudioManager.AidioManager.Play("click");

        Exist = false;

        _message.SetActive(false);

        PlayerInfo.Email = _emailInput.text;
        PlayerInfo.Username = _loginInput.text;
        PlayerInfo.Password = _passwordInput.text;

        if(_emailInput.text == "" || _loginInput.text == "" || _passwordInput.text == "")
        {
            _message.SetActive(true);
            _message.transform.GetChild(0).GetComponent<TMP_Text>().text = "Введите данные";
        }
        if (!DatabaseCommunicator.CheckUser())
        {
            StartTask(); 
        } 
        else
        {
            Exist = true;
            _message.SetActive(true);
            _message.transform.GetChild(0).GetComponent<TMP_Text>().text = "Пользователь с таким логином уже существует";
        }
    }

    private void StartTask()
    {
        Task.Run(() => RegistrationPlayer());
        UncorrectEmail = true;
    }

    public void RegistrationPlayer()
    {
        string emailPlayer = _emailInput.text;

        System.Random rnd = new System.Random();
        Code = rnd.Next(1000, 9999);

        MailAddress from = new MailAddress("farm3d@mail.ru", "Разработчик");
        MailAddress to = new MailAddress(emailPlayer, "Пользователь");

        var mail = new MailMessage(from, to);

        mail.Subject = "Регистрация в игре Farm3D";
        mail.Body = $"Код подтверждения для регистрации в приложении: {Code}";

        var client = new SmtpClient("smtp.mail.ru", 587)
        {
            Credentials = new NetworkCredential("farm3d@mail.ru", "dzw7VnbrugYJEP6GhKaC"),
            EnableSsl = true
        };
        try
        {
            client.Send(mail);
            OpenCode = true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
    

