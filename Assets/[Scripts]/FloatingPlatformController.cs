using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FloatingPlatformController : MonoBehaviour
{
    public Transform mPlatform4Start;
    public Transform mPlatform4End;
    public float fTimer;
    public bool bIsActive;

    public AudioSource[] mSFX;

    private Vector3 vDistance;
    private Vector3 vStartingScale;
    private Vector3 vTargetScale;

    private void Start()
    {

        bIsActive = false;
        vStartingScale = Vector3.one;
        vTargetScale = Vector3.zero;
        vDistance = mPlatform4End.position - mPlatform4Start.position;
        mSFX = GetComponents<AudioSource>();
    }

    private void Update()
    {
        fTimer += Time.deltaTime;
        FloatPlatform();
        if (bIsActive)
        {
            ShrinkPlatform();
        }
        else
        {
            ResetPlatform();
        }

        Debug.Log(bIsActive);
    }

    private void FloatPlatform()
    {
        float fDistanceY = 0;
        if (mPlatform4Start.position.y < mPlatform4End.position.y)
        {
            fDistanceY = mPlatform4Start.position.y + Mathf.PingPong(fTimer * 0.2f, vDistance.y);
        }
        transform.position = new Vector3(mPlatform4Start.position.x, fDistanceY, 0.0f);
    }

    private void ShrinkPlatform()
    {
        mSFX[0].Play();
        transform.localScale -= Vector3.one * (Time.deltaTime * 0.5f);
        //float fShrinkTimer = 0;
        //fShrinkTimer += Time.deltaTime;
        //Vector3 vCurrentScale = Vector3.Lerp(vStartingScale, vTargetScale, Time.deltaTime * 0.5f);
        //transform.localScale = vCurrentScale;
        //if (transform.localScale == vTargetScale)
        //{
        //    bIsActive = false;
        //    if (!bIsActive)
        //    {
        //        ResetPlatform();
        //    }
        //}
    }

    private void ResetPlatform()
    {
        mSFX[1].Play();
        Vector3 vCurrentScale = Vector3.Lerp(transform.localScale, vStartingScale, Time.deltaTime * 0.5f);
        transform.localScale = vCurrentScale;
    }
}