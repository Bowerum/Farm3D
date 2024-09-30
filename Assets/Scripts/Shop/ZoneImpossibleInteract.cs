using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneImpossibleInteract : MonoBehaviour
{
    private static bool _inZone;

    [SerializeField] private GameObject _player;

    [SerializeField] private EventBus _eventBus;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _player)
        {
            _inZone = true;

            _eventBus.Raise(new PlayerInZoneEvent(_inZone));
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _player)
        {
            _inZone = false; 

            _eventBus.Raise(new PlayerInZoneEvent(_inZone));
        }
    }
}
