using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuseBoxExplosion : MonoBehaviour
{
    [SerializeField] UnityEvent reachedExplosionArea;

    private void Start() {
        reachedExplosionArea.AddListener(UpdateObjs);
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("Trigger fusebox explosion");
            reachedExplosionArea.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdateObjs() {
        ObjectivesManager objectives = FindObjectOfType<ObjectivesManager>();
        objectives.UpdateObjective("Find parts to fix the elevator [0/3]");
    }
}
