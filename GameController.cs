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
        if (Input.GetKeyDown(KeyCode.Space)) { // 스페이스 키로 스킬 사용 테스트
            player.UseFireballSkill(enemy);
        }
    }
}
