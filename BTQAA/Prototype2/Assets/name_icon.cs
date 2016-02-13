using UnityEngine;
using System.Collections;

public class name_icon : MonoBehaviour
{

    public TextMesh itself;

    void Start()
    {
        this.GetComponent<Renderer>().sortingLayerID =
        this.transform.parent.GetComponent<Renderer>().sortingLayerID;
        itself.text = itself.GetComponentInParent<AuthoredGuestManager>().actor_name;
    }


    void Update()
    {
    }
}