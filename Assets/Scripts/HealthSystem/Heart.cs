using UnityEngine;
using UnityEngine.UI;
public class Heart : MonoBehaviour
{
    [SerializeField] private Image heartFill;

    public void DeactivateHearthFill()
    {
        heartFill.gameObject.SetActive(false);
    }
    
    public void ActivateHearthFill()
    {
        heartFill.gameObject.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
