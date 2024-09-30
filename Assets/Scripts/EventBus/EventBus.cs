using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    private Dictionary<Type, List<WeakReference<IBaseEventReceiver>>> _receivers;
    private Dictionary<int, WeakReference<IBaseEventReceiver>> _receiverHashToReference;

    public EventBus()
    {
        _receivers = new Dictionary<Type, List<WeakReference<IBaseEventReceiver>>>();
        _receiverHashToReference = new Dictionary<int, WeakReference<IBaseEventReceiver>>();
    }
    public void Register<T>(IEventReceiver<T> receiver) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        if (!_receivers.ContainsKey(eventType))
            _receivers[eventType] = new List<WeakReference<IBaseEventReceiver>>();

        WeakReference<IBaseEventReceiver> reference = new WeakReference<IBaseEventReceiver>(receiver);

        _receivers[eventType].Add(reference);
        _receiverHashToReference[receiver.GetHashCode()] = reference;
    }
    public void UnRegister<T>(IEventReceiver<T> receiver) where T : struct, IEvent
    {
        Type eventType = typeof(T);
        int receiverHash = receiver.GetHashCode();
        if (!_receivers.ContainsKey(eventType) || !_receiverHashToReference.ContainsKey(receiverHash))
            return;

        WeakReference<IBaseEventReceiver> reference = _receiverHashToReference[receiverHash];
        _receivers[eventType].Remove(reference);
        _receiverHashToReference.Remove(receiverHash);
    }
    public void Raise<T>(T @event) where T: struct,IEvent   
    {
        Type eventType = typeof(T);
        if (!_receivers.ContainsKey(eventType))
            return;

        foreach(WeakReference<IBaseEventReceiver> reference in _receivers[eventType])
        {
            if(reference.TryGetTarget(out IBaseEventReceiver receiver))
            {
                ((IEventReceiver<T>)receiver).OnEvent(@event);
            }
        }
    }
}
