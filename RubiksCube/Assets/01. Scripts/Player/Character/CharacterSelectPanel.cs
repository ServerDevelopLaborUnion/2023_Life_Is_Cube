using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectPanel : MonoBehaviour
{
    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private Character current;
    public Character Current
    {
        get { return current; }
        set { current = value; }
    }

    [Header("UI")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;

    [SerializeField] private GameObject rightLock;
    [SerializeField] private GameObject leftLock;
    #region Slider Group
    private Slider hp;
    private Slider speed;
    private Slider critical;
    #endregion

    #region Icon Group
    private Image skillIcon;
    private Image classIcon;

    [SerializeField] private Image descriptionSkillIcon;
    #endregion

    #region TextGroup
    [SerializeField] private TextMeshProUGUI skillTitle;
    [SerializeField] private TextMeshProUGUI description;
    #endregion

    private int idx = 0;

    private void Awake()
    {
        // �����̴���
        Transform gauge = infoPanel.transform.GetChild(0);

        hp = gauge.GetChild(0).GetComponent<Slider>();
        speed = gauge.GetChild(1).GetComponent<Slider>();
        critical = gauge.GetChild(2).GetComponent<Slider>();

        // �����ܵ�
        Transform IconPanel = infoPanel.transform.GetChild(1);

        skillIcon = IconPanel.GetChild(0).GetComponent<Image>();
        classIcon = IconPanel.GetChild(1).GetComponent<Image>();


        // �ڽĿ� ��ϵ� ĳ���͵��� ����Ʈ��
        characters = GetComponentsInChildren<Character>().ToList();
        // ���� ������ ����������
        current = characters[idx];
        // UI �ʱ�ȭ
        ChangeCurrent();
    }

    /// <summary>
    /// ������ ȭ��ǥ �������� ��������
    /// </summary>
    public void MoveRight()
    {
        if (idx >= characters.Count - 1)
        {
            Debug.LogWarning("You are trying to access wrong index");
            return;
        }
        current = characters[++idx];
        ChangeCurrent();
    }

    /// <summary>
    /// ���� ȭ��ǥ �������� ��������
    /// </summary>
    public void MoveLeft()
    {
        if (idx <= 0)
        {
            Debug.LogWarning("You are trying to access wrong index");
            return;
        }
        current = characters[--idx];
        ChangeCurrent();
    }

    private void ChangeCurrent()
    {

        /*  
        idx   type
        0   : hp
        1   : speed
        2   : ad
        3   : critical
        */

        hp.value = current.stats[0].value;
        speed.value = current.stats[1].value;
        critical.value = current.stats[3].value;

        skillIcon.sprite = current.skillIcon;
        descriptionSkillIcon.sprite = current.skillIcon;

        classIcon.sprite = current.classIcon;

        skillTitle.text = current.skillName;
        description.text = current.skillDescription;


        bool right = (idx == characters.Count - 1);
        bool left = (idx == 0);
        
        rightButton.interactable = !right;
        leftButton.interactable = !left;
        
        rightLock.SetActive(right);
        leftLock.SetActive(left);
    }

    public void GameStart()
    {
        //MapTest => ���� �ΰ��� ���̸����� �����ϱ�
        SceneLoader.Instance.LoadSceneAsync("MapTest", () =>
        {
            PlayerStat playerStat = DEFINE.PlayerTrm.GetComponent<PlayerStat>();

            foreach (var item in current.stats)
            {
                playerStat.SetStat(item.statType, item);
            }
            // �ε����� �Ѱ��ָ� �ű� ����Ʈ ������� ������
            DEFINE.PlayerTrm.GetComponent<SkillHandler>().SetSkill(idx);
        });
    }

    public void BackButtonClick()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
