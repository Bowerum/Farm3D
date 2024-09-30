using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IEventReceiver<ChangeSlotEvent>,IEventReceiver<PlayerInShopZoneInteractEvent>
{   
    [SerializeField] private GameObject _hotbarPanel;
    [SerializeField] private GameObject _inventoryPanel;

    [SerializeField] private GameObject _slot;
    
    [SerializeField] private EventBus _eventBus;

    private bool _inventoryOpened = true;

    private List<Slot> _slotsHotBar = new List<Slot>(3);
    private List<Slot> _slotsInventory = new List<Slot>(14);

    private List<Slot> _slots = new List<Slot>();

    private PlayerInputActions _inputActions;

    public static Slot SelectedSlot = null;

    public static int ActiveSlot;
    private void OnEnable()
    {
        _eventBus.Register(this as IEventReceiver<ChangeSlotEvent>);
        _eventBus.Register(this as IEventReceiver<PlayerInShopZoneInteractEvent>);
    }
    private void OnDisable()
    {
        _eventBus.UnRegister(this as IEventReceiver<ChangeSlotEvent>);
        _eventBus.UnRegister(this as IEventReceiver<PlayerInShopZoneInteractEvent>);
    }
    private void Start()
    {
        for (int i = 0; i < _slotsHotBar.Capacity; i++)
        {
            Instantiate(_slot, _hotbarPanel.transform);
        }
        for (int i = 0; i < _slotsInventory.Capacity; i++)
        {
            Instantiate(_slot, _inventoryPanel.transform);
        }

        _slotsHotBar = _hotbarPanel.GetComponentsInChildren<Slot>().ToList();
        _slotsInventory = _inventoryPanel.GetComponentsInChildren<Slot>().ToList();

        for (int i = 0; i < _slotsHotBar.Count; i++)
        {
            _slotsHotBar[i].FillSlot(i);
        }
        for (int i = 0; i < _slotsInventory.Count; i++)
        {
            _slotsInventory[i].FillSlot(i + 3);
        }

        _slots.AddRange(_slotsHotBar);
        _slots.AddRange(_slotsInventory);

        OpenInventory(new InputAction.CallbackContext());

        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.OpenInventory.performed += OpenInventory;
        _inputActions.Player.Active.performed += ChangeActive;
    }
    private void ChangeActive(InputAction.CallbackContext context)
    {
        ActiveSlot = int.Parse(context.control.displayName) - 1;

        foreach (Slot slot in _slotsHotBar)
            slot.ActiveSlot(ActiveSlot);
    }
    private void OpenInventory(InputAction.CallbackContext context)
    {
        if (!_inventoryOpened)
        {
            _inventoryOpened = true;
            _inventoryPanel.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            _inventoryOpened = false;
            _inventoryPanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnEvent(ChangeSlotEvent @event)
    {
        _slots[@event.Id].FillSlot(@event.Id);
    }

    public void OnEvent(PlayerInShopZoneInteractEvent @event)
    {
        foreach (Slot s in _slots)
            s.SwitchStatePriceText(@event.InZoneInteract);
    }
}
