using UnityEngine;

namespace Script.Drop_And_PickUp
{
    public class PickupUi : MonoBehaviour
    {
        private GameObject playerObject; // Renamed for clarity
        public float speed; // Consider adding a comment for clarity

        private void Start()
        {
            playerObject = GameObject.Find("Player");
        }

        void Update()
        {
            if (playerObject != null) // Use a more explicit null check
            {
                transform.position = Vector3.MoveTowards(transform.position, 
                    playerObject.transform.position, speed * Time.deltaTime);
            }
        }
    }
}