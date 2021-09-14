using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTitle : MonoBehaviour
{
    public RectTransform targetTransform;
    public Vector3 targetPosition;
    public float initialWait;
    public float speed = 1f;
    bool moving = false;

    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.localPosition;
        StartCoroutine(startTimer());
    }

    IEnumerator startTimer () {
        moving = false;
        yield return new WaitForSeconds(initialWait);
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving) return;
        Vector3 from = targetTransform.localPosition;
        Vector3 to = targetPosition;

        targetTransform.localPosition = Vector3.Lerp (from, to, Time.deltaTime * speed);

        if (Vector3.Distance (from, to) < 0.02) {
            moving = false;
            gameObject.SetActive(false);
        }
    }
}
