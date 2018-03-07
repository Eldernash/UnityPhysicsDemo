using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour {

    public float m_xSpeed = 1.0f;
    public float m_ySpeed = 1.0f;

    public float m_xLimit = 90.0f;
    public float m_yLimit = 90.0f;

    private float m_xRotation = 0.0f;
    private float m_yRotation = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        m_xRotation += Input.GetAxis("Mouse X") * m_xSpeed;
        m_xRotation = Mathf.Clamp(m_xRotation, -m_xLimit, m_xLimit);

        m_yRotation += Input.GetAxis("Mouse Y") * m_ySpeed;
        m_yRotation = Mathf.Clamp(m_yRotation, -m_yLimit, m_yLimit);

        transform.localEulerAngles = new Vector3(-m_yRotation, m_xRotation, 0) * Time.deltaTime;
    }
}
