

using System.Collections;
using UnityEngine;



public class Entity : MonoBehaviour
{
    public Entity opponent;
    public AttackSwooshScript attackScript;
    public bool isAlive = true;
    public bool hasAttacked;
    public int health;
    public int damage;
    public int defence;
    public int dodgeChance;
    public int criticalChance;
    public int advantage;

    public int wins;

    public struct StartingStats
    {
        public bool IsAlive;
        public int Health;
    }

    public StartingStats stats;

    private void Start()
    {
        attackScript = GetComponentInChildren<AttackSwooshScript>();
        stats.Health = health;
        stats.IsAlive = true;
    }

    public virtual void Critical()
    {
        print(name +": Critical Hit!");
        StartCoroutine(opponent.takeDamage(3*damage));
    }

    public IEnumerator takeDamage(int damageDealt)
    {
        health -= damageDealt - defence;
        print(name + " took " + (damageDealt - defence) + " Damage");
        if (IsDead())
        {
            isAlive = false;
            print(name + " Was Killed");
            opponent.wins++;
        }

        yield return new WaitForSeconds(2);
    }

    

    public IEnumerator  WaitForAtackAnim()
    {
        attackScript.attacking = true;
        yield return new WaitForSeconds(attackScript.attackingTime);
    }
    

    public bool IsDead()
    {
        if (health <= 0)
        {
            return true;
        }

        return false;
    }
}
