using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private float _damage = 10f;
    [SerializeField]
    private float _range = 100f;
    [SerializeField]
    private float _fireRate = 15f;
    [SerializeField]
    private int _maxAmmo = 30;
    [SerializeField]
    private float _reloadTime = 1f;

    [SerializeField]
    private Text _ammoTxt;

    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private ParticleSystem _muzzleFlash;
    [SerializeField]
    private GameObject _impactEffect;
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private AudioClip _shootAudio, _reloadAudio;
    [SerializeField]
    private AudioSource _source;

    private int _currentAmmo;
    private bool _isReloading = false;

    private float _nextTimeToFire = 0f;

    private void Start()
    {
        _currentAmmo = _maxAmmo;
        UpdateUiAmmo();
    }
    private void Update()
    {
        if (_isReloading) return;

        if(_currentAmmo<=0||(Input.GetKeyDown(KeyCode.R)&&_currentAmmo<_maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetButton("Fire1")&&Time.time>=_nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
        }
    }
    private void OnEnable()
    {
        _isReloading = false;
        _animator.SetBool("isReloading", false);
        UpdateUiAmmo();
    }

    private void Shoot()
    {
        _muzzleFlash.Play();
        _source.PlayOneShot(_shootAudio);
        _currentAmmo--;
        UpdateUiAmmo();
        RaycastHit hitInfo;
        if(Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitInfo, _range))
        {
            Target target = hitInfo.transform.GetComponent<Target>();
            GameObject impactGO = Instantiate(_impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(impactGO, 1f);
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
        }
    }

    private IEnumerator Reload()
    {
        _source.PlayOneShot(_reloadAudio);
        _isReloading = true;
        _animator.SetBool("isReloading", true);
        yield return new WaitForSeconds(_reloadTime-0.25f);
        _animator.SetBool("isReloading", false);
        yield return new WaitForSeconds(0.25f);

        _currentAmmo = _maxAmmo;
        UpdateUiAmmo();
        _isReloading = false;
    }

    private void UpdateUiAmmo()
    {
        _ammoTxt.text = _currentAmmo.ToString() + "/" + _maxAmmo.ToString();
    }
}
