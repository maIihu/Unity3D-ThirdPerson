
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ExpBall : MonoBehaviour, IInteractable
{
    [SerializeField] private float exp;
    [SerializeField] private float speed;

    private PlayerMovement _playerTarget;
    private bool _isAttracting;

    private void Update()
    {
        if (_isAttracting && _playerTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerTarget.transform.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _playerTarget.transform.position) < 0.5f)
            {
                _playerTarget.Exp(exp);
                _isAttracting = false;
                this.gameObject.SetActive(false);
            }
        }
    }
    public void Interact(PlayerMovement player)
    {   
        _playerTarget = player;
        _isAttracting = true;
    }
}
