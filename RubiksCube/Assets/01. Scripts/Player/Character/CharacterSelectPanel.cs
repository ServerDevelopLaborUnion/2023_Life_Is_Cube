using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSelectPanel : MonoBehaviour
{
    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private Character current;
    public Character Current
    {
        get { return current; }
        set { current = value; }
    }

    private void Awake()
    {
        characters = GetComponentsInChildren<Character>().ToList();
        current = characters[0];
    }

    public void GameStart()
    {
        Debug.Log(1);
        //MapTest => 실제 인게임 씬이름으로 변경하기
        SceneLoader.Instance.LoadSceneAsync("MapTest", () =>
        {
            Debug.Log(2);
            PlayerStat playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();

            Debug.Log(3);

            foreach (var item in current.stats)
            {
                playerStat.SetStat(item.statType, item);
            }
        });
    }
}
