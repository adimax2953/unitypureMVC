using UnityEngine;
using System.Collections;


public class DragRigidbody : MonoBehaviour {
	//轉動的速度,數值越小越慢
	public float spring=80.0f;

	public float damper = 50.0f;

	//拖曳慣性:突然停止時,來回緩衝的動態程度 數值越小動態越明顯
	public float drag= 10.0f;

	//旋轉慣性:突然停止時,來回緩衝的動態程度 數值越小動態越明顯
	public float angularDrag= 5f;

	//滑鼠要在物件上移動多少距離,才觸發拖曳動作
	public float distance = 0.001f;

	public bool attachToCenterOfMass= false;
	private SpringJoint springJoint;
	private Rigidbody body;

	

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		if(!Input.GetMouseButtonDown(0)) return;
        Camera mainCamera = FindCamera();
        //Camera mainCamera = GameObject.Find("unlockCamera").GetComponent<Camera>();
        RaycastHit hit;  
		//檢測滑鼠按鍵是否點擊 
		if(!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),out hit,100)) return;
		//是否有點擊到剛性物體或剛性物體可被物理控制 
		if(!hit.rigidbody||hit.rigidbody.isKinematic) return;
        if (!springJoint)
        { //彈性物體是否存在
			//新增一個名稱為"Rigidbody dragger"的遊戲物件
			GameObject go = new GameObject("Rigidbody dragger");  
			body = (Rigidbody)go.AddComponent<Rigidbody>();
			springJoint = (SpringJoint)go.AddComponent<SpringJoint>();
			body.isKinematic = true;

        }
		springJoint.transform.position = hit.point;
		if(attachToCenterOfMass){
			Vector3 anchor = transform.TransformDirection(hit.rigidbody.centerOfMass)+
				hit.rigidbody.transform.position;
			anchor = springJoint.transform.InverseTransformPoint(anchor);
			springJoint.anchor = anchor;
		}else{
			springJoint.anchor = Vector3.zero;
		}
		
		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = hit.rigidbody;
		//執行協程(執行緒)，處理拖行物體
		StartCoroutine("DragObject",hit.distance);
        
    }
	
	IEnumerator DragObject(float distance){
		float oldDrag = springJoint.connectedBody.drag;
		float oldAngularDrag = springJoint.connectedBody.angularDrag;
		springJoint.connectedBody.drag = drag; 
		springJoint.connectedBody.angularDrag = angularDrag;
		Camera mainCamera = FindCamera();
		while(Input.GetMouseButton(0)){
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			springJoint.transform.position = ray.GetPoint(distance);  
			yield return 0;
			
		}
		
		if(springJoint.connectedBody){
			springJoint.connectedBody.drag = oldDrag;
			springJoint.connectedBody.angularDrag = oldAngularDrag;
			springJoint.connectedBody = null;
			
		}
		
	}
	
	//取得主要攝影機
	Camera FindCamera()
    {
		if(this.GetComponent<Camera>())
        {
			return this.GetComponent<Camera>();
		}
        else
        {
			return this.GetComponent<Camera>();  //回傳標註為MainCamera的攝影機物件
		}
	}
    //Camera FindCamera()
    //{
    //    if (GameObject.Find("Camera").GetComponent<Camera>())
    //    {
    //        return GameObject.Find("Camera").GetComponent<Camera>();
    //    }
    //    else
    //    {
    //        return Camera.main;  //回傳標註為MainCamera的攝影機物件
    //    }
    //}
}