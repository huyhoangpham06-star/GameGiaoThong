
using UnityEngine;

public class TrafficUI : MonoBehaviour
{
    void OnGUI()
    {
        if (GameManager.Instance == null)
            return;

        // Bang diem
        GUI.Box(new Rect(15, 15, 260, 120), "");

        GUIStyle title = new GUIStyle(GUI.skin.label);
        title.fontSize = 21;
        title.fontStyle = FontStyle.Bold;
        title.normal.textColor = Color.yellow;

        GUIStyle text = new GUIStyle(GUI.skin.label);
        text.fontSize = 20;
        text.normal.textColor = Color.white;

        GUI.Label(
            new Rect(30, 25, 260, 35),
            "GAME GIAO THONG",
            title
        );

        GUI.Label(
            new Rect(30, 65, 230, 30),
            "DIEM: " + GameManager.Instance.score,
            text
        );

        GUI.Label(
            new Rect(30, 95, 230, 30),
            "Dieu khien: WASD",
            text
        );

        // Man hinh ket thuc
        if (GameManager.Instance.gameOver)
        {
            GUI.Box(
                new Rect(
                    Screen.width / 2 - 180,
                    Screen.height / 2 - 90,
                    360, 180
                ),
                ""
            );

            GUI.Label(
                new Rect(
                    Screen.width / 2 - 100,
                    Screen.height / 2 - 60,
                    250, 40
                ),
                "GAME OVER"
            );

            if (GUI.Button(
                new Rect(
                    Screen.width / 2 - 90,
                    Screen.height / 2 + 20,
                    180, 40
                ),
                "CHOI LAI"
            ))
            {
                GameManager.Instance.RestartGame();
            }
        }
    }
}
