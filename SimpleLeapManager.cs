using UnityEngine;
using Leap.Unity;

public class SimpleLeapManager : MonoBehaviour
{
    public LeapServiceProvider leapServiceProvider;
    public SimpleController simpleController;
    // public WsClient wsClient;
    public Recorder recorder;

    public void Bind()
    {
        leapServiceProvider = GameObject.FindObjectOfType<LeapServiceProvider>();

        simpleController = GameObject.FindObjectOfType<SimpleController>();
        simpleController.leapServiceProvider = leapServiceProvider;

        // wsClient = GameObject.FindObjectOfType<WsClient>();
        // wsClient.simpleController = simpleController;

        recorder = GameObject.FindObjectOfType<Recorder>();
        recorder.simpleController = simpleController;
        PlayBinder[] binders = GameObject.FindObjectsOfType<PlayBinder>();
        foreach (PlayBinder binder in binders)
        {
            if (binder.handType == SimpleController.Type.LEFT)
            {
                recorder.leftBinder = binder;
            }
            else
            {
                recorder.rightBinder = binder;
            }
        }
    }
}
