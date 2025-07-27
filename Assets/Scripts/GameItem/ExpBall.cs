using System.Collections;
using UnityEngine;

public class ExpBall : MonoBehaviour, IInteractable
{
    [SerializeField] private float exp;
    [SerializeField] private float speed;
    [SerializeField] private float hoverHeight = 0.3f; 
    [SerializeField] private float hoverDuration = 0.3f;
    [SerializeField] private float yPos = 0.2f;

    private PlayerMovement _playerTarget;
    private bool _isAttracting;
    private bool _canAttract;

    public void InitAt(Vector3 setPos)
    {
        _canAttract = false;
        transform.position = setPos;
        StartCoroutine(HoverEffect());
    }

    private IEnumerator HoverEffect()
    {
        Vector3 originalPos = new Vector3(transform.position.x, yPos, transform.position.z);
        Vector3 hoverPos = originalPos + Vector3.up * hoverHeight;

        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / (hoverDuration / 2);
            transform.position = Vector3.Lerp(originalPos, hoverPos, timer);
            yield return null;
        }

        timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / (hoverDuration / 2);
            transform.position = Vector3.Lerp(hoverPos, originalPos, timer);
            yield return null;
        }

        _canAttract = true;
    }

    private void Update()
    {
        if (_isAttracting && _playerTarget && _canAttract)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerTarget.transform.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _playerTarget.transform.position) < 0.5f)
            {
                _playerTarget.Exp(exp);
                _isAttracting = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void Interact(PlayerMovement player)
    {
        _playerTarget = player;
        _isAttracting = true;
    }
    
}