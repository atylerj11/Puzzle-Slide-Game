using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private GameObject selectedGO;
    private Camera mc;
    float lx, ly;
    private Rigidbody rb;
    private bool isColliding=false;
    private Vector3 origin;
    private int directionalFlag = 1;
    void Start()
    {
        origin = transform.position;
        if(mc==null) mc=Camera.main;
        if(rb==null) rb=GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        stopPlayerMovement();
    }

    private void OnMouseDown()
    {
        RaycastHit hit=findingAnObjectRC();
        if(selectedGO==null)
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                selectedGO=hit.collider.gameObject;
                Cursor.visible=false;
                selectedGO.GetComponent<Renderer>().material.color=Color.yellow;
            }

    }
    private void OnMouseDrag()
    {
        if(!isColliding&&selectedGO!=null)
        {
            if (selectedGO.transform.rotation.eulerAngles.y == 90)
            {
                if (origin.x - transform.position.x <= 0)
                {
                    directionalFlag = 1;


                }
                else
                {
                    directionalFlag = -1;
                }
                lx=Input.mousePosition.x;
                ly=Input.mousePosition.y;
                Vector3 position = new Vector3(lx,ly,
                mc.WorldToScreenPoint(selectedGO.transform.position).z);
                Vector3 worldPos=mc.ScreenToWorldPoint(position);
                rb.MovePosition(new Vector3(worldPos.x, 0.20f, origin.z));
            }
            else{
                if (origin.x - transform.position.x <= 0)
                {
                    directionalFlag = 1;


                }
                else
                {
                    directionalFlag = -1;
                }
            lx=Input.mousePosition.x;
            ly=Input.mousePosition.y;
            Vector3 position = new Vector3(lx,ly,
            mc.WorldToScreenPoint(selectedGO.transform.position).z);
            Vector3 worldPos=mc.ScreenToWorldPoint(position);
            rb.MovePosition(new Vector3(origin.x, 0.20f, worldPos.z));
            }
        }
    }
    private void OnMouseUp()
    {
        if (selectedGO.transform.rotation.eulerAngles.y == 90){
        Cursor.visible=true;
        Vector3 position = new Vector3(lx,ly,
        mc.WorldToScreenPoint(selectedGO.transform.position).z);
        Vector3 worldPos=mc.ScreenToWorldPoint(position);
        Vector3 playerPos=rb.position;
        if(isColliding)
        {
            rb.MovePosition(new Vector3(rb.position.x, rb.position.y,
            rb.position.z-0.2f));
            selectedGO=null;
            isColliding=false;
        }
            rb.MovePosition(new Vector3(Mathf.Round(rb.position.x), rb.position.y,
            rb.position.z));
            }
        else{
            Cursor.visible=true;
            Vector3 position = new Vector3(lx,ly,
            mc.WorldToScreenPoint(selectedGO.transform.position).z);
            Vector3 worldPos=mc.ScreenToWorldPoint(position);
            Vector3 playerPos=rb.position;
            if(isColliding)
            {
                rb.MovePosition(new Vector3(rb.position.x, rb.position.y,
                rb.position.z-0.2f));
                selectedGO=null;
                isColliding=false;
            }
            rb.MovePosition(new Vector3(rb.position.x, rb.position.y,
                Mathf.Round(rb.position.z)));
            }

            
    }
    private void stopPlayerMovement()
    {
        Vector3 origin=transform.position;
        Vector3 direction=directionalFlag*transform.forward;
        float dangerDistance=1.3f;
        Ray ray=new Ray(origin, direction);
        Debug.DrawRay(origin, direction*dangerDistance, Color.white);
        if(Physics.Raycast(ray, out RaycastHit h, dangerDistance))
        {
            if(h.collider.gameObject.CompareTag("Wall02") || h.collider.gameObject.CompareTag("Player")){
                isColliding=true;}
        }
    }

    private RaycastHit findingAnObjectRC()
    {
        Vector2 mouseSceenPosition=Input.mousePosition;
        Ray ray=mc.ScreenPointToRay(mouseSceenPosition);
        Physics.Raycast(ray, out RaycastHit h);
        return h;
    }
}
