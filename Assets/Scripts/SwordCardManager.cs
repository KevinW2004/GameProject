using CardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordCardManager : MonoBehaviour
{
    public static SwordCardManager Instance;
    AudioSource audioSource;
    public AudioClip pick_sound;
    public AudioClip select_sound;


    //��������
    [SerializeField] public int SwordNum = 0;

    //�����ı�
    [SerializeField] private Text SwordText;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }



    public SwordCardManager(Text text)
    {
        this.SwordText = text;
    }

    public void PickUpCard()
    {
        audioSource.clip = pick_sound;
        audioSource.Play();
        this.SwordNum++;
        updateText();

    }
    //isRelease���Ƿ�Ҫ�ͷż���
    public void UseCard(bool isRelease)
    {
        if (Player.Instance.player_state == Player.Player_State.Sword || Player.Instance.player_state == Player.Player_State.SwordPlus)
        {
            return;
        }



        Button button = GetComponent<Button>();
        if (button.tag == "SwordCard")
        {
            Debug.Log("sword --");
            if (this.SwordNum > 0)
            {

                this.SwordNum--;
                audioSource.clip = select_sound;
                audioSource.Play();



                if (isRelease)
                {
                    if (Player.Instance.player_state == Player.Player_State.Bow || Player.Instance.player_state == Player.Player_State.BowPlus)
                    {
                        BowCardManager.Instance.BowNum += 1;
                        BowCardManager.Instance.updateText();
                    }

                    if (Player.Instance.player_state == Player.Player_State.Bomb)
                    {
                        BombCardManager.Instance.BombNum += 1;
                        BombCardManager.Instance.updateText();
                    }

                    useSword(UpgradeSwordCard.Instance.isUpgraded);


                }

            }


        }

        updateText();


    }

    public void getUpdated()
    {
        GetComponent<Image>().color = Color.blue;
    }

    public  void updateText()
    {
        this.SwordText.text = this.SwordNum.ToString();
    }

    //ʹ�ý���Ч��������Ϊ�Ƿ�Ϊ������
    private void useSword(bool isUpgraded)
    {
        if (isUpgraded)
        {//��������
         // GetComponent<Image>().color = new Color(255, 255, 255);
            GetComponent<Image>().color = Color.red;
            Player.Instance.player_state = Player.Player_State.SwordPlus;
            Player.Instance.setAnimeOn("U-SwordOn");
            UpgradeSwordCard.Instance.DownGradeSword();
        }
        else
        {//����ͨ��
            GetComponent<Image>().color = Color.red;
            Debug.Log("release");
            Player.Instance.player_state = Player.Player_State.Sword;
            Player.Instance.setAnimeOn("SwordOn");
        }
        return;
    }



}
