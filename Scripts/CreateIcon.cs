using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateIcon : MonoBehaviour
{
    public Material folder;
    public Material text;
    public Material setting;
    public Material media;
    public Material picture;
    public Material backButton;
    public Material exe;

    public string _mainPath = "C:\\";
    private float _positionX = -10.0f;
    private float _positionY = 2.0f;
    private float _positionZ = 10.0f;

    private GameObject[] _icon;               //폴더 아이콘
    private GameObject[] _iconName;        //폴더 이름
    private GameObject _curPath;             //현재경로
    private GameObject _backIcon;           //뒤로가기

    public Stack<string> _backStack = new Stack<string>();    //뒤로가기 스택
    FileSystemInfo[] dirInfo;


    private void Start()
    {
        //현재경로
        _curPath = new GameObject();
        _curPath.transform.position = new Vector3(_positionX - 0.3f, _positionY + 1.6f, _positionZ - 0.4f);
        _curPath.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        _curPath.transform.localRotation = new Quaternion(0, 0, 0, 0);
        _curPath.AddComponent<TextMesh>().text = _mainPath;
        _curPath.GetComponent<TextMesh>().fontSize = 15;

        _backIcon = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _backIcon.transform.position = new Vector3(_positionX, _positionY + 2.5f, _positionZ);
        _backIcon.transform.localRotation = new Quaternion(0, 180, 0, 0);
        _backIcon.transform.localScale = new Vector3(1, 1, 0.01f);
        _backIcon.name = "back";
        _backIcon.GetComponent<BoxCollider>().isTrigger = true;
        _backIcon.GetComponent<MeshRenderer>().material = backButton;

        Create();
    }

    public void Create()
    {
        //파일 가져오기
        DirectoryInfo dir = new DirectoryInfo(_mainPath);
        try
        {
            dirInfo = dir.GetFileSystemInfos();
        }
        catch (Exception e)
        {
            return;
        }
        
        _icon = new GameObject[dirInfo.Length];
        _iconName = new GameObject[dirInfo.Length];
        
        _curPath.GetComponent<TextMesh>().text = _mainPath;

        int index = 0;
        foreach (var file in dirInfo)
        {
            string filePath = file.ToString();          //실제 파일 경로
            string displayFilePath;                     //화면에 보이는 파일이름
            string[] pathArr = filePath.Split('\\');

            //글자 초과시 ... 으로 표시
            displayFilePath = pathArr[pathArr.Length - 1];
            if (displayFilePath.Length > 6)
                displayFilePath = displayFilePath.Substring(0, 5) + "...";

            String getExt = Path.GetExtension(file.ToString());
            //아이콘
            if (getExt == string.Empty)
            {
                _icon[index] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _icon[index].GetComponent<MeshRenderer>().material = folder;
                _icon[index].GetComponent<BoxCollider>().isTrigger = true;
            }
            else if (getExt == ".log" || getExt == ".txt")
            {
                _icon[index] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _icon[index].GetComponent<MeshRenderer>().material = text;
                _icon[index].GetComponent<BoxCollider>().isTrigger = true;
            }
            else if (getExt == ".avi" || getExt == ".mp3")
            {
                _icon[index] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _icon[index].GetComponent<MeshRenderer>().material = media;
                _icon[index].GetComponent<BoxCollider>().isTrigger = true;
            }
            else if (getExt == ".jpeg" || getExt == ".ioc" || getExt == ".png" || getExt == ".gif")
            {
                _icon[index] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _icon[index].GetComponent<MeshRenderer>().material = picture;
                _icon[index].GetComponent<BoxCollider>().isTrigger = true;
            }
            else if (getExt == "exe")
            {
                _icon[index] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _icon[index].GetComponent<MeshRenderer>().material = exe;
                _icon[index].GetComponent<BoxCollider>().isTrigger = true;
            }
            else 
            {
                _icon[index] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _icon[index].GetComponent<MeshRenderer>().material = setting;
                _icon[index].GetComponent<BoxCollider>().isTrigger = true;
            }

            _icon[index].transform.position = new Vector3(_positionX, _positionY, _positionZ);
            _icon[index].transform.localRotation = new Quaternion(0, 180, 0, 0);
            _icon[index].transform.localScale = new Vector3(1, 1, 0.01f);
            _icon[index].name = file.ToString();

            //아이콘 이름
            _iconName[index] = new GameObject();
            _iconName[index].transform.position = new Vector3(_positionX - 0.3f, _positionY - 0.6f, _positionZ - 0.4f);
            _iconName[index].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            _iconName[index].transform.localRotation = new Quaternion(0, 0, 0, 0);
            _iconName[index].AddComponent<TextMesh>().text = displayFilePath;
            _iconName[index].AddComponent<BoxCollider>().isTrigger = true;
            _iconName[index].GetComponent<TextMesh>().fontSize = 10;

            //아이콘 위치선정
            _positionX += 2;

            if (_positionX > 10)
            {
                _positionY -= 3;
                _positionX = -10.0f;
                _positionZ = 10.0f;
            }

            ++index;
        }
    }

    //아이콘 갱신
    public void Destory()
    {
        for (int i = 0; i < _icon.Length; ++i)
        {
            _icon[i].AddComponent<Rigidbody>();
            _iconName[i].AddComponent<Rigidbody>();
        }

        _positionX = -10.0f;
        _positionY = 2.0f;
        _positionZ = 10.0f;
    }
}