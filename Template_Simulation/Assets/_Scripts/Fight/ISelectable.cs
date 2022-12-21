using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable 
{
     void HighlightGood();
     void HighlightBad();

    void UnSelect();

    void ClickedGood();
     void ClickedBad();

}
