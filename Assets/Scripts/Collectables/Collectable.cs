using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float collectionDuration = 3.0f;
    [SerializeField] Color highlightColor = Color.white;
    [SerializeField] float emissionIntensity = 1.2f;

    private Material instanceMaterial;

    void Start()
    {
        gameObject.tag = "Collectable";

        Renderer renderer = GetComponentInChildren<MeshRenderer>();
        instanceMaterial = new Material(renderer.material);
        instanceMaterial.EnableKeyword("_EMISSION");
        instanceMaterial.SetColor("_EmissionColor", highlightColor * emissionIntensity);
        renderer.material = instanceMaterial;

        DisableHighlight();
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
    
}
