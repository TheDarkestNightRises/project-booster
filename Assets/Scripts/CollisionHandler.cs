using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly fire");
                break;
            case "Finish":
                Debug.Log("Congrats!");
                break ;
            default:
                Debug.Log("Sorry you blew up");
                break;
        }
    }
}
