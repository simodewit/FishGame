using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInteractable : MonoBehaviour
{
    [Tooltip("The script placed on the table")]
    public Table table;
    [Tooltip("The state that the ui has to go in after interacting with this interactable")]
    public stateOfUI state;
    [Tooltip("The normal material of this object")]
    public Material baseMaterial;
    [Tooltip("The material if the object is selected")]
    public Material selectedMaterial;

    private Renderer renderer;

    public void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void IfHoveredEntered()
    {
        renderer.material = selectedMaterial;
    }

    public void IfHoveredExit()
    {
        renderer.material = baseMaterial;
    }

    public void TriggerInteractable()
    {
        table.state = state;
    }
}
