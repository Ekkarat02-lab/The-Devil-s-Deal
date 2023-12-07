using System;
using System.Collections;
using System.Collections.Generic;
using Script.Respawn;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private FallDeath respawn;
    private BoxCollider2D checkPoint;

    void Awake()
    {
        checkPoint = GetComponent<BoxCollider2D>();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<FallDeath>();
    }

    // Start is called before the first frame update
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
            respawn.respawnPoint = this.gameObject;
            checkPoint.enabled = false;
        }
    }
}
