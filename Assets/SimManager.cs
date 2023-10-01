
using UnityEngine;

public class SimManager : MonoBehaviour
{
    private Entity _currentEntity;

    public Entity knight;

    public Entity dragon;

    public int numberOfIterations= 10;
    
    private int _currentIteration;

    private int _totalRounds;

    private bool _firstRoundAttack;

    private void NewRound()
    {
        int chance = Random.Range(0, 100);
        print(chance);
        _currentEntity = chance <= 50 + knight.advantage - dragon.advantage ? knight : dragon;
        
        //  < a ? b : c >  if a is true then use b otherwise use c
        // if random is less than or equal to 50 set current entity to knight 
        // otherwise set current entity to dragon
        
        _firstRoundAttack = true; // if it is currently the first half of the current round 
        _totalRounds += 1;

        knight.hasAttacked = false;
        dragon.hasAttacked = false;
    }

    private void NewIteration()
    {
        
        dragon.health = dragon.stats.Health;
        dragon.isAlive = dragon.stats.IsAlive;
        
        
        knight.health = knight.stats.Health;
        knight.isAlive = knight.stats.IsAlive;

        _totalRounds = 0;
    }


    void Update()
    {
        if (numberOfIterations > _currentIteration)
        {
            if (_currentEntity != null && _currentEntity.attackScript.attacking)
            {
                return;
            }

            if (!knight.isAlive || !dragon.isAlive)
            {
                print("Fight Over : " + _totalRounds + " Rounds Survived");
                _currentIteration++;
                NewIteration();
            }
            else
            {
                //round Start
                if (!_firstRoundAttack)
                {
                    NewRound();
                }
                else
                {
                    _currentEntity = _currentEntity == dragon ? knight : dragon;
                    _firstRoundAttack = false;
                }

                print(_currentEntity.name + " Attacks!");
                StartCoroutine(_currentEntity.WaitForAtackAnim());
                //Does enemy Dodge attack?
                if (Random.Range(0, 100) <= _currentEntity.opponent.dodgeChance)
                {
                    print(_currentEntity.opponent.name + " Dodged the attack!");

                }
                else
                {
                    //Do we deal critical damage, otherwise attack normally 
                    if (Random.Range(0, 100) <= _currentEntity.criticalChance)
                    {
                        _currentEntity.Critical();
                    }
                    else
                    {
                        StartCoroutine(_currentEntity.opponent.takeDamage(_currentEntity.damage));
                    }
                }

                _currentEntity.hasAttacked = true;
            }
        }
    }
}
