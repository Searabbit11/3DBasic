using UnityEngine;
using UnityEngine.Serialization;

public class RepeatingRotator : MonoBehaviour
{
    [System.Serializable]
    public struct RotationInfo
    {
        public Vector3 axis;
        public float speed;
        public bool isLocal;
    }

    public RotationInfo rotationInfo;
    
    // Update is called once per frame
    void Update()
    {
        float deltaAngle = rotationInfo.speed * Time.deltaTime;
        Vector3 deltaEuler = rotationInfo.axis.normalized * deltaAngle;
        Space axisSpace = rotationInfo.isLocal ? Space.Self : Space.World; 
        transform.Rotate(deltaEuler, axisSpace);
    }
}

