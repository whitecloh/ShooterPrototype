using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private float _maxHealths;
    [SerializeField]
    private Text _healthsTxt;
    [SerializeField]
    private Image _healthBar;

    private float _currentHealths;

    private void Start()
    {
        _currentHealths = _maxHealths;
    }

    private void Update()
    {
        _currentHealths = Mathf.Clamp(_currentHealths,0,_maxHealths);
        UpdateHealtshUI();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(11);
        }
    }

    private void UpdateHealtshUI()
    {
        _healthBar.fillAmount = _currentHealths / _maxHealths;
        _healthsTxt.text = _currentHealths.ToString();
    }

    private void TakeDamage(float damage)
    {
        _currentHealths -= damage;

    }

}
