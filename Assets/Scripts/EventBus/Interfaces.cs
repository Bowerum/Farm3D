using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    public interface IEvent { }
    public interface IBaseEventReceiver { }
    public interface IEventReceiver<T> : IBaseEventReceiver where T : struct, IEvent
    {
        void OnEvent(T @event);
    }
