using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour, Damageable
{
    #region Variables

    [SerializeField] private ShieldManager _shield;
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
    [SerializeField] private float _blockedKnockbackStrength;
    [SerializeField] private float hitDuration;
    public bool isHit = false;
    [SerializeField] private float _drag;
    [SerializeField] private float _blockedDrag;
    public bool isInModule;
    public bool isInCutscene;
    public bool isBlocked;

    [SerializeField] float blockedDuration;
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

    private void Start()
    {
        for (int i = 0; i < obtainedModule.Count; i++)
        {
            Module module = obtainedModule[i];
            module.LinkModule();
            if (module.isFixedUpdate) PlayerController.instance.activeModulesFixed.Add(module);
            else PlayerController.instance.activeModulesUpdate.Add(module);
        }
    }

    public IEnumerator Hit(EnemyManager enemy)
    {
        yield return new WaitForFixedUpdate();
        if (enemy.isBlocked)
        {
            isBlocked = true;
            _shield.TakeDamage(1);
            KnockBack(enemy, _blockedDrag, _blockedKnockbackStrength);
            yield return new WaitForSeconds(blockedDuration);
            isBlocked = false;
            PlayerController.instance.playerRb.drag = 0;
            PlayerController.instance.playerRb.velocity = Vector3.zero;
            yield break;
        }
        
        isHit = true;
        KnockBack(enemy, _drag, knockbackStrength);
       int damage = enemy.damage;
        Debug.Log($"{enemy.name} a infligé {damage} dégâts");
        TakeDamage(damage);
        yield return new WaitForSeconds(hitDuration);
        PlayerController.instance.playerRb.drag = 0;
        PlayerController.instance.playerRb.velocity = Vector3.zero;

        isHit = false;
    }

    void KnockBack(EnemyManager enemy, float drag, float strengh)
    {
        PlayerController.instance.playerRb.velocity = Vector3.zero;
        
        Vector3 knockback = new Vector3(hitDirection.x, 0, hitDirection.z);
        knockback.Normalize();
        knockback *= strengh*enemy.Machine.PlayerKnockBackFactor;
        
        Debug.DrawRay(transform.position, knockback, Color.yellow, 1f);

        PlayerController.instance.playerRb.AddForce(knockback);
        PlayerController.instance.playerRb.drag = drag;
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
        if (other.CompareTag("Enemy") && !isHit && !isBlocked)
        {
            EnemyManager enemy = other.GetComponentInParent<EnemyManager>();
            
            hitDirection = transform.position - enemy.transform.position;
            Debug.DrawRay(transform.position, hitDirection, Color.magenta, 1);
            StartCoroutine(Hit(enemy));
        }
        if (other.CompareTag("EventTrigger"))
        {
            other.gameObject.GetComponent<ListenerTrigger>().Raise();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EventTriggerStay"))
        {
            other.gameObject.GetComponent<ListenerTriggerStay>().Raise();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EventTriggerStay"))
        {
            other.gameObject.GetComponent<ListenerTriggerStay>().EndRaise();
        }
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
