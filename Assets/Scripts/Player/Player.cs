using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var enemy = hit.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        var enemy = hit.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            SceneManager.LoadScene(0);
        }
    }
}