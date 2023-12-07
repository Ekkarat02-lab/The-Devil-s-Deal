using Script.Enemy;
using UnityEngine;

namespace Script.PlayerAttack
{
    public class TestProjectile : MonoBehaviour
    {
        public float damage;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name != "Player")
            {
                if (collision.GetComponent<EnemyRecieveDamage>() != null)
                {
                    collision.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
                }
                Destroy(gameObject);
            }
        }
    }
}
