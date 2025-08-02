using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterAvartarShop : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            VideoManagerAndCutScene.instance.PlayCutScene("CustomeAvartar");
        }
    }
   



}

