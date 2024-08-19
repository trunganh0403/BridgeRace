using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOut : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player)))
        {
            GameManager.Instance.WinGame();
        }

        if (collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_1))
              || collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_2))
              || collision.gameObject.CompareTag(GameTag.ToString(GameTag.Tag.Player_3)))
        {
            GameManager.Instance.LoseGame();
        }

    }

}
