using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public GameObject Player;
    public InputActionProperty Input;
    public float Speed;
    public GameObject Controller;
    public GameObject DeTurn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Joystick = Input.action.ReadValue<Vector2>();
        Joystick = Joystick.normalized;
        Player.transform.localEulerAngles = new Vector3(0, Controller.transform.localEulerAngles.y, 0);
        DeTurn.transform.localEulerAngles = new Vector3(0, -Controller.transform.localEulerAngles.y, 0);
        Player.transform.Translate(Joystick.x * Speed, 0, Joystick.y * Speed) ;
        Debug.Log(Player.transform.eulerAngles.y);
    }
}