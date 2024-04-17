using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Board : MonoBehaviour
{
    public GameObject card;
    // Start is called before the first frame update
    void Start()
    {
        int[] arr = {0, 1, 1, 0, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8 ,8 , 9 , 9 };
        arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();// 섞는 알고리즘 수정 갯수에 따라서 몇명
        for (int i = 0; i < 20; i++)
        {
               
            GameObject go = Instantiate(card, this.transform);
            float x = (i % 4)*1.4f - 2.1f;
            float y = (i / 4)*1.4f - 4f;
            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
            Debug.Log(arr[i]);
        }
        GameManager.Instance.cardcount = arr.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
