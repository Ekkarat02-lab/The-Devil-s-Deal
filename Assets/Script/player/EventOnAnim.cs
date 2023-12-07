using UnityEngine;
using UnityEngine.Events;

namespace Script.player
{
    public class EventOnAnim : MonoBehaviour
    {
        public UnityEvent eventOnAnimation;

        public void Trigger0()
        {
            eventOnAnimation.Invoke();
        }
    }
}
