using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScore : MonoBehaviour
{
    [SerializeField] private EventBus _eventBus;
    public void Buy()
    {
        if (PlayerIndicators.Money >= 100)
        {
            AudioManager.AidioManager.Play("click");

            _eventBus.Raise(new ChangeMoneyEvent(-100));
            PlayerIndicators.Score += 100;
        }
    }
}
