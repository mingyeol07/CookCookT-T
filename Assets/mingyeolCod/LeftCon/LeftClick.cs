using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeftClick : MonoBehaviour
{
    [SerializeField] private ChoiceDir choice;

    private void OnMouseDown()
    {
        Click();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F))
        {
            Click();
        }
    }

    private void Click()
    {
        choice.gameObject.GetComponent<Animator>().SetTrigger("IsClick");
        if (choice.food.Any())
        {
            choice.food[0].GetComponent<Plate>().InstantEffect();
            choice.food[0].GetComponent<LeftFood>().LeftAway();
            choice.food.RemoveAt(0);
        }
    }
}
