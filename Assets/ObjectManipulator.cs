using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{
	[SerializeField] GameObject[] blockmanArray;
	public GameObject blockPrefab;
	int frame = 0;

	// Start is called before the first frame update
	void Start()
    {

		MakeBlockMan();
		int index = 0;

        foreach (GameObject go in blockmanArray)
        {
            go.SetActive(true);
            go.transform.position = GetJointVector(index);
            index++;
        }

        GameObject root = new GameObject("Blockman Root");//create root for block man, just an empty gameobjecct, we want it to be position at the same spot as the pelvis but at y = 0
        Vector3 pelvisPos = blockmanArray[0].transform.position; //get pelvis position
        root.transform.position = new Vector3(pelvisPos.x, 0, pelvisPos.z);//set root position on y=0 below pelvis
        ParentBlockManToRoot(root.transform); //parent all blocks to root
        root.transform.forward = -Camera.main.transform.forward;//flip root around to invert

        print("Initialized");
	}

    // Update is called once per frame
    void Update()
    {
		//rotateChildCubes();
		//flipBlockman();
	}
    #region not being used right now

    void UpdateBlockman()
    {
        int index = -1;
        foreach (string joint in new[] { "" })
        {
            index++;
            string[] jointPositionQuat = joint.Split('#');

            try
            {
                float x = float.Parse(jointPositionQuat[0].Replace("@", string.Empty));
                float y = float.Parse(jointPositionQuat[1]);
                float z = float.Parse(jointPositionQuat[2]);

                float qw = float.Parse(jointPositionQuat[3]);
                float qx = float.Parse(jointPositionQuat[4]);
                float qy = float.Parse(jointPositionQuat[5]);
                float qz = float.Parse(jointPositionQuat[6]);

                Vector3 v = new Vector3(x, -y, z) * 0.004f;
                Quaternion r = new Quaternion(qx, qy, qz, qw);

                GameObject obj = blockmanArray[index];
                obj.transform.SetPositionAndRotation(v, r);
            }
            catch (Exception e)
            {
                Debug.Log("jointPositionQuat: " + jointPositionQuat);
                Debug.LogError(e.GetBaseException().ToString());
                Debug.LogError(e.ToString());
                Debug.LogError(e.Message);
            }
        }
    }
    //void rotateChildCubes()
    //{
    //	if (frame > 360) { frame = 0; }
    //	float rotationStep = frame % 360f;
    //	// Debug.Log("rotationStep: " + rotationStep);

    //	int index = 0;
    //	foreach (GameObject go in blockmanArray)
    //	{
    //		go.transform.Rotate(0.0f, rotationStep, 0.0f, Space.Self);
    //		// go.transform.rotation = Quaternion.Euler(0, 90, 0);
    //		index++;
    //	}
    //	frame++;
    //}

    void flipBlockman()
	{
		if (frame < 360) { frame = 0; }
		float rotationStep = frame % 360f;
		// Debug.Log("rotationStep: " + rotationStep);
		int index = 0;
		foreach (GameObject go in blockmanArray)
		{
			if (rotationStep % 180 == 0 && index > 3 && index < 20)
			{
				go.transform.position = new Vector3(go.transform.position.x, -go.transform.position.y, go.transform.position.z);
			}
			else if (rotationStep % 180 == 0 && index > 3 && index < 20)
			{
				go.transform.position = new Vector3(go.transform.position.x, -go.transform.position.y, go.transform.position.z);
			}
			go.transform.Rotate(0.0f, rotationStep, 0.0f, Space.Self);
			index++;
		}
	}
    #endregion
    #region  joint data
    Vector3 GetJointVector(int jointIndex)
	{
		Vector3[] joints = new[]{
			new Vector3(0.3f, 0.3f, 8.3f),
			new Vector3(0.4f, 1.0f, 8.2f),
			new Vector3(0.6f, 1.5f, 8.2f),
			new Vector3(0.8f, 2.4f, 8.2f),
			new Vector3(0.9f, 2.2f, 8.2f),
			new Vector3(1.4f, 2.1f, 8.1f),
			new Vector3(2.3f, 1.5f, 8.4f),
			new Vector3(3.1f, 1.1f, 8.1f),
			new Vector3(0.7f, 2.3f, 8.3f),
			new Vector3(0.1f, 2.4f, 8.3f),
			new Vector3(-0.9f, 2.7f, 8.3f),
			new Vector3(-1.5f, 3.4f, 7.9f),
			new Vector3(0.6f, 0.2f, 8.1f),
			new Vector3(0.4f, -1.4f, 8.3f),
			new Vector3(0.3f, -2.9f, 8.8f),
			new Vector3(0.1f, -3.4f, 8.3f),
			new Vector3(0.0f, 0.3f, 8.4f),
			new Vector3(-1.4f, 0.1f, 7.7f),
			new Vector3(-1.6f, -1.4f, 7.9f),
			new Vector3(-2.1f, -1.8f, 7.7f),
			new Vector3(0.8f, 2.7f, 8.1f),
			new Vector3(0.7f, 3.0f, 7.5f),
			new Vector3(0.9f, 3.1f, 7.6f),
			new Vector3(1.2f, 3.0f, 8.0f),
			new Vector3(0.6f, 3.1f, 7.7f),
			new Vector3(0.5f, 3.0f, 8.2f)
		};

		return joints[jointIndex];
	}

	string GetJointName(int jointIndex)
	{
		string[] joints = new[]{
			"Pelvis", // 0
			"SpineNaval", // 1
			"SpineChest", // 2
			"Neck", // 3

			"ClavicleLeft", // 4
			"ShoulderLeft", // 5
			"ElbowLeft", // 6
			"WristLeft", // 7
			"ClavicleRight", // 8
			"ShoulderRight", // 9
			"ElbowRight", // 10
			"WristRight", // 11
			"HipLeft", // 12
			"KneeLeft", // 13
			"AnkleLeft", // 14
			"FootLeft", // 15
			"HipRight", // 16
			"KneeRight", // 17
			"AnkleRight", // 18
			"FootRight", // 19

			"Head", // 20
			"Nose", // 21

			"EyeLeft", // 22 
			"EarLeft", // 23
			"EyeRight", // 24
			"EarRight", // 25
		};

		return joints[jointIndex];
	}
    #endregion

	void MakeBlockMan()
	{
		int numberOfJoints = 20; //don't need nose, eyes or ears yet// 32; //  (int)JointId.Count;

		blockmanArray = new GameObject[numberOfJoints];

		for (var i = 0; i < numberOfJoints; i++)
		{
			GameObject jointCube = Instantiate(blockPrefab, transform);
			//deactivate it - (its Start() or OnEnable() won't be called)
			jointCube.SetActive(false);
			jointCube.name = GetJointName(i); // Enum.GetName(typeof(JointId), i);
			//why do we multiply by .4?  idk
			jointCube.transform.localScale = Vector3.one * 0.4f;
			blockmanArray[i] = jointCube;
		}
	}

    void ParentBlockManToRoot(Transform parent)
    {
        foreach (GameObject g in blockmanArray)
        {
            g.transform.parent = parent;
        }
    }
}
