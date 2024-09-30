using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Eating : MonoBehaviour
{
    [SerializeField] private EventBus _eventBus;

    private PlayerInputActions _inputActions;

    private void Start()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Player.Enable();
        _inputActions.Player.Eat.performed += Eat;

        StartCoroutine(HungryProcess());
        StartCoroutine(FluitProcess());
    }

    private void Eat(InputAction.CallbackContext obj)
    {
        Item activeItem = PlayerInventory.ItemsInventory[Inventory.ActiveSlot].Item;

        if (activeItem.ItemType == ItemType.Food)
        {
            _eventBus.Raise(new ChangeIndicatorsHungryEvent(activeItem.Food.Hungry));
            _eventBus.Raise(new ChangeIndicatorsFluitEvent(activeItem.Food.Fluit));
            _eventBus.Raise(new ChangeItemEvent(activeItem, -1));
        }
    }
    private IEnumerator HungryProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            _eventBus.Raise(new ChangeIndicatorsHungryEvent(-1));
        }
    }
    private IEnumerator FluitProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            _eventBus.Raise(new ChangeIndicatorsFluitEvent(-1));
        }
    }
}
