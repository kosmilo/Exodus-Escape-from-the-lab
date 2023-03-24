using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumPadLights : MonoBehaviour
{
    Material buttonMaterial;

    private void Awake()
    {
        buttonMaterial = GetComponent<MeshRenderer>().material;
        buttonMaterial.SetColor("_EmissionColor", new Color32(0, 0, 0, 0));
    }

    public void PressLight()
    {
        StartCoroutine(ButtonPressLight());
    }

    public void UnlockLight()
    {
        StartCoroutine(ButtonUnlockLight());
    }

    public void IncorrectLight()
    {
        StartCoroutine(ButtonIncorrectLight());
    }

    public IEnumerator ButtonPressLight()
    {
        buttonMaterial.SetColor("_EmissionColor", new Color32(255, 255, 255, 255));
        yield return new WaitForSeconds(0.3f);
        buttonMaterial.SetColor("_EmissionColor", new Color32(0, 0, 0, 0));
    }

    public IEnumerator ButtonUnlockLight()
    {
        buttonMaterial.SetColor("_EmissionColor", new Color32(0, 255, 0, 255));
        yield return null;
    }

    public IEnumerator ButtonIncorrectLight()
    {
        buttonMaterial.SetColor("_EmissionColor", new Color32(255, 0, 0, 255));
        yield return new WaitForSeconds(2f);
        buttonMaterial.SetColor("_EmissionColor", new Color32(0, 0, 0, 0));
    }

}
