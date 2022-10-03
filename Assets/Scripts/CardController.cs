using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Transform shotPointRight;
    public Transform shotPointLeft;
    public GameObject card;

    private float timeBtwShots;
    public float startTimeBtwShots;
    private void Update()
    {
        if(timeBtwShots <= 0)
        {
            Shoot();
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            timeBtwShots = startTimeBtwShots;
            StartCoroutine(ShootRight(0.1f));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            timeBtwShots = startTimeBtwShots;
            StartCoroutine(ShootLeft(0.1f));
        }
    }
    private IEnumerator ShootRight(float value)
    {
        Instantiate(card, shotPointRight.position, shotPointRight.rotation);
        yield return new WaitForSeconds(value);
        Instantiate(card, shotPointRight.position, shotPointRight.rotation);
        yield return new WaitForSeconds(value);
        Instantiate(card, shotPointRight.position, shotPointRight.rotation);
    }
    private IEnumerator ShootLeft(float value)
    {
        Instantiate(card, shotPointLeft.position, shotPointLeft.rotation);
        yield return new WaitForSeconds(value);
        Instantiate(card, shotPointLeft.position, shotPointLeft.rotation);
        yield return new WaitForSeconds(value);
        Instantiate(card, shotPointLeft.position, shotPointLeft.rotation);
    }
}
