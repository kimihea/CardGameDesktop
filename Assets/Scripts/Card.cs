using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour

{
    public GameObject front;
    public GameObject back;
    public Animator anim;
    public SpriteRenderer frontimage;
    public SpriteRenderer backImage;
    public int idx = 0;
    AudioSource audiosource;
    public AudioClip audioclip;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        backImage = back.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setting(int number)
    {
        idx = number;
        frontimage.sprite = Resources.Load<Sprite>($"image{idx}");
    }
    public void OpenCard()
    {
        StartCoroutine(DelayCloseCoroutine());
        if (GameManager.Instance.secondCard != null)
            return;
       
        audiosource.PlayOneShot(audioclip);
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);
        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }
    }
    public void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }
    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }
    public void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
        backImage.color = new Color(180f / 255f, 180f / 255f, 180f / 255f);
    }
    public void CloseCard2()
    {
        Invoke("CloseCardInvoke",0.0f);
        GameManager.Instance.firstCard = null;
    }

    IEnumerator DelayCloseCoroutine()
    {
        yield return new WaitForSeconds(5);//5초 뒤에 카드를 다시 뒤집기
        DelayClose();//혹시나 문제가 생긴다면 코드 전체를 옮겨야 하는 번거로움을 해소하기 위해 일부러 메서드로 빼놓음
    }
    void DelayClose()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
        if (GameManager.Instance.firstCard == this)
            GameManager.Instance.firstCard = null;
    }
}
