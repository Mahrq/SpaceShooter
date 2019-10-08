using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ActorObject : MonoBehaviour
{
    [SerializeField]
    protected int health = 1;
    public int CurrentHealth { get; protected set; }
    [SerializeField]
    [Range(0.1f, 20f)]
    protected float movementSpeed = 1f;
    [SerializeField]
    protected Transform actorTransform;
    [SerializeField]
    protected Rigidbody actorRigidbody;
    protected GameObject actorGameObject;
    public bool IsDead { get; protected set; }

    virtual protected void Start()
    {
        actorGameObject = this.gameObject;
        actorTransform = this.GetComponent<Transform>();
        actorRigidbody = this.GetComponent<Rigidbody>();
        CurrentHealth = health;
    }

    public virtual void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        Debug.Log($"{actorGameObject.name} took {amount} Damage and has {CurrentHealth} health left");
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsDead = true;
            Death(IsDead);
        }
    }

    virtual protected void Death(bool deathStatus)
    {
        if (deathStatus)
        {
            actorGameObject.SetActive(false);
        }        
    }

}
