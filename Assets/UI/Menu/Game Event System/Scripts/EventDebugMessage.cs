using UnityEngine;

public class EventDebugMessage : MonoBehaviour
{
    //  Default event message if no message is sent from the "GameEventListener"...
    public string eventMessage = "Event response message from script...";

    public void DebugMessage(string msg)
    {
        if (msg == null)
            msg = eventMessage;
        Debug.Log("Event response by : "+gameObject.name + " : "+msg);
    }
}
