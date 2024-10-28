using UnityEngine;

public class GameController : MonoBehaviour
{
    private Player player;
    private Enemy enemy;

    private void Start() {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) { // �����̽� Ű�� ��ų ��� �׽�Ʈ
            player.UseFireballSkill(enemy);
        }
    }
}
