using UnityEngine;

public class InputHandler : MonoBehaviour
{
     private BridgeController bridge;
    
    void Start()
    {
        bridge = GameManager.Instance.bridge;
    }
    
    void Update()
    {
        if (!GameManager.Instance.isGameActive || GameManager.Instance.isGamePaused)
            return;
            
        // Обработка касаний на мобильных устройствах
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    bridge.StartGrowing();
                    break;
                    
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    bridge.StopGrowing();
                    break;
            }
        }
        
        // Обработка мыши для тестирования на ПК
        if (Input.GetMouseButtonDown(0))
        {
            bridge.StartGrowing();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            bridge.StopGrowing();
        }
    }
}
