using UnityEngine;
using UnityEngine.UI;

namespace Script.Enemy
{
    public class FloatingHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private new Camera camera;
    
        public void UpdateHealthBar(float currentValue, float maxValue)
        {
            slider.value = currentValue / maxValue;
        }
        // Update is called once per frame
        void Update()
        {
            transform.rotation = camera.transform.rotation;
        
        }
    }
}
