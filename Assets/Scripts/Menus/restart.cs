using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void restartfunction()
    {
      
            SceneManager.LoadScene("Island");
        
    }
}
