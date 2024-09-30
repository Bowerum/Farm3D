using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotShop : MonoBehaviour, IEventReceiver<UpLevelEvent>
{
    private Image _itemImage;

    private GameObject _coin;
    private GameObject _star;

    private TMP_Text _priceText;
    private TMP_Text _levelText;

    private EventBus _eventBus;

    private Item _item;
    private void Start()
    {
        _eventBus = FindFirstObjectByType<EventBus>();

        _eventBus.Register(this as IEventReceiver<UpLevelEvent>);
    }
    public void FillShopSlot(Item item)
    {
        _item = item;

        _itemImage = transform.GetChild(0).GetComponent<Image>();

        _coin = transform.GetChild(2).gameObject;

        _star = transform.GetChild(3).gameObject;

        _levelText = _star.transform.GetChild(0).GetComponentInChildren<TMP_Text>();

        _levelText.text = _item.Seed.LevelUnlock.ToString();

        _priceText = transform.GetChild(1).GetComponentInChildren<TMP_Text>();

        _coin.SetActive(false);

        _itemImage.sprite = Resources.Load<Sprite>(_item.SpriteUrl);

        ChangeSlot();
    }
    public void Buy()
    {
        if(PlayerIndicators.Money >= _item.CostOfPurchase)
        {
            AudioManager.AidioManager.Play("buy");

            _eventBus.Raise(new ChangeMoneyEvent(-_item.CostOfPurchase));
            _eventBus.Raise(new ChangeItemEvent(_item, 1));
        }
    }

    public void OnEvent(UpLevelEvent @event)
    {
        ChangeSlot();
    }
    private void ChangeSlot()
    {
        if (PlayerIndicators.Level >= _item.Seed.LevelUnlock)
        {
            _star.SetActive(false);

            _coin.SetActive(true);

            _priceText.text = $"{_item.CostOfPurchase}";

            var color = _itemImage.color;
            color.a = 255f;
            _itemImage.color = color;

            GetComponent<Button>().onClick.AddListener(delegate { Buy(); });
        }
    }
}
