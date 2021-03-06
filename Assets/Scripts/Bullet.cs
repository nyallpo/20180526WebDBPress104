﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private bool isRender = false;

    private Vector3 direct = Vector3.forward;
    private float speed = 20f;
    private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnWillRenderObject()
    {
        Camera cam = Camera.current;
        isRender |= (cam.cameraType == CameraType.Game);
    }

    // Update is called once per frame
    void Update () {
	}

    // 弾を表示させる
    public void Shoot(Vector3 pos, Quaternion rot, float spd)
    {
        direct = rot * Vector3.up;
        speed = spd;
        transform.SetPositionAndRotation(pos, rot);
        isRender = true;
        gameObject.SetActive(true);
    }

    // 弾を動かす
    public bool Run(int index, float elapsedTime)
    {
        var move = direct * (speed * elapsedTime);
        transform.Translate(move, Space.World);

        renderer.sortingOrder = index;

        var ret = isRender;
        isRender = false;
        return ret;
    }

    // 弾を非表示にする
    public void Vanish()
    {
        isRender = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        isRender = false;
        gameObject.SetActive(false);
    }
}
