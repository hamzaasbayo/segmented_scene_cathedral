using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    public GameObject lastHit = null;
    public Vector3 collision = Vector3.zero;
    public GameObject selectedObject = null;

    public AudioSource source = null;


    // Update is called once per frame
    void Update()
    {

        // test git 
        RaycastHit hit;
        var ray = new Ray(this.transform.position, this.transform.forward);
      
        if (Physics.Raycast(ray, out hit, 1000))
        {
            lastHit = hit.transform.gameObject;
            collision = hit.point;

            SelectObject(lastHit);
        }
        else
        {
            ClearSelection();
        }
    }

    void SelectObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            if (obj == selectedObject)
                return;

            ClearSelection();
        }
        // Play the audio 
        selectedObject = obj;
        if (selectedObject.GetComponent<AudioSource>()) {
            source = selectedObject.GetComponent<AudioSource>();
            source.Play();
        }

        // Change the color of the hitted gameObject 
        Renderer r = selectedObject.GetComponent<Renderer>();
        Material m = r.material;
        m.color = Color.green;
    }

    void ClearSelection()
    {
        if (selectedObject == null)
            return;

        Renderer r = selectedObject.GetComponent<Renderer>();
        Material m = r.material;
        m.color = Color.gray;

        selectedObject = null;
    }

    void OnDrawGizmos()
    {
        Update();

        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, collision);
        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}
