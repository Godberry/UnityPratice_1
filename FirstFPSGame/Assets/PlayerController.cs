using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator AnimatorController;
    public Transform rotateYTransform;
    public Transform rotateXTransform;
    public float rotateSpeed;
    public float fCurrentRotateX = 0;
    public float m_fMoveSpeed;
    float nCurrentSpeed = 0;

    public Rigidbody rigidBody;

    public JumpSensor JumpSensor;
    public float fJumpSpeed;
    public GunManager gunManager;

	// Use this for initialization
	void Start () {
        AnimatorController = this.GetComponent<Animator>();
        Cursor.visible = false;
	}


	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton (0))
        {
            gunManager.TryToTriggerGun();
        }

        /// 決定鍵盤Input的結果
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveDirection.z += 1;
        if (Input.GetKey(KeyCode.S)) moveDirection.z -= 1;
        if (Input.GetKey(KeyCode.A)) moveDirection.x -= 1;
        if (Input.GetKey(KeyCode.D)) moveDirection.x += 1;
        moveDirection = moveDirection.normalized;

        if (moveDirection.magnitude == 0 || !JumpSensor.IsCanJump())
        {
            nCurrentSpeed = 0;
        }
        else
        {
            if (moveDirection.z < 0)
                nCurrentSpeed = -m_fMoveSpeed;
            else
                nCurrentSpeed = m_fMoveSpeed; 
        }
        AnimatorController.SetFloat("Speed", nCurrentSpeed);

        /// 轉換成世界座標的方向
        Vector3 wordSpaceDirection = moveDirection.z * rotateYTransform.transform.forward + 
                                     moveDirection.x * rotateYTransform.transform.right;

        Vector3 velocity = rigidBody.velocity;
        velocity.x = wordSpaceDirection.x * m_fMoveSpeed;
        velocity.z = wordSpaceDirection.z * m_fMoveSpeed;

        /// 跳躍處理
        if (Input.GetKey (KeyCode.Space) && JumpSensor.IsCanJump ())
        {
            velocity.y = fJumpSpeed;
        }
        rigidBody.velocity = velocity; 

        /// 計算滑鼠操作
        rotateYTransform.transform.localEulerAngles += new Vector3(0, Input.GetAxis("Horizontal"), 0) * rotateSpeed;
        fCurrentRotateX -= Input.GetAxis("Vertical") * rotateSpeed;

        if (fCurrentRotateX > 90)
            fCurrentRotateX = 90;
        else if (fCurrentRotateX < -90)
            fCurrentRotateX = -90;

        rotateXTransform.transform.localEulerAngles = new Vector3(fCurrentRotateX, 0, 0);
    }
}
