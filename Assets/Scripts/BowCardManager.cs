using CardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowCardManager : MonoBehaviour
{
    public static BowCardManager Instance;
    AudioSource audioSource;
    public AudioClip pick_sound;
    public AudioClip select_sound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }
    //��������
    [SerializeField] public int BowNum = 0;

    //�����ı�
    [SerializeField] private Text BowText;

    public BowCardManager(Text text)
    {
        this.BowText = text;
    }

    public void PickUpCard()
    {
        audioSource.clip = pick_sound;
        audioSource.Play();
        this.BowNum++;
        updateText();
    }
    //isRelease���Ƿ�Ҫ�ͷż���
    public void UseCard(bool isRelease)
    {
        if (Player.Instance.player_state == Player.Player_State.Bow || Player.Instance.player_state == Player.Player_State.BowPlus)
        {
            return;
        }

        Button button = GetComponent<Button>();
        if (button.tag == "BowCard")
        {
            Debug.Log("bow --");
            if (this.BowNum > 0)
            {
                GetComponent<Image>().color = new Color(253, 83, 75);
                audioSource.clip = select_sound;
                audioSource.Play();



                this.BowNum--;
                if (isRelease)
                {
                    
                    if (Player.Instance.player_state == Player.Player_State.Sword|| Player.Instance.player_state == Player.Player_State.SwordPlus)
                    {
                        SwordCardManager.Instance.SwordNum++;
                        SwordCardManager.Instance.updateText();
                    }

                    if (Player.Instance.player_state == Player.Player_State.Bomb)
                    {
                        BombCardManager.Instance.BombNum += 1;
                        BombCardManager.Instance.updateText();
                    }



                    useBow(UpgradeBowCard.Instance.isUpgraded);

                }

            }

        }

        updateText();


    }

    public void getUpdated()
    {
        GetComponent<Image>().color = Color.blue;
    }

    public void updateText()
    {
        this.BowText.text = this.BowNum.ToString();
    }

    //ʹ�ù���Ч��
    private void useBow(bool isUpgraded)
    {
        if (isUpgraded)
        {//��������
         //GetComponent<Image>().color = new Color(255, 255, 255);
            GetComponent<Image>().color = Color.red;
            UpgradeBowCard.Instance.DownGradeBow();
            Player.Instance.player_state = Player.Player_State.BowPlus;
            Player.Instance.setAnimeOn("U-BowOn");
        }
        else
        {//����ͨ��
            GetComponent<Image>().color = Color.red;
            Debug.Log("release");
            Player.Instance.player_state = Player.Player_State.Bow;
            Player.Instance.setAnimeOn("BowOn");
        }
        return;
    }
}
