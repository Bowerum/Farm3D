using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Sprite _activeImage;
    [SerializeField] private Sprite _selectedImage;
    [SerializeField] private Sprite _emptyImage;

    [SerializeField] private ItemInventory _itemInventory;

    [SerializeField] private Image _slotImage;

    [SerializeField] private int _id;

    private TMP_Text _countText;

    private TMP_Text _priceText;

    private Image _coinImage;

    private Image _itemImage;

    private EventBus _eventBus;
    public void FillSlot(int id)
    {
        _id = id;

        _itemInventory = PlayerInventory.ItemsInventory[_id];

        SwitchStatePriceText(ShopInteract.InZoneShopInteract);

        ChangeItem();
    }
    public void ActiveSlot(int id)
    {
        if (_id == id)
            _slotImage.sprite = _activeImage;
        else
            _slotImage.sprite = _emptyImage;
    }
    public void SwitchStatePriceText(bool enable)
    {
        if (_itemInventory.Item.ItemType != ItemType.Empty && _itemInventory.Item.CostOfSale != 0)
        {
            _coinImage.enabled = enable;
            _priceText.enabled = enable;
            _priceText.text = _itemInventory.Item.CostOfSale.ToString();
        }
    }
    private void Awake()
    {
        _slotImage = transform.GetComponent<Image>();

        _itemImage = transform.GetChild(0).GetComponent<Image>();

        _countText = transform.GetChild(1).GetComponent<TMP_Text>();

        _priceText = transform.GetChild(2).GetComponent<TMP_Text>();

        _coinImage = transform.GetChild(3).GetComponent<Image>();

        _priceText.enabled = false;
        _coinImage.enabled = false;

        GetComponent<Button>().onClick.AddListener(ClickButton);
    }

    private void ChangeItem()
    {
        if (_itemInventory.Item.ItemType != ItemType.Empty)
        {
            _itemImage.sprite = Resources.Load<Sprite>(_itemInventory.Item.SpriteUrl);

            var color = _itemImage.color;
            color.a = 255f;
            _itemImage.color = color;

            if (_itemInventory.Item.ItemType != ItemType.Instrument)
                _countText.text = _itemInventory.Count.ToString();
            else
                _countText.text = "";     
        }
        else
        {           
            var color = _itemImage.color;
            color.a = 0f;
            _itemImage.color = color;

            _priceText.enabled = false;
            _coinImage.enabled = false;

            _countText.text = "";
        }
    }
    private void ClickButton()
    {
        if (ShopInteract.InZoneShopInteract)
        {
            _eventBus = FindFirstObjectByType<EventBus>();

            if (_itemInventory.Item.CostOfSale != 0 && _itemInventory.Item.ItemType != ItemType.Empty)
            {
                AudioManager.AidioManager.Play("sell");

                _eventBus.Raise(new ChangeMoneyEvent(_itemInventory.Item.CostOfSale));
                _eventBus.Raise(new ChangeItemEvent(_itemInventory.Item, -1));
            }
        }
        else
        {
            AudioManager.AidioManager.Play("click");
            SwitchItem();
        }
    }
    private void SwitchItem()
    {
        if (Inventory.SelectedSlot == null)
        {
            _slotImage.sprite = _selectedImage;

            Inventory.SelectedSlot = this;
        }

        else if (Inventory.SelectedSlot == this)
        {
            _slotImage.sprite = _emptyImage;

            Inventory.SelectedSlot = null;
        }
        else
        {
            Inventory.SelectedSlot._slotImage.sprite = _emptyImage;

            ItemInventory itemInventory = PlayerInventory.ItemsInventory[_id];

            PlayerInventory.ItemsInventory[_id] = PlayerInventory.ItemsInventory[Inventory.SelectedSlot._id];
            _itemInventory = Inventory.SelectedSlot._itemInventory;

            PlayerInventory.ItemsInventory[Inventory.SelectedSlot._id] = itemInventory;
            Inventory.SelectedSlot._itemInventory = itemInventory;

            Inventory.SelectedSlot.ChangeItem();

            ChangeItem();

            Inventory.SelectedSlot = null;
        }
    }
}
