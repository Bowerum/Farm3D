using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIndicators : MonoBehaviour, IEventReceiver<ChangeMoneyEvent>, IEventReceiver<ChangeIndicatorsHungryEvent>, IEventReceiver<ChangeExperienceEvent>,IEventReceiver<ChangeIndicatorsFluitEvent>
{
    public static int FoodIndicator = 100;
    public static int FluitIndicator = 100;

    public static int Money = 30;

    public static int Level = 1;
    public static int Experience = 0;

    public static int Score;

    [SerializeField] private TMP_Text _moneyText;

    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _experienceText;

    [SerializeField] private Slider _foodSlider;
    [SerializeField] private Slider _fluitSlider;

    [SerializeField] private EventBus _eventBus;
    private void OnEnable()
    {
        _eventBus.Register(this as IEventReceiver<ChangeMoneyEvent>);
        _eventBus.Register(this as IEventReceiver<ChangeIndicatorsHungryEvent>);
        _eventBus.Register(this as IEventReceiver<ChangeIndicatorsFluitEvent>);
        _eventBus.Register(this as IEventReceiver<ChangeExperienceEvent>);
    }
    private void OnDisable()
    {
        _eventBus.UnRegister(this as IEventReceiver<ChangeMoneyEvent>);
        _eventBus.UnRegister(this as IEventReceiver<ChangeIndicatorsHungryEvent>);
        _eventBus.UnRegister(this as IEventReceiver<ChangeIndicatorsFluitEvent>);
        _eventBus.UnRegister(this as IEventReceiver<ChangeExperienceEvent>);
    }
    private void Start()
    {
        _moneyText.text = Money.ToString();

        _levelText.text = Level.ToString();
        _experienceText.text = Experience.ToString() + "/" + Levels.LevelsPlayer[Level];

        _foodSlider.value = FoodIndicator;
        _fluitSlider.value = FluitIndicator;

    }
    public void OnEvent(ChangeMoneyEvent @event)
    {
        Money += @event.Price;
        _moneyText.text = Money.ToString();
    }
    public void OnEvent(ChangeIndicatorsHungryEvent @event)
    {
        FoodIndicator += @event.Food; 
        _foodSlider.value = FoodIndicator;
        if (FoodIndicator == 0)
            Application.Quit();
    }

    public void OnEvent(ChangeIndicatorsFluitEvent @event)
    {
        FluitIndicator += @event.Fluit;
        _fluitSlider.value = FluitIndicator;
        if (FluitIndicator == 0)
            Application.Quit();
    }
    public void OnEvent(ChangeExperienceEvent @event)
    {
        Experience += @event.Experience;

        if (Experience >= Levels.LevelsPlayer[Level]) 
        {
            Experience -= Levels.LevelsPlayer[Level];
            Level++;
            _eventBus.Raise(new UpLevelEvent());
        }
        _levelText.text = Level.ToString();
        _experienceText.text = Experience.ToString() + "/" + Levels.LevelsPlayer[Level];
    }
}
