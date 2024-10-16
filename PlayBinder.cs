using UnityEngine;

public class PlayBinder : MonoBehaviour
{
    public SimpleController.Type handType;

    public Transform ELBOW;
    public Transform WRIST;

    public Transform THUMB_METACARPAL;
    public Transform THUMB_PROXIMAL;
    public Transform THUMB_INTERMEDIATE;
    public Transform THUMB_DISTAL;

    public Transform INDEX_METACARPAL;
    public Transform INDEX_PROXIMAL;
    public Transform INDEX_INTERMEDIATE;
    public Transform INDEX_DISTAL;

    public Transform MIDDLE_METACARPAL;
    public Transform MIDDLE_PROXIMAL;
    public Transform MIDDLE_INTERMEDIATE;
    public Transform MIDDLE_DISTAL;

    public Transform RING_METACARPAL;
    public Transform RING_PROXIMAL;
    public Transform RING_INTERMEDIATE;
    public Transform RING_DISTAL;

    public Transform PINKY_METACARPAL;
    public Transform PINKY_PROXIMAL;
    public Transform PINKY_INTERMEDIATE;
    public Transform PINKY_DISTAL;

    private Vector3 rotateVector = new Vector3(0, -90, 180);

    void Start(){
        if(handType == SimpleController.Type.RIGHT){
            rotateVector *= -1;
        }
    }

    [ContextMenu("Bind")]
    public void Bind(){
        ELBOW = FindChildRecursively(transform, "Elbow");
        WRIST = FindChildRecursively(transform, "Wrist");

        THUMB_METACARPAL = FindChildRecursively(transform, "thumb_meta");
        THUMB_PROXIMAL = FindChildRecursively(transform, "thumb_meta");
        THUMB_INTERMEDIATE = FindChildRecursively(transform, "thumb_a");
        THUMB_DISTAL = FindChildRecursively(transform, "thumb_b");

        INDEX_METACARPAL = FindChildRecursively(transform, "index_meta");
        INDEX_PROXIMAL = FindChildRecursively(transform, "index_a");
        INDEX_INTERMEDIATE = FindChildRecursively(transform, "index_b");
        INDEX_DISTAL = FindChildRecursively(transform, "index_c");
        
        MIDDLE_METACARPAL = FindChildRecursively(transform, "middle_meta");
        MIDDLE_PROXIMAL = FindChildRecursively(transform, "middle_a");
        MIDDLE_INTERMEDIATE = FindChildRecursively(transform, "middle_b");
        MIDDLE_DISTAL = FindChildRecursively(transform, "middle_c");
        
        RING_METACARPAL = FindChildRecursively(transform, "ring_meta");
        RING_PROXIMAL = FindChildRecursively(transform, "ring_a");
        RING_INTERMEDIATE = FindChildRecursively(transform, "ring_b");
        RING_DISTAL = FindChildRecursively(transform, "ring_c");

        PINKY_METACARPAL = FindChildRecursively(transform, "pinky_meta");
        PINKY_PROXIMAL = FindChildRecursively(transform, "pinky_a");
        PINKY_INTERMEDIATE = FindChildRecursively(transform, "pinky_b");
        PINKY_DISTAL = FindChildRecursively(transform, "pinky_c");
    }

