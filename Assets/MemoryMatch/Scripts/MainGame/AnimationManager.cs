using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    static AnimationManager instance;
    public static AnimationManager Instance => instance;
    private void Awake() {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] List<GameObject> animations = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation(CardCategory category) {
        if (PlayerManager.Instance.CurrentTurnPlayerIndex == 0) {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        animations[(int)category].SetActive(true);
    }
}
