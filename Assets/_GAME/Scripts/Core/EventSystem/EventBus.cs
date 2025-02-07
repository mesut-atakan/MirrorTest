using System.Collections.Generic;
using UnityEngine;
using JetBrains.Annotations;
using System.Text;

namespace NotsGame.Core.EventSystem
{
    public static class EventBus<TEvent>
    {
        private static readonly string TypeName = typeof(EventBus<TEvent>).Name;

        private static readonly List<EventListener<TEvent>> Listeners = new List<EventListener<TEvent>>();

        [PublicAPI]
        public static void AddListener(EventListener<TEvent> listener)
        {
            // TODO[] : Log Add Listener
            // .
            // .
            // .

            Listeners.Add(listener);
        }

        [PublicAPI]
        public static void RemoveListener(EventListener<TEvent> listener)
        {
            // TODO[] : Log Add Listener
            // .
            // .
            // .

            Listeners.Remove(listener);
        }

        [PublicAPI]
        public static void Emit(object sender, TEvent e)
        {
            // TODO[] : Maybe Log Emit 

            for (var i = Listeners.Count - 1; i >= 0; i--)
            {
                var listener = Listeners[i];
                
                // TODO[] : Config . Event Errors
                try 
                {
                    listener.Invoke(sender, e);
                }
                catch(System.Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }

        private static int GetListenerCount()
        {
            return Listeners.Count;
        }

        private static void MaybeLogEmit(object sender, TEvent e)
        {
            // TODO[] : if !Config . LogEmit
            // .
            // . | RETURN |
            // .

            var sb = new StringBuilder();
            sb.AppendLine($"{TypeName}: Emitting to {Listeners.Count} listeners");

            // TODO[] : Config . LogArguments
            // .
            // .
            // .

            Debug.Log(sb?.ToString());
        }
    }
}