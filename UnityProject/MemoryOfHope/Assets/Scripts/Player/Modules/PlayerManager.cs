using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
    
    public Vector3 hitDirection;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private float hitDuration;
    public bool isHit = false;
    [SerializeField] private float drag;

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
        isHit = true;

        PlayerController.instance.playerRb.velocity = Vector3.zero;
        
        Vector3 knockback = new Vector3(hitDirection.x, 0, hitDirection.z);
        knockback.Normalize();
        knockback *= knockbackStrength;
        
        Debug.DrawRay(transform.position, knockback, Color.yellow, 1f);

        PlayerController.instance.playerRb.AddForce(knockback);
        PlayerController.instance.playerRb.drag = drag;

        int damage = enemy.damage;
        Debug.Log($"{enemy.name} a infligé {damage} dégâts");

        TakeDamage(damage);

        yield return new WaitForSeconds(hitDuration);

        PlayerController.instance.playerRb.drag = 0;
        PlayerController.instance.playerRb.velocity = Vector3.zero;

        isHit = false;
    }
    
    public void TakeDamage(int damages)
    {
        health -= damages;
        StartCoroutine(Feedbacks.instance.VignetteFeedbacks(.5f, Color.red));
        if (health <= 0)
        {
            health = 0;
            Death();
        }
        if(UIInstance.instance != null)
        UIInstance.instance.DisplayLife();
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
        if (other.CompareTag("Enemy") && !isHit)
        {
            EnemyManager enemy = other.GetComponentInParent<EnemyManager>();
            
            hitDirection = transform.position - enemy.transform.position;
            Debug.DrawRay(transform.position, hitDirection, Color.magenta, 1);
            StartCoroutine(Hit(enemy));
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
