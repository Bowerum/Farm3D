using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMenu : MonoBehaviour, IEventReceiver<InteractMenuEvent>
{
    [SerializeField] private GameObject _inventoryPanel;

    [SerializeField] private EventBus _eventBus;
    private void OnEnable()
    {
        _eventBus.Register(this as IEventReceiver<InteractMenuEvent>);
    }
    private void OnDisable()
    {
        _eventBus.UnRegister(this as IEventReceiver<InteractMenuEvent>);
    }
    public void OnEvent(InteractMenuEvent @event)
    {
        if (@event.InteractMenu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            _inventoryPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -100);
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _inventoryPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
    }
}
