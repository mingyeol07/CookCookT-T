using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeftFood : MonoBehaviour
{
    private float awaySpeed = 0.3f;
    private float moveSpeed = 5f;

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * moveSpeed;
    }

    public void LeftAway()
    {
        StartCoroutine(LeftMove());
    }

    private IEnumerator LeftMove()
    {

        transform.DOMove(new Vector3(-11, 5, 0), awaySpeed);
        transform.DORotate(new Vector3(0, 0, 180), awaySpeed);
        yield return new WaitForSeconds(awaySpeed);
        Destroy(gameObject);
    }
}
