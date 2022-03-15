using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, Damageable
{
    #region Variables

    public List<Module> obtainedModule;
    public int money;
    public bool hasGlitch;

    public int health
    {
        get => healthPlayer;
        set => healthPlayer = value;
    }
    public int maxHealth
    {
        get => maxHealthPlayer;
        set => maxHealthPlayer = value;
    }
    public bool isDead 
    { 
        get => isDeadPlayer;
        set => isDeadPlayer = value; 
    }
    
    public int healthPlayer;
    public int maxHealthPlayer;
    public bool isDeadPlayer;

    [SerializeField] private float hitDuration;
    [SerializeField] private bool canBeHit = true;

    #endregion

    #region Instance

    public static PlayerManager instance;
    private void Awake()
    {
        if (instance is { })
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
    }

    #endregion

    #region Main Functions
    
    public IEnumerator Hit(EnemyManager enemy)
    {
        canBeHit = false;
        int damage = enemy.damage;
        Debug.Log($"{enemy.name} a infligé {damage} dégâts");

        TakeDamage(damage);

        yield return new WaitForSeconds(hitDuration);

        canBeHit = true;
    }
    
    public void TakeDamage(int damages)
    {
        health -= damages;
        if (health <= 0)
        {
            Death();
        }
    }

    public void Heal(int heal)
    {
        
    }

    public void Death()
    {
        Debug.Log("Player is dead");
    }

    #endregion

    #region Modules

    public void AddModule(Module mod)
    {
        mod.LinkModule();
        if (mod.isFixedUpdate) PlayerController.instance.activeModulesFixed.Add(mod);
        else PlayerController.instance.activeModulesUpdate.Add(mod);
        obtainedModule.Add(mod);
    }

    #endregion

    #region Trigger & Collision

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && canBeHit)
        {
            EnemyManager enemy = other.GetComponentInParent<EnemyManager>();
            StartCoroutine(Hit(enemy));
            
            //other.ClosestPoint()
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {

    }

    private void OnCollisionStay(Collision other)
    {
        
    }


    private void OnCollisionExit(Collision other)
    {
        
    }

    #endregion
    
}
