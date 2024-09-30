using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsUI : MonoBehaviour
{
    public void CheckCode(TMP_InputField inputField)
    {
        AudioManager.AidioManager.Play("click");
        if (Registration.Code == int.Parse(inputField.text))
        {     
            DatabaseCommunicator.Registration();
            PlayerInfo.Id = DatabaseCommunicator.SelectUserId();
            SceneManager.LoadScene(1);
        }
    }
    public void TurnOnGameobject(GameObject gameObject)
    {
        AudioManager.AidioManager.Play("click");
        gameObject.SetActive(true);
    }
    public void TurnOffGameobject(GameObject gameObject)
    {
        AudioManager.AidioManager.Play("click");
        gameObject.SetActive(false);
    }
    public void TurnOffOpenCode()
    {
        AudioManager.AidioManager.Play("click");
        Registration.OpenCode = false;
    }
    public void NewGame()
    {
        AudioManager.AidioManager.Play("click");
        SceneManager.LoadScene(2);
    }
    public void TurnOnMessage(GameObject gameObject)
    {
        AudioManager.AidioManager.Play("click");
        if (Registration.UncorrectEmail)
            StartCoroutine(Waiting(gameObject));
    }
    public void TurnOnMessageCode(GameObject gameObject)
    {
        AudioManager.AidioManager.Play("click");
        StartCoroutine(Waiting(gameObject));
    }
    private IEnumerator Waiting(GameObject gameObject)
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(true);
    }
    public void ExitClick()
    {
        AudioManager.AidioManager.Play("click");
        Application.Quit();
    }
    public void TurnOffCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
