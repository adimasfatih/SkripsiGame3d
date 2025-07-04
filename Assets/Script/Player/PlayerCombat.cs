using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;
    Animator anim;
    [SerializeField] Weapon weapon;

    [SerializeField] private inputManager inputManager;

    private bool queuedNextAttack;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (inputManager._Attack)
        {
            if (!IsInAttackState())
            {
                PerformAttack();
            }
            else
            {
                queuedNextAttack = true;
            }
        }

        ExitAttack();
    }

    void PerformAttack()
    {
        if (Time.time - lastComboEnd > 0.2f && comboCounter <= combo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.2f)
            {
                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                anim.CrossFadeInFixedTime("Attack", 0.1f, 0);
                weapon.damage = combo[comboCounter].damage;

                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        var state = anim.GetCurrentAnimatorStateInfo(0);

        // Queue next attack if requested
        if (state.IsTag("Attack") && state.normalizedTime > 0.85f)
        {
            if (queuedNextAttack)
            {
                queuedNextAttack = false;
                PerformAttack(); // Chain next attack
            }
        }

        // Reset combo after finishing attack animation
        if (state.IsTag("Attack") && state.normalizedTime > 0.95f)
        {
            Invoke("EndCombo", 0.5f);
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }

    bool IsInAttackState()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
    }

    public void EnableWeaponHitbox()
    {
        weapon.EnableTriggerBox();
        //Debug.Log("HITBOX ENABLED");
    }

    public void DisableWeaponHitbox()
    {
        weapon.DisableTriggerBox();
        //Debug.Log("HITBOX DISABLED");
    }

}
