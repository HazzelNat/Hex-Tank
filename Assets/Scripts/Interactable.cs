using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface Interactable
{
    void OnClicked();
    void OnDeselected();
}
