using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextGame/InteractableObject")]
public class InteractableObject : ScriptableObject
{
    public string noun = "name";
    [TextArea]
    public string description = "Description in room";

    public bool showsDescriptionInRoom;
    public bool canPickup;

    public AudioClip playOnPickup;

    public Interaction[] interactions;
}

