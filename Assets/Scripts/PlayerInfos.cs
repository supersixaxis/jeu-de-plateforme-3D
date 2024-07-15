using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfos : MonoBehaviour
{

    public static PlayerInfos pi;
    public int playerHealth = 3;
    public int nbCoins = 0;
    public Image[] hearts;
    public Text coinTxt;
    public Text scoreTxt;
    public CheckpointMgr checkpoint;
    void Awake()
    {
        pi = this;
    }
    // Start is called before the first frame update
    public void setHealth(int val){
        playerHealth += val;
        if(playerHealth > 3){
            playerHealth = 3;
        }
        if (playerHealth <= 0){
            playerHealth = 0;
           checkpoint.Respawn();
        }
        setHealthBar();

    }
    public void getCoin()
    {
        nbCoins ++;
        coinTxt.text = nbCoins.ToString();
    }
    public  void setHealthBar(){
        foreach(Image img in hearts){
            img.enabled = false;
        }
        for(int i=0; i<(playerHealth); i++){
            hearts[i].enabled = true;
        }
    }

    public int getScore(){
        int scoreFinal = (nbCoins * 5) + (playerHealth * 10);
        scoreTxt.text = "Score " + scoreFinal;
        return scoreFinal;
    }
}
