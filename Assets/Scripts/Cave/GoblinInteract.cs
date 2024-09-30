using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinInteract : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _canvas;

    [SerializeField] private EventBus _eventBus;

    private bool _interact;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _player)
        {
            _interact = true;

            _canvas.SetActive(true);

            _eventBus.Raise(new InteractMenuEvent(_interact));
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == _player)
        {
            _interact = false;

            _canvas.SetActive(false);

            _eventBus.Raise(new InteractMenuEvent(_interact));
        }
    }
}
