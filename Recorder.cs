using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class Recorder : MonoBehaviour
{
    public SimpleController simpleController;
    private int handVectorCount = 49;

    // record

    public int recordingFPS = 30;
    private int _recordingFPS = 30;
    private BinaryWriter binaryWriter;
    private string _path = "Record/.record";
    private bool recording = false;
    private float recordingTimer = 0.0f;
    private float recordingInterval = 0.0f;
    private string csvHeader = "left_elbow_x,left_elbow_y,left_elbow_z,left_wrist_x,left_wrist_y,left_wrist_z," +
                               "left_00_x,left_00_y,left_00_z,left_01_x,left_01_y,left_01_z,left_02_x,left_02_y,left_02_z,left_03_x,left_03_y,left_03_z,left_04_x,left_04_y,left_04_z," +
                               "left_10_x,left_10_y,left_10_z,left_11_x,left_11_y,left_11_z,left_12_x,left_12_y,left_12_z,left_13_x,left_13_y,left_13_z,left_14_x,left_14_y,left_14_z," +
                               "left_20_x,left_20_y,left_20_z,left_21_x,left_21_y,left_21_z,left_22_x,left_22_y,left_22_z,left_23_x,left_23_y,left_23_z,left_24_x,left_24_y,left_24_z," +
                               "left_30_x,left_30_y,left_30_z,left_31_x,left_31_y,left_31_z,left_32_x,left_32_y,left_32_z,left_33_x,left_33_y,left_33_z,left_34_x,left_34_y,left_34_z," +
                               "left_40_x,left_40_y,left_40_z,left_41_x,left_41_y,left_41_z,left_42_x,left_42_y,left_42_z,left_43_x,left_43_y,left_43_z,left_44_x,left_44_y,left_44_z," +
                               "left_elbow_rx,left_elbow_ry,left_elbow_rz,left_wrist_rx,left_wrist_ry,left_wrist_rz," +
                               "left_00_rx,left_00_ry,left_00_rz,left_01_rx,left_01_ry,left_01_rz,left_02_rx,left_02_ry,left_02_rz,left_03_rx,left_03_ry,left_03_rz," +
                               "left_10_rx,left_10_ry,left_10_rz,left_11_rx,left_11_ry,left_11_rz,left_12_rx,left_12_ry,left_12_rz,left_13_rx,left_13_ry,left_13_rz," +
                               "left_20_rx,left_20_ry,left_20_rz,left_21_rx,left_21_ry,left_21_rz,left_22_rx,left_22_ry,left_22_rz,left_23_rx,left_23_ry,left_23_rz," +
                               "left_30_rx,left_30_ry,left_30_rz,left_31_rx,left_31_ry,left_31_rz,left_32_rx,left_32_ry,left_32_rz,left_33_rx,left_33_ry,left_33_rz," +
                               "left_40_rx,left_40_ry,left_40_rz,left_41_rx,left_41_ry,left_41_rz,left_42_rx,left_42_ry,left_42_rz,left_43_rx,left_43_ry,left_43_rz," +
                               "right_elbow_x,right_elbow_y,right_elbow_z,right_wrist_x,right_wrist_y,right_wrist_z," +
                               "right_00_x,right_00_y,right_00_z,right_01_x,right_01_y,right_01_z,right_02_x,right_02_y,right_02_z,right_03_x,right_03_y,right_03_z,right_04_x,right_04_y,right_04_z," +
                               "right_10_x,right_10_y,right_10_z,right_11_x,right_11_y,right_11_z,right_12_x,right_12_y,right_12_z,right_13_x,right_13_y,right_13_z,right_14_x,right_14_y,right_14_z," +
                               "right_20_x,right_20_y,right_20_z,right_21_x,right_21_y,right_21_z,right_22_x,right_22_y,right_22_z,right_23_x,right_23_y,right_23_z,right_24_x,right_24_y,right_24_z," +
                               "right_30_x,right_30_y,right_30_z,right_31_x,right_31_y,right_31_z,right_32_x,right_32_y,right_32_z,right_33_x,right_33_y,right_33_z,right_34_x,right_34_y,right_34_z," +
                               "right_40_x,right_40_y,right_40_z,right_41_x,right_41_y,right_41_z,right_42_x,right_42_y,right_42_z,right_43_x,right_43_y,right_43_z,right_44_x,right_44_y,right_44_z," +
                               "right_elbow_rx,right_elbow_ry,right_elbow_rz,right_wrist_rx,right_wrist_ry,right_wrist_rz," +
                               "right_00_rx,right_00_ry,right_00_rz,right_01_rx,right_01_ry,right_01_rz,right_02_rx,right_02_ry,right_02_rz,right_03_rx,right_03_ry,right_03_rz," +
                               "right_10_rx,right_10_ry,right_10_rz,right_11_rx,right_11_ry,right_11_rz,right_12_rx,right_12_ry,right_12_rz,right_13_rx,right_13_ry,right_13_rz," +
                               "right_20_rx,right_20_ry,right_20_rz,right_21_rx,right_21_ry,right_21_rz,right_22_rx,right_22_ry,right_22_rz,right_23_rx,right_23_ry,right_23_rz," +
                               "right_30_rx,right_30_ry,right_30_rz,right_31_rx,right_31_ry,right_31_rz,right_32_rx,right_32_ry,right_32_rz,right_33_rx,right_33_ry,right_33_rz," +
                               "right_40_rx,right_40_ry,right_40_rz,right_41_rx,right_41_ry,right_41_rz,right_42_rx,right_42_ry,right_42_rz,right_43_rx,right_43_ry,right_43_rz,";

    public void StartRecord()
    {
        if (recording)
        {
            Debug.LogWarning("Record has already started!");
            return;
        }

        _recordingFPS = recordingFPS;
        recordingInterval = 1f / _recordingFPS;
        recordingTimer = 0.0f;

        string directoryPath = Path.GetDirectoryName(_path);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        binaryWriter = new BinaryWriter(File.Open(_path, FileMode.Create));

        Debug.Log("Record Started!");
        recording = true;
    }

    public void FinishRecord()
    {
        if (!recording)
        {
            Debug.LogWarning("No recording is running!");
            return;
        }

        recording = false;
        recordingTimer = 0.0f;
        binaryWriter.Close();

        string savePath = "Record\\" + GetDateTimeString() + ".csv";
        BinaryReader binaryReader = new BinaryReader(File.Open(_path, FileMode.Open));

        using (StreamWriter streamWriter = new StreamWriter(savePath, false))
        {
            streamWriter.WriteLine(csvHeader + _recordingFPS);
            while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                string line = "";

                for (int j = 0; j < 2; j++)
                {
                    if (binaryReader.ReadBoolean())
                    {
                        for (int i = 0; i < handVectorCount; i++)
                        {
                            Vector3 vector = ReadVector3f(binaryReader);
                            line += vector.x + "," + vector.y + "," + vector.z + ",";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < handVectorCount; i++)
                        {
                            line += ",,,";
                        }
                    }
                }

                streamWriter.WriteLine(line);
            }
        }

        savedPath = savePath;
        Debug.Log("Record Finished!");
    }

    // play

    private StreamReader streamReader;
    public string savedPath = "Record/record.csv";
    private bool playing = false;
    private int _playingFPS;
    private float playingTimer = 0.0f;
    private float playingInterval = 0.0f;
    public PlayBinder leftBinder;
    public PlayBinder rightBinder;

    public void StartPlay()
    {
        if (playing)
        {
            Debug.LogWarning("Play has already started!");
            return;
        }

        if (!File.Exists(savedPath))
        {
            Debug.LogWarning("File does not exist!");
            return;
        }
        streamReader = new StreamReader(savedPath);
        string line = streamReader.ReadLine();

        string[] line_array = line.Split(',');
        _playingFPS = int.Parse(line_array[line_array.Length - 1]);
        playingInterval = 1f / _playingFPS;
        playingTimer = 0.0f;

        Debug.Log("Play Started!\n" + line);
        leftBinder.gameObject.SetActive(false);
        rightBinder.gameObject.SetActive(false);
        playing = true;
    }

    public void FinishPlay()
    {
        if (!playing)
        {
            Debug.LogWarning("No record is playing!");
            return;
        }
        playing = false;

        streamReader.Close();
        playingTimer = 0.0f;

        leftBinder.gameObject.SetActive(false);
        rightBinder.gameObject.SetActive(false);
        Debug.Log("Play Finished!");
    }
    
    [ContextMenu("Generate CSV Header")]
    public void GenerateCsvHeader(){
        string result = "";
        for (int i = 0; i < 2; i++)
        {
            string handType = i == 0 ? "left" : "right";
            result += handType + "_elbow_x," + handType + "_elbow_y," + handType + "_elbow_z,";
            result += handType + "_wrist_x," + handType + "_wrist_y," + handType + "_wrist_z,";

            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    result += handType + "_" + j + "" + k + "_x," +
                              handType + "_" + j + "" + k + "_y," +
                              handType + "_" + j + "" + k + "_z,";
                }
            }

            result += handType + "_elbow_rx," + handType + "_elbow_ry," + handType + "_elbow_rz,";
            result += handType + "_wrist_rx," + handType + "_wrist_ry," + handType + "_wrist_rz,";

            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    result += handType + "_" + j + "" + k + "_rx," +
                              handType + "_" + j + "" + k + "_ry," +
                              handType + "_" + j + "" + k + "_rz,";
                }
            }
        }

        Debug.Log(result);
    }

    private static void WriteVector3f(BinaryWriter writer, Vector3 input)
    {
        writer.Write(input.x);
        writer.Write(input.y);
        writer.Write(input.z);
    }

    private static Vector3 ReadVector3f(BinaryReader reader)
    {
        return new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
    }

    private static string GetDateTimeString()
    {
        DateTime currentDateTime = DateTime.Now;

        return "" + currentDateTime.Year +
                "-" + currentDateTime.Month +
                "-" + currentDateTime.Day +
                "-" + currentDateTime.Hour +
                "-" + currentDateTime.Minute +
                "-" + currentDateTime.Second;
    }

    void Start()
    {
        leftBinder.gameObject.SetActive(false);
        rightBinder.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (recording)
        {
            if (recordingTimer >= recordingInterval)
            {
                recordingTimer = 0.0f;

                bool hasLeft = simpleController.HAS_LEFT;
                bool hasRight = simpleController.HAS_RIGHT;

                binaryWriter.Write(hasLeft);
                if (hasLeft)
                {
                    foreach (Vector3 vector in simpleController.LEFT.GetPositionsList())
                    {
                        WriteVector3f(binaryWriter, vector);
                    }

                    foreach (Vector3 vector in simpleController.LEFT.GetRotationsList())
                    {
                        WriteVector3f(binaryWriter, vector);
                    }
                }

                binaryWriter.Write(hasRight);
                if (hasRight)
                {
                    foreach (Vector3 vector in simpleController.RIGHT.GetPositionsList())
                    {
                        WriteVector3f(binaryWriter, vector);
                    }

                    foreach (Vector3 vector in simpleController.RIGHT.GetRotationsList())
                    {
                        WriteVector3f(binaryWriter, vector);
                    }
                }
            }

            if (recordingFPS != _recordingFPS)
            {
                recordingFPS = _recordingFPS;
                Debug.LogWarning("You cannot change capture FPS during recording!");
            }

            recordingTimer += Time.deltaTime;
        }

        if (playing)
        {
            if (playingTimer >= playingInterval)
            {
                playingTimer = 0.0f;

                string line;
                if ((line = streamReader.ReadLine()) != null){
                    string[] vectorsStr = line.Split(',');
                    Vector3[] vectorsL = new Vector3[handVectorCount];
                    Vector3[] vectorsR = new Vector3[handVectorCount];

                    if (vectorsStr[0] != "")
                    {
                        for (int i = 0; i < handVectorCount; i++)
                        {
                            vectorsL[i] = new Vector3(float.Parse(vectorsStr[3 * i]),
                                                      float.Parse(vectorsStr[3 * i + 1]),
                                                      float.Parse(vectorsStr[3 * i + 2]));
                        }
                        leftBinder.GenerateTransforms(vectorsL);
                        leftBinder.gameObject.SetActive(true);
                    }
                    else
                    {
                        leftBinder.gameObject.SetActive(false);
                    }

                    if (vectorsStr[handVectorCount * 3] != "")
                    {
                        for (int i = 0; i < handVectorCount; i++)
                        {
                            vectorsR[i] = new Vector3(float.Parse(vectorsStr[handVectorCount * 3 + 3 * i]),
                                                      float.Parse(vectorsStr[handVectorCount * 3 + 3 * i + 1]),
                                                      float.Parse(vectorsStr[handVectorCount * 3 + 3 * i + 2]));

                        }
                        rightBinder.GenerateTransforms(vectorsR);
                        rightBinder.gameObject.SetActive(true);
                    }
                    else
                    {
                        rightBinder.gameObject.SetActive(false);
                    }
                }

                else
                {
                    FinishPlay();
                }
            }

            playingTimer += Time.deltaTime;
        }
    }

    void OnDestroy()
    {
        if (recording)
        {
            FinishRecord();
        }
        if (playing)
        {
            FinishPlay();
        }
    }

}
