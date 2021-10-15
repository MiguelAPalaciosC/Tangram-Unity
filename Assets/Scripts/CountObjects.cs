using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountObjects : MonoBehaviour
{
    public static List<Collider2D> listaObjetos = new List<Collider2D>();
    public static int l;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("hit detected");
        //colider = true;
        if (other.gameObject.tag == "Objeto")
        {
            listaObjetos.Add(other);
            l = listaObjetos.Count;
            Debug.Log("Trigger= " + l + " objetos");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Objeto")
        {
            listaObjetos.Remove(other);
            l = listaObjetos.Count;
            Debug.Log("Trigger= " + l + " objetos");
        }
    }
}
