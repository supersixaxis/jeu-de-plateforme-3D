using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HelpFriends : MonoBehaviour
{
    GameObject cage;
    public Text infosTxt;
    bool canOpen = false;
   private void OnTriggerEnter(Collider other)
   {
    if(other.gameObject.tag == "cage")
    {
        cage = other.gameObject;
        infosTxt.text = "Appuyer sur E pour ouvrir la cage...";
        canOpen = true;
    }
   }

      private void OnTriggerExit(Collider other)
   {
    if(other.gameObject.tag == "cage")
    {
        cage = null;
        infosTxt.text = "";
        canOpen = false;
    }
   }

   private  void Update()
   {
    if(Input.GetKeyDown(KeyCode.E) && canOpen){
        iTween.ShakeScale(cage, new Vector3(145,145,145), 1f);
        Destroy(cage, 1.2f);
    }
   }
}
