using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void LateUpdate()
    {
        _image.fillAmount = (float)_playerHealth.GetCurrentHealth() / _playerHealth.GetTotalHealth();
    }
}
