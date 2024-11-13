using System;
using System.Collections;
using UnityEngine;

public class PowerEffect : MonoBehaviour
{
    [SerializeField] private float _destroyEffectAfterSeconds = 2f;
    [Space(10)]
    [SerializeField] private GameObject _healAreaEffect;
    [SerializeField] private GameObject _shieldAreaEffect;
    [SerializeField] private GameObject _attackSpeedEffect;


    private void Awake()
    {
        _healAreaEffect.SetActive(false);
        _shieldAreaEffect.SetActive(false);
        _attackSpeedEffect.SetActive(false);
    }

    public void HealAreaEffect(Vector3 position)
    {
        AreaEffect(position, _healAreaEffect);
    }

    public void ShieldAreaEffect(Vector3 position)
    {
        AreaEffect(position, _shieldAreaEffect);
    }

    public void AttackSpeedAreaEffect(Vector3 position)
    {
        AreaEffect(position, _attackSpeedEffect);
    }

    private void AreaEffect(Vector3 position, GameObject area)
    {
        area.SetActive(true);
        area.transform.position = position;
        
        ParticleSystem ps = area.GetComponent<ParticleSystem>();
        ps.Play();

        StartCoroutine(Utils.WaitRoutine(_destroyEffectAfterSeconds, () => area.SetActive(false)));
    }
}
