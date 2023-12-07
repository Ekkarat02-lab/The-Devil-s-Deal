using Script.player;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    public enum PickupObject
    {
        COIN,
        GEM
    };

    public PickupObject currentObject;
    public int pickupQuantity;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider != null && otherCollider.name == "Player")
        {
            PlayerHealth playerHealth = otherCollider.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
                playerHealth.AddCurrency(this);
            }
            
            Destroy(gameObject);
        }
    }
}