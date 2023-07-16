using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIMovement : MonoBehaviour
{   
    public float moveSpeed;
    public float waitCounter;
    public float walkCounter;
    float WalkDirection;
    float walkTime;
    float waitTime;
    Animator anim;
    Vector3 stopPosition;
    public bool isWalking;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        walkTime = Random.Range(3, 6);
        waitTime = Random.Range(5, 7);

        waitCounter = waitTime;
        walkCounter = walkTime;
        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            anim.SetBool("walk",true);
            walkCounter -= Time.deltaTime;
            switch(WalkDirection){
                case 0:
                transform.DORotate(new Vector3(0f,0f,0f),1f) ;
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                break;
                case 1:
                transform.DORotate(new Vector3(0f,90f,0f),1f);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                break;
                case 2:
                transform.DORotate(new Vector3(0f,-90f,0f),1f);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                break;
                case 3:
                transform.DORotate(new Vector3(0f,180f,0f),1f);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                break;



            }
            if (walkCounter <= 0)
            {
                stopPosition = new Vector3(transform.position.x , transform.position.y,transform.position.z);
                isWalking = false;

                transform.position = stopPosition;
                anim.SetBool("walk",false);
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);

        isWalking = true;
        walkCounter = walkTime;
    }
}
