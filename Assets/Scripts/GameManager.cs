using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] PipeArr;
    [SerializeField] GameObject[] Point;
    [SerializeField] GameObject MCamera;
    [SerializeField] GameObject BPlayer;
    [SerializeField] GameObject[] StatePic;
    [SerializeField] Text MaxScore;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MaxScore"))
        {
            MaxScore.text = "Max Score: " + PlayerPrefs.GetInt("MaxScore").ToString();
        }

        for (int i = 0, j = 0; i < PipeArr.Length; i += 2, j++)
        {
            float x = Random.Range(-7f, -2f);
            Vector3 cur = PipeArr[i].transform.position;
            cur.y = x;
            PipeArr[i].transform.position = cur;

            cur.y += 10f;
            PipeArr[i + 1].transform.position = cur;

            cur.y -= 5f;
            Point[j].transform.position = cur;
            // Debug.Log("Random Value is " + x);
        }
    }
    // Start is called before the first frame update

    GameObject State;
    bool InGame = false, Started = false;

    public void HandleButton()
    {
        Started = true;
    }

    void RealStart()
    {
        Destroy(State);
        Instantiate(BPlayer, new Vector3(0, 0, 0), Quaternion.identity);
        Init("Background"); Init("Ground"); Init("Pipe"); Init("Point");
        InGame = true;
    }

    void Init(string s)
    {
        GameObject[] Objects = GameObject.FindGameObjectsWithTag(s);
        foreach(GameObject Obj in Objects)
        {
            Obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-3f, 0f);
        }
    }

    [SerializeField] GameObject[] MenusTobeToggled;
    bool First = false;
    // Update is called once per frame
    void Update()
    {
        if (Started)
        {
            Started = false;
            foreach (GameObject Obj in MenusTobeToggled)
            {
                if (Obj.activeSelf) Obj.SetActive(false);
                else Obj.SetActive(true);
            }

            State = Instantiate(StatePic[0], new Vector3(0, 0, 0), Quaternion.identity);
            Invoke("RealStart", 3.0f);
        }
        if (InGame && GameObject.FindWithTag("Player") == null && !First)
        {
            First = true;
            State = Instantiate(StatePic[1], new Vector3(MCamera.transform.position.x, MCamera.transform.position.y, 0), Quaternion.identity);
            Invoke("EndGame", 3.0f);
        }
    }

    void EndGame()
    {
        Destroy(State);
        First = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
