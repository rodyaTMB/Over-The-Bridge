using UnityEngine;

namespace OverBridge
{
    public class BridgeController : MonoBehaviour
    {
        private PlayerInput _playerInput;
        float currentLength = 0f;
        float buildSpeed = 1f;
        public bool IsBuild = false;
        bool isBridgeCompeate = false;
        [SerializeField] private GameObject bridgeVisual;

        void Awake()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            Debug.Log(_playerInput == false ? "Объект не найден" : "Объект найден");
        }

        void FixedUpdate()
        {

            if (IsBuild && bridgeVisual.transform.localScale.y <= 0f)
            {
                BuildBridge();
            }
            else if (IsBuild && bridgeVisual.transform.localScale.y > 0f)
            {
                BuildBridge();
            }
            else if (!IsBuild && bridgeVisual.transform.localScale.y > 0f)
            {
                DownBridge();
            }
        }

        void BuildBridge()
        {
            currentLength += buildSpeed * Time.fixedDeltaTime;

            bridgeVisual.transform.localScale = new Vector3(1f, currentLength, 1f);
        }

        void DownBridge()
        {
            Debug.Log("Мост падает");

            // Текущий угол поворота по Z
            float currentZAngle = bridgeVisual.transform.eulerAngles.z;
            // Конвертируем в диапазон -180 до 180
            if (currentZAngle > 180f) currentZAngle -= 360f;

            if (currentZAngle <= -90f)
            {
                Debug.Log("Мост полностью опустился");
                Debug.Log("Персонаж начал идти прямо");
                return;
            }

            // Плавное вращение
            float rotation = 30f * Time.deltaTime;
            bridgeVisual.transform.Rotate(0, 0, -rotation);
        }
    }
}


