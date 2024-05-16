using UnityEngine;
[CreateAssetMenu(menuName = "Events/Collection")]
public class EventCollection : ScriptableObject
{
    [SerializeField] Event[] Events;

    public Event GetEvent(int eventIndex)
    {
        if (eventIndex < 0 || eventIndex >= Events.Length)
            return null;

        return Events[eventIndex];
    }
}