using UnityEngine;

namespace OverBridge
{
    public class DethZone : MonoBehaviour
    {
        [SerializeField] private HUDCOntroller _hud;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Charackter"))
            {
                _hud.OnGameOver.Invoke();
            }
        }

    }
}


