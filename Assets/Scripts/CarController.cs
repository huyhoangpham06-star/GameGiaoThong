
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Thong so xe")]
    public float speed = 7f;
    public float turnSpeed = 100f;

    [Header("Vi tri giao thong")]
    public float stopLine = -5.2f;
    public float finishLine = 10f;

    private bool scored = false;
    private bool redLightViolationReported = false;

    void Update()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.gameOver)
            return;

        float move = 0f;
        float turn = 0f;

        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.UpArrow))
            move = 1f;

        if (Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow))
            move = -1f;

        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow))
            turn = -1f;

        if (Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow))
            turn = 1f;

        Vector3 movement =
            transform.forward * move * speed * Time.deltaTime;

        Vector3 targetPosition =
            transform.position + movement;

        // Kiem tra xe vuot vach den do
        if (move > 0 &&
            TrafficLight.IsRed &&
            transform.forward.z > 0.7f &&
            transform.position.z < stopLine &&
            targetPosition.z >= stopLine &&
            !redLightViolationReported)
        {
            redLightViolationReported = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(-10);
                Debug.Log("VI PHAM: VUOT DEN DO! -10 DIEM");
            }
        }

        // Cap nhat vi tri xe
        transform.position = targetPosition;

        // Xoay xe
        transform.Rotate(
            Vector3.up * turn * turnSpeed * Time.deltaTime
        );

        // Kiem tra xe den dich
        if (transform.position.z >= finishLine && !scored)
        {
            scored = true;

            if (GameManager.Instance != null)
                GameManager.Instance.AddScore(10);

            // Dua xe ve diem xuat phat
            transform.position = new Vector3(0, 0.5f, -10);

            transform.rotation = Quaternion.identity;

            // Cho phep ghi nhan vi pham o luot tiep theo
            redLightViolationReported = false;

            scored = false;
        }
    }
}
