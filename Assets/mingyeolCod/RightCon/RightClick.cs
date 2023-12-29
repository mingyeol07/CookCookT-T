using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RightClick : MonoBehaviour
{
    [SerializeField] private ChoiceDir choice;
   
    private void OnMouseDown()
    {
        Click();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
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
            choice.food[0].GetComponent<RightFood>().RightAway();
            choice.food.RemoveAt(0);
        }
        
    }
}
