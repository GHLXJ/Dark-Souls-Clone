using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//ActorController==moveController
public class CameraController : MonoBehaviour
{
    //public PlayerInput pi;
    private IUserInput pi;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 100.0f;
    public float camerDampValue = 0.1f;
    public Image lockDot;
    public bool lockState;
    public bool isAI = false;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerx;
    private GameObject model;
    private GameObject camera;

    private Vector3 cameraDampVelocity;

    private LockTarget lockTarget;
    void Start()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerx = 20.0f;

        pi = playerHandle.GetComponent<moveController>().pi;
        model = playerHandle.GetComponent<moveController>().model;

        if (!isAI)
        {
            camera = Camera.main.gameObject;
            lockDot.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            lockDot.enabled = false;
        }//else是自己加的


        lockState = false;

    }

    void FixedUpdate()
    {
        if (lockTarget == null)
        {


            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);

            tempEulerx -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
            tempEulerx = Mathf.Clamp(tempEulerx, -40, 30);

            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerx, 0, 0);

            model.transform.eulerAngles = tempModelEuler;

        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            cameraHandle.transform.LookAt(lockTarget.obj.transform);
        }

        if (!isAI)
        {
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, camerDampValue);
            camera.transform.LookAt(cameraHandle.transform);
        }


    }

    private void Update()
    {
        if (lockTarget != null)
        {
            if (!isAI)
            {
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0,lockTarget.halfHight,0));

            }
            if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f)//超过距离放弃锁定
            {
                LockProcessA(null, false, false, isAI);
            }

            //ActorManager targetAm = lockTarget.obj.GetComponent<ActorManager>();
            if (lockTarget.am != null && lockTarget.am.sm.isDie)
            {
                LockProcessA(null, false, false, isAI);
            }
        }
    }

    private void LockProcessA(LockTarget _lockTarget,bool _lockDotEnable,bool _lockState,bool _isAI)
    {
        lockTarget = _lockTarget;
        if(!_isAI)
        {
            lockDot.enabled = _lockDotEnable;
        }
        lockState = _lockState;
    }

    public void LockUnlock()
    {

        Vector3 modelOrigin1 = model.transform.position;
        Vector3 modelOrigin2 = model.transform.position + new Vector3(0, 1, 0);
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask(isAI?"Player":"Enemy"));
        if (cols.Length == 0)
        {
            LockProcessA(null, false, false, isAI);
        }
        else
        {
            foreach (Collider col in cols)
            {
                if (lockTarget !=null && lockTarget.obj == col.gameObject)
                {
                    LockProcessA(null, false, false, isAI);
                    break;
                }

                lockTarget = new LockTarget(col.gameObject,col.bounds.extents.y);
                LockProcessA(lockTarget, true, true, isAI);
                break;
            }
        }

    }

    private class LockTarget
    {
        public GameObject obj;
        public float halfHight;
        public ActorManager am;

        
        public LockTarget(GameObject _obj,float _halfHeight)
        {
            obj = _obj;
            halfHight = _halfHeight;
            am = _obj.GetComponent<ActorManager>();
        }
    }
}
