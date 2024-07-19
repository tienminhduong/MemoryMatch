using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private RawImage _background;
    [SerializeField] private float _x, _y;
    void Update()
    {
        _background.uvRect = new Rect(_background.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _background.uvRect.size);
    }
}
