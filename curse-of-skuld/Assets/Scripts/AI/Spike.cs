using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    private float speed;
    
    private bool _shouldMove = false;
    private Animator _animator;
    private void OnTriggerEnter(Collider other)
    {
        var playercController = other.GetComponent<PlayerController>();
        if (playercController!=null)
        {
            playercController.Die();
        }
    }
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Speed",speed);
    }

    private void Update()
    {

        if (_shouldMove)
        {
            //animation in untiy 
            
            _animator.SetBool("Attack",true);
            
            
            
            //animation in code
            
            
            // _time += Time.deltaTime;
            // this.transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime,
            //     transform.position.z);
            // if (_time > _destroyTime)
            // {
            //     print("destroyed");
            //     Destroy(this.gameObject);
            //     
            // }
                
        }

        if (!_shouldMove)
        {
            
            _animator.SetBool("Attack", false);
        }
    }       

    //these two might be merged later 
    public void Move()
    {
        _shouldMove = true;
    }

    public void Stop()
    {
        _shouldMove = false;
    }
}
