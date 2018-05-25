using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZoneBehavior : MonoBehaviour
{
    public float healLifePerSeconds = 8f;

    private PlayerBehavior _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _player = other.gameObject.GetComponent<PlayerBehavior>();
        }
    }

    private void Update()
    {
        if (_player != null)
        {
            _player.SetLife(healLifePerSeconds * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _player = null;
        }
    }
}