    public void GenerateTransforms(Vector3[] vectors){

        /*
         *  positions
         */

        ELBOW.position = vectors[0];
        WRIST.position = vectors[1];

        THUMB_METACARPAL.position = vectors[2];
        THUMB_PROXIMAL.position = vectors[3];
        THUMB_INTERMEDIATE.position = vectors[4];
        THUMB_DISTAL.position = vectors[5];

        INDEX_METACARPAL.position = vectors[7];
        INDEX_PROXIMAL.position = vectors[8];
        INDEX_INTERMEDIATE.position = vectors[9];
        INDEX_DISTAL.position = vectors[10];

        MIDDLE_METACARPAL.position = vectors[12];
        MIDDLE_PROXIMAL.position = vectors[13];
        MIDDLE_INTERMEDIATE.position = vectors[14];
        MIDDLE_DISTAL.position = vectors[15];

        RING_METACARPAL.position = vectors[17];
        RING_PROXIMAL.position = vectors[18];
        RING_INTERMEDIATE.position = vectors[19];
        RING_DISTAL.position = vectors[20];

        PINKY_METACARPAL.position = vectors[22];
        PINKY_PROXIMAL.position = vectors[23];
        PINKY_INTERMEDIATE.position = vectors[24];
        PINKY_DISTAL.position = vectors[25];

        /*
         *  rotations
         */

        ELBOW.rotation = Quaternion.Euler(vectors[27]) * Quaternion.Euler(rotateVector);
        WRIST.rotation = Quaternion.Euler(vectors[28]) * Quaternion.Euler(rotateVector);
        
        THUMB_METACARPAL.rotation = Quaternion.Euler(vectors[29]) * Quaternion.Euler(rotateVector);
        THUMB_PROXIMAL.rotation = Quaternion.Euler(vectors[30]) * Quaternion.Euler(rotateVector);
        THUMB_INTERMEDIATE.rotation = Quaternion.Euler(vectors[31]) * Quaternion.Euler(rotateVector);
        THUMB_DISTAL.rotation = Quaternion.Euler(vectors[32]) * Quaternion.Euler(rotateVector);

        INDEX_METACARPAL.rotation = Quaternion.Euler(vectors[33]) * Quaternion.Euler(rotateVector);
        INDEX_PROXIMAL.rotation = Quaternion.Euler(vectors[34]) * Quaternion.Euler(rotateVector);
        INDEX_INTERMEDIATE.rotation = Quaternion.Euler(vectors[35]) * Quaternion.Euler(rotateVector);
        INDEX_DISTAL.rotation = Quaternion.Euler(vectors[36]) * Quaternion.Euler(rotateVector);

        MIDDLE_METACARPAL.rotation = Quaternion.Euler(vectors[37]) * Quaternion.Euler(rotateVector);
        MIDDLE_PROXIMAL.rotation = Quaternion.Euler(vectors[38]) * Quaternion.Euler(rotateVector);
        MIDDLE_INTERMEDIATE.rotation = Quaternion.Euler(vectors[39]) * Quaternion.Euler(rotateVector);
        MIDDLE_DISTAL.rotation = Quaternion.Euler(vectors[40]) * Quaternion.Euler(rotateVector);

        RING_METACARPAL.rotation = Quaternion.Euler(vectors[41]) * Quaternion.Euler(rotateVector);
        RING_PROXIMAL.rotation = Quaternion.Euler(vectors[42]) * Quaternion.Euler(rotateVector);
        RING_INTERMEDIATE.rotation = Quaternion.Euler(vectors[43]) * Quaternion.Euler(rotateVector);
        RING_DISTAL.rotation = Quaternion.Euler(vectors[44]) * Quaternion.Euler(rotateVector);

        PINKY_METACARPAL.rotation = Quaternion.Euler(vectors[45]) * Quaternion.Euler(rotateVector);
        PINKY_PROXIMAL.rotation = Quaternion.Euler(vectors[46]) * Quaternion.Euler(rotateVector);
        PINKY_INTERMEDIATE.rotation = Quaternion.Euler(vectors[47]) * Quaternion.Euler(rotateVector);
        PINKY_DISTAL.rotation = Quaternion.Euler(vectors[48]) * Quaternion.Euler(rotateVector);
    }

    private static Transform FindChildRecursively(Transform parent, string targetName)
    {
        Transform childTransform = parent.Find(targetName);
        if (childTransform != null)
        {
            return childTransform; 
        }
        
        else
        {
            foreach (Transform child in parent)
            {
                Transform result = FindChildRecursively(child, targetName);
                if (result != null)
                {
                    return result; 
                }
            }
        }

        return null; 
    }
}
