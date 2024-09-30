using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCave : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private EventBus _eventBus;

    [SerializeField] private bool _isEnter;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _player)
        {
            _eventBus.Raise(new TeleportEvent());

            if (_isEnter)
                _player.transform.position = new Vector3(13, -4, 15);
            else
            {
                _player.transform.position = new Vector3(13, 1, 15);
            }
        }
    }
}
