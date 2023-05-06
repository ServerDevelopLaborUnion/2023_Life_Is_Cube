using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSelectPanel : MonoBehaviour
{
    [SerializeField] private List<Character> characters = new List<Character>();
    private Character current;
    public Character Current
    {
        get { return current; }
        set { current = value; }
    }

    public static CharacterSelectPanel instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple SelectPanel Exist");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        characters = transform.GetChild(0).GetComponentsInChildren<Character>().ToList();
    }

    public void GameStart()
    {
        Debug.Log(1123123);
        //MapTest => 실제 인게임 씬이름으로 변경하기
        SceneLoader.Instance.AddSceneAsync("MapTest", () =>
        {
            PlayerStat playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
            
            if (playerStat == null)
            {
                Debug.LogError($"PlayerStat : {playerStat}");
            }

            foreach (var item in current.stats)
            {
                playerStat.SetStat(item.statType, item.defaultValue);
            }
            //JaeHee => 실제 캐릭터 선택창 씬이름으로 변경하기
            SceneLoader.Instance.UnloadSceneAsync("JaeHee");
        });
    }
}
