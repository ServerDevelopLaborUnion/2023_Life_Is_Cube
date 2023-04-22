using UnityEngine;

public abstract class ItemEvent : MonoBehaviour
{

    public abstract void ActiveEvent(); // 액티브 시킬 이벤트
    public abstract void InitEvent();   // 아이템이 먹힐때 실행되는 초기화 용도   
    public abstract void UpdateEvent(); // 업데이트마다 실행되는 함수
}
