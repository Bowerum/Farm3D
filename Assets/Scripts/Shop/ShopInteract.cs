using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteract : MonoBehaviour
{
    public static bool InZoneShopInteract;

    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _shopPanel;

    [SerializeField] private EventBus _eventBus;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _player)
        {
            InZoneShopInteract = true;

            _shopPanel.SetActive(InZoneShopInteract);

            _eventBus.Raise(new InteractMenuEvent(InZoneShopInteract));

            _eventBus.Raise(new PlayerInShopZoneInteractEvent(InZoneShopInteract));
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _player)
        {
            InZoneShopInteract = false;

            _shopPanel.SetActive(InZoneShopInteract);

            _eventBus.Raise(new InteractMenuEvent(InZoneShopInteract));

            _eventBus.Raise(new PlayerInShopZoneInteractEvent(InZoneShopInteract));
        }
    }
}
