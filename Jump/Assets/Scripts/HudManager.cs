using UnityEngine;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    [SerializeField] TMP_Text textLife;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void updateLifes(int value)
    {
        textLife.text = value.ToString();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
