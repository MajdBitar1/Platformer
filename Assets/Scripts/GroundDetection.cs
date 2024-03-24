using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool Grounded = true;
   // public bool AboveDropDown = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == true || collision.gameObject.CompareTag("DropDown") == true)
        {
            Grounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == true)
        {
            Grounded = false;
        }
        if (collision.gameObject.CompareTag("DropDown") == true)
        {
            Grounded = false;
           // AboveDropDown = true;
        }
    }
}
