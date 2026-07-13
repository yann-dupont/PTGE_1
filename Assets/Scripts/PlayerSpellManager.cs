using UnityEngine;

public class PlayerSpellManager : MonoBehaviour
{
    [SerializeField] Transform spellSpawnPoint;
    public static PlayerSpellManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    

    public void CastSpell(GameObject spellPrefab)
    {
        if (Instance == null)
        {
            Debug.LogError("PlayerSpellManager singleton not initialized.");
            return;
        }
        FireballMovement.FireballHorizontalMovement castedSpell = Instantiate(spellPrefab, spellSpawnPoint.position, spellSpawnPoint.rotation).GetComponent<FireballMovement.FireballHorizontalMovement>();
        castedSpell.Init(GetComponent<Rigidbody>().linearVelocity);
    }
}