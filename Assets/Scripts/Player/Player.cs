using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

public class Player : MonoBehaviour
{
    public PlayerController controller;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        this.AssignComponent(ref controller);
    }
}
