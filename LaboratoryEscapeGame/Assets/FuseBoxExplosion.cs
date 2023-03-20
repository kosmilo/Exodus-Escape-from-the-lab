using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuseBoxExplosion : MonoBehaviour
{
    [SerializeField] UnityEvent reachedExplosionArea;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("Trigger fusebox explosion");
            reachedExplosionArea.Invoke();
            Destroy(gameObject);
        }
    }
}
