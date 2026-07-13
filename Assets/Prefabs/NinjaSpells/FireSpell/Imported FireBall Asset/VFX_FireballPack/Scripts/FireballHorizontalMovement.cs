using UnityEngine;
using UnityEngine.VFX;

namespace FireballMovement
{    public class FireballHorizontalMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float acceleration;
        [SerializeField] private float duration = 6f;
        [SerializeField] private VisualEffect _fireballTrails;
        private Rigidbody _fireball;
        private Vector3 currentVelocity;
        private float timer = 0f;
        

        private const string _fireballTrailsActiveString = "FireballTrailsActive";

        void Awake()
        {
            Debug.Log("Awake of fireball");
            _fireball  = GetComponent<Rigidbody>();
            _fireballTrails.enabled = true;
            _fireballTrails.SendEvent("OnPlay");
        }

        public void Init(Vector3 startingVelocity)
        {
            currentVelocity = startingVelocity;
        }

        void Update()
        {
            if (timer < duration)
            {
                timer += Time.deltaTime;
                if(currentVelocity.magnitude < _moveSpeed)
                {
                    currentVelocity += currentVelocity.normalized * acceleration * Time.deltaTime;
                    
                }
                else if(currentVelocity.magnitude >= _moveSpeed)
                {
                    currentVelocity = _moveSpeed * currentVelocity.normalized;
                }
                _fireball.linearVelocity = currentVelocity;
            }
            else if (timer >= duration)
            { 
                ResetAll();
            }

            if (Mathf.Abs(_fireball.linearVelocity.magnitude) > 0.1)
            {
                _fireballTrails.SetBool(_fireballTrailsActiveString, true);
            }
            else
            {
                _fireballTrails.SetBool(_fireballTrailsActiveString, false);
            }
        }

        private void ResetAll()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Destroy(gameObject);
        }
    }
}