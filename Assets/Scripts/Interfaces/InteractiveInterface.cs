using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnClick();
    void OnDeselected();
    void OnHoverEnter();
    void OnHoverExit();
}
