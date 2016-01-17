using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillGauge : MonoBehaviour {
    public Image skillBar;

    private PlayerController _player;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        skillBar.fillAmount = 0.0f;
    }

    void Update ()
    {
        skillBar.fillAmount = _player.GetSkillGaugeValue();
    }
}
