using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int damage;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<PlayerHealth>() != null)
        {
            collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
