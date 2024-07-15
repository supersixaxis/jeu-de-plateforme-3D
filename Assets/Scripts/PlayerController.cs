using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private CharacterController cc;
  // variabl pour le déplacement
  public float moveSpeed;
  public float jumpForce;
  public float gravity;
  // vecteur direction souhaitée
  private Vector3 moveDir;
  private Animator anim;
  bool isWalking = false;

  private void Start()
  {
    cc = GetComponent<CharacterController>();
    anim = GetComponent<Animator>();
  }
  void Update()
  {   // calcul de la direction
    moveDir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, Input.GetAxis("Vertical") * moveSpeed);

    // check de la touche espace et si il est au sol
    if (Input.GetButtonDown("Jump") && cc.isGrounded)
    {
      // on saute
      moveDir.y = jumpForce;

    }   // on applique la gravité
    moveDir.y -= gravity * Time.deltaTime;
    // si on se déplace ( si mouvement != 0)
    if (moveDir.x != 0 || moveDir.z != 0)
    {
      // on tourne le personnage dans la bonne dir
      isWalking = true;
      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z)), 0.15f);  // agir sur la rotation
    }
    else
    {
      isWalking = false;
    }
    // on applique le déplacement
    cc.Move(moveDir * Time.deltaTime);

    anim.SetBool("isWalking", isWalking);
  }
}