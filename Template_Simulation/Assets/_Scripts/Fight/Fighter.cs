using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Entity
{
    public float CurrentHealth;
    public float MaxHealth = 100;

    public float BaseDefense = 5;
    private float RoundDefense;
    public float Defense => BaseDefense + RoundDefense;

    public float InitialBaseAttack = 10;
    public float BaseAttack = 10;
    private float RoundAttack;
    public float Attack => BaseAttack + RoundAttack;

    private float BaseSpeed;
    private float RoundSpeed;
    public float Speed => BaseSpeed + RoundSpeed;

    public int CurrentAmmo;
    public int MaxAmmo = 2;

    public List<FightCommandTypes> PossibleCommands;

    public static Action OnChange;

    public bool IsAlive => CurrentHealth > 0;

    // Start is called before the first frame update
    void Awake()
    {
        CurrentHealth = MaxHealth;
    }



    public void TakeDamage(float damage)
    {
        float realDamage = damage - (BaseDefense + RoundDefense);
        realDamage = Mathf.Max(realDamage, 0);

        CurrentHealth -= realDamage;

        OnChange?.Invoke();
        if (CurrentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void AddDefense(float amount)
    {
        RoundDefense += amount;
        OnChange?.Invoke();
    }

    public void AddAttack(float amount)
    {
        RoundAttack += amount;
        OnChange?.Invoke();
    }

    public void AddDefensePermanent(float amount)
    {
        BaseDefense += amount;
        OnChange?.Invoke();
    }

    public void AddAttackPermanent(float amount)
    {
        BaseAttack += amount;
        OnChange?.Invoke();
    }

    public void AddSpeed(float amount)
    {
        RoundSpeed += amount;
        OnChange?.Invoke();
    }

    public void ResetFighter()
    {
        RoundDefense = 0;
        RoundAttack = 0;
        RoundSpeed = 0;
        OnChange?.Invoke();
    }

    public void SetParameters(float hp, float attack, float defense, int ammo)
    {
        MaxHealth = hp;
        CurrentHealth = MaxHealth;

        BaseAttack = attack;
        InitialBaseAttack = BaseAttack;
        BaseDefense = defense;

        MaxAmmo = ammo;
        CurrentAmmo = MaxAmmo;

        RoundDefense = 0;
        RoundAttack = 0;
        RoundSpeed = 0;
    }
}
