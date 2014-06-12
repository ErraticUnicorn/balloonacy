using UnityEngine;
using System.Collections;

public enum NotificationType
{
    OnBalloonPlayerCollision,
    OnEvent,
    OnAchievableEvent,
    BalloonPop,
    TotalNotifications
};

public delegate void OnNotificationDelegate(Notification note);

// Usage:
// NotificationCenter.defaultCenter.addListener( onNotification );
// NotificationCenter.defaultCenter.sendNotification( new Notification( NotificationTypes.OnStuff, this ) );
// NotificationCenter.defaultCenter.removeListener( onNotification, NotificationType.OnStuff );

public class NotificationCenter {

    private static NotificationCenter instance;
    private ArrayList[] listeners = new ArrayList[(int)NotificationType.TotalNotifications];

    public NotificationCenter()
    {
        if (instance != null)
        {
            Debug.Log("NotificationCenter instance is not null");
            return;
        }
        instance = this;
    }

    void NotificationCenterSetToNull()
    {
        instance = null;
    }

    public static NotificationCenter defaultCenter
    {
        get
        {
            if( instance == null )
                new NotificationCenter();
            return instance;
        }
    }

    public void addListener(OnNotificationDelegate newListenerDelegate, NotificationType type)
    {
        int typeInt = (int)type;

        // Create the listener ArrayList lazily
        if (listeners[typeInt] == null)
            listeners[typeInt] = new ArrayList();

        listeners[typeInt].Add(newListenerDelegate);
    }

    public void removeListener(OnNotificationDelegate listenerDelegate, NotificationType type)
    {
        int typeInt = (int)type;

        if (listeners[typeInt] == null)
            return;

        if (listeners[typeInt].Contains(listenerDelegate))
            listeners[typeInt].Remove(listenerDelegate);

        // Clean up empty listener ArrayLists
        if (listeners[typeInt].Count == 0)
            listeners[typeInt] = null;
    }


    public void postNotification(Notification note)
    {
        int typeInt = (int)note.type;

        if (listeners[typeInt] == null)
            return;

        foreach (OnNotificationDelegate delegateCall in listeners[typeInt])
        {
            delegateCall(note);
        }
    }
}
