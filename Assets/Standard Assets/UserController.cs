using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
    [Header("Camera")]
    public float rotX;
    public float lookSensitivityX;
    public float lookSensitivityY;

    Animator animator;
    public float aim;
    bool isAiming;

    public Transform aimPos;
    public Transform noAimPos;

    Camera camera;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical") *0.5f;
        float h = Input.GetAxis("Horizontal") * 0.5f;

        animator.SetFloat("Forward", v);
        //animator.SetFloat("Turn", h);

        
        
        if (Input.GetButton("Fire1"))
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        animator.SetFloat("Aim", aim);

        CamLook();
    }
    public float ys = 0;
    private void CamLook()
    {

        ys = Mathf.Lerp(ys, Input.GetAxis("Mouse X"), 0.1f);
        ys = Mathf.Clamp(ys, -1, 1);


        rotX += Input.GetAxis("Mouse Y") * lookSensitivityY;
        rotX = Mathf.Clamp(rotX, 0, 1);

        animator.SetFloat("Look up", rotX);
        animator.SetFloat("Turn", ys);
    }

    private void FixedUpdate()
    {
        if (isAiming)
        {
            aim = Mathf.Lerp(aim, 1, 0.2f);

            
            camera.transform.position = Vector3.Lerp(camera.transform.position,aimPos.position,0.3f);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, aimPos.rotation, 0.3f);
        }
        else
        {
            aim = Mathf.Lerp(aim, 0, 0.2f);

            camera.transform.position = Vector3.Lerp(camera.transform.position, noAimPos.position, 0.3f);
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, noAimPos.rotation, 0.3f);
        }

        animator.SetFloat("Aim", aim);
    }


}
