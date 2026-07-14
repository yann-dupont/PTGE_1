using UnityEngine;
using UnityEngine.VFX;

public class FireballSpell : Spell
{
    
    [Header("Movement")]
    [SerializeField] private float _maxMoveSpeed = 10f;
    [SerializeField] private float acceleration;
    
    
    private Rigidbody _fireball;
    private float currentVelocity;
    private Vector3 direction;
    private float timer = 0f;
    

    private const string _fireballTrailsActiveString = "FireballTrailsActive";

    protected override void Awake()
    {
        base.Awake();
        _fireball  = GetComponent<Rigidbody>();
    }

    public void Init(float startingVelocity, Vector3 direction)
    {
        timer = 0f;
        currentVelocity = startingVelocity;
        this.direction = direction;
    }
    
    protected override void HandleSpell()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            ResetAll();
            return;
        }
        
        transform.position += direction * currentVelocity * Time.deltaTime;
        
        if (currentVelocity < _maxMoveSpeed)
        {
            currentVelocity += acceleration * Time.deltaTime;

            if (currentVelocity >= _maxMoveSpeed)
            {
                currentVelocity = _maxMoveSpeed;
            }
        }
    }
    
    void HandleFireballTrails()
    {
        if (Mathf.Abs(currentVelocity) > 0)
        {
            visualEffect.SetBool(_fireballTrailsActiveString, true);
        }
        else
        {
            visualEffect.SetBool(_fireballTrailsActiveString, false);
        }
    }

    void Update()
    {
        base.Update();
        HandleFireballTrails();
    }

    private void ResetAll()
    {
        Destroy(gameObject);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                break;
            
            default:
                Destroy(gameObject);
                break;
                
        }
    }
}
