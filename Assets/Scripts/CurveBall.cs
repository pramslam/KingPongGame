using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using DLO;


class CurveBall : Ball
{
    public AnimationCurve curveX;
    public AnimationCurve curveY;
    public float AnimationPeriod = 1;
    public float AnimationAmplitude = 1;

    private float timer = 0;

    public override void Update()
    {
        if (!base.isPaused)
        {
            timer += Time.deltaTime;
            timer = timer % AnimationPeriod;

            float ValueX = curveX.Evaluate(timer) * AnimationAmplitude;
            float ValueY = curveY.Evaluate(timer) * AnimationAmplitude;
            Vector3 dir = rigidBody.velocity.normalized;
           // base.rigidBody.velocity += new Vector2(ValueX, ValueY);
            dir = dir * ValueX + Vector3.Cross(dir, new Vector3(0, 0, 1)) * ValueY;
            Vector2 dir2d = dir;
            base.rigidBody.velocity += dir2d;
        }
    }
}

