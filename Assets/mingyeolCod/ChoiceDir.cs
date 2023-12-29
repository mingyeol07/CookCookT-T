using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDir : MonoBehaviour
{
    public List<GameObject> food = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Plate"))
        {
            food.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plate"))
        {
            collision.GetComponent<SpriteRenderer>().color = Color.white;
            food.Remove(collision.gameObject);
        }
    }
}
