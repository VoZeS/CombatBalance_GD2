using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonController : MonoBehaviour
{
   // public Color[] Colors;
    public ActionButton ActionButtonPrefab;

    public List<FightCommandTypes> PossibleCommands;

    public CombatManager CombatManager;

    private List<GameObject> CurrentButtons;

    private CanvasGroup _canvasGroup;

    //public CubeColor Cube;

    public delegate void ChangeColorDelegate(Color color);
    public static ChangeColorDelegate OnChangeColor;
    // Start is called before the first frame update
    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void MakeActionButtons()
    {
        ClearButtons();
        for (int i = 0; i < PossibleCommands.Count; i++)
        {
            CurrentButtons.Add( MakeOneActionButton(i));
        }
    }

    internal void ChooseTarget(Entity activeEntity)
    {
        Hide();
    }

    void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }

    void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }

    private void ClearButtons()
    {
        if (CurrentButtons == null)
            CurrentButtons = new List<GameObject>();

        foreach (var button in CurrentButtons)
        {
            Destroy(button);
        }
        CurrentButtons.Clear();
    }

    internal void NewTurn(Entity activeEntity)
    {
        PossibleCommands.Clear();
        if (activeEntity is Fighter)
        {
            var fighter = activeEntity as Fighter;
            foreach (var possibleCommand in fighter.PossibleCommands)
            {
                PossibleCommands.Add(possibleCommand);
            }
            MakeActionButtons();
            Show();
        }
        else Debug.LogError("Entity is not a fighter");
        
    }

    private GameObject MakeOneActionButton(int i)
    {
        var actionButton = Instantiate(ActionButtonPrefab) as ActionButton;
        actionButton.transform.SetParent(transform);
        actionButton.Init(PossibleCommands[i], this);

        return actionButton.gameObject;
    }

    public void OnButtonPressed(FightCommandTypes fightCommandType)
    {
        CombatManager.DoAction(fightCommandType);
        //OnChangeColor?.Invoke(color);
        //Cube.SetColor(color); 
    }
}
