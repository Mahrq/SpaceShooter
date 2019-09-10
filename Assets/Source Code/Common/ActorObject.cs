using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ActorObject : MonoBehaviour
{
    [SerializeField]
    protected int health = 1;
    public int Health { get { return health; } /*private set { health = value; }*/ }
    [SerializeField]
    [Range(0.1f, 20f)]
    protected float movementSpeed = 1f;
    [SerializeField]
    protected Transform actorTransform;
    [SerializeField]
    protected Rigidbody actorRigidbody;
    protected GameObject actorGameObject;
    public bool IsDead { get; private set; }

    virtual protected void Start()
    {
        actorGameObject = this.gameObject;
        actorTransform = this.GetComponent<Transform>();
        actorRigidbody = this.GetComponent<Rigidbody>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
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
