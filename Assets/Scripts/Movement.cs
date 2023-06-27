using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public GameObject Player;
    public InputActionProperty Walk;
    public float Speed;
    public GameObject Controller;
    public GameObject DeTurn;
    public InputActionProperty Jetpack;
    public InputActionProperty Snapturn;
    public float throttle;
    public float force;
    public float drag;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 Joystick = Walk.action.ReadValue<Vector2>();
        Joystick = Joystick.normalized;

        throttle = Jetpack.action.ReadValue<float>();
        Player.GetComponent<Rigidbody>().AddForce(Controller.transform.forward * throttle * force);


        Player.transform.localEulerAngles = new Vector3(0, Controller.transform.localEulerAngles.y, 0);
        DeTurn.transform.localEulerAngles = new Vector3(0, -Controller.transform.localEulerAngles.y, 0);

        Player.GetComponent<Rigidbody>().AddRelativeForce(Joystick.x * Speed * Time.deltaTime, 0, Joystick.y * Speed * Time.deltaTime, ForceMode.Impulse);
        Player.GetComponent<Rigidbody>().AddForce(new Vector3(-Player.GetComponent<Rigidbody>().velocity.x * drag, 0, -Player.GetComponent<Rigidbody>().velocity.z * drag) * Time.deltaTime, ForceMode.Impulse);

        // Player.transform.Translate(Joystick.x * Speed, 0, Joystick.y * Speed);

        if (Snapturn.action.triggered && Mathf.Abs(Snapturn.action.ReadValue<Vector2>().x) > 0.5f)
        {
            // Snap turn by rotating the player by 25 degrees in the appropriate direction
            float turnAmount = Mathf.Sign(Snapturn.action.ReadValue<Vector2>().x) * 25f;
            Player.transform.Rotate(0, turnAmount, 0);
        }
    }
}