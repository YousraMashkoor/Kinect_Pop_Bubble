using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

using Windows.Kinect;
// to differentiate b/w kinect and unity joint
using Joint = Windows.Kinect.Joint;

public class BodySourceView : MonoBehaviour 
{
    public BodySourceManager mBodySourceManager;
    //refrence to prefab
    public GameObject mJointObject;
    
    //list of bodies that camera can correnty see
    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();
    // list of joints we're connecting prefab to
    private List<JointType> _joints = new List<JointType>
    {
        JointType.HandLeft,
        JointType.HandRight,
    };

    void Update()
    {
        #region GET KINECT DATA
        //region get kinect data
        Body[] data = mBodySourceManager.GetData();
        
       // Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        //ids for bodies that kinect can see 
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }

        #endregion

        #region DELETE KINECT DATA
        //keys from body dictionary
        List<ulong> knownIds = new List<ulong>(mBodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                // destroy body object
                Destroy(mBodies[trackingId]);
                // delete from list
                mBodies.Remove(trackingId);
            }
        }
        #endregion

        #region CREATE KINECT BODIES

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {

                //if body isn't tracked create body
                if (!mBodies.ContainsKey(body.TrackingId))
                {
                    mBodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                
                //update positions
                UpdateBodyObject(body, mBodies[body.TrackingId]);
            }
        }

        #endregion
    }

    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        foreach (JointType joint in _joints)
        {
            //Create objects ... instantiating the prefab
            GameObject newJoint = Instantiate(mJointObject);
            newJoint.name = joint.ToString();

            //Parent to body
            newJoint.transform.parent = body.transform;

        }
        return body;
    }
    
    private void UpdateBodyObject(Body body, GameObject bodyObject)
    {
        // jointtype is an enum
        foreach (JointType _joint in _joints)
        {
            //Get new target position.. passing enum to joinType
            Joint sourceJoint = body.Joints[_joint];
            Vector3 targetPosition = GetVector3FromJoint(sourceJoint);
            targetPosition.z = 0;
            // GET JOINT, SET NEW POSITION
            Transform jointObject = bodyObject.transform.Find(_joint.ToString());
            jointObject.position = targetPosition;
             
        }
    }
    
   /* private static Color GetColorForState(TrackingState state)
    {
        switch (state)
        {
        case TrackingState.Tracked:
            return Color.green;

        case TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    } */
    
    private static Vector3 GetVector3FromJoint(Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
