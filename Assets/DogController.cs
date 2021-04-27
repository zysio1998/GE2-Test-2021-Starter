using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DogController : MonoBehaviour
{
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new ComeToPlayer());
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class ComeToPlayer : State
{
    public override void Enter()
    {
        owner.GetComponent<Seek>().targetGameObject = owner.GetComponent<Boid>().player;
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.GetComponent<Boid>().player.transform.position, owner.transform.position) < 10) //10 units away
        {
            owner.GetComponent<Boid>().ball.transform.parent = null;
            owner.ChangeState(new DogLooksAtPlayer());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
    }
}

public class DogLooksAtPlayer : State
{
    public override void Enter()
    {
        owner.GetComponent<Boid>().acceleration = Vector3.zero;
        owner.GetComponent<Boid>().velocity = Vector3.zero;
        owner.GetComponent<Seek>().enabled = false;  
    }

    public override void Think()
    {
        Vector3 distance = owner.GetComponent<Boid>().player.transform.position - owner.transform.position;
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, Quaternion.LookRotation(distance), Time.deltaTime);

        if (Vector3.Distance(owner.GetComponent<Boid>().ball.transform.position, owner.GetComponent<Boid>().player.transform.position) > 10)
        {
            owner.ChangeState(new FetchTheBall());
        }
    }

    public override void Exit()
    {
    }
}

public class FetchTheBall : State
{
    public override void Enter()
    {
        AudioSource music = owner.GetComponent<AudioSource>();
        music.Play();

        owner.GetComponent<Seek>().targetGameObject = owner.GetComponent<Boid>().ball;
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(owner.GetComponent<Boid>().ball.transform.position, owner.transform.position) < 2.5f) //2 unit away from the ball
        {
            owner.ChangeState(new ComeToPlayer());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().enabled = false;
        owner.GetComponent<Boid>().ball.transform.parent = owner.transform;
        owner.GetComponent<Boid>().ball.transform.position = owner.GetComponent<Boid>().attachPoint.position;
    }
}





