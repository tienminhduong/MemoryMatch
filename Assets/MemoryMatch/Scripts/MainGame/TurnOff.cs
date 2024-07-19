using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOff : MonoBehaviour
{
    public void TurnOffAnimation() {
        gameObject.SetActive(false);
    }
}
