using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Leap;
using Leap.Unity;
using Leap.Unity.Encoding;
using System;

public class SimpleController : MonoBehaviour
{
    public LeapServiceProvider leapServiceProvider;
    public bool HAS_LEFT = false;
    public bool HAS_RIGHT = false;
    public SimpleHand LEFT;
    public SimpleHand RIGHT;

    public enum Type
    {
        LEFT,
        RIGHT
    }

    [Serializable]
    public class SimpleHand
    {
        Type type;

        public Vector3 ELBOW_P;
        public Vector3 WRIST_P;
        public Vector3 ELBOW_R;
        public Vector3 WRIST_R;

        public List<List<Vector3>> FINGERS_P;
        public List<List<Vector3>> FINGERS_R;

        public SimpleHand()
        {
            ELBOW_P = new Vector3();
            WRIST_P = new Vector3();
            ELBOW_R = new Vector3();
            WRIST_R = new Vector3();

            FINGERS_P = new List<List<Vector3>>(5);
            FINGERS_R = new List<List<Vector3>>(5);

            for (int i = 0; i < 5; i++)
            {
                FINGERS_P.Add(new List<Vector3>(5));
                FINGERS_R.Add(new List<Vector3>(4));

                for (int j = 0; j < 5; j++)
                {
                    FINGERS_P[i].Add(new Vector3());
                }

                for (int j = 0; j < 4; j++)
                {
                    FINGERS_R[i].Add(new Vector3());
                }
            }
        }

        public SimpleHand(Hand hand)
        {
            ELBOW_P = hand.Arm.ElbowPosition;
            WRIST_P = hand.WristPosition;
            ELBOW_R = hand.Arm.Rotation.eulerAngles;
            WRIST_R = hand.Rotation.eulerAngles;

            FINGERS_P = new List<List<Vector3>>(5);
            FINGERS_R = new List<List<Vector3>>(5);

            for (int i = 0; i < 5; i++)
            {
                FINGERS_P.Add(new List<Vector3>(5));
                FINGERS_R.Add(new List<Vector3>(4));
            }

            foreach (Finger finger in hand.Fingers)
            {
                Finger.FingerType finerType = finger.Type;

                Vector3 metacarpalPrevJoint = finger.Bone(Bone.BoneType.TYPE_METACARPAL).PrevJoint;
                Vector3 proximalPrevJoint = finger.Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
                Vector3 intermediatePrevJoint = finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).PrevJoint;
                Vector3 distalPrevJoint = finger.Bone(Bone.BoneType.TYPE_DISTAL).PrevJoint;
                Vector3 distalNextJoint = finger.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint;

                FINGERS_P[(int)finerType].Add(metacarpalPrevJoint);
                FINGERS_P[(int)finerType].Add(proximalPrevJoint);
                FINGERS_P[(int)finerType].Add(intermediatePrevJoint);
                FINGERS_P[(int)finerType].Add(distalPrevJoint);
                FINGERS_P[(int)finerType].Add(distalNextJoint);

                Vector3 metacarpalRotation = finger.Bone(Bone.BoneType.TYPE_METACARPAL).Rotation.eulerAngles;
                Vector3 proximalRotation = finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Rotation.eulerAngles;
                Vector3 intermediateRotation = finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Rotation.eulerAngles;
                Vector3 distalRotation = finger.Bone(Bone.BoneType.TYPE_DISTAL).Rotation.eulerAngles;

                FINGERS_R[(int)finerType].Add(metacarpalRotation);
                FINGERS_R[(int)finerType].Add(proximalRotation);
                FINGERS_R[(int)finerType].Add(intermediateRotation);
                FINGERS_R[(int)finerType].Add(distalRotation);
            }
        }

        public List<Vector3> GetPositionsList()
        {
            List<Vector3> positions = new List<Vector3>();

            positions.Add(ELBOW_P);
            positions.Add(WRIST_P);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    positions.Add(FINGERS_P[i][j]);
                }
            }

            return positions;
        }

        public List<Vector3> GetRotationsList()
        {
            List<Vector3> rotations = new List<Vector3>();

            rotations.Add(ELBOW_R);
            rotations.Add(WRIST_R);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    rotations.Add(FINGERS_R[i][j]);
                }
            }

            return rotations;
        }
    }

    void Start()
    {
        HAS_LEFT = false;
        HAS_RIGHT = true;
        LEFT = new SimpleHand();
        RIGHT = new SimpleHand();
    }

    void Update()
    {
        Frame frame = leapServiceProvider.CurrentFrame;

        HAS_LEFT = false;
        HAS_RIGHT = false;

        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft)
            {
                HAS_LEFT = true;
                LEFT = new SimpleHand(hand);
            }
            else
            {
                HAS_RIGHT = true;
                RIGHT = new SimpleHand(hand);
            }
        }
    }
}
