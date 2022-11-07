using System.Collections;
using System.Collections.Generic;
using Members.Carlos.Scripts.Dialogues;
using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Tarea", menuName = "Tarea")]
public class Tarea : ScriptableObject
{
    public DialogueObject[] dialogueObjects;

    public string interactionString;
    public new string nombre;
    public string descripcion;

    public string minParaCompletarla;
    public string segParaCompletarla;

    public bool Tiempo;

    public bool checkpoint1;
    public bool checkpoint2;
    public bool checkpoint3;

    public void Print()
    {
        Debug.Log(nombre + ": " + descripcion + " | Tienes " + minParaCompletarla + " min para completarla");
    }

}
