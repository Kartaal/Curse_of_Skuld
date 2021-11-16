using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    private static StaminaManager _instance;
    public static StaminaManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] 
    Slider staminaBar;

    private Coroutine _increaseStaminaCoroutine= null;
    private Coroutine _decreaseStaminaCoroutine = null;
   
    
    [Range(0,1)] 
    public float maxStamina=1;
    public float currentStamina;
    [SerializeField] private float increasingStaminaTime;
    [SerializeField] private float increaseingStaminaAmount;
    [SerializeField] private float decreasingStaminaTime;
    [SerializeField] private float decreasingStaminaAmount;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void Start()
    {
        staminaBar.value = maxStamina;
        currentStamina = maxStamina;
    }

    // public void Update()
    // {
    //     print(currentStamina);
    // }

    public void StartDecreaseStamina()
    {
        if(_increaseStaminaCoroutine !=null)
            StopCoroutine(_increaseStaminaCoroutine);
        _decreaseStaminaCoroutine = StartCoroutine(DecreaseStaminaOverTime(decreasingStaminaAmount));
    }

    public void StopDecreasingStamina()
    {
        if(_decreaseStaminaCoroutine!=null)
            StopCoroutine(_decreaseStaminaCoroutine);
        _increaseStaminaCoroutine = StartCoroutine(IncreaseStaminaOverTime(increaseingStaminaAmount));
    }

    private IEnumerator DecreaseStaminaOverTime(float amount)
    {
        while (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;
            yield return new WaitForSeconds(decreasingStaminaTime);
        }

        currentStamina = 0;
        _decreaseStaminaCoroutine = null;
    }
    private IEnumerator IncreaseStaminaOverTime(float amount)
    {
        while (currentStamina + amount <= maxStamina)
        {
            currentStamina += amount;
            staminaBar.value = currentStamina;
            yield return new WaitForSeconds(increasingStaminaTime);
        }

        currentStamina = maxStamina;
        _increaseStaminaCoroutine = null;
    }
}
