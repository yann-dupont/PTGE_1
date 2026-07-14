using System;
using UnityEngine;
using UnityEngine.VFX;

public abstract class Spell : MonoBehaviour
{
    [Header("Visual&Sound")]
    [SerializeField] protected VisualEffect visualEffect;
    [SerializeField] protected float duration;
    [SerializeField] public AudioClip soundEffect;
    
    protected abstract void HandleSpell();

    
    protected virtual void Awake()
    {
        Debug.Log("Spell : " + name + " Awake");
        visualEffect.enabled = true;
        visualEffect.SendEvent("OnPlay");
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleSpell();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        
    }
}
