using UnityEngine;

[CreateAssetMenu(fileName = "CookieShape", menuName = "Scriptable Objects/CookieShape")]
public class CookieShape : ScriptableObject
{
    public string shapeName; //for debugging later
    public GameObject shapePrefab;       // heart, square, circle
    public GameObject[] baseVariants;    // Prefabs for base depending on shape
    public GameObject[] syrupVariants;   // Prefabs for syrup depending on shape
    public GameObject[] decorVariants;   // Prefabs for decors -> IK this part is static for all shapes but for cleaner code sake!
}
