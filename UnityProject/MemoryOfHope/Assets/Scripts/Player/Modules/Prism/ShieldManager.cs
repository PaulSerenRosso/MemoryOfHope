using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class ShieldManager : MonoBehaviour, Damageable
{
    [SerializeField]
    private MeshRenderer _mesh;
    [SerializeField]
    private Collider _collider;
    private void OnValidate()
    {
        _health = _maxHealth;
    }

    public ShieldMirror Mirror;
    private bool _inputShield;
    public bool InputShield
    {
        get
        {
            return _inputShield;
        }
        set
        {
            _inputShield = value;
            if (!isDead)
            {
                _mesh.enabled = value;
                            Mirror.IsActiveReturnable = value;
                            _collider.enabled = value;
                            
            }
           
        }
    }

    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private bool _isDead;

    [SerializeField]
    private float timeDeath;
    private float timerDeath;
    public int health
    {
        get { return _health; }
        set { _health = value; }
    }

    public int maxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    public bool isDead {  
        get { return _isDead; }
        set { _isDead = value; }
     }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
       
            isDead = true;
            health = 0;
            Death();
        }

    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
    }

    public void Death()
    {
        _mesh.enabled = false; 
        Mirror.IsActiveReturnable = false;
        _collider.enabled = false ; 
   
    }

    private void Update()
    {
        if (isDead)
        {
            if (timeDeath > timerDeath)
            {
             
                timerDeath += Time.deltaTime;
            }
            else
            { 
                _isDead = false;
                timerDeath = 0;
                if (_inputShield)
                {
                       
                                    _mesh.enabled = true;
                                    _collider.enabled = true; 
                                    Mirror.IsActiveReturnable = true;
                }
           
                Heal(maxHealth);
            }
        }
    }
    
}
