using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CameraChange : MonoBehaviour
{
    bool worldView;

    [Header("Cameras for level 1")]
    [SerializeField] CinemachineVirtualCamera far1;
    [SerializeField] CinemachineVirtualCamera close1;
    [Header("Cameras for level 2")]
    [SerializeField] CinemachineVirtualCamera far2;
    [SerializeField] CinemachineVirtualCamera close2;


    CinemachineVirtualCamera far;
    CinemachineVirtualCamera close;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        worldView = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetAllCameras();

        switch (scene.buildIndex)
        {
            case 1:
                far = far1;
                close = close1;
                break;
            case 2:
                far = far2;
                close = close2;
                break;
            default:
                break;
        }

        if (!worldView)
        {            
            far.Priority = 5;
            close.Priority = 100;
        }
        else
        {
            far.Priority = 100;
            close.Priority = 5;
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        //worldView = true;
        //far.Priority = 100;
        //close.Priority = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (worldView)
            {
                worldView = false;
                far.Priority = 5;
                close.Priority = 100;                
            } else
            {
                worldView = true;
                far.Priority = 100;
                close.Priority = 5;

            }
        }
    }

    private void ResetAllCameras()
    {
        far1.Priority = 5;
        close1.Priority = 5;
        far2.Priority = 5;
        close2.Priority = 5;
    }
}
