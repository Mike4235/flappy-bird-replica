using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectShifter : MonoBehaviour
{

    private void Update()
    {
        /*if (GameObject.FindWithTag("Player") != null)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(3f, 0f);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }*/
    }

    private int numOfBackgrounds = 13, distOfBackgrounds = 6,
        numofGrounds = 11, distOfGrounds = 7,
        numofPipes = 9, distOfPipes = 5;

    private int cnt = 0; 
    private float cur = 0;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Background"))
        {
            GameObject bg = collision.gameObject;
            bg.GetComponent<Transform>().position += new Vector3(numOfBackgrounds * distOfBackgrounds, 0, 0);
        }
        else if (collision.CompareTag("Ground"))
        {
            GameObject gr = collision.gameObject;
            gr.GetComponent<Transform>().position += new Vector3(numofGrounds * distOfGrounds, 0, 0);
        }
        else if (collision.CompareTag("Pipe"))
        {
            GameObject pp = collision.gameObject;
            pp.GetComponent<Transform>().position += new Vector3(numofPipes * distOfPipes, 0, 0);

            cnt++;
            if (cnt % 2 == 1) cur = Random.Range(-7f, -2f);
            if (pp.transform.localScale.y == 1)
            {
                Vector3 temp = pp.transform.position;
                temp.y = cur;
                pp.transform.position = temp;
            }
            else
            {
                Vector3 temp = pp.transform.position;
                temp.y = cur + 10f;
                pp.transform.position = temp;
            }
        }
        else if (collision.CompareTag("Point"))
        {
            GameObject pt = collision.gameObject;
            pt.GetComponent<Transform>().position += new Vector3(numofPipes * distOfPipes, 0, 0);
            Vector3 temp = pt.transform.position;
            temp.y = cur + 5f;
            pt.transform.position = temp;
        }
    }
}
