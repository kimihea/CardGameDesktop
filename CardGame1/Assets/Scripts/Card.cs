using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front;
    public GameObject back;
    public SpriteRenderer backImage;
    public Button cardButton;

    public Animator anim;

    AudioSource audioSource;
    public AudioClip clip;

    public SpriteRenderer frontImage;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"카드 이미지/Image{idx}");
    }

    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        if(GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
            GameManager.Instance.SetFirstCardTime();
        }
        else
        {
            if(GameManager.Instance.firstCard != this)
            {
                GameManager.Instance.secondCard = this;
                GameManager.Instance.Matched();
            }
        }
    }

    public void DestoryCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
        backImage.color = new Color(180f / 255f, 180f / 255f, 180f / 255f);
    }
}
