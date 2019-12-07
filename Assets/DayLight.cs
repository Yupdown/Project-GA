using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLight : MonoBehaviour
{
    [SerializeField]
    private Gradient _dayGradient;

    private Transform _transform;
    private Light _light;

    private float _value;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _light = GetComponent<Light>();

        _value = 0f;
    }

    private void Update()
    {
        // SetTime(Time.time * 0.1f);
    }

    private void OnGUI()
    {
        _value = GUI.HorizontalSlider(new Rect(10f, 10f, 100f, 10f), _value, 0f, 1f);

        SetTime(_value);
    }

    public void SetTime(float time)
    {
        _transform.localRotation = Quaternion.Euler(0f, -90f + Mathf.Repeat(360f * time, 180f), 0f);
        _light.color = _dayGradient.Evaluate(Mathf.Repeat(time, 1f));
    }
}
