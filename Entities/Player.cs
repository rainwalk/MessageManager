using UnityEngine;
public enum PlayerSkillState
{
    Ready,
    Cooldown,
}

public class Player : MonoBehaviour
{
    public PlayerSkillState skillState = PlayerSkillState.Ready;
    public int damage = 10;

    public void UseFireballSkill(Enemy target) {
        if (skillState == PlayerSkillState.Ready) {
            int enemyId = target.GetInstanceID();
            var fireballMessage = new MessageProcessFireball(enemyId, damage); // 10의 피해를 주는 파이어볼

            MessageManager.Instance.AddMessage(fireballMessage, false, false);

            skillState = PlayerSkillState.Cooldown;

            var cooldownMessage = new MessageProcessExecuteDelay(() => {
                skillState = PlayerSkillState.Ready;
                Debug.Log("파이어볼을 재사용 할 수 있습니다");
            }, 2f);

            MessageManager.Instance.AddMessage(cooldownMessage, false, false);
        }
        else {
            Debug.Log("스킬 쿨타임중입니다");
        }
    }
}
