using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float collectionDuration = 3.0f;
    [SerializeField] Color highlightColor = Color.white;
    [SerializeField] float emissionIntensity = 1.2f;

    private Material instanceMaterial;
    private Vector3 initialScale;

    void Start()
    {
        gameObject.tag = "Collectable";

        Renderer renderer = GetComponentInChildren<MeshRenderer>();
        instanceMaterial = new Material(renderer.material);
        instanceMaterial.EnableKeyword("_EMISSION");
        instanceMaterial.SetColor("_EmissionColor", highlightColor * emissionIntensity);
        renderer.material = instanceMaterial;

        DisableHighlight();

        initialScale = transform.localScale;

    }

    public float  GetCollectionDuration()
    {
        return collectionDuration;
    }

    public void EnableHighlight()
    {
        instanceMaterial.EnableKeyword("_EMISSION");       
    }

    public void DisableHighlight()
    {
        instanceMaterial.DisableKeyword("_EMISSION");   
    }

    public void CollectEffect(float currentCollectingTime)
    {
        float progress = Mathf.Clamp01(currentCollectingTime / collectionDuration);

        float currentIntensity = Mathf.Lerp(emissionIntensity, 0f, progress);

        instanceMaterial.SetColor("_EmissionColor", highlightColor * currentIntensity);
    }
}
