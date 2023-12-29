using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightFood : MonoBehaviour
{
    private float awaySpeed = 0.3f;
    private float moveSpeed = 5f;

    private void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * moveSpeed;
    }

    public void RightAway()
    {
        StartCoroutine(RightMove());
    }

    private IEnumerator RightMove()
    {

        transform.DOMove(new Vector3(11, 5, 0), awaySpeed);
        transform.DORotate(new Vector3(0, 0, -180), awaySpeed);
        yield return new WaitForSeconds(awaySpeed);
        Destroy(gameObject);
    }
}
