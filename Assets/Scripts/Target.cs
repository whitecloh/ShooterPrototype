using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float _healths = 50f;
    [SerializeField]
    private Material _defaultMaterial,_onHitMaterial;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
    public void TakeDamage(float amount)
    {
        _healths -= amount;
        StartCoroutine(ChangeColorOnTakeDamage());
        if(_healths<=0)
        {
            Die();
        }
    }

    private IEnumerator ChangeColorOnTakeDamage()
    {
        _meshRenderer.material = _onHitMaterial;
        yield return new WaitForSeconds(0.1f);
        _meshRenderer.material = _defaultMaterial;
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
