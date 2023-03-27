using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorFuseBox : MonoBehaviour
{
    [SerializeField] int mechanicalPartsCounter;
    [SerializeField] GameObject extras;
    [SerializeField] GameObject[] mechanicalPartModels;
    [SerializeField] GameObject[] lights;
    [SerializeField] UnityEvent elevatorFixed;
    [SerializeField] Material emGreen;

    void Start() {
        extras.SetActive(true);

        // Disable all mechanical part models
        foreach (GameObject part in mechanicalPartModels) {
            part.SetActive(false);
        }
    }
    
    public void AddParts() {

        // CHECK IF HAS MECHANICAL PARTS SELECTED
        if (FindObjectOfType<Inventory>().CheckForItem(12) != null) {

            FindObjectOfType<Inventory>().RemoveItem(12);

            // Set one of the mechanical parts models active and increment counter by one
            mechanicalPartModels[mechanicalPartsCounter].SetActive(true);

            // Change light to green
            
            var materialsCopy = lights[mechanicalPartsCounter].GetComponent<MeshRenderer>().materials;
            materialsCopy[1] = emGreen;
            lights[mechanicalPartsCounter].GetComponent<MeshRenderer>().materials = materialsCopy;
            mechanicalPartsCounter++;

            if (mechanicalPartsCounter == 3) {
                elevatorFixed.Invoke();
            }
        }
    }
}
