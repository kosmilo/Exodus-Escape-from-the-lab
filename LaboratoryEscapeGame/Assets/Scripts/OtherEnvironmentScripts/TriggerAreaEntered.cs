using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAreaEntered : MonoBehaviour
{
    [SerializeField] UnityEvent areaEntered;
    [SerializeField] bool changeObjective;
    [SerializeField] string newObjective;

    private void Start() {
        if (changeObjective) {
            areaEntered.AddListener(UpdateObjs);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log("Trigger area entered");
            areaEntered.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdateObjs() {
        ObjectivesManager objectives = FindObjectOfType<ObjectivesManager>();
        objectives.UpdateObjective(newObjective);
    }
}
