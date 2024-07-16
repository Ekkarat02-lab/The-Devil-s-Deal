using UnityEngine;
using UnityEngine.Events;

public class EventOnAnim : MonoBehaviour
{
    public UnityEvent eventOnAnimation;

    public void Trigger0()
    {
        eventOnAnimation.Invoke();
    } 
}