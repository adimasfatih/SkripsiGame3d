using UnityEngine;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputActionReference Move,Dodge,Attack,Skill, Pause;
    public static Vector2 Movement;
    public static bool _Dodge;
    public static bool _Attack;
    public static bool _Skill;
    public static bool _Esc;


    // Update is called once per frame
    void Update()
    {
        Movement = Move.action.ReadValue<Vector2>();
       
        _Dodge = Dodge.action.IsPressed();

        _Attack = Attack.action.IsPressed();

        _Skill = Skill.action.IsPressed();

        _Esc = Pause.action.IsPressed();
    }
}
