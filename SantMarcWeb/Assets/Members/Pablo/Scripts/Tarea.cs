using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Tarea", menuName = "Tarea")]
public class Tarea : ScriptableObject
{

    public new string nombre;
    public string descripcion;

    public string minParaCompletarla;
    public string segParaCompletarla;

    public Sprite recompensa;

    public bool Recompensa;
    public bool Tiempo;

    public void Print()
    {
        Debug.Log(nombre + ": " + descripcion + " | Tienes " + minParaCompletarla + " min para completarla");
    }

}
