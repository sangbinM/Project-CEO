using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillGauge : MonoBehaviour {
    public Image skillBar;

    private PlayerController _player;
    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update () {
        skillBar.fillAmount = _player.GetSkillGaugeValue();
    }
}
