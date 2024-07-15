using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject mobEffect;
     public GameObject waterEffect;
    public GameObject loot;
    bool canInstantiate = true;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public GameObject recompense;
    bool isInvincible = false;
    public int bossHealth;
    public AudioClip hitSound;
    public AudioClip hitCoin;
     public AudioClip hitWater;
    AudioSource audioSource;
    public SkinnedMeshRenderer rend;
   
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin") // on a touché une pièce
        {
            audioSource.PlayOneShot(hitCoin);
            GameObject go = Instantiate(pickupEffect, other.transform.position, Quaternion.identity);
            Destroy(go, 0.5f);
            PlayerInfos.pi.getCoin();
            Destroy(other.gameObject);
        }

        if(other.gameObject.name == "Fin"){
            print("Score finale = " + PlayerInfos.pi.getScore());
        }
        if (other.gameObject.tag == "cam1")
        {
            cam1.SetActive(true);
        }
        if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(true);
        }
        if (other.gameObject.tag == "cam3")
        {
            cam3.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cam1")
        {
            cam1.SetActive(false);
        }
        if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(false);
        }
        if (other.gameObject.tag == "cam3")
        {
            cam3.SetActive(false);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        {
            if (collision.gameObject.tag == "hurt" && !isInvincible)
            {
                isInvincible = true;
                PlayerInfos.pi.setHealth(-1);
                iTween.PunchPosition(gameObject, Vector3.back * 2, .5f);
                iTween.PunchScale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
                StartCoroutine("ResetInvincible");
            }
            if (collision.gameObject.tag == "mob" && canInstantiate)
            {
                collision.gameObject.transform.parent.gameObject.GetComponent<Collider>().enabled = false;
                canInstantiate = false;
                audioSource.PlayOneShot(hitSound);
                iTween.PunchScale(collision.gameObject.transform.parent.gameObject, new Vector3(50, 50, 50), 0.4f);
                GameObject go = Instantiate(mobEffect, collision.transform.position, Quaternion.identity);
                GameObject bonus = Instantiate(loot, collision.transform.position + Vector3.forward, Quaternion.identity * Quaternion.Euler(90,0,0));
                Destroy(go, 0.3f);
                Destroy(collision.gameObject.transform.parent.gameObject, 0.5f);
                StartCoroutine("ResetInstantiate");
            }
             if (collision.gameObject.tag == "boss" && canInstantiate)
            {
                collision.gameObject.transform.parent.gameObject.GetComponent<Collider>().enabled = false;
                canInstantiate = false;
                audioSource.PlayOneShot(hitSound);
                iTween.PunchScale(collision.gameObject.transform.parent.gameObject, new Vector3(50, 50, 50), 0.4f);
                GameObject go = Instantiate(mobEffect, collision.transform.position, Quaternion.identity);
                Destroy(go, 0.3f);
                bossHealth = bossHealth -1;
                StartCoroutine("ResetInstantiate");
                collision.gameObject.transform.parent.gameObject.GetComponent<Collider>().enabled = true;
                if(bossHealth == 0){
                    Destroy(collision.gameObject.transform.parent.gameObject, 0.5f);
                    recompense.SetActive(true);
                    
                }
            }
        }
        if(collision.gameObject.tag == "fall"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
         if(collision.gameObject.tag == "water" && canInstantiate){
            canInstantiate = false;
            audioSource.PlayOneShot(hitWater);
            GameObject go = Instantiate(waterEffect, collision.transform.position, Quaternion.identity);
            StartCoroutine("ResetInstantiate");
            StartCoroutine("RestartScene");
         
        }

    }
     IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator ResetInstantiate()
    {
        yield return new WaitForSeconds(0.8f);
        canInstantiate = true;
    }
    IEnumerator ResetInvincible()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(.2f);
            rend.enabled = !rend.enabled;
        }
        yield return new WaitForSeconds(.2f);
        rend.enabled = true;
        isInvincible = false;
    }

}