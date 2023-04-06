using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCredits : MonoBehaviour
{
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public void StartRollCredits() {
        StartCoroutine(RollCredits());
    }

    IEnumerator RollCredits() {
        yield return new WaitForSeconds(2f);
        animator.Play("SceneFadeToCredits");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);
        yield return null;
    }
}
