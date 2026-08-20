using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    [SerializeField]
    string levelToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameplayManager.instance.LoadLevel(levelToLoad);
        }
    }
}
