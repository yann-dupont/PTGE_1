using UnityEngine;

public class PlayerSpellManager : MonoBehaviour
{
    [SerializeField] Transform spellSpawnPoint;
    public static PlayerSpellManager Instance { get; private set; }
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        Spell spellToBeCasted = spellPrefab.GetComponent<Spell>();
        audioSource.PlayOneShot(spellToBeCasted.soundEffect);
        switch(spellToBeCasted)
        {
            case FireballSpell:
                FireballSpell fireballSpell = Instantiate(spellPrefab, spellSpawnPoint.position, spellSpawnPoint.rotation).GetComponent<FireballSpell>();
                fireballSpell.Init(GetComponent<Rigidbody>().linearVelocity.magnitude, spellSpawnPoint.forward );
                break;
            
            case HealSpell:
                HealSpell healSpell = Instantiate(spellPrefab, transform.position, spellSpawnPoint.rotation,transform).GetComponent<HealSpell>();
                healSpell.Init(gameObject.GetComponent<PlayerHealth>());
                break;
        }
    }
}