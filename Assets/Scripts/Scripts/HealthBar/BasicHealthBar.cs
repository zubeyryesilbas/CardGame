using System.Collections;
using PoolingSystem;
using UnityEngine;
using UnityEngine.UI;

public class BasicHealthBar : MonoBehaviour, IPooledObject
{
    [SerializeField] private Image _healthFill;
    [SerializeField] private Image _damageFill;

    private float _healthFillCurrentValue;
    private float _damageFillCurrentValue;
    private Coroutine _healthFillCoroutine;
    private Coroutine _damageFillCoroutine;

    public GameObject PoolObj => gameObject;

    public void SetValue(float end)
    {
        Debug.Log("Health bar set value: " + end);

        // Stop any existing coroutines to prevent overlapping animations
        if (_healthFillCoroutine != null)
            StopCoroutine(_healthFillCoroutine);

        if (_damageFillCoroutine != null)
            StopCoroutine(_damageFillCoroutine);

        // Start health fill animation
        _healthFillCoroutine = StartCoroutine(ChangeValueOverTime(_healthFill.fillAmount, end, 0.1f));

        // Start damage fill animation after a delay
        _damageFillCoroutine = StartCoroutine(DamageBarCo(_damageFill.fillAmount, end, 1f, 0.7f));
    }

    private IEnumerator ChangeValueOverTime(float startValue, float targetValue, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            _healthFillCurrentValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            _healthFill.fillAmount = _healthFillCurrentValue; // Update the UI fill for health
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the final value is set correctly
        _healthFill.fillAmount = targetValue;
    }

    private IEnumerator DamageBarCo(float startValue, float targetValue, float delay, float duration)
    {
        // Wait for the delay before starting the damage fill animation
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            _damageFillCurrentValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            _damageFill.fillAmount = _damageFillCurrentValue; // Update the UI fill for damage
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final value is set correctly
        _damageFill.fillAmount = targetValue;
    }

    public void OnGetFromPool()
    {
        // Reset the fills when the object is reused from the pool
        _healthFill.fillAmount = 1f;
        _damageFill.fillAmount = 1f;
    }

    public void OnReturnToPool()
    {
        // You can add any cleanup logic here when returning the object to the pool
    }

    public PoolType PoolType { get; }
}
