using ReflectionFactory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatManager_Auto : MonoBehaviour
{
    public EntityManager EntityManager;

    public Invoker Invoker;


    private FightCommandTypes _chosenType;
    private CommandFactory _factory;
    private FightCommand _currentCommand;

    int _round;
    // Start is called before the first frame update
    void Start()
    {
        _factory = new CommandFactory();
        Sensitivity();
    }
    int[] winA = new int[20];
    int[] winB = new int[20];
    int iteration = 0;

    int[] _hp = new int[20];

    void Sensitivity()
    {
        for (int i = 0; i < 20; i++)
        {
            _hp[i] = i * 10;
            RepeatBattle();
            iteration++;
        }
        WriteFile();
    }

    void WriteFile()
    {
        string path = "Assets/test.txt";

        //Write some text to the test.txt file

        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine("\nNew Battle!");

        for (int i = 0; i < 20; i++)
        {
            writer.WriteLine("Variable: HP ->" + _hp[i] + "; Win A: " + winA[i] + "; Win B: " + winB[i]);
        }

        writer.Close();

        Debug.Log("All Writted");
    }

    void RepeatBattle()
    {


        int nSimulations = 10;
        for (int i = 0; i < nSimulations; i++)
        {
            StartBattle();
        }
        //Debug.Log("WinA: " + winA + "  WinB: " + winB);
    }

    void StartBattle()
    {
        SetParameters();
        DoAllTurns();
    }

    void SetParameters()
    {
        foreach (var fighter in EntityManager.AllFighters)
        {
            fighter.SetParameters((int)fighter.MaxHealth, (int)fighter.BaseAttack, (int)fighter.BaseDefense);
        }

        EntityManager.AllFighters[1].SetParameters(_hp[iteration], (int)EntityManager.AllFighters[1].BaseAttack, (int)EntityManager.AllFighters[1].BaseDefense);
    }

    void DoAllTurns()
    {
        Team winner = Team.None;
        while (winner == Team.None)
        {
            DoOneTurn();
            winner = GetWinner();
            //Debug.Log("Round: " + ++_round);
        }
        //Debug.Log("Winner is: " + winner);
        if (winner == Team.TeamA)
            winA[iteration]++;
        if (winner == Team.TeamB)
            winB[iteration]++;

    }

    private Team GetWinner()
    {
        return EntityManager.GetWinner();
    }

    void DoOneTurn()
    {
        // ---------------------------------------------------------------------- Ability Test
        int rdm = Random.Range(0, 20);
        if(rdm >= 10 && rdm <= 11)
        {
            if(EntityManager.ActiveFighter.CurrentAmmo <= 0)
            {
                DoAction(FightCommandTypes.Reload);

                Debug.Log("Reloaded!");

            }
            else
            {
                EntityManager.ActiveFighter.BaseAttack *= 1.1f;
                _currentCommand = GetCommand();
                var target = ChooseTarget(_currentCommand);
                _currentCommand.SetFighters(EntityManager.ActiveFighter, target);
                _currentCommand.Excecute();
                EntityManager.ActiveFighter.ResetFighter();
                EntityManager.SetNextEntity();

                Debug.Log("Attack *= 1.1f");

            }

        }
        else if(rdm >= 4 && rdm <= 16)
        {
            if (EntityManager.ActiveFighter.CurrentAmmo <= 0)
            {
                DoAction(FightCommandTypes.Reload);

                Debug.Log("Reloaded!");

            }
            else
            {
                _currentCommand = GetCommand();
                var target = ChooseTarget(_currentCommand);
                _currentCommand.SetFighters(EntityManager.ActiveFighter, target);
                _currentCommand.Excecute();
                EntityManager.ActiveFighter.ResetFighter();
                EntityManager.SetNextEntity();

                Debug.Log("Normal Attack");

            }
        }
        else
        {
            Debug.Log("Failed!");
        }
    }

    FightCommand GetCommand()
    {
        var possibleCommandsList = ((Fighter)EntityManager.ActiveEntity).PossibleCommands;
        if (possibleCommandsList.Count == 0)
            return null;

        int rdm = Random.Range(0, possibleCommandsList.Count);
        return _factory.GetCommand(possibleCommandsList[rdm]);
    }

    public void DoAction(FightCommandTypes commandType)
    {
        _chosenType = commandType;
        _currentCommand = _factory.GetCommand(_chosenType);

        ChooseTarget(_currentCommand);
        //DoAction(EntityManager.ActiveEntity, EntityManager.OtherEntity, type);
        //NextTurn();

    }



    private Fighter ChooseTarget(FightCommand _currentCommand)
    {
        var targetTypes = _currentCommand.PossibleTargets;

        Entity[] possibleTargets;

        switch (targetTypes)
        {
            case TargetTypes.Enemy:
                possibleTargets = EntityManager.Enemies;
                break;
            case TargetTypes.Friend:
                possibleTargets = EntityManager.Friends;
                break;
            case TargetTypes.FriendNotSelf:
                possibleTargets = EntityManager.FriendsNotSelf;
                break;
            case TargetTypes.Self:
                possibleTargets = new Entity[1];
                possibleTargets[0] = EntityManager.ActiveEntity;
                break;

            default:
                possibleTargets = EntityManager.Enemies;
                break;
        }

        if (possibleTargets.Length == 0)
            return null;

        int rdm = Random.Range(0, possibleTargets.Length);
        return possibleTargets[rdm] as Fighter;
        //ActionButtonController.ChooseTarget(EntityManager.ActiveEntity);
        //TargetChooser.StartChoose(possibleTargets);
    }




}
