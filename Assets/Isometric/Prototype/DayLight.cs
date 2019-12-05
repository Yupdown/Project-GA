using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLight : MonoBehaviour
{
    [SerializeField]
    private Gradient _dayGradient;

    private Transform _transform;
    private Light _light;

    private float _time;
    private float _updateFactor;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _light = GetComponent<Light>();

        _time = 0f;
    }

    private void Update()
    {
        _updateFactor = Mathf.Lerp(_updateFactor, 1f, Time.deltaTime * 5f);
        _time = _time + Time.deltaTime * 0.02f * _updateFactor;

        SetTime(_time);
    }

    private void OnGUI()
    {
        float lastTime = _time;

        _time = GUI.HorizontalSlider(new Rect(10f, 10f, 100f, 10f), _time, 0f, 1f);

        if (_time != lastTime)
        {
            _updateFactor = 0f;
            SetTime(_time);
        }
    }

    public void SetTime(float time)
    {
        _transform.localRotation = Quaternion.Euler(0f, -90f + Mathf.Repeat(360f * time, 180f), 0f);
        _light.color = _dayGradient.Evaluate(Mathf.Repeat(time, 1f));
    }
}
