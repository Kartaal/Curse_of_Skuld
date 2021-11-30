using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float coolDownTime = 2;

    private bool _canAttack = true;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null && _canAttack)
        {
            anim.SetTrigger("Attack");
            _canAttack = false;
            Invoke("SetCoolDown", coolDownTime);
        }
    }

    void SetCoolDown() => _canAttack = true;
}
