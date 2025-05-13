using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public UnityEvent events;

    public void EventsFunction()
    {
        events.Invoke();
    }
}
