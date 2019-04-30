using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VrController : MonoBehaviour
{
    public AudioSource _clickSound;
    private int _fpsCnt = 0;
    private bool _isClicked = false;
    public GameObject _cameraRig;
    public string _clickedPath;

    public GameObject _back;
    private bool _isBack = false;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Start()
    {
        GetComponent<SteamVR_LaserPointer>().PointerIn += new PointerEventHandler(TriggerEnter);
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

   public bool DoubleClick()
    {
        if (_isClicked)
        {
            if (Controller.GetHairTriggerDown())
            {
                _isClicked = false;
                return true;
            }
        }
        return false;
    }

    void TriggerEnter(object sender, PointerEventArgs e)
    {
        if (e.target.name.Equals("cDrive"))
        {
            Debug.Log("CCC");
            _clickedPath = "C:\\";
            _isBack = false;
        }
        else if (e.target.name.Equals("dDrive"))
        {
            Debug.Log("DDD");
            _clickedPath = "D:\\";
            _isBack = false;
        }
        else if (e.target.name.Equals("back"))
        {
            _isBack = true;
        }
        else
        {
            _clickedPath = e.target.ToString().Substring(0, e.target.ToString().Length - 24);
            _isBack = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isClicked)
        {
            if (_fpsCnt >= 60)
            {
                _fpsCnt = 0;
                _isClicked = false;
            }
            ++_fpsCnt;

            //한번 클릭 시 드래그
        }

        //더블 클릭 시 폴더 이동
        if (DoubleClick())
        {
            try
            {
                string fileType = Path.GetExtension(_cameraRig.GetComponent<CreateIcon>()._mainPath);
                if (fileType == string.Empty)
                {
                    Debug.Log("폴더");
                    if (_isBack)
                    {
                        _cameraRig.GetComponent<CreateIcon>()._mainPath = _cameraRig.GetComponent<CreateIcon>()._backStack.Pop();
                        _cameraRig.GetComponent<CreateIcon>().Destory();
                        _cameraRig.GetComponent<CreateIcon>().Create();
                    }
                    else
                    {
                        _cameraRig.GetComponent<CreateIcon>()._backStack.Push(_cameraRig.GetComponent<CreateIcon>()._mainPath);
                        _cameraRig.GetComponent<CreateIcon>()._mainPath = _clickedPath;
                        _cameraRig.GetComponent<CreateIcon>().Destory();
                        _cameraRig.GetComponent<CreateIcon>().Create();
                    }
                }
            }
            catch (IOException e)
            {
                return;
            }
        }

        if (Controller.GetHairTriggerDown())
        {
            _isClicked = true;
            _clickSound.Play();
        }
    }
}
