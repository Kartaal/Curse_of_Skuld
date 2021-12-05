using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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
    private Coroutine _punishmentCoolDown = null;
    
    [Range(0,1)] 
    public float maxStamina=1;

    public float DebugVignette;
    public float currentStamina;
    public Volume postProcessing;
    [SerializeField] private float increasingStaminaTime;
    [SerializeField] private float increaseingStaminaAmount;
    [SerializeField] private float decreasingStaminaTime;
    [SerializeField] private float decreasingStaminaAmount;
    [SerializeField] private float punishmentCoolDownTime;
    [SerializeField] private float maxVignette;
    [SerializeField] private float minVignette;

    private Vignette _vignette;
    private bool _punishmentState;
    private bool _canIncrease;
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
        postProcessing.profile.TryGet<Vignette>(out _vignette);
        _vignette.intensity.value = 0.2f;
    }

    public void Start()
    {
        if(staminaBar!=null)
        staminaBar.value = 1-maxStamina;
        currentStamina = maxStamina;
        
        _punishmentState = false;
        _canIncrease = true;
        //change the initilazation in the awake too 
        _vignette.intensity.value = 0.2f;
    }

    public void Update()
    {
        //for debuging purposes only 
        DebugVignette = _vignette.intensity.value;
      //  print(_punishmentState);
    }

    public void StartDecreaseStamina()
    {
        _canIncrease = false;
        if(_increaseStaminaCoroutine !=null)
            StopCoroutine(_increaseStaminaCoroutine);
        if (!_punishmentState)
        {
            // if(_increaseStaminaCoroutine !=null)
            //     StopCoroutine(IncreaseStaminaOverTime(0));
            _decreaseStaminaCoroutine = StartCoroutine(DecreaseStaminaOverTime(decreasingStaminaAmount));
        }
    }

    public void StopDecreasingStamina()
    {
        _canIncrease = true;
        if(_decreaseStaminaCoroutine!=null)
            StopCoroutine(_decreaseStaminaCoroutine);
        if (currentStamina>0 )
        {       
            // print("ss");
            // StopCoroutine(_punishmentCoolDown);
           //if(_increaseStaminaCoroutine==null)
                _increaseStaminaCoroutine = StartCoroutine(IncreaseStaminaOverTime(increaseingStaminaAmount));
        }  else
        {
            _punishmentState = true;
            if(_punishmentCoolDown==null)
                _punishmentCoolDown = StartCoroutine(PunishmentCoolDown(punishmentCoolDownTime));
        } 
    }

    private IEnumerator DecreaseStaminaOverTime(float amount)
    {
        while (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            // _vignette.intensity.value =math.lerp(_vignette.intensity.value,maxVignette,decreasingStaminaTime);
            _vignette.intensity.value =1-((maxVignette - minVignette) * currentStamina + minVignette);
            staminaBar.value = 1-currentStamina;
            yield return new WaitForSeconds(decreasingStaminaTime);
        }

        currentStamina = 0;
        _decreaseStaminaCoroutine = null;
    }
    private IEnumerator IncreaseStaminaOverTime(float amount)
    {
        while (currentStamina + amount <= maxStamina &&_canIncrease)
        {
            currentStamina += amount;
            // print("increasing");
            // _vignette.intensity.value =math.lerp(_vignette.intensity.value,minVignette,increasingStaminaTime);
            _vignette.intensity.value = 1-((maxVignette - minVignette) * currentStamina + minVignette);
            staminaBar.value = 1-currentStamina;
            yield return new WaitForSeconds(increasingStaminaTime);
        }

        currentStamina = maxStamina;
        _increaseStaminaCoroutine = null;
    }

    private IEnumerator PunishmentCoolDown(float waitingTime)
    {
        //print("cooolDown");
        yield return new WaitForSeconds(waitingTime); 
        _increaseStaminaCoroutine = StartCoroutine(IncreaseStaminaOverTime(increaseingStaminaAmount));
        _punishmentState = false;
        _punishmentCoolDown = null;
    }
}
