using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrematoriumPuzzleManager : MonoBehaviour
{
    [SerializeField] UnityEvent cremationDone;
    [SerializeField] List<GameObject> pipes;
    [SerializeField] List<Vector3> correctRotations;
    [SerializeField] string newObjective; 

    // Start is called before the first frame update
    void Awake()
    {
        // Get all pipe objs
        TurningPipe[] allPipes = GetComponentsInChildren<TurningPipe>();
        foreach (TurningPipe pipe in allPipes) {
            pipes.Add(pipe.gameObject);
        }

        foreach (GameObject pipe in pipes) {
            correctRotations.Add(pipe.transform.eulerAngles);
        }
    }

    private void Start() {
        cremationDone.AddListener(UpdateObjs);
    }

    public bool CheckCompletion()
    {
        bool isCompleted = true;

        for (int i = 0; i < pipes.Count; i++) {
            switch (pipes[i].GetComponent<TurningPipe>().pipeType) {
                case 2: // if pipe type 2, check for two possible correct rotations
                    Vector3 otherCorrectRot = correctRotations[i]; // Get the other correct rotation
                    if (otherCorrectRot.y > 180) {
                        otherCorrectRot.y -= 180;
                    } 
                    else {
                        otherCorrectRot.y += 180;
                    }
                    isCompleted = isCompleted && (pipes[i].transform.eulerAngles == correctRotations[i] || pipes[i].transform.eulerAngles == otherCorrectRot);
                    break;
                case 4: // if pipe type 4, don't check for anything
                    break;
                default:
                    isCompleted = isCompleted && pipes[i].transform.eulerAngles == correctRotations[i];
                    break;
            }
        }
        Debug.Log(isCompleted);

        if (isCompleted) {
            cremationDone.Invoke();
        }

        return isCompleted;
    }

    private void UpdateObjs() {
        ObjectivesManager objectives = FindObjectOfType<ObjectivesManager>();
        objectives.UpdateObjective(newObjective);
    }
}
