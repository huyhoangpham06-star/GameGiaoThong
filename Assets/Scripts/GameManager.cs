
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Quan ly game duy nhat
    public static GameManager Instance;

    // Bien diem so
    public int score = 0;

    // Trang thai ket thuc game
    public bool gameOver = false;

    private void Awake()
    {
        // Kiem tra GameManager trung lap
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Cong diem

    public void AddScore(int amount)
    {
        if (gameOver) return;

        score += amount;

        // Khong cho diem am
        if (score < 0)
            score = 0;

        Debug.Log("Diem hien tai: " + score);
    }


    // Ket thuc game
    public void EndGame(string message)
    {
        if (gameOver) return;

        gameOver = true;

        Debug.Log("KET THUC GAME: " + message);
    }

    // Choi lai game
    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
