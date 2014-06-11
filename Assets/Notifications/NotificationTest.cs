using UnityEngine;
using System.Collections;
/*
public class NotificationTest : MonoBehaviour
{
	private bool notificationTestStarted = false;
	
	// Use this for initialization
	void Start()
	{
		// Add Notification Listeners
		NotificationCenter.defaultCenter.addListener( onReceiveNotificationTypeOne, NotificationType.OnOtherStuff );
		NotificationCenter.defaultCenter.addListener( onReceiveNotificationTypeTwo, NotificationType.OnStuff );
		NotificationCenter.defaultCenter.addListener( onReceiveNotificationTypeThree, NotificationType.OnSomeEvent );
	}
	
	// Update is called once per frame
	void Update()
	{
		if( !notificationTestStarted )
		{
			Debug.Log( "starting test" );
			StartCoroutine( startNotificationTest() );
			notificationTestStarted = true;
		}
	}
	

	IEnumerator startNotificationTest()
	{
		// Setup some notifications
		Debug.Log( "Adding listeners for all three notification types" );
		Notification typeOne = new Notification( NotificationType.OnOtherStuff, "type one here" );
		Notification typeTwo = new Notification( NotificationType.OnStuff, "type two here" );
		SuperNotification typeThree = new SuperNotification( NotificationType.OnSomeEvent, 4.5f, 2 );
		
		yield return new WaitForSeconds( 1.0f );
		
		// Send off all three notifications
		Debug.Log( "Sending all three notifications" );
		NotificationCenter.defaultCenter.postNotification( typeOne );
		NotificationCenter.defaultCenter.postNotification( typeTwo );
		NotificationCenter.defaultCenter.postNotification( typeThree );
		
		yield return new WaitForSeconds( 1.0f );
		
		// Remove ourself as a listener for two of the notifications
		Debug.Log( "Removing 2 of the 3 notification listeners" );
		NotificationCenter.defaultCenter.removeListener( onReceiveNotificationTypeOne, NotificationType.OnOtherStuff );
		NotificationCenter.defaultCenter.removeListener( onReceiveNotificationTypeTwo, NotificationType.OnStuff );
		
		// Send off all three notifications but we should only get one of them this time
		Debug.Log( "Sending all three notifications" );
		NotificationCenter.defaultCenter.postNotification( typeOne );
		NotificationCenter.defaultCenter.postNotification( typeTwo );
		NotificationCenter.defaultCenter.postNotification( typeThree );
		
		yield return new WaitForSeconds( 1.0f );
		
		// Remove ourself as a listener for the last notification
		Debug.Log( "Removing the last notification listener" );
		NotificationCenter.defaultCenter.removeListener( onReceiveNotificationTypeThree , NotificationType.OnSomeEvent );
		
		// Send off all three notifications but we should only get one of them this time
		Debug.Log( "Sending all three notifications" );
		NotificationCenter.defaultCenter.postNotification( typeOne );
		NotificationCenter.defaultCenter.postNotification( typeTwo );
		NotificationCenter.defaultCenter.postNotification( typeThree );
	}
	
		
	void onReceiveNotificationTypeOne( Notification note )
	{
		Debug.Log( string.Format( "Received notification typeOne with object: {0}", note.userInfo.ToString() ) );
	}
	
	
	void onReceiveNotificationTypeTwo( Notification note )
	{
		Debug.Log( string.Format( "Received notification typeTwo with object: {0}", note.userInfo.ToString() ) );
	}
	
	
	void onReceiveNotificationTypeThree( Notification note )
	{
		SuperNotification superNote = (SuperNotification)note;
		Debug.Log( string.Format( "Received notification typeThree with float: {0}", superNote.varFloat ) );
	}
	
	
	
}

*/