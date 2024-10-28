# MessageManager

## 요약

MessageManager는 다양한 이벤트와 행동들을 메시지로 처리하고 그 메시지들이 개별적으로 독립적인 기능을 수행할 수 있도록 설계하였습니다. 첨부한 파일은 기존의 프로젝트에서 분리하여,  플레이어가 적에게 스킬을 사용하는 상황을 예제로 만들었습니다. 비어있는 씬에 Enemy, Player, GameController를 배치하면 테스트 할 수 있습니다.

## **이 구조의 장점**

- **유연성 - 메시지 기반 구조** : 다양한 기능을 메시지로 캡슐화하여 독립적으로 처리할 수 있어 기능 확장 및 커스터마이징이 용이합니다.
- **관리 용이성** : 관련 클래스들이 분리되어 있어, 메시지 시스템의 유지보수 및 확장이 쉽고, 각 클래스의 역할이 명확하게 구분됩니다.
- **의존성 최소화 및 테스트 용이성** : Enemy, Player, MessageManager 등의 클래스가 각자의 역할만 수행하며 서로 의존성이 낮아, 독립적인 단위 테스트가 가능해집니다. 이로 인해 새로운 기능을 추가하거나 리팩토링할 때 부담이 줄어들고 유지보수가 용이합니다.
- **확장성 :** 새로운 메시지나 매니저, 엔티티 클래스를 추가할 때 구조가 직관적이므로 필요한 위치에 빠르게 추가할 수 있어, 코드 확장성이 우수합니다.
- **모듈화 :** 폴더별로 독립적인 기능을 담고 있어, 코드 재사용이 용이하며 필요한 클래스만 다른 프로젝트에서 가져와 사용하기 쉽습니다.
- **디버깅 편리성 :** MessageManager가 메시지를 하나의 큐에서 관리하고 처리하므로, 메시지 순서를 조정하거나 특정 메시지만 추적하기가 용이합니다. 예를 들어, MessageProcessFireball과 MessageProcessExecuteDelay와 같은 메시지를 각각 다르게 처리하고, 메시지 단위로 디버깅할 수 있어 버그 추적이 수월합니다.

## 기본 구조

- **MessageType**
MessageType은 각 메시지의 종류를 정의합니다. 각 메시지 타입은 고유의 행동을 나타내며, 여기서는 ProcessParam, ExecuteDelayDelegate가 정의되어 있습니다. 이를 통해 메시지가 어떤 종류인지 식별하고, 이후 메시지 풀(MessagePool)에서 이 타입에 맞는 메시지를 생성하거나 재활용할 수 있습니다.
- **MessageData 클래스**
MessageData는 모든 메시지 클래스의 기본 클래스로, 메시지의 MessageType을 가지고 있으며, SendInfo()와 같은 가상 메서드를 포함하여 메시지의 정보를 전송하는 메서드를 제공합니다. 이새로운 스킬이나 행동들을 추가할때 MessageData의 서브클래스를 통해 확장할 수 있습니다.
- **MessageManager 클래스**
메시지 관리 클래스인 MessageManager는 메시지를 저장하고, 필요한 경우 큐에 있는 메시지를 처리하며, 메시지를 재사용할 수 있도록 메모리 풀을 관리합니다.

## 작동 방식

1. **메시지 생성 및 추가**
MessageManager를 통해 게임 내 이벤트(예: 플레이어의 공격, 스킬 사용)가 발생하면 관련 정보를 포함하는 메시지가 생성되고 messageQueue에 추가됩니다.
2. **메시지 처리**
ProcessMessages가 메시지 큐에 있는 메시지를 하나씩 꺼내어 해당 타입에 맞는 동작을 수행합니다.
예를 들어, MessageProcessAttack 타입 메시지가 도착하면 Enemy를 찾아 데미지를 입히고 상태를 갱신합니다.
MessageProcessExecuteDelay 타입의 경우 일정 시간 후에 특정 동작을 수행하도록 설정합니다.
3. **메시지 완료 후 반환**
메시지 처리가 완료되면 CompleteMessage에서 메시지를 메모리 풀에 반환합니다. 이렇게 하면 필요할 때 기존 메시지를 재사용하여 메모리 할당 비용을 줄입니다.
