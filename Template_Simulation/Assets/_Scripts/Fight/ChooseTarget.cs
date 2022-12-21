using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTarget : MonoBehaviour
{
    bool _choosing;
    private List<ISelectable> PossibleTargets;
    public CombatManager CombatManager;

    private ISelectable _selected;

    public static Action<Fighter> OnSelected;
    // Start is called before the first frame update
    public void StartChoose(ISelectable[] targets)
    {
        PossibleTargets = new List<ISelectable>();
        foreach (var target in targets)
        {
            PossibleTargets.Add(target);
        }
        _choosing = true;
    }

    public void StopChoose()
    {
        _choosing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_choosing)
            return;

        if(_selected!=null)
            _selected.UnSelect();
        var entity = FindEntity();
        if (entity!=null)
        {
            _selected = entity;
            TrySelect(entity);
            TryClick(entity);
        }
        else
        {
            _selected = null;
        }
        
    }

    //private void UnselectAll()
    //{
    //    foreach (var target in PossibleTargets)
    //    {
    //        target.UnSelect();
    //    }
    //}

    private void TryClick(ISelectable entity)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsSelectable(entity))
            {
                entity.ClickedGood();
                CombatManager.TargetChosen(entity);
                StopChoose();
            }
                
            else
                entity.ClickedBad();
        }
    }

    private void TrySelect(ISelectable entity)
    {
        if (IsSelectable(entity))
        {
            entity.HighlightGood();
            OnSelected?.Invoke((Fighter)entity);
        }
           
        else
            entity.HighlightBad();
    }

    bool IsSelectable(ISelectable entity)
    {
        return PossibleTargets.Contains(entity);
    }

    ISelectable FindEntity()
    {
        //find scene objects            
        RaycastHit2D hitData = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if (hitData)
        {
            var target = hitData.collider.GetComponent<ISelectable>();
           
            if (target != null)
            {
                return target;
            }
        }
        return null;
    }
}
