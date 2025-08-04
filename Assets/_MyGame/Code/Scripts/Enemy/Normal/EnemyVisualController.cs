
using System;
using System.Collections;
using UnityEngine;

public class EnemyVisualController : MonoBehaviour
{
    [SerializeField] private GameObject damagePopup;
    private EnemyBase _enemyBase;
    private SpriteRenderer _sr;
    private Coroutine _flashCoroutine;

    private void Awake()
    {
        _enemyBase = GetComponent<EnemyBase>();
        _sr = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void OnEnable()
    {
        _enemyBase.OnVisualChanged += HandleVisualChange;
        damagePopup.SetActive(false);
    }

    private void OnDisable()
    {
        if (_flashCoroutine != null)
            StopCoroutine(_flashCoroutine);
        _flashCoroutine = null;
        _sr.material.color = Color.white;
        _enemyBase.OnVisualChanged -= HandleVisualChange;
    }
    
    private void HandleVisualChange(Color damageColor)
    {
        if(_flashCoroutine != null)
            StopCoroutine(_flashCoroutine);
        
        damagePopup.SetActive(true);
        _flashCoroutine = StartCoroutine(FlashColor(damageColor));
    }
    
    private IEnumerator FlashColor(Color damageColor)
    {
        _sr.material.color = damageColor;
        yield return new WaitForSeconds(0.2f);
        _sr.material.color = Color.white;
        _flashCoroutine = null;
    }

    public void HideDamagePopup()
    {
        damagePopup.SetActive(false);
    }

}
