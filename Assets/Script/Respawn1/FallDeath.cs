using UnityEngine;

namespace Script.Respawn
{
    public class FallDeath : MonoBehaviour
    {
    
        //public GameOver gameOverManager;
        //public int fall;
        // Start is called before the first frame update
        public GameObject player;
        public GameObject respawnPoint;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                player.transform.position = respawnPoint.transform.position;
            }
        }

    }
}
