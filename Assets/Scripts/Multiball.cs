using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using DLO;
class Multiball : Ball
{
    /*
    public GameObject prefab;

    private List<Ball> ManagedBalls = new List<Ball>();

    public int MaxBalls = 5;

    public override void SwitchPaused()
    {
        // Pause
        if (!isPaused)
        {
            isPaused = true;
            pausedVector = rigidBody.velocity;
            rigidBody.velocity = Vector2.zero;
        }
        // Unpause
        else if (isPaused)
        {
            isPaused = false;
            rigidBody.velocity = pausedVector;
        }

        foreach(Ball b in ManagedBalls)
        {
            b.SwitchPaused();
        }
    }


    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        
        for(int i = ManagedBalls.Count - 1; i >= 0; i --)
        {
            if (ManagedBalls[i] == null)
            {
                ManagedBalls.RemoveAt(i);
            }
        }

        // Hit the left paddle
        if (col.gameObject == leftPaddle)
        {
            // Calculate hit Factor
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(1, y).normalized;

            // Set Velocity with dir * speed
            rigidBody.velocity = dir * speed;

            // Play bounce sound
            gameManager.PlayBounce();

            if(ManagedBalls.Count < MaxBalls)ServeNewBall();
        }

        // Hit the right paddle
        if (col.gameObject == rightPaddle)
        {
            // Calculate hit Factor
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(-1, y).normalized;

            // Set Velocity with dir * speed
            rigidBody.velocity = dir * speed;

            // Play bounce sound
            gameManager.PlayBounce();

            if (ManagedBalls.Count < MaxBalls) ServeNewBall();
        }
    }

    void ServeNewBall()
    {
        GameObject ballGO = Instantiate(prefab) as GameObject;
        Ball ball = ballGO.GetComponent<Ball>();
        ManagedBalls.Add(ball);

        Vector3 dir = base.rigidBody.velocity.normalized;
        
        
            ball.transform.position = transform.position + dir * 2;
            ball.rigidBody.velocity = rigidBody.velocity;
        
    }

    public override void PauseBall()
    {
        // Pause
        if (!isPaused)
        {
            isPaused = true;
            pausedVector = rigidBody.velocity;
            rigidBody.velocity = Vector2.zero;
        }
        foreach (Ball b in ManagedBalls)
        {
            b.PauseBall();
        }
    }

    public override void UnPauseBall()
    {
        // Unpause
        if (isPaused)
        {
            isPaused = false;
            rigidBody.velocity = pausedVector;
        }
        foreach (Ball b in ManagedBalls)
        {
            b.UnPauseBall();
        }
    }

    public override void DoDisable()
    {
        foreach (Ball b in ManagedBalls)
        {
            Destroy(b.gameObject);
        }
        ManagedBalls.Clear();
    }
    */
}

