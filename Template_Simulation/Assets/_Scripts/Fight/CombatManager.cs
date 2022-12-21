using ReflectionFactory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EntityManager EntityManager;
    public ActionButtonController ActionButtonController;
    public ChooseTarget TargetChooser;
    public Invoker Invoker;
    public StatsUI Stats;

    private FightCommandTypes _chosenType;
    private CommandFactory _factory;
    private FightCommand _currentCommand;

    // Start is called before the first frame update
    void Start()
    {
        _factory = new CommandFactory();
        StartBattle();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            Undo();
    }

    void StartBattle()
    {
        ActionButtonController.NewTurn(EntityManager.ActiveEntity);
        Stats.SetEntity((Fighter)EntityManager.ActiveEntity);
    }

    public void DoAction(FightCommandTypes commandType)
    {
        _chosenType = commandType;
        _currentCommand = _factory.GetCommand(_chosenType);

        ChooseTarget(_currentCommand);
        //DoAction(EntityManager.ActiveEntity, EntityManager.OtherEntity, type);
        //NextTurn();

    }

    private void ChooseTarget(FightCommand _currentCommand)
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
        ActionButtonController.ChooseTarget(EntityManager.ActiveEntity);
        TargetChooser.StartChoose(possibleTargets);
    }

    private void DoAction(Entity actor, Entity target, FightCommandTypes type)
    {
        var command = _factory.GetCommand(_chosenType);
        command.SetFighters((Fighter)actor, (Fighter)target);
        Invoker.AddCommand(command);
        NextTurn();
    }

    private void Undo()
    {
        if (!Invoker.CanUndo())
            return;
        Invoker.Undo();
        EntityManager.SetPreviousEntity();
        ActionButtonController.NewTurn(EntityManager.ActiveEntity);
        Stats.SetEntity((Fighter)EntityManager.ActiveEntity);
    }


    public void NextTurn()
    {
        ((Fighter)(EntityManager.ActiveEntity)).ResetFighter();
        EntityManager.SetNextEntity();
        
        ActionButtonController.NewTurn(EntityManager.ActiveEntity);
        Stats.SetEntity((Fighter)EntityManager.ActiveEntity);
    }

    internal void TargetChosen(ISelectable entity)
    {
        if(!(entity is Entity))
        {
            Debug.LogError("Selected is not entity");
            return;
        }
        DoAction(EntityManager.ActiveEntity, (Entity)entity, _chosenType);
    }
}
