using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [SerializeField] private List<Entity> Entities;
    private int _currentIndex;

    public Entity ActiveEntity => Entities[_currentIndex];

    public Fighter ActiveFighter => Entities[_currentIndex] as Fighter;

    //public Entity OtherEntity => Entities[++_currentIndex % Entities.Count];
    public Fighter[] AllFighters
    {
        get
        {
            List<Fighter> allAlive = new List<Fighter>();
            foreach (var other in Entities)
            {
                
                    allAlive.Add(other as Fighter);
            }
            return allAlive.ToArray();
        }
    }
    public Fighter[] AllAlive
    {
        get
        {
            List<Fighter> allAlive = new List<Fighter>();
            foreach (var other in Entities)
            {
                if (((Fighter)other).IsAlive)
                    allAlive.Add(other as Fighter);
            }
            return allAlive.ToArray();
        }
    }


    public Fighter[] Friends { 
        get 
        {
            List<Fighter> friends = new List<Fighter>();
            foreach (var other in Entities)
            {
                if (other.Team == ActiveEntity.Team)
                    friends.Add(other as Fighter);
            }
            return friends.ToArray();
        } 
    }
    public Fighter[] FriendsNotSelf
    {
        get
        {
            List<Fighter> friends = new List<Fighter>();
            foreach (var other in Entities)
            {
                if (other.Team == ActiveEntity.Team && other!=ActiveEntity)
                    friends.Add(other as Fighter);
            }
            return friends.ToArray();
        }
    }
    public Fighter[] Enemies
    {
        get
        {
            List<Fighter> friends = new List<Fighter>();
            foreach (var other in Entities)
            {
                if (other.Team != ActiveEntity.Team)
                    friends.Add(other as Fighter);
            }
            return friends.ToArray();
        }
    }

    public Fighter[] FriendsAlive => Friends.Where(x => x.IsAlive).ToArray();
    public Fighter[] EnemiesAlive => Enemies.Where(x => x.IsAlive).ToArray();

    public Fighter[] FriendsNotSelfAlive => FriendsNotSelf.Where(x => x.IsAlive).ToArray();

    public void SetNextEntity()
    {
        do
        {
            _currentIndex++;
            _currentIndex = _currentIndex % Entities.Count;
        } while (!ActiveFighter.IsAlive);
       
    }

    public void SetPreviousEntity()
    {
        do
        {
            _currentIndex--;
            if (_currentIndex < 0)
                _currentIndex = Entities.Count - 1;
        } while (!ActiveFighter.IsAlive);
    }

    public Team GetWinner()
    {
        int aliveA = AllAlive.Count(x => x.Team == Team.TeamA);
        int aliveB = AllAlive.Count(x => x.Team == Team.TeamB);

        if (aliveA > 0 && aliveB > 0)
            return Team.None;

        if (aliveA > 0)
            return Team.TeamA;
        if (aliveB > 0)
            return Team.TeamB;

        Debug.LogError("No team Alive");
        return Team.TeamC;

    }
}
