using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class moveObject : MainClass
{
    [SerializeField] private Vector3 movePosition;
    [SerializeField] private float moveSpeed;
    [SerializeField] [Range(0, 1)] private float moveProgress;
    private Vector3 startPosition;

    protected override void Start()
    {
        startPosition = transform.position;
    }

    protected override void Update()
    {
        moveProgress = Mathf.PingPong(Time.time * moveSpeed, 1);

        Vector3 offset = movePosition * moveProgress;
        transform.position = startPosition + offset;
    }
}
