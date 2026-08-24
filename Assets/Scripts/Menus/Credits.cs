using UnityEngine;

public class Credits : MonoBehaviour
{

    public Canvas mainMenuCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retun()
    {
        gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
        
    }
}
