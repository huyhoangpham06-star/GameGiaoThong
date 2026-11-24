
using UnityEngine;

public class TrafficCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        CheckCollision(collision.gameObject);
    }

    private void CheckCollision(GameObject other)
    {
        if (other.CompareTag("TrafficCar") ||
            other.CompareTag("Obstacle"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EndGame(
                    "Ban da va cham giao thong!"
                );
            }
        }
    }
}
