using UnityEngine;

namespace Script.player
{
    public class CameraFollow : MonoBehaviour
    {
        public float dampTime = 0.15f;
        public Vector3 velocity = Vector3.zero;
        public Transform target;
        public new Camera camera;

        private void Start()
        {
            camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (target)
            {
                Vector3 point = camera.WorldToViewportPoint(target.position);
                Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }
        }
    }
}