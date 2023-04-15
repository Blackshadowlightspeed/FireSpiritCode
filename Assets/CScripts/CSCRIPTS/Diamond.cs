using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int coinValue;
    public GameObject Sounder;
    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 0, 0);
        Sounder.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            PlayerManager.score += coinValue;
            Destroy(gameObject);
            Debug.Log("Coin Collected!");
            Sounder.SetActive(true);
        }
    }
}