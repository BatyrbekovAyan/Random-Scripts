using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class AROriginController : MonoBehaviour
{
    #region Declaration

    private bool modelIsOpen = false;
    private bool tour3D = false;
    private bool Image3DMaketTouched = false;
    private bool Image3DMaketOn = false;
    private bool Image3DMaketWasOn = false;
    private bool hitObjectZeroIsNull = true;
    private bool soundPlayed = false;
    private bool modelJustOpened = false;
    private bool modelTouched = false;
    private bool planeTouched = false;
    private bool justMoved = true;
    private bool modelChanged = false;
    private bool uiButtonPressed = false;
    private bool touchEnabled = true;
    private bool scannerEnabled = true;
    private bool normalView = true;
    private bool rotateCameraShowedOnce = false;
    private bool fingerTapShowedOnce = false;
    private bool phoneInHandShowedOnce = false;
    private bool wallHit = false;
    private bool joystickEnabled = false;
    private bool movementEnabled = false;
    private bool rulerOn = false;
    private bool furnitureOn = true;
    private bool WallsTransparencyOn = false;
    private bool MaketsPageOpen = false;
    private bool RoomLightsOkButtonPressed = true;
    private bool zhkIsAlreadyDownloaded = false;
    private bool zhkFinishedDownload = false;
    private bool promoFinished = false;
    private bool MusicMuted = false;
    private bool MusicEnabled = false;
    private bool LightsOn = true;


    private float placedPrefabScaleCooficient = 0.5f;
    private float planeModelDistance = default;
    private float planeModelMinDistance = default;
    private float initialTouchesDistance;
    private float currentTouchesDistance;
    private float modelYPosition = 0;
    private float modelXPosition = 0;
    private float modelZPosition = 0;
    private float zoomInOut = 0;
    private float rotateLeftRight = 0;
    private float moveFrontBack = 0;
    private float moveLeftRight = 0;
    private float lowestPlaneInitialYPosition;
    private float Image3DMaketRotateLeftRight = 0;
    private float Image3DMaketRotateUpDown = 0;
    private float Image3DMaketMoveUpDown = 0;
    private float Image3DMaketMoveLeftRight = 0;
    private float JoystickHandleRadius = 100;
    private float joystickHandleDisabledRadius = 30;
    private float movementMaxSpeed = 2;
    private float rotateMaxSpeed = 300;
    private float frameRate;

    private int soundtrackNumber = 0;
    private int allSoundtracksNumber = 6;
    private int wallMaterialIndex = 0;
    private int floorMaterialIndex = 0;
    private int randomWallMaterialIndex = 0;
    private int randomFloorMaterialIndex = 0;

    private String lastCollidedObjectName = "Nothing";
    private String lastUsedZhkName = "";
    private String phoneNumber;
    private String siteAddress;

    private Vector3 Image3DMaketFormatedScale;
    private Vector3 placedPrefabScale;
    private Vector3 JoystickHandleInitialPosition;
    private Vector3 distanceCameraModel;
    private Vector3 movedModelPositionDifference;
    private Vector3 arCameraPreveousPosition;
    private Vector3 lastSelectedObjectPreveousPosition;
    private Vector3 lastSelectedObjectInitialPosition;
    private Vector3 initialHitPose;
    private Vector3 touchMovedDistance;
    private Vector3 modelPosition;
    private Quaternion modelRotation;

    private Vector2 touchPosition = default;
    private Vector2 touchPositionRotateZero = default;
    private Vector2 touchPositionOne = default;
    private Vector2 touchPositionRotateOne = default;
    private Vector2 Image3DMaketTouchPositionZero = default;
    private Vector2 Image3DMaketTouchPositionOne = default;
    private Vector2 ZhkNameTextPosition = default;


    [SerializeField] private GameObject ZhkImageLeftRightImages;
    [SerializeField] private GameObject Image3DMaketPosition;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject CompaniesList;
    [SerializeField] private GameObject ZhksPanels;
    [SerializeField] private GameObject ZhkImageSlider;
    [SerializeField] private GameObject ZhkImageNavigationPanel;
    [SerializeField] private GameObject ZhkPhotoAlbumPanel;
    [SerializeField] private GameObject loadingImage;
    [SerializeField] private GameObject TopPanel;
    [SerializeField] private GameObject CompaniesPanel;
    [SerializeField] private GameObject phoneInHandText;
    [SerializeField] private GameObject phoneInHand;
    [SerializeField] private GameObject fingerTapText;
    [SerializeField] private GameObject fingerTap;
    [SerializeField] private GameObject rotateCameraText;
    [SerializeField] private GameObject rotateCamera;
    [SerializeField] private GameObject PlacerGreen;
    [SerializeField] private GameObject PlacerRed;
    [SerializeField] private GameObject ZhkNamePanel;
    [SerializeField] private GameObject NavigationPanelOpenedModel;
    [SerializeField] private GameObject PressToSelect;
    [SerializeField] private GameObject ModelsPanel;
    [SerializeField] private GameObject NavigationPanelMain;
    [SerializeField] private GameObject NavigationPanel;
    [SerializeField] private GameObject AdditionalPanel;
    [SerializeField] private GameObject ZhkDetailsPanel;
    [SerializeField] private GameObject ZhkImageInfoPanel;
    [SerializeField] private GameObject MapPointerArrow;
    [SerializeField] private GameObject FakeObject;
    [SerializeField] private GameObject RoomLightsOffPanel;
    [SerializeField] private GameObject SkyboxCube;
    [SerializeField] private GameObject ConnectionErrorMessage;
    [SerializeField] private GameObject ZhkDetailsPanelContentInitialPosition;
    [SerializeField] private GameObject ZhkImageInfoPanelContentInitialPosition;
    [SerializeField] private GameObject MenuPlaceholder;
    [SerializeField] private GameObject ToolsPanel;

    private GameObject PlacerGreenOne = null;
    private GameObject FakeObjectPrivate;
    private GameObject placedPrefab;
    private GameObject hitObjectZeroObject = null;
    private GameObject ModelsPanelContent = null;
    private GameObject ZhkPhotoAlbumContent = null;
    private GameObject ModelsPanelContentSample;
    private GameObject ZhkPhotoAlbumContentSample;
    private GameObject ZhkImagePanelSample;
    private GameObject Image3DMaket;
    private GameObject LeftSwipeImage;
    private GameObject RightSwipeImage;
    private GameObject JoystickHandle;
    private GameObject progress;


    private AudioSource[] MapPointerArrowSounds;
    private AudioSource[] GeneralSounds;
    private AudioSource[] arCameraSounds;
    private Material[] wallMaterials;
    private Material[] floorMaterials;
    private GameObject[] ModelsPanelContentRoundImageButtons;
    private GameObject[] zhkModelsArray;
    private TextAsset[] ZhkImageInfoPanelTextsArray;

    private List<ZhkModelsArray> zhkModelsArraysList;
    private List<PlacementObject> modelsInScene;

    private PlacementObject lastSelectedObject;
    private PlacementObject lastOpenedModel;


    private AudioSource BackgroundMusic;
    private AudioSource FootstepsSound;
    private AudioSource WallHitSound;
    private AudioSource WallThroghSound;
    private AudioSource ChangeMapViewSound;
    private AudioSource ModelPlacingSound;
    private AudioSource ModelDeletingSound;
    private AudioSource ModelOpenningSound;
    private AudioSource ModelClosingSound;
    private AudioSource ModelSelectingSound;
    private AudioSource TapSound;
    private AudioSource RestartSound;
    private AudioSource ModelMovingSound;
    private AudioSource SwitchLightsSound;


    [SerializeField] private VideoPlayer PromoVideoPlayer;


    [SerializeField] private Material furnitureWhite;
    [SerializeField] private Material furnitureBlack;
    [SerializeField] private Material wallTransparent;
    

    [SerializeField] private Button MoveMaketButton;
    [SerializeField] private Button RotateMaketButton;
    [SerializeField] private Button HomeButton;
    [SerializeField] private Button Show3DButton;
    [SerializeField] private Button Tour3DButton;
    [SerializeField] private Button ShowMaketsPageButton;
    [SerializeField] private Button ShowInfoButton;
    [SerializeField] private Button ZhkDetailsButton;
    [SerializeField] private Button ZhkPhotoAlbumButton;
    [SerializeField] private Button OpenModelButton;
    [SerializeField] private Button CloseModelButton;
    [SerializeField] private Button TogglePlaneDetectionButton;
    [SerializeField] private Button ResetPlaneDetectionButton;
    [SerializeField] private Button DeleteModelButton;
    [SerializeField] private Button ShowModelsPanelButton;
    [SerializeField] private Button ToggleToolsButton;
    [SerializeField] private Button ToggleLightsButton;
    [SerializeField] private Button ToggleRulerButton;
    [SerializeField] private Button ToggleFurnitureButton;
    [SerializeField] private Button ChangeWallsButton;
    [SerializeField] private Button ChangeFloorsButton;
    [SerializeField] private Button ToggleWallsButton;
    [SerializeField] private Button CentraliseModelButton;
    [SerializeField] private Button ToggleMuteMusicButton;
    [SerializeField] private Button ChangeMusicButton;
    [SerializeField] private Button AdditionalPanelButton;
    [SerializeField] private Button BackToCompaniesListButton;
    [SerializeField] private Button BackToMenuButton;
    [SerializeField] private Button BackToImagesButton;
    [SerializeField] private Button SwitchCamerasButton;
    [SerializeField] private Button Joystick;
    [SerializeField] private Button RoomLightsOffPanelOkButton;
    [SerializeField] private Button CallButton;
    [SerializeField] private Button SiteButton;
    [SerializeField] private Button skipVideoButton;
    [SerializeField] private Button PrivacyPolicyButton;


    [SerializeField] private Canvas myCanvas;

    [SerializeField] private Scrollbar progressScrollbar;

    [SerializeField] private Camera promoCamera;
    [SerializeField] private Camera topCamera;
    [SerializeField] private Camera arCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private ARSession arSession;

    [SerializeField] private ARCameraManager aRCameraManager;
    [SerializeField] private ARPlane ARPlaneCustomSample;

    private System.Random random;

    private UnityWebRequest url;
    private AssetBundle zhkBundle;
    private AssetBundle promoBundle;

    private ARPlaneManager aRPlaneManager;
    private RaycastHit hit = new RaycastHit();

    private ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Collider cameraCollider;
    private ARCameraCollisionController arCameraCollisionController;

    #endregion


    void Awake()
    {
        StartCoroutine(DownloadMenu());

        GeneralSounds = transform.gameObject.GetComponents<AudioSource>();
        BackgroundMusic = GeneralSounds[0];
        TapSound = GeneralSounds[1];
        SwitchLightsSound = GeneralSounds[2];
        ModelClosingSound = GeneralSounds[3];

        MapPointerArrowSounds = MapPointerArrow.GetComponents<AudioSource>();
        FootstepsSound = MapPointerArrowSounds[0];
        ChangeMapViewSound = MapPointerArrowSounds[1];

        arCameraSounds = arCamera.GetComponents<AudioSource>();
        ModelPlacingSound = arCameraSounds[0];
        ModelDeletingSound = arCameraSounds[1];
        ModelOpenningSound = arCameraSounds[2];
        ModelSelectingSound = arCameraSounds[3];
        RestartSound = arCameraSounds[4];
        ModelMovingSound = arCameraSounds[5];
        WallHitSound = arCameraSounds[6];
        WallThroghSound = arCameraSounds[7];


        wallMaterials = Resources.LoadAll<Material>($"Materials/Wall");
        floorMaterials = Resources.LoadAll<Material>($"Materials/Floor");

        cameraCollider = arCamera.gameObject.GetComponent<Collider>();
        arCameraCollisionController = arCamera.gameObject.GetComponent<ARCameraCollisionController>();

        ZhkNameTextPosition = ZhkNamePanel.GetComponentInChildren<Text>().rectTransform.anchoredPosition;
        placedPrefabScale = new Vector3();
        arCameraPreveousPosition = Vector3.zero;
        lastSelectedObjectPreveousPosition = new Vector3();
        lastSelectedObjectInitialPosition = new Vector3();
        initialHitPose = new Vector3();
        touchMovedDistance = new Vector3();
        frameRate = Time.deltaTime;

        zhkModelsArraysList = new List<ZhkModelsArray>();
        ModelsPanelContentSample = ModelsPanel.transform.GetChild(0).gameObject;
        ZhkPhotoAlbumContentSample = ZhkPhotoAlbumPanel.transform.GetChild(0).gameObject;
        progress = loadingImage.transform.GetChild(1).gameObject;
        ZhkImagePanelSample = ZhkImageSlider.transform.GetChild(0).gameObject;
        LeftSwipeImage = ZhkImageLeftRightImages.transform.GetChild(0).gameObject;
        RightSwipeImage = ZhkImageLeftRightImages.transform.GetChild(1).gameObject;
        JoystickHandle = Joystick.transform.GetChild(0).gameObject;

        arRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
        aRPlaneManager.enabled = false;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        random = new System.Random();

        modelsInScene = new List<PlacementObject>();

        PromoVideoPlayer.loopPointReached += ShowCanvas;

        if (skipVideoButton != null)
        {
            skipVideoButton.onClick.AddListener(skipVideo);
        }

        if (MoveMaketButton != null)
        {
            MoveMaketButton.onClick.AddListener(MoveRotateMaket);
        }

        if (RotateMaketButton != null)
        {
            RotateMaketButton.onClick.AddListener(MoveRotateMaket);
        }

        if (HomeButton != null)
        {
            HomeButton.onClick.AddListener(ShowHome);
        }

        if (Show3DButton != null)
        {
            Show3DButton.onClick.AddListener(Show3D);
        }

        if (Tour3DButton != null)
        {
            Tour3DButton.onClick.AddListener(Tour3D);
        }

        if (ShowMaketsPageButton != null)
        {
            ShowMaketsPageButton.onClick.AddListener(ShowMaketsPage);
        }

        if (ShowInfoButton != null)
        {
            ShowInfoButton.onClick.AddListener(ShowInfo);
        }

        if (ZhkDetailsButton != null)
        {
            ZhkDetailsButton.onClick.AddListener(ZhkDetails);
        }

        if (ZhkPhotoAlbumButton != null)
        {
            ZhkPhotoAlbumButton.onClick.AddListener(ShowZhkPhotoAlbum);
        }

        if (OpenModelButton != null)
        {
            OpenModelButton.onClick.AddListener(OpenModel);
            OpenModelButton.enabled = false;
        }

        if (CloseModelButton != null)
        {
            CloseModelButton.onClick.AddListener(CloseModel);
        }

        if (TogglePlaneDetectionButton != null)
        {
            TogglePlaneDetectionButton.onClick.AddListener(TogglePlaneDetection);
        }

        if (ResetPlaneDetectionButton != null)
        {
            ResetPlaneDetectionButton.onClick.AddListener(ResetScene);
        }

        if (DeleteModelButton != null)
        {
            DeleteModelButton.onClick.AddListener(DeleteModel);
        }

        if (ShowModelsPanelButton != null)
        {
            ShowModelsPanelButton.onClick.AddListener(ToggleModelPanel);
        }

        if (ToggleToolsButton != null)
        {
            ToggleToolsButton.onClick.AddListener(ToggleTools);
        }

        if (ToggleLightsButton != null)
        {
            ToggleLightsButton.onClick.AddListener(ToggleLights);
        }

        if (ToggleRulerButton != null)
        {
            ToggleRulerButton.onClick.AddListener(ToggleRuler);
        }

        if (ToggleFurnitureButton != null)
        {
            ToggleFurnitureButton.onClick.AddListener(ToggleFurniture);
        }

        if (ChangeWallsButton != null)
        {
            ChangeWallsButton.onClick.AddListener(ChangeWalls);
        }

        if (ChangeFloorsButton != null)
        {
            ChangeFloorsButton.onClick.AddListener(ChangeFloors);
        }

        if (ToggleWallsButton != null)
        {
            ToggleWallsButton.onClick.AddListener(ToggleWalls);
        }

        if (CentraliseModelButton != null)
        {
            CentraliseModelButton.onClick.AddListener(CentraliseModel);
        }

        if (ToggleMuteMusicButton != null)
        {
            ToggleMuteMusicButton.onClick.AddListener(ToggleMuteMusic);
        }

        if (ChangeMusicButton != null)
        {
            ChangeMusicButton.onClick.AddListener(ChangeMusic);
        }

        if (AdditionalPanelButton != null)
        {
            AdditionalPanelButton.onClick.AddListener(ToggleAdditionalPanel);
        }

        if (BackToCompaniesListButton != null)
        {
            BackToCompaniesListButton.onClick.AddListener(BackToCompanies);
        }

        if (BackToMenuButton != null)
        {
            BackToMenuButton.onClick.AddListener(BackToMenu);
        }

        if (BackToImagesButton != null)
        {
            BackToImagesButton.onClick.AddListener(BackToImages);
        }

        if (SwitchCamerasButton != null)
        {
            SwitchCamerasButton.onClick.AddListener(SwitchCameras);
        }

        if (RoomLightsOffPanelOkButton != null)
        {
            RoomLightsOffPanelOkButton.onClick.AddListener(RoomLightsOffPanelOk);
        }

        if (CallButton != null)
        {
            CallButton.onClick.AddListener(MakeCall);
        }

        if (SiteButton != null)
        {
            SiteButton.onClick.AddListener(OpenSite);
        }

        if (PrivacyPolicyButton != null)
        {
            PrivacyPolicyButton.onClick.AddListener(OpenPolicy);
        }
    }


    void Update()
    {
        if (loadingImage.activeSelf)
        {
            progress.GetComponent<Text>().text = Mathf.RoundToInt(url.downloadProgress * 100).ToString() + "%";

            progressScrollbar.size = url.downloadProgress;
        }

        if (Image3DMaketOn)
        {
            if (Input.touchCount == 0 && Image3DMaketTouched)
            {
                Image3DMaketTouched = false;

                if (Image3DMaketFormatedScale.x * 5 < Image3DMaket.transform.localScale.x)
                {
                    Image3DMaket.transform.localScale = new Vector3(Image3DMaketFormatedScale.x * 5, Image3DMaketFormatedScale.y * 5, Image3DMaketFormatedScale.z * 5);
                }
                else if (Image3DMaketFormatedScale.x * 0.5f > Image3DMaket.transform.localScale.x)
                {
                    Image3DMaket.transform.localScale = new Vector3(Image3DMaketFormatedScale.x * 0.5f, Image3DMaketFormatedScale.y * 0.5f, Image3DMaketFormatedScale.z * 0.5f);
                }

                if (Image3DMaket.transform.position.x > Image3DMaketPosition.transform.position.x + Image3DMaket.transform.localScale.x * 0.5f)
                {
                    Image3DMaket.transform.position = new Vector3(Image3DMaketPosition.transform.position.x + Image3DMaket.transform.localScale.x * 0.5f, Image3DMaket.transform.position.y, Image3DMaket.transform.position.z);
                }
                else if (Image3DMaket.transform.position.x < Image3DMaketPosition.transform.position.x - Image3DMaket.transform.localScale.x * 0.5f)
                {
                    Image3DMaket.transform.position = new Vector3(Image3DMaketPosition.transform.position.x - Image3DMaket.transform.localScale.x * 0.5f, Image3DMaket.transform.position.y, Image3DMaket.transform.position.z);
                }

                if (Image3DMaket.transform.position.z > Image3DMaketPosition.transform.position.z + Image3DMaket.transform.localScale.z * 0.5f)
                {
                    Image3DMaket.transform.position = new Vector3(Image3DMaket.transform.position.x, Image3DMaket.transform.position.y, Image3DMaketPosition.transform.position.z + Image3DMaket.transform.localScale.z * 0.5f);
                }
                else if (Image3DMaket.transform.position.z < Image3DMaketPosition.transform.position.z - Image3DMaket.transform.localScale.z * 0.5f)
                {
                    Image3DMaket.transform.position = new Vector3(Image3DMaket.transform.position.x, Image3DMaket.transform.position.y, Image3DMaketPosition.transform.position.z - Image3DMaket.transform.localScale.z * 0.5f);
                }
            }

            else if (Input.touchCount == 1)
            {
                if (EventSystem.current.currentSelectedGameObject != null)
                {
                    return;
                }


                Touch touch = Input.GetTouch(0);

                if (MoveMaketButton.gameObject.activeSelf)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Image3DMaketTouched = true;

                        Image3DMaketTouchPositionZero = touch.position;
                        Image3DMaketRotateLeftRight = 0;
                        Image3DMaketRotateUpDown = 0;
                    }

                    else if (touch.phase == TouchPhase.Moved)
                    {
                        Image3DMaketTouchPositionOne = touch.position;

                        Image3DMaketRotateLeftRight = (Image3DMaketTouchPositionOne.x - Image3DMaketTouchPositionZero.x) * 5;
                        Image3DMaketRotateUpDown = (Image3DMaketTouchPositionOne.y - Image3DMaketTouchPositionZero.y) * 5;

                        Image3DMaketTouchPositionZero = Image3DMaketTouchPositionOne;

                        if (Image3DMaketRotateLeftRight != 0)
                        {
                            Image3DMaket.transform.RotateAround(Image3DMaket.transform.position, Vector3.back, Time.deltaTime * Image3DMaketRotateLeftRight);
                        }

                        if (Image3DMaketRotateUpDown != 0)
                        {
                            Image3DMaket.transform.RotateAround(Image3DMaket.transform.position, Vector3.right, Time.deltaTime * Image3DMaketRotateUpDown);
                        }
                    }
                }

                else if (RotateMaketButton.gameObject.activeSelf)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        Image3DMaketTouched = true;

                        Image3DMaketTouchPositionZero = touch.position;
                        Image3DMaketMoveUpDown = 0;
                        Image3DMaketMoveLeftRight = 0;
                    }

                    else if (touch.phase == TouchPhase.Moved)
                    {
                        Image3DMaketTouchPositionOne = touch.position;

                        Image3DMaketMoveUpDown = (Image3DMaketTouchPositionOne.x - Image3DMaketTouchPositionZero.x) * 0.05f;
                        Image3DMaketMoveLeftRight = (Image3DMaketTouchPositionOne.y - Image3DMaketTouchPositionZero.y) * 0.05f;

                        Image3DMaketTouchPositionZero = Image3DMaketTouchPositionOne;

                        if (Image3DMaketMoveUpDown != 0)
                        {
                            Image3DMaket.transform.Translate(Vector3.right * Time.deltaTime * Image3DMaketMoveUpDown, mainCamera.transform);
                        }

                        if (Image3DMaketMoveLeftRight != 0)
                        {
                            Image3DMaket.transform.Translate(Vector3.up * Time.deltaTime * Image3DMaketMoveLeftRight, mainCamera.transform);
                        }
                    }
                }
            }

            else if (Input.touchCount >= 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    Image3DMaketTouched = true;
                }

                if (touchZero.phase == TouchPhase.Ended)
                {
                    Image3DMaketTouchPositionZero = touchOne.position;
                }
                if (touchOne.phase == TouchPhase.Ended)
                {
                    Image3DMaketTouchPositionZero = touchZero.position;
                }
            }

            return;
        }

        if ((ZhkImageSlider.activeSelf && !modelIsOpen) || Menu.activeSelf || ModelsPanel.activeSelf)
        {
            return;
        }

        if (!BackgroundMusic.isPlaying && !MusicMuted && MusicEnabled)
        {
            soundtrackNumber++;
            if (soundtrackNumber == allSoundtracksNumber)
            {
                soundtrackNumber = 0;
            }

            BackgroundMusic.clip = Resources.Load<AudioClip>("Music/" + soundtrackNumber);
            BackgroundMusic.Play();
        }

        if (modelIsOpen)
        {
            if (scannerEnabled)
            {
                if (lastSelectedObject.plane.gameObject.transform.position.y !=  modelYPosition)
                {
                    lastSelectedObject.plane.gameObject.transform.position = new Vector3(lastSelectedObject.plane.gameObject.transform.position.x, modelYPosition, lastSelectedObject.plane.gameObject.transform.position.z);
                }

                if (lastSelectedObject.transform.position.y != lastSelectedObject.plane.gameObject.transform.position.y)
                {
                    Vector3 openModelsPlaneYPosition = new Vector3(lastSelectedObject.transform.position.x, lastSelectedObject.plane.gameObject.transform.position.y, lastSelectedObject.transform.position.z);

                    lastSelectedObject.transform.position = openModelsPlaneYPosition;
                }
            }


            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (rotateCamera.activeSelf)
                    {
                        rotateCamera.SetActive(false);
                        rotateCameraText.SetActive(false);
                    }

                    if (EventSystem.current.currentSelectedGameObject != null)
                    {
                        if (EventSystem.current.currentSelectedGameObject.name.Equals(Joystick.name))
                        {
                            joystickEnabled = true;
                        }
                    }

                    rotateLeftRight = 0;
                    touchPositionRotateZero = touch.position;

                    if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXEditor)
                    {
                        rotateMaxSpeed = 200;
                    }
                    else if (Application.platform == RuntimePlatform.Android)
                    {
                        rotateMaxSpeed = 300;
                    }
                }

                else if (touch.phase == TouchPhase.Moved)
                {
                    touchPositionRotateOne = touch.position;
                    touchPositionOne = touch.position;


                    if (!joystickEnabled)
                    {
                        rotateLeftRight = (touchPositionRotateOne.x - touchPositionRotateZero.x) * 5;

                        touchPositionRotateZero = touchPositionRotateOne;

                        if (rotateLeftRight > rotateMaxSpeed)
                        {
                            rotateLeftRight = rotateMaxSpeed;
                        }
                        else if (rotateLeftRight < -rotateMaxSpeed)
                        {
                            rotateLeftRight = -rotateMaxSpeed;
                        }
                    }

                    else if (joystickEnabled && !wallHit)
                    {
                        JoystickHandle.transform.position = new Vector3(touchPositionOne.x, touchPositionOne.y, JoystickHandle.transform.position.z);

                        if (JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y > JoystickHandleRadius)
                        {
                            JoystickHandle.transform.position = new Vector3(JoystickHandle.transform.position.x, JoystickHandleInitialPosition.y + JoystickHandleRadius, JoystickHandle.transform.position.z);
                        }
                        else if (JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y < -JoystickHandleRadius)
                        {
                            JoystickHandle.transform.position = new Vector3(JoystickHandle.transform.position.x, JoystickHandleInitialPosition.y - JoystickHandleRadius, JoystickHandle.transform.position.z);
                        }

                        if (JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x > JoystickHandleRadius)
                        {
                            JoystickHandle.transform.position = new Vector3(JoystickHandleInitialPosition.x + JoystickHandleRadius, JoystickHandle.transform.position.y, JoystickHandle.transform.position.z);
                        }
                        else if (JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x < -JoystickHandleRadius)
                        {
                            JoystickHandle.transform.position = new Vector3(JoystickHandleInitialPosition.x - JoystickHandleRadius, JoystickHandle.transform.position.y, JoystickHandle.transform.position.z);
                        }

                        float handleXDifference = JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x;
                        float handleYDifference = JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y;

                        float handleXPosition = JoystickHandleRadius * handleXDifference / (float)Math.Sqrt(handleXDifference * handleXDifference + handleYDifference * handleYDifference);
                        float handleYPosition = JoystickHandleRadius * handleYDifference / (float)Math.Sqrt(handleXDifference * handleXDifference + handleYDifference * handleYDifference);

                        if (Math.Sqrt(handleXDifference * handleXDifference + handleYDifference * handleYDifference) > JoystickHandleRadius)
                        {
                            JoystickHandle.transform.position = new Vector3(handleXPosition + JoystickHandleInitialPosition.x, handleYPosition + JoystickHandleInitialPosition.y, JoystickHandle.transform.position.z);
                        }

                        if (Math.Sqrt(handleXDifference * handleXDifference + handleYDifference * handleYDifference) > joystickHandleDisabledRadius)
                        {
                            movementEnabled = true;
                        }

                        if (movementEnabled)
                        {
                            moveFrontBack = (JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y) * 0.02f;
                            moveLeftRight = (JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x) * 0.02f;

                            if (moveFrontBack > movementMaxSpeed)
                            {
                                moveFrontBack = movementMaxSpeed;
                            }
                            else if (moveFrontBack < -movementMaxSpeed)
                            {
                                moveFrontBack = -movementMaxSpeed;
                            }

                            if (moveLeftRight > movementMaxSpeed)
                            {
                                moveLeftRight = movementMaxSpeed;
                            }
                            else if (moveLeftRight < -movementMaxSpeed)
                            {
                                moveLeftRight = -movementMaxSpeed;
                            }
                        }

                        if (FootstepsSound.isPlaying)
                        {
                            FootstepsSound.pitch = (float) Math.Sqrt((JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x) * (JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x)
                                                                + (JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y) * (JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y)) * 0.01f;
                            if (FootstepsSound.pitch < 0.7f)
                            {
                                FootstepsSound.pitch = 0.7f;
                            }
                        }
                    }
                }

                else if (touch.phase == TouchPhase.Ended)
                {
                    wallHit = false;
                    moveFrontBack = 0;
                    moveLeftRight = 0;

                    joystickEnabled = false;
                    movementEnabled = false;

                    JoystickHandle.transform.position = JoystickHandleInitialPosition;

                    if (FootstepsSound.isPlaying)
                    {
                        FootstepsSound.Pause();
                    }
                }
            }

            else if (Input.touchCount == 2)
            {
                if (rotateCamera.activeSelf)
                {
                    rotateCamera.SetActive(false);
                    rotateCameraText.SetActive(false);
                }

                zoomInOut = 0;

                var touchZero = Input.GetTouch(0);
                var touchOne = Input.GetTouch(1);

                if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled
                    || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                {
                    if (touchZero.phase == TouchPhase.Ended)
                    {
                        touchPositionRotateZero = touchOne.position;
                    }

                    if (touchOne.phase == TouchPhase.Ended)
                    {
                        touchPositionRotateZero = touchZero.position;
                    }

                    rotateLeftRight = 0;

                    wallHit = false;
                    return;
                }

                if (joystickEnabled)
                {
                    if (touchOne.phase == TouchPhase.Began)
                    {
                        rotateLeftRight = 0;
                        touchPositionRotateZero = touchOne.position;
                    }

                    if (touchOne.phase == TouchPhase.Moved)
                    {
                        touchPositionRotateOne = touchOne.position;

                        rotateLeftRight = (touchPositionRotateOne.x - touchPositionRotateZero.x) * 5;

                        touchPositionRotateZero = touchPositionRotateOne;

                        if (rotateLeftRight > rotateMaxSpeed)
                        {
                            rotateLeftRight = rotateMaxSpeed;
                        }
                        else if (rotateLeftRight < -rotateMaxSpeed)
                        {
                            rotateLeftRight = -rotateMaxSpeed;
                        }
                    }

                    if (touchZero.phase == TouchPhase.Moved && !wallHit)
                    {
                        JoystickHandle.transform.position = new Vector3(touchPositionOne.x, touchPositionOne.y, JoystickHandle.transform.position.z);

                        if (JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y > JoystickHandleRadius)
                        {
                            JoystickHandle.transform.position = new Vector3(JoystickHandle.transform.position.x, JoystickHandleInitialPosition.y + JoystickHandleRadius, JoystickHandle.transform.position.z);
                        }
                        else if (JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y < -JoystickHandleRadius)
                        {
                            JoystickHandle.transform.position = new Vector3(JoystickHandle.transform.position.x, JoystickHandleInitialPosition.y - JoystickHandleRadius, JoystickHandle.transform.position.z);
                        }

                        if (JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x > JoystickHandleRadius)
                        {
                            JoystickHandle.transform.position = new Vector3(JoystickHandleInitialPosition.x + JoystickHandleRadius, JoystickHandle.transform.position.y, JoystickHandle.transform.position.z);
                        }
                        else if (JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x < -JoystickHandleRadius)
                        {
                            JoystickHandle.transform.position = new Vector3(JoystickHandleInitialPosition.x - JoystickHandleRadius, JoystickHandle.transform.position.y, JoystickHandle.transform.position.z);
                        }

                        float handleXDifference = JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x;
                        float handleYDifference = JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y;

                        float handleXPosition = JoystickHandleRadius * handleXDifference / (float)Math.Sqrt(handleXDifference * handleXDifference + handleYDifference * handleYDifference);
                        float handleYPosition = JoystickHandleRadius * handleYDifference / (float)Math.Sqrt(handleXDifference * handleXDifference + handleYDifference * handleYDifference);

                        if (Math.Sqrt(handleXDifference * handleXDifference + handleYDifference * handleYDifference) > JoystickHandleRadius)
                        {
                            JoystickHandle.transform.position = new Vector3(handleXPosition + JoystickHandleInitialPosition.x, handleYPosition + JoystickHandleInitialPosition.y, JoystickHandle.transform.position.z);
                        }

                        if (Math.Sqrt(handleXDifference * handleXDifference + handleYDifference * handleYDifference) > joystickHandleDisabledRadius)
                        {
                            movementEnabled = true;
                        }

                        if (movementEnabled)
                        {
                            moveFrontBack = (JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y) * 0.02f;
                            moveLeftRight = (JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x) * 0.02f;

                            if (moveFrontBack > movementMaxSpeed)
                            {
                                moveFrontBack = movementMaxSpeed;
                            }
                            else if (moveFrontBack < -movementMaxSpeed)
                            {
                                moveFrontBack = -movementMaxSpeed;
                            }

                            if (moveLeftRight > movementMaxSpeed)
                            {
                                moveLeftRight = movementMaxSpeed;
                            }
                            else if (moveLeftRight < -movementMaxSpeed)
                            {
                                moveLeftRight = -movementMaxSpeed;
                            }
                        }

                        if (FootstepsSound.isPlaying)
                        {
                            FootstepsSound.pitch = (float)Math.Sqrt((JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x) * (JoystickHandle.transform.position.x - JoystickHandleInitialPosition.x)
                                                                + (JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y) * (JoystickHandle.transform.position.y - JoystickHandleInitialPosition.y)) * 0.01f;
                            if (FootstepsSound.pitch < 0.7f)
                            {
                                FootstepsSound.pitch = 0.7f;
                            }
                        }
                    }
                }

                else
                {
                    if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                    {
                        initialTouchesDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    }

                    if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
                    {
                        currentTouchesDistance = Vector2.Distance(touchZero.position, touchOne.position);

                        if (!wallHit)
                        {
                            if (Mathf.Approximately(initialTouchesDistance, 0))
                            {
                                return;
                            }

                            zoomInOut = (currentTouchesDistance - initialTouchesDistance) * 0.1f;

                            initialTouchesDistance = currentTouchesDistance;
                        }
                    }
                }
            }


            if (arCameraCollisionController.isColliding && !arCameraCollisionController.collidedObjectTag.Equals("Door") && !arCameraCollisionController.collidedObjectTag.Equals("Wall"))
            {
                if (Input.touchCount == 0 && moveFrontBack == 0 && moveLeftRight == 0)
                {
                    if (modelJustOpened)
                    {
                        lastSelectedObject.transform.position += Vector3.back * 0.1f;

                        modelJustOpened = false;
                    }

                    else
                    {
                        if (arCameraPreveousPosition == null)
                        {
                            arCameraPreveousPosition = arCamera.transform.position;
                        }

                        Vector3 arCameraPositionDifference = arCamera.transform.position - arCameraPreveousPosition;
                        arCameraPositionDifference.Normalize();

                        lastSelectedObject.transform.position += arCameraPositionDifference * 0.1f;


                        if (!WallHitSound.isPlaying)
                        {
                            WallHitSound.Play();
                        }
                    }
                }

                else
                {
                    wallHit = true;

                    moveFrontBack = 0;
                    moveLeftRight = 0;
                    rotateLeftRight = 0;
                    joystickEnabled = false;
                    JoystickHandle.transform.position = JoystickHandleInitialPosition;
                    Joystick.gameObject.SetActive(false);
                    Joystick.gameObject.SetActive(true);

                    initialTouchesDistance = currentTouchesDistance;
                    zoomInOut = 0;

                    if (lastSelectedObjectPreveousPosition == null)
                    {
                        lastSelectedObjectPreveousPosition = lastSelectedObject.transform.position;
                    }

                    movedModelPositionDifference = lastSelectedObject.transform.position - lastSelectedObjectPreveousPosition;
                    movedModelPositionDifference.Normalize();

                    lastSelectedObject.transform.position -= 0.1f * movedModelPositionDifference;

                    if (FootstepsSound.isPlaying)
                    {
                        FootstepsSound.Pause();
                    }

                    if (!WallHitSound.isPlaying)
                    {
                        //Handheld.Vibrate();
                        WallHitSound.Play();
                    }
                }
            }

            else
            {
                if (arCameraCollisionController.isColliding && arCameraCollisionController.collidedObjectTag.Equals("Door"))
                {
                    if (!ModelOpenningSound.isPlaying)
                    {
                        ModelOpenningSound.Play();
                    }
                }

                if (arCameraCollisionController.isColliding && arCameraCollisionController.collidedObjectTag.Equals("Wall"))
                {
                    if (!arCameraCollisionController.collidedObjectName.Equals(lastCollidedObjectName))
                    {
                        if (!WallThroghSound.isPlaying)
                        {
                            WallThroghSound.Play();
                        }
                    }

                    lastCollidedObjectName = arCameraCollisionController.collidedObjectName;
                }

                arCameraPreveousPosition = arCamera.transform.position;
                lastSelectedObjectPreveousPosition = lastSelectedObject.transform.position;

                if (zoomInOut != 0)
                {
                    if (normalView)
                    {
                        lastSelectedObject.transform.Translate(Vector3.forward * Time.deltaTime * -zoomInOut, arCamera.transform);
                        SkyboxCube.transform.Translate(Vector3.forward * Time.deltaTime * -zoomInOut, arCamera.transform);

                        modelYPosition = lastSelectedObject.transform.position.y;

                        if (modelYPosition < -1f)
                        {
                            modelYPosition = -1f;
                            lastSelectedObject.transform.position = new Vector3(lastSelectedObject.transform.position.x, modelYPosition, lastSelectedObject.transform.position.z);
                            SkyboxCube.transform.position = new Vector3(lastSelectedObject.transform.position.x, modelYPosition, lastSelectedObject.transform.position.z);
                        }
                        else if (modelYPosition > 1)
                        {
                            modelYPosition = 1;
                            lastSelectedObject.transform.position = new Vector3(lastSelectedObject.transform.position.x, modelYPosition, lastSelectedObject.transform.position.z);
                            SkyboxCube.transform.position = new Vector3(lastSelectedObject.transform.position.x, modelYPosition, lastSelectedObject.transform.position.z);
                        }

                        if (FootstepsSound.isPlaying)
                        {
                            FootstepsSound.Pause();
                        }
                    }

                    else
                    {
                        topCamera.GetComponent<TopCameraController>().cameraHeight -= zoomInOut * 0.3f;
                    }
                }
                else
                {
                    if (rotateLeftRight != 0)
                    {
                        lastSelectedObject.transform.RotateAround(arCamera.transform.position, Vector3.up, frameRate * -rotateLeftRight);
                        SkyboxCube.transform.RotateAround(arCamera.transform.position, Vector3.up, frameRate * -rotateLeftRight);

                        rotateLeftRight /= 1.02f;

                        if (System.Math.Abs(rotateLeftRight) < 1)
                        {
                            rotateLeftRight = 0;
                        }
                    }


                    if (moveFrontBack != 0 || moveLeftRight != 0)
                    {
                        lastSelectedObject.transform.Translate(Vector3.forward * Time.deltaTime * -moveFrontBack, arCamera.transform);
                        lastSelectedObject.transform.Translate(Vector3.left * Time.deltaTime * moveLeftRight, arCamera.transform);

                        lastSelectedObject.transform.position = new Vector3(lastSelectedObject.transform.position.x, modelYPosition, lastSelectedObject.transform.position.z);

                        SkyboxCube.transform.Translate(Vector3.forward * Time.deltaTime * -moveFrontBack, arCamera.transform);
                        SkyboxCube.transform.Translate(Vector3.left * Time.deltaTime * moveLeftRight, arCamera.transform);

                        SkyboxCube.transform.position = new Vector3(lastSelectedObject.transform.position.x, modelYPosition, lastSelectedObject.transform.position.z);


                        modelXPosition = lastSelectedObject.transform.position.x;
                        modelZPosition = lastSelectedObject.transform.position.z;

                        if (modelXPosition > 15)
                        {
                            modelXPosition = 15;
                            lastSelectedObject.transform.position = new Vector3(modelXPosition, lastSelectedObject.transform.position.y, lastSelectedObject.transform.position.z);
                            SkyboxCube.transform.position = new Vector3(modelXPosition, lastSelectedObject.transform.position.y, lastSelectedObject.transform.position.z);

                        }
                        else if (modelXPosition < -15)
                        {
                            modelXPosition = -15;
                            lastSelectedObject.transform.position = new Vector3(modelXPosition, lastSelectedObject.transform.position.y, lastSelectedObject.transform.position.z);
                            SkyboxCube.transform.position = new Vector3(modelXPosition, lastSelectedObject.transform.position.y, lastSelectedObject.transform.position.z);
                        }

                        if (modelZPosition > 15)
                        {
                            modelZPosition = 15;
                            lastSelectedObject.transform.position = new Vector3(lastSelectedObject.transform.position.x, lastSelectedObject.transform.position.y, modelZPosition);
                            SkyboxCube.transform.position = new Vector3(lastSelectedObject.transform.position.x, lastSelectedObject.transform.position.y, modelZPosition);
                        }
                        else if (modelZPosition < -15)
                        {
                            modelZPosition = -15;
                            lastSelectedObject.transform.position = new Vector3(lastSelectedObject.transform.position.x, lastSelectedObject.transform.position.y, modelZPosition);
                            SkyboxCube.transform.position = new Vector3(lastSelectedObject.transform.position.x, lastSelectedObject.transform.position.y, modelZPosition);
                        }

                        if (!FootstepsSound.isPlaying && System.Math.Abs(moveFrontBack) > 0.1)
                        {
                            FootstepsSound.Play();
                        }
                    }
                }
            }
        }

        else if(!modelIsOpen && MaketsPageOpen)
        {
            if (lastSelectedObject != null && lastSelectedObject.Selected)
            {
                distanceCameraModel = arCamera.transform.position - lastSelectedObject.transform.position;

                if (lastSelectedObject.transform.localScale.sqrMagnitude < distanceCameraModel.sqrMagnitude)
                {
                    lastSelectedObject.initialScale = lastSelectedObject.transform.localScale;
                }

                if (lastSelectedObject.transform.localScale.sqrMagnitude > 3 * lastSelectedObject.initialScale.sqrMagnitude)
                {
                    lastSelectedObject.transform.localScale = (float)Math.Sqrt(3) * lastSelectedObject.initialScale;
                }
            }


            if (Input.touchCount == 0)
            {
                if (scannerEnabled)
                {
                    if (lastSelectedObject == null && placedPrefab != null)
                    {
                        if (!aRPlaneManager.enabled)
                        {
                            aRPlaneManager.enabled = true;

                            foreach (ARPlane plane in aRPlaneManager.trackables)
                            {
                                Color color = plane.GetComponent<Renderer>().material.color;
                                color.a = 0.4f;
                                plane.GetComponent<Renderer>().material.color = color;

                                color = plane.GetComponent<LineRenderer>().material.color;
                                color.a = 1;
                                plane.GetComponent<LineRenderer>().material.color = color;
                            }
                        }

                        if (Physics.Raycast(arCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)), arCamera.transform.forward, out hit))
                        {
                            PlacerGreen.transform.position = hit.point;

                            placedPrefabScale = placedPrefabScaleCooficient * 0.67f * placedPrefab.transform.localScale * hit.distance;

                            if (placedPrefab.transform.localScale.x > placedPrefab.transform.localScale.z)
                            {
                                PlacerGreen.transform.localScale = placedPrefabScaleCooficient * 0.67f * new Vector3(placedPrefab.transform.localScale.x, placedPrefab.transform.localScale.y, placedPrefab.transform.localScale.x) * hit.distance;
                            }
                            else
                            {
                                PlacerGreen.transform.localScale = placedPrefabScaleCooficient * 0.67f * new Vector3(placedPrefab.transform.localScale.z, placedPrefab.transform.localScale.y, placedPrefab.transform.localScale.z) * hit.distance;
                            }


                            PlacerGreen.SetActive(true);
                            PlacerRed.SetActive(false);

                            foreach (PlacementObject modelInScene in modelsInScene)
                            {
                                if (hit.collider.gameObject == modelInScene.gameObject)
                                {
                                    if (PlacerGreen.activeSelf)
                                    {
                                        if (hit.transform.localScale.x > hit.transform.localScale.z)
                                        {
                                            PlacerRed.transform.localScale = 1.5f * new Vector3(hit.transform.localScale.x, hit.transform.localScale.y, hit.transform.localScale.x);
                                        }
                                        else
                                        {
                                            PlacerRed.transform.localScale = 1.5f * new Vector3(hit.transform.localScale.z, hit.transform.localScale.y, hit.transform.localScale.z);
                                        }

                                        PlacerRed.transform.position = hit.point;

                                        PlacerGreen.SetActive(false);
                                        PlacerRed.SetActive(true);
                                    }
                                }
                            }


                            if (PlacerGreen.activeSelf)
                            {
                                phoneInHand.SetActive(false);
                                phoneInHandText.SetActive(false);

                                if (!fingerTapShowedOnce)
                                {
                                    fingerTap.SetActive(true);
                                    fingerTapText.SetActive(true);
                                }

                                if (hit.distance < 0.2f)
                                {
                                    PlacerRed.transform.position = PlacerGreen.transform.position;
                                    PlacerRed.transform.localScale = PlacerGreen.transform.localScale;

                                    PlacerGreen.SetActive(false);
                                    PlacerRed.SetActive(true);
                                }
                            }
                        }

                        else
                        {
                            if (!phoneInHandShowedOnce)
                            {
                                phoneInHand.SetActive(true);
                                phoneInHandText.SetActive(true);
                            }

                            fingerTap.SetActive(false);
                            fingerTapText.SetActive(false);


                            FakeObjectPrivate = Instantiate(FakeObject, arCamera.transform, false);
                            modelPosition = FakeObjectPrivate.transform.position;
                            Destroy(FakeObjectPrivate);

                            if (placedPrefab.transform.localScale.x > placedPrefab.transform.localScale.z)
                            {
                                PlacerRed.transform.localScale = placedPrefabScaleCooficient * 1.5f * new Vector3(placedPrefab.transform.localScale.x, placedPrefab.transform.localScale.y, placedPrefab.transform.localScale.x);
                            }
                            else
                            {
                                PlacerRed.transform.localScale = placedPrefabScaleCooficient * 1.5f * new Vector3(placedPrefab.transform.localScale.z, placedPrefab.transform.localScale.y, placedPrefab.transform.localScale.z);
                            }

                            PlacerRed.transform.position = new Vector3(modelPosition.x, modelPosition.y, modelPosition.z);

                            PlacerGreen.SetActive(false);
                            PlacerRed.SetActive(true);
                        }
                    }

                    else
                    {
                        if (aRPlaneManager.enabled)
                        {
                            aRPlaneManager.enabled = false;

                            foreach (ARPlane plane in aRPlaneManager.trackables)
                            {
                                Color color = plane.GetComponent<Renderer>().material.color;
                                color.a = 0.1f;
                                plane.GetComponent<Renderer>().material.color = color;

                                color = plane.GetComponent<LineRenderer>().material.color;
                                color.a = 0.1f;
                                plane.GetComponent<LineRenderer>().material.color = color;
                            }
                        }

                        if (lastSelectedObject != null)
                        {
                            if (lastSelectedObject.transform.localScale.x > lastSelectedObject.transform.localScale.z)
                            {
                                PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.x, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.x);
                            }
                            else
                            {
                                PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.z, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.z);
                            }
                        }
                    }


                    foreach (PlacementObject modelInScene in modelsInScene)
                    {
                        ARPlane modelInScenePlane = null;

                        foreach (ARPlane plane in aRPlaneManager.trackables)
                        {
                            if (modelInScene.plane == null)
                            {
                                if (modelInScenePlane == null)
                                {
                                    modelInScenePlane = plane;
                                }

                                if (modelInScenePlane.gameObject.transform.position.y < plane.gameObject.transform.position.y)
                                {
                                    modelInScenePlane = plane;
                                }
                            }

                            else if (modelInScene.plane == plane)
                            {
                                modelInScenePlane = plane;

                                if (modelInScene.transform.position.y != plane.gameObject.transform.position.y)
                                {
                                    Vector3 planeYPosition = new Vector3(modelInScene.transform.position.x, plane.gameObject.transform.position.y, modelInScene.transform.position.z);

                                    modelInScene.transform.position = planeYPosition;
                                    break;
                                }
                            }
                        }

                        modelInScene.plane = modelInScenePlane;
                    }
                }

                else
                {
                    if (lastSelectedObject == null && placedPrefab != null)
                    {
                        if (Physics.Raycast(arCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)), arCamera.transform.forward, out hit))
                        {
                            PlacerGreen.SetActive(false);

                            foreach (PlacementObject modelInScene in modelsInScene)
                            {
                                if (hit.collider.gameObject == modelInScene.gameObject)
                                {
                                    if (hit.transform.localScale.x > hit.transform.localScale.z)
                                    {
                                        PlacerRed.transform.localScale = 1.5f * new Vector3(hit.transform.localScale.x, hit.transform.localScale.y, hit.transform.localScale.x);
                                    }
                                    else
                                    {
                                        PlacerRed.transform.localScale = 1.5f * new Vector3(hit.transform.localScale.z, hit.transform.localScale.y, hit.transform.localScale.z);
                                    }

                                    PlacerRed.transform.position = hit.point;

                                    PlacerRed.SetActive(true);
                                    break;
                                }
                            }
                        }

                        else
                        {
                            if (!fingerTapShowedOnce)
                            {
                                fingerTap.SetActive(true);
                                fingerTapText.SetActive(true);
                            }

                            FakeObjectPrivate = Instantiate(FakeObject, arCamera.transform, false);
                            modelPosition = FakeObjectPrivate.transform.position;
                            Destroy(FakeObjectPrivate);

                            placedPrefabScale = placedPrefabScaleCooficient * 1.5f * placedPrefab.transform.localScale;

                            if (placedPrefab.transform.localScale.x > placedPrefab.transform.localScale.z)
                            {
                                PlacerGreen.transform.localScale = placedPrefabScaleCooficient * 1.5f * new Vector3(placedPrefab.transform.localScale.x, placedPrefab.transform.localScale.y, placedPrefab.transform.localScale.x);
                            }
                            else
                            {
                                PlacerGreen.transform.localScale = placedPrefabScaleCooficient * 1.5f * new Vector3(placedPrefab.transform.localScale.z, placedPrefab.transform.localScale.y, placedPrefab.transform.localScale.z);
                            }

                            PlacerGreen.transform.position = new Vector3(modelPosition.x, modelPosition.y, modelPosition.z);

                            PlacerGreen.SetActive(true);
                            PlacerRed.SetActive(false);
                        }
                    }
                }
            }

            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject = new RaycastHit();

                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }

                if (scannerEnabled)
                {
                    touchPosition = touch.position;

                    planeTouched = false;

                    if (!uiButtonPressed)
                    {
                        if (touchEnabled && Physics.Raycast(ray, out hitObject))
                        {
                            foreach (ARPlane plane in aRPlaneManager.trackables)
                            {
                                if (hitObject.collider.gameObject == plane.gameObject)
                                {
                                    planeTouched = true;
                                    break;
                                }
                            }

                            if (!planeTouched)
                            {
                                modelTouched = false;

                                foreach (PlacementObject modelInScene in modelsInScene)
                                {
                                    if (hitObject.collider.gameObject == modelInScene.gameObject)
                                    {
                                        if (lastSelectedObject == null)
                                        {
                                            ModelSelectingSound.Play();

                                            if (modelChanged)
                                            {
                                                modelChanged = false;
                                            }
                                        }

                                        else
                                        {
                                            if (hitObject.collider.gameObject != lastSelectedObject.gameObject)
                                            {
                                                ModelSelectingSound.Play();
                                            }
                                        }

                                        touchEnabled = false;

                                        modelTouched = true;
                                        break;
                                    }
                                }


                                if (modelTouched)
                                {
                                    lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();

                                    if (lastSelectedObject != null)
                                    {
                                        foreach (PlacementObject modelInScene in modelsInScene)
                                        {
                                            modelInScene.Selected = modelInScene == lastSelectedObject;
                                        }

                                        if (hitObject.transform.localScale.x > hitObject.transform.localScale.z)
                                        {
                                            PlacerGreen.transform.localScale = 1.5f * new Vector3(hitObject.transform.localScale.x, hitObject.transform.localScale.y, hitObject.transform.localScale.x);
                                        }
                                        else
                                        {
                                            PlacerGreen.transform.localScale = 1.5f * new Vector3(hitObject.transform.localScale.z, hitObject.transform.localScale.y, hitObject.transform.localScale.z);
                                        }

                                        PlacerGreen.transform.position = lastSelectedObject.transform.position;

                                        PlacerGreen.SetActive(true);
                                        PlacerRed.SetActive(false);
                                    }
                                }
                            }
                        }

                        if (arRaycastManager.Raycast(touchPosition, hits))
                        {
                            Pose hitPose = hits[0].pose;

                            if (lastSelectedObject == null && touch.phase == TouchPhase.Began)
                            {
                                if (PlacerGreen.activeSelf)
                                {
                                    FakeObjectPrivate = Instantiate(FakeObject, arCamera.transform, false);
                                    modelRotation = FakeObjectPrivate.transform.rotation;
                                    modelRotation = Quaternion.Euler(0, modelRotation.eulerAngles.y, 0);
                                    Destroy(FakeObjectPrivate);

                                    placedPrefab.GetComponent<Lean.Touch.LeanDragTranslate>().enabled = false;

                                    lastSelectedObject = Instantiate(placedPrefab, hit.point, modelRotation).GetComponent<PlacementObject>();

                                    lastSelectedObject.transform.localScale = 0.67f * placedPrefabScale;

                                    ModelPlacingSound.Play();

                                    fingerTap.SetActive(false);
                                    fingerTapText.SetActive(false);
                                    fingerTapShowedOnce = true;
                                    phoneInHandShowedOnce = true;

                                    lastSelectedObject.initialPrefab = placedPrefab;
                                    //add to list
                                    modelsInScene.Add(lastSelectedObject);

                                    foreach (PlacementObject modelInScene in modelsInScene)
                                    {
                                        modelInScene.Selected = modelInScene == lastSelectedObject;
                                    }

                                    if (modelsInScene.Count > 0)
                                    {
                                        OpenModelButton.gameObject.SetActive(true);
                                        DeleteModelButton.gameObject.SetActive(true);
                                    }

                                    randomWallMaterialIndex = random.Next(0, wallMaterials.Length);
                                    randomFloorMaterialIndex = random.Next(0, floorMaterials.Length);

                                    if (rulerOn)
                                    {
                                        ToggleRuler();
                                    }
                                    else
                                    {
                                        SetShellMaterial();
                                        ChangeWallsMaterial(randomWallMaterialIndex);
                                        ChangeFloorsMaterial(randomFloorMaterialIndex);
                                    }

                                    if (WallsTransparencyOn)
                                    {
                                        ToggleWalls();
                                    }
                                    else
                                    {
                                        SetShellMaterial();
                                        ChangeWallsMaterial(randomWallMaterialIndex);
                                    }

                                    if (!furnitureOn)
                                    {
                                        OnOffFurniture();
                                    }


                                    //model scale
                                    lastSelectedObject.initialScale = lastSelectedObject.transform.localScale;
                                    lastSelectedObject.initialPrefabScale = placedPrefab.transform.localScale;

                                    OpenModelButton.enabled = true;

                                    if (modelChanged)
                                    {
                                        modelChanged = false;
                                    }

                                    lastSelectedObjectInitialPosition = lastSelectedObject.transform.position;
                                    initialHitPose = hitPose.position;

                                    foreach (ARPlane plane in aRPlaneManager.trackables)
                                    {
                                        if (hit.collider.gameObject == plane.gameObject)
                                        {
                                            lastSelectedObject.plane = plane;
                                        }
                                    }
                                }
                                else
                                {
                                    WallHitSound.Play();
                                    Handheld.Vibrate();
                                }
                            }
                        }

                        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                        {
                            Pose hitPose = hits[0].pose;

                            if (touch.phase == TouchPhase.Moved)
                            {
                                if (lastSelectedObject.Selected && !planeTouched)
                                {
                                    if (justMoved)
                                    {
                                        if (!soundPlayed)
                                        {
                                            ModelMovingSound.Play();
                                            soundPlayed = true;
                                        }

                                        lastSelectedObjectInitialPosition = lastSelectedObject.transform.position;
                                        initialHitPose = hitPose.position;

                                        if (System.Math.Abs(hitPose.position.y - lastSelectedObject.plane.gameObject.transform.position.y) > 0.01f)
                                        {
                                            lastSelectedObjectInitialPosition = Vector3.zero;
                                            initialHitPose = hitPose.position = Vector3.zero;
                                        }

                                        justMoved = false;
                                    }

                                    touchMovedDistance = hitPose.position - initialHitPose;

                                    if (touchMovedDistance.sqrMagnitude < 0.2f || Math.Abs(hitPose.position.y - initialHitPose.y) > 0.1f)
                                    {
                                        lastSelectedObject.transform.position = lastSelectedObjectInitialPosition + touchMovedDistance;

                                        lastSelectedObjectInitialPosition = lastSelectedObject.transform.position;
                                        initialHitPose = hitPose.position;
                                    }


                                    PlacerGreen.transform.position = lastSelectedObject.transform.position;

                                    if (!PlacerGreen.activeSelf)
                                    {
                                        PlacerGreen.SetActive(true);
                                        PlacerRed.SetActive(false);
                                    }

                                    foreach (ARPlane plane in aRPlaneManager.trackables)
                                    {
                                        planeModelMinDistance = lastSelectedObject.plane.gameObject.transform.position.y - lastSelectedObject.transform.position.y;
                                        planeModelDistance = plane.gameObject.transform.position.y - lastSelectedObject.transform.position.y;

                                        if (System.Math.Abs(planeModelDistance) < System.Math.Abs(planeModelMinDistance))
                                        {
                                            planeModelMinDistance = System.Math.Abs(planeModelDistance);
                                            lastSelectedObject.plane = plane;
                                        }
                                    }
                                }
                            }
                        }
                    }


                    if (touch.phase == TouchPhase.Ended)
                    {
                        soundPlayed = false;

                        touchEnabled = true;
                        uiButtonPressed = false;
                        justMoved = true;

                        if (PlacerGreen.activeSelf)
                        {
                            PlacerGreen.SetActive(false);
                        }

                        if (modelChanged)
                        {
                            lastSelectedObject = null;
                        }
                    }
                }

                else
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (Physics.Raycast(ray, out hitObject))
                        {
                            modelTouched = false;

                            foreach (PlacementObject modelInScene in modelsInScene)
                            {
                                if (hitObject.collider.gameObject == modelInScene.gameObject)
                                {
                                    if (lastSelectedObject == null)
                                    {
                                        ModelSelectingSound.Play();

                                        if (modelChanged)
                                        {
                                            modelChanged = false;
                                        }
                                    }
                                    else
                                    {
                                        if (hitObject.collider.gameObject != lastSelectedObject.gameObject)
                                        {
                                            ModelSelectingSound.Play();
                                        }
                                    }

                                    modelTouched = true;
                                    break;
                                }
                            }

                            if (modelTouched)
                            {
                                lastSelectedObject = hitObject.transform.GetComponent<PlacementObject>();
                                if (lastSelectedObject != null)
                                {
                                    foreach (PlacementObject selectedObject in modelsInScene)
                                    {
                                        selectedObject.Selected = selectedObject == lastSelectedObject;
                                    }

                                    if (lastSelectedObject.transform.localScale.x > lastSelectedObject.transform.localScale.z)
                                    {
                                        PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.x, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.x);
                                    }
                                    else
                                    {
                                        PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.z, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.z);
                                    }

                                    PlacerGreen.transform.position = lastSelectedObject.transform.position;

                                    PlacerRed.SetActive(false);
                                    PlacerGreen.SetActive(true);
                                }
                            }
                        }

                        else
                        {
                            if (lastSelectedObject == null)
                            {
                                if (PlacerGreen.activeSelf)
                                {
                                    FakeObjectPrivate = Instantiate(FakeObject, arCamera.transform, false);
                                    modelPosition = FakeObjectPrivate.transform.position;
                                    modelRotation = FakeObjectPrivate.transform.rotation;
                                    modelRotation = Quaternion.Euler(0, modelRotation.eulerAngles.y, 0);
                                    Destroy(FakeObjectPrivate);

                                    placedPrefab.GetComponent<Lean.Touch.LeanDragTranslate>().enabled = true;

                                    lastSelectedObject = Instantiate(placedPrefab, modelPosition, modelRotation).GetComponent<PlacementObject>();

                                    ModelPlacingSound.Play();

                                    lastSelectedObject.transform.localScale = 0.67f * placedPrefabScale;

                                    fingerTap.SetActive(false);
                                    fingerTapText.SetActive(false);
                                    fingerTapShowedOnce = true;

                                    lastSelectedObject.initialPrefab = placedPrefab;

                                    modelsInScene.Add(lastSelectedObject);

                                    foreach (PlacementObject selectedObject in modelsInScene)
                                    {
                                        selectedObject.Selected = selectedObject == lastSelectedObject;
                                    }

                                    if (modelsInScene.Count > 0)
                                    {
                                        OpenModelButton.gameObject.SetActive(true);
                                        DeleteModelButton.gameObject.SetActive(true);
                                    }

                                    randomWallMaterialIndex = random.Next(0, wallMaterials.Length);
                                    randomFloorMaterialIndex = random.Next(0, floorMaterials.Length);

                                    if (rulerOn)
                                    {
                                        ToggleRuler();
                                    }
                                    else
                                    {
                                        SetShellMaterial();
                                        ChangeWallsMaterial(randomWallMaterialIndex);
                                        ChangeFloorsMaterial(randomFloorMaterialIndex);
                                    }

                                    if (WallsTransparencyOn)
                                    {
                                        ToggleWalls();
                                    }
                                    else
                                    {
                                        SetShellMaterial();
                                        ChangeWallsMaterial(randomWallMaterialIndex);
                                    }

                                    if (!furnitureOn)
                                    {
                                        OnOffFurniture();
                                    }


                                    lastSelectedObject.initialScale = lastSelectedObject.transform.localScale;
                                    lastSelectedObject.initialPrefabScale = placedPrefab.transform.localScale;

                                    OpenModelButton.enabled = true;

                                    if (modelChanged)
                                    {
                                        modelChanged = false;
                                    }
                                }

                                else
                                {
                                    WallHitSound.Play();
                                    Handheld.Vibrate();
                                }
                            }
                        }
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        if (lastSelectedObject != null)
                        {
                            if (!soundPlayed && PlacerGreen.activeSelf) {
                                ModelMovingSound.Play();
                                soundPlayed = true;
                            }

                            PlacerGreen.transform.position = lastSelectedObject.transform.position;
                        }
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        soundPlayed = false;

                        if (PlacerGreen.activeSelf)
                        {
                            PlacerGreen.SetActive(false);
                        }

                        if (modelChanged)
                        {
                            lastSelectedObject = null;
                        }
                    }
                }
            }

            if (Input.touchCount >= 2)
            {
                if (scannerEnabled)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);
                    Ray rayZero = arCamera.ScreenPointToRay(touchZero.position);
                    Ray rayOne = arCamera.ScreenPointToRay(touchOne.position);
                    RaycastHit hitObjectZero = new RaycastHit();
                    RaycastHit hitObjectOne = new RaycastHit();

                    if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                    {
                        if (Physics.Raycast(rayZero, out hitObjectZero))
                        {
                            foreach (ARPlane plane in aRPlaneManager.trackables)
                            {
                                if (hitObjectZero.collider.gameObject == plane.gameObject)
                                {
                                    planeTouched = true;
                                    break;
                                }
                            }

                            if (!planeTouched)
                            {
                                modelTouched = false;

                                foreach (PlacementObject modelInScene in modelsInScene)
                                {
                                    if (hitObjectZero.collider.gameObject == modelInScene.gameObject)
                                    {
                                        hitObjectZeroIsNull = false;

                                        hitObjectZeroObject = hitObjectZero.collider.gameObject;

                                        if (lastSelectedObject == null)
                                        {
                                            ModelSelectingSound.Play();

                                            if (modelChanged)
                                            {
                                                modelChanged = false;
                                            }
                                        }

                                        else
                                        {
                                            if (hitObjectZero.collider.gameObject != lastSelectedObject.gameObject)
                                            {
                                                ModelSelectingSound.Play();
                                            }
                                        }

                                        modelTouched = true;
                                        break;
                                    }
                                }

                                if (modelTouched)
                                {
                                    lastSelectedObject = hitObjectZero.transform.GetComponent<PlacementObject>();
                                    if (lastSelectedObject != null)
                                    {
                                        foreach (PlacementObject modelInScene in modelsInScene)
                                        {
                                            modelInScene.Selected = modelInScene == lastSelectedObject;
                                        }
                                    }

                                    if (hitObjectZero.transform.localScale.x > hitObjectZero.transform.localScale.z)
                                    {
                                        PlacerGreen.transform.localScale = 1.5f * new Vector3(hitObjectZero.transform.localScale.x, hitObjectZero.transform.localScale.y, hitObjectZero.transform.localScale.x);
                                    }
                                    else
                                    {
                                        PlacerGreen.transform.localScale = 1.5f * new Vector3(hitObjectZero.transform.localScale.z, hitObjectZero.transform.localScale.y, hitObjectZero.transform.localScale.z);
                                    }

                                    PlacerGreen.transform.position = lastSelectedObject.transform.position;

                                    PlacerGreen.SetActive(true);
                                    PlacerRed.SetActive(false);
                                }
                            }
                        }

                        if (Physics.Raycast(rayOne, out hitObjectOne))
                        {
                            planeTouched = false;

                            foreach (ARPlane plane in aRPlaneManager.trackables)
                            {
                                if (hitObjectOne.collider.gameObject == plane.gameObject)
                                {
                                    planeTouched = true;
                                    break;
                                }
                            }

                            if (!planeTouched)
                            {
                                modelTouched = false;

                                foreach (PlacementObject modelInScene in modelsInScene)
                                {
                                    if (hitObjectOne.collider.gameObject == modelInScene.gameObject)
                                    {
                                        if (lastSelectedObject == null)
                                        {
                                            ModelSelectingSound.Play();

                                            if (modelChanged)
                                            {
                                                modelChanged = false;
                                            }
                                        }

                                        else
                                        {
                                            if (hitObjectOne.collider.gameObject != lastSelectedObject.gameObject && !hitObjectZeroIsNull)
                                            {
                                                hitObjectZeroIsNull = true;

                                                PlacerGreenOne = Instantiate(PlacerGreen, hitObjectZero.collider.gameObject.transform.position, PlacerGreen.transform.rotation);

                                                if (hitObjectZero.collider.gameObject.transform.localScale.x > hitObjectZero.collider.gameObject.transform.localScale.z)
                                                {
                                                    PlacerGreenOne.transform.localScale = 1.5f * new Vector3(hitObjectZero.collider.gameObject.transform.localScale.x, hitObjectZero.collider.gameObject.transform.localScale.y, hitObjectZero.collider.gameObject.transform.localScale.x);
                                                }
                                                else
                                                {
                                                    PlacerGreenOne.transform.localScale = 1.5f * new Vector3(hitObjectZero.collider.gameObject.transform.localScale.z, hitObjectZero.collider.gameObject.transform.localScale.y, hitObjectZero.collider.gameObject.transform.localScale.z);
                                                }

                                                ModelSelectingSound.Play();
                                            }
                                        }

                                        modelTouched = true;
                                        break;
                                    }
                                }

                                if (modelTouched)
                                {
                                    lastSelectedObject = hitObjectOne.transform.GetComponent<PlacementObject>();

                                    if (lastSelectedObject != null)
                                    {
                                        foreach (PlacementObject modelInScene in modelsInScene)
                                        {
                                            modelInScene.Selected = modelInScene == lastSelectedObject;
                                        }
                                    }

                                    if (hitObjectOne.transform.localScale.x > hitObjectOne.transform.localScale.z)
                                    {
                                        PlacerGreen.transform.localScale = 1.5f * new Vector3(hitObjectOne.transform.localScale.x, hitObjectOne.transform.localScale.y, hitObjectOne.transform.localScale.x);
                                    }
                                    else
                                    {
                                        PlacerGreen.transform.localScale = 1.5f * new Vector3(hitObjectOne.transform.localScale.z, hitObjectOne.transform.localScale.y, hitObjectOne.transform.localScale.z);
                                    }

                                    PlacerGreen.transform.position = lastSelectedObject.transform.position;

                                    PlacerGreen.SetActive(true);
                                    PlacerRed.SetActive(false);
                                }
                            }
                        }
                    }

                    if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
                    {
                        if (lastSelectedObject != null)
                        {
                            if (!soundPlayed && PlacerGreen.activeSelf)
                            {
                                ModelMovingSound.Play();
                                soundPlayed = true;
                            }

                            if (lastSelectedObject.transform.localScale.x > lastSelectedObject.transform.localScale.z)
                            {
                                PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.x, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.x);
                            }
                            else
                            {
                                PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.z, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.z);
                            }

                            PlacerGreen.transform.position = lastSelectedObject.transform.position;

                            if (PlacerGreenOne != null && hitObjectZeroObject != null)
                            {
                                if (!soundPlayed && PlacerGreenOne.activeSelf)
                                {
                                    ModelMovingSound.Play();
                                    soundPlayed = true;
                                }

                                if (hitObjectZeroObject.transform.localScale.x > hitObjectZeroObject.transform.localScale.z)
                                {
                                    PlacerGreenOne.transform.localScale = 1.5f * new Vector3(hitObjectZeroObject.transform.localScale.x, hitObjectZeroObject.transform.localScale.y, hitObjectZeroObject.transform.localScale.x);
                                }
                                else
                                {
                                    PlacerGreenOne.transform.localScale = 1.5f * new Vector3(hitObjectZeroObject.transform.localScale.z, hitObjectZeroObject.transform.localScale.y, hitObjectZeroObject.transform.localScale.z);
                                }

                                PlacerGreenOne.transform.position = hitObjectZeroObject.transform.position;
                            }
                        }
                    }

                    if (touchZero.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Ended)
                    {
                        if (touchOne.phase == TouchPhase.Ended && hitObjectZeroObject != null)
                        {
                            lastSelectedObject = hitObjectZeroObject.transform.GetComponent<PlacementObject>();
                        }

                        soundPlayed = false;

                        hitObjectZeroObject = null;

                        PlacerGreen.SetActive(false);
                        Destroy(PlacerGreenOne);
                    }
                }

                else
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);
                    Ray rayZero = arCamera.ScreenPointToRay(touchZero.position);
                    Ray rayOne = arCamera.ScreenPointToRay(touchOne.position);
                    RaycastHit hitObjectZero = new RaycastHit();
                    RaycastHit hitObjectOne = new RaycastHit();

                    if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                    {
                        if (Physics.Raycast(rayZero, out hitObjectZero))
                        {
                            modelTouched = false;
                            hitObjectZeroIsNull = false;

                            hitObjectZeroObject = hitObjectZero.collider.gameObject;

                            foreach (PlacementObject modelInScene in modelsInScene)
                            {
                                if (hitObjectZero.collider.gameObject == modelInScene.gameObject)
                                {
                                    if (lastSelectedObject == null)
                                    {
                                        ModelSelectingSound.Play();

                                        if (modelChanged)
                                        {
                                            modelChanged = false;
                                        }
                                    }
                                    else
                                    {
                                        if (hitObjectZero.collider.gameObject != lastSelectedObject.gameObject)
                                        {
                                            ModelSelectingSound.Play();
                                        }
                                    }

                                    modelTouched = true;
                                    break;
                                }
                            }

                            if (modelTouched)
                            {
                                lastSelectedObject = hitObjectZero.transform.GetComponent<PlacementObject>();
                                if (lastSelectedObject != null)
                                {
                                    foreach (PlacementObject selectedObject in modelsInScene)
                                    {
                                        selectedObject.Selected = selectedObject == lastSelectedObject;
                                    }
                                }

                                if (lastSelectedObject.transform.localScale.x > lastSelectedObject.transform.localScale.z)
                                {
                                    PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.x, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.x);
                                }
                                else
                                {
                                    PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.z, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.z);
                                }

                                PlacerGreen.transform.position = lastSelectedObject.transform.position;

                                PlacerRed.SetActive(false);
                                PlacerGreen.SetActive(true);
                            }
                        }

                        if (Physics.Raycast(rayOne, out hitObjectOne))
                        {
                            modelTouched = false;

                            foreach (PlacementObject modelInScene in modelsInScene)
                            {
                                if (hitObjectOne.collider.gameObject == modelInScene.gameObject)
                                {
                                    if (lastSelectedObject == null)
                                    {
                                        ModelSelectingSound.Play();

                                        if (modelChanged)
                                        {
                                            modelChanged = false;
                                        }
                                    }
                                    else
                                    {
                                        if (hitObjectOne.collider.gameObject != lastSelectedObject.gameObject && !hitObjectZeroIsNull)
                                        {
                                            hitObjectZeroIsNull = true;

                                            PlacerGreenOne = Instantiate(PlacerGreen, hitObjectZero.collider.gameObject.transform.position, PlacerGreen.transform.rotation);

                                            if (hitObjectZero.collider.gameObject.transform.localScale.x > hitObjectZero.collider.gameObject.transform.localScale.z)
                                            {
                                                PlacerGreenOne.transform.localScale = 1.5f * new Vector3(hitObjectZero.collider.gameObject.transform.localScale.x, hitObjectZero.collider.gameObject.transform.localScale.y, hitObjectZero.collider.gameObject.transform.localScale.x);
                                            }
                                            else
                                            {
                                                PlacerGreenOne.transform.localScale = 1.5f * new Vector3(hitObjectZero.collider.gameObject.transform.localScale.z, hitObjectZero.collider.gameObject.transform.localScale.y, hitObjectZero.collider.gameObject.transform.localScale.z);
                                            }

                                            ModelSelectingSound.Play();
                                        }
                                    }

                                    modelTouched = true;
                                    break;
                                }
                            }

                            if (modelTouched)
                            {
                                lastSelectedObject = hitObjectOne.transform.GetComponent<PlacementObject>();

                                if (lastSelectedObject != null)
                                {
                                    foreach (PlacementObject selectedObject in modelsInScene)
                                    {
                                        selectedObject.Selected = selectedObject == lastSelectedObject;
                                    }
                                }

                                if (lastSelectedObject.transform.localScale.x > lastSelectedObject.transform.localScale.z)
                                {
                                    PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.x, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.x);
                                }
                                else
                                {
                                    PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.z, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.z);
                                }

                                PlacerGreen.transform.position = lastSelectedObject.transform.position;

                                PlacerRed.SetActive(false);
                                PlacerGreen.SetActive(true);
                            }
                        }
                    }

                    if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
                    {
                        if (lastSelectedObject != null)
                        {
                            if (!soundPlayed && PlacerGreen.activeSelf)
                            {
                                ModelMovingSound.Play();
                                soundPlayed = true;
                            }

                            if (lastSelectedObject.transform.localScale.x > lastSelectedObject.transform.localScale.z)
                            {
                                PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.x, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.x);
                            }
                            else
                            {
                                PlacerGreen.transform.localScale = 1.5f * new Vector3(lastSelectedObject.transform.localScale.z, lastSelectedObject.transform.localScale.y, lastSelectedObject.transform.localScale.z);
                            }

                            PlacerGreen.transform.position = lastSelectedObject.transform.position;

                            if (PlacerGreenOne != null && hitObjectZeroObject != null)
                            {
                                if (!soundPlayed && PlacerGreenOne.activeSelf)
                                {
                                    ModelMovingSound.Play();
                                    soundPlayed = true;
                                }

                                if (hitObjectZeroObject.gameObject.transform.localScale.x > hitObjectZeroObject.gameObject.transform.localScale.z)
                                {
                                    PlacerGreenOne.transform.localScale = 1.5f * new Vector3(hitObjectZeroObject.gameObject.transform.localScale.x, hitObjectZeroObject.gameObject.transform.localScale.y, hitObjectZeroObject.gameObject.transform.localScale.x);
                                }
                                else
                                {
                                    PlacerGreenOne.transform.localScale = 1.5f * new Vector3(hitObjectZeroObject.gameObject.transform.localScale.z, hitObjectZeroObject.gameObject.transform.localScale.y, hitObjectZeroObject.gameObject.transform.localScale.z);
                                }

                                PlacerGreenOne.transform.position = hitObjectZeroObject.transform.position;
                            }
                        }
                    }

                    if (touchZero.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Ended)
                    {
                        soundPlayed = false;

                        hitObjectZeroObject = null;

                        PlacerGreen.SetActive(false);
                        Destroy(PlacerGreenOne);
                    }
                }
            }
        }
    }


    /////////////////////////////////////MAIN BUTTONS/////////////////////////////////////

    private void ShowHome()
    {
        if (ZhkImageSlider.GetComponent<ImageSwiper>().panelMoved)
        {
            return;
        }

        if (ZhkDetailsPanel.activeSelf)
        {
            ZhkDetailsHide();
        }

        if (ZhkImageInfoPanel.activeSelf)
        {
            HideInfo();
        }

        if (ZhkPhotoAlbumPanel.activeSelf)
        {
            HideZhkPhotoAlbum();
        }

        if (zhkModelsArray.Length > 1)
        {
            ZhkImageLeftRightImages.SetActive(true);
        }

        ZhkNamePanel.GetComponentInChildren<Text>().alignment = TextAnchor.LowerLeft;
        ZhkNamePanel.GetComponentInChildren<Text>().rectTransform.anchoredPosition = ZhkNameTextPosition;

        Image3DMaketOn = false;

        Destroy(Image3DMaket);

        mainCamera.gameObject.SetActive(false);
        arCamera.gameObject.SetActive(true);

        RotateMaketButton.gameObject.SetActive(false);
        MoveMaketButton.gameObject.SetActive(false);

        ZhkImageSlider.SetActive(true);
        ZhkImageNavigationPanel.SetActive(true);

        BackToMenuButton.gameObject.SetActive(true);

        Show3DButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/3dIcon");
        HomeButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/homeIconColored");
    }

    private void Show3D()
    {
        if (ZhkImageSlider.GetComponent<ImageSwiper>().panelMoved)
        {
            return;
        }

        if (Image3DMaketOn)
        {
            return;
        }

        if (ZhkDetailsPanel.activeSelf)
        {
            ZhkDetailsHide();
        }

        if (ZhkImageInfoPanel.activeSelf)
        {
            HideInfo();
        }

        if (ZhkPhotoAlbumPanel.activeSelf)
        {
            HideZhkPhotoAlbum();
        }

        Image3DMaketOn = true;

        arCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);


        placedPrefab = zhkModelsArray[ZhkImageSlider.GetComponent<ImageSwiper>().currentPage - 1];

        Image3DMaket = Instantiate(placedPrefab, Image3DMaketPosition.transform);
        Image3DMaket.transform.position = Image3DMaketPosition.transform.position;
        Image3DMaket.transform.rotation = Image3DMaketPosition.transform.rotation;
        Image3DMaket.transform.GetComponent<Lean.Touch.LeanDragTranslate>().enabled = false;


        Vector3 Image3DMaketInitialScale = Image3DMaket.transform.localScale;
        Vector3 v3ViewPort = new Vector3(0, 0, 2);
        Vector3 v3BottomLeft = Camera.main.ViewportToWorldPoint(v3ViewPort);
        v3ViewPort.Set(1, 1, 2);
        Vector3 v3TopRight = Camera.main.ViewportToWorldPoint(v3ViewPort);

        float Image3DMaketInitialScaleX = v3TopRight.x - v3BottomLeft.x;
        Image3DMaketInitialScaleX -= Image3DMaketInitialScaleX / 10;
        float Image3DMaketScaleXRatio = Image3DMaketInitialScaleX / Image3DMaketInitialScale.x;

        Image3DMaket.transform.localScale = new Vector3(Image3DMaketInitialScaleX, Image3DMaketInitialScale.y * Image3DMaketScaleXRatio, Image3DMaketInitialScale.z * Image3DMaketScaleXRatio);
        Image3DMaketFormatedScale = Image3DMaket.transform.localScale;

        randomWallMaterialIndex = random.Next(0, wallMaterials.Length);
        randomFloorMaterialIndex = random.Next(0, floorMaterials.Length);

        if (rulerOn)
        {
            ToggleRuler();
        }
        else
        {
            SetShellMaterial();
            ChangeWallsMaterial(randomWallMaterialIndex);
            ChangeFloorsMaterial(randomFloorMaterialIndex);
        }

        if (WallsTransparencyOn)
        {
            ToggleWalls();
        }
        else
        {
            SetShellMaterial();
            ChangeWallsMaterial(randomWallMaterialIndex);
        }

        if (!furnitureOn)
        {
            OnOffFurniture();
        }

        ZhkNamePanel.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
        ZhkNamePanel.GetComponentInChildren<Text>().rectTransform.anchoredPosition = new Vector2(0, -9);

        MoveMaketButton.gameObject.SetActive(true);

        ZhkImageLeftRightImages.SetActive(false);

        ZhkImageSlider.SetActive(false);
        ZhkImageNavigationPanel.SetActive(false);

        phoneInHand.SetActive(false);
        phoneInHandText.SetActive(false);
        fingerTap.SetActive(false);
        fingerTapText.SetActive(false);

        PlacerGreen.SetActive(false);
        PlacerRed.SetActive(false);

        ToggleToolsButton.gameObject.SetActive(true);
        BackToMenuButton.gameObject.SetActive(false);

        aRPlaneManager.enabled = false;
        foreach (ARPlane plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }

        HomeButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/homeIcon");
        Show3DButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/3dIconColored");
    }

    private void Tour3D()
    {
        if (ZhkImageSlider.GetComponent<ImageSwiper>().panelMoved)
        {
            return;
        }

        if (Image3DMaketOn)
        {
            Image3DMaketWasOn = true;

            ShowHome();
        }

        if (ZhkDetailsPanel.activeSelf)
        {
            ZhkDetailsHide();
        }

        if (ZhkImageInfoPanel.activeSelf)
        {
            HideInfo();
        }

        if (ZhkPhotoAlbumPanel.activeSelf)
        {
            HideZhkPhotoAlbum();
        }

        placedPrefab = zhkModelsArray[ZhkImageSlider.GetComponent<ImageSwiper>().currentPage - 1];


        modelYPosition = 0;

        foreach (PlacementObject model in modelsInScene)
        {
            model.gameObject.SetActive(false);
        }

        lastSelectedObject = Instantiate(placedPrefab,
                    new Vector3(arCamera.transform.position.x, modelYPosition, arCamera.transform.position.z),
                    placedPrefab.transform.rotation).GetComponent<PlacementObject>();

        SkyboxCube.transform.position = new Vector3(arCamera.transform.position.x, modelYPosition, arCamera.transform.position.z);

        if (scannerEnabled)
        {
            ARPlane lowestPlane = new ARPlane();

            if (aRPlaneManager.trackables.count != 0)
            {
                foreach (ARPlane plane in aRPlaneManager.trackables)
                {
                    lowestPlane = plane;
                }
            }
            else
            {
                lowestPlane = ARPlaneCustomSample;
            }

            lowestPlane.transform.position = new Vector3(lowestPlane.transform.position.x, arCamera.transform.position.y - 1.5f, lowestPlane.transform.position.z);

            lastSelectedObject.plane = lowestPlane;

            modelYPosition = lowestPlane.transform.position.y;
        }

        aRPlaneManager.enabled = false;

        foreach (ARPlane plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }

        lastSelectedObject.transform.localScale = lastSelectedObject.transform.localScale * 10;

        cameraCollider.enabled = true;

        ModelOpenningSound.Play();

        modelIsOpen = true;

        if (!rotateCameraShowedOnce)
        {
            rotateCameraShowedOnce = true;
            rotateCamera.SetActive(true);
            rotateCameraText.SetActive(true);
        }

        if (AdditionalPanel.activeSelf)
        {
            ToggleAdditionalPanel();
        }

        randomWallMaterialIndex = random.Next(0, wallMaterials.Length);
        randomFloorMaterialIndex = random.Next(0, floorMaterials.Length);

        if (rulerOn)
        {
            ToggleRuler();
        }
        else
        {
            SetShellMaterial();
            ChangeWallsMaterial(randomWallMaterialIndex);
            ChangeFloorsMaterial(randomFloorMaterialIndex);
        }

        if (WallsTransparencyOn)
        {
            ToggleWalls();
        }
        else
        {
            SetShellMaterial();
            ChangeWallsMaterial(randomWallMaterialIndex);
        }

        if (!furnitureOn)
        {
            OnOffFurniture();
        }

        StartCoroutine(EnableMusic(1));
        if (!MusicMuted)
        {
            ChangeMusicButton.gameObject.transform.parent.gameObject.SetActive(true);
        }

        ZhkNamePanel.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
        ZhkNamePanel.GetComponentInChildren<Text>().rectTransform.anchoredPosition = new Vector2(0, -9);

        tour3D = true;
        ZhkImageSlider.SetActive(false);
        ZhkImageLeftRightImages.SetActive(false);
        ZhkImageNavigationPanel.SetActive(false);
        ZhkNamePanel.SetActive(false);
        MapPointerArrow.SetActive(true);
        NavigationPanelOpenedModel.gameObject.SetActive(true);
        SwitchCamerasButton.gameObject.SetActive(true);
        NavigationPanelMain.SetActive(false);
        ZhkDetailsButton.gameObject.SetActive(false);
        BackToMenuButton.gameObject.SetActive(false);
        ToggleToolsButton.gameObject.SetActive(true);
        CentraliseModelButton.gameObject.transform.parent.gameObject.SetActive(true);
        ToggleMuteMusicButton.gameObject.transform.parent.gameObject.SetActive(true);
        topCamera.gameObject.SetActive(true);
        PressToSelect.SetActive(false);
        AdditionalPanelButton.gameObject.SetActive(false);
        Joystick.gameObject.SetActive(true);
        JoystickHandleInitialPosition = Joystick.transform.position;
        SkyboxCube.SetActive(true);
    }

    private void ShowMaketsPage() 
    {
        MaketsPageOpen = true;

        if (ZhkImageSlider.GetComponent<ImageSwiper>().panelMoved)
        {
            return;
        }

        if (Image3DMaketOn)
        {
            Image3DMaketWasOn = true;

            ShowHome();
        }

        if (ZhkDetailsPanel.activeSelf)
        {
            ZhkDetailsHide();
        }

        if (ZhkImageInfoPanel.activeSelf)
        {
            HideInfo();
        }

        if (ZhkPhotoAlbumPanel.activeSelf)
        {
            HideZhkPhotoAlbum();
        }

        if (rulerOn)
        {
            ToggleRuler();
        }

        if (WallsTransparencyOn)
        {
            ToggleWalls();
        }

        if (!furnitureOn)
        {
            OnOffFurniture();
        }


        ToggleToolsButton.gameObject.SetActive(true);


        ChangeModel(ZhkImageSlider.GetComponent<ImageSwiper>().currentPage - 1);
        ToggleModelPanel();

        if (scannerEnabled)
        {
            aRPlaneManager.enabled = true;
            foreach (ARPlane plane in aRPlaneManager.trackables)
            {
                plane.gameObject.SetActive(true);
            }
        }


        foreach (PlacementObject model in modelsInScene)
        {
            model.gameObject.SetActive(true);
        }

        ZhkNamePanel.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
        ZhkNamePanel.GetComponentInChildren<Text>().rectTransform.anchoredPosition = new Vector2(0, -9);

        RoomLightsOkButtonPressed = false;
        ZhkImageSlider.SetActive(false);
        ZhkImageLeftRightImages.SetActive(false);
        ZhkImageNavigationPanel.SetActive(false);
        NavigationPanelMain.SetActive(false);
        ZhkDetailsButton.gameObject.SetActive(false);
        NavigationPanel.SetActive(true);
        AdditionalPanelButton.gameObject.SetActive(true);
        BackToMenuButton.gameObject.SetActive(false);
        BackToImagesButton.gameObject.SetActive(true);
        ZhkPhotoAlbumButton.gameObject.SetActive(false);
        ModelsPanel.SetActive(false);
    }

    private void ShowInfo()
    {
        if (ZhkImageSlider.GetComponent<ImageSwiper>().panelMoved)
        {
            return;
        }

        ZhkImageInfoPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text =
        ZhkImageInfoPanelTextsArray[ZhkImageSlider.GetComponent<ImageSwiper>().currentPage - 1].text;

        if (Image3DMaketOn)
        {
            ShowHome();

            MoveMaketButton.gameObject.SetActive(false);
            RotateMaketButton.gameObject.SetActive(false);
        }

        if (ZhkDetailsPanel.activeSelf)
        {
            ZhkDetailsHide();
        }

        if (ZhkPhotoAlbumPanel.activeSelf)
        {
            HideZhkPhotoAlbum();
        }

        ZhkNamePanel.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
        ZhkNamePanel.GetComponentInChildren<Text>().rectTransform.anchoredPosition = new Vector2(0, -9);

        Show3DButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/3dIcon");
        HomeButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/homeIcon");
        ShowInfoButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/infoImageColored");

        ZhkImageSlider.SetActive(false);
        ZhkImageInfoPanel.SetActive(true);

        BackToMenuButton.gameObject.SetActive(false);

        StartCoroutine(ResizePanels(0.01f));
    }

    private void HideInfo()
    {
        ZhkImageInfoPanel.SetActive(false);

        ZhkImageInfoPanel.GetComponent<ScrollRect>().content.transform.localPosition = ZhkImageInfoPanelContentInitialPosition.transform.localPosition;

        if (Image3DMaketOn)
        {
            MoveMaketButton.gameObject.SetActive(true);
            Show3DButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/3dIconColored");
        }
        else
        {
            HomeButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/homeIconColored");
        }

        ShowInfoButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/infoImage");
    }

    private void ZhkDetails()
    {
        if (ZhkImageSlider.GetComponent<ImageSwiper>().panelMoved)
        {
            return;
        }

        if (Image3DMaketOn)
        {
            ShowHome();

            MoveMaketButton.gameObject.SetActive(false);
            RotateMaketButton.gameObject.SetActive(false);
        }

        if (ZhkImageInfoPanel.activeSelf)
        {
            HideInfo();
        }

        if (ZhkPhotoAlbumPanel.activeSelf)
        {
            HideZhkPhotoAlbum();
        }

        ZhkNamePanel.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
        ZhkNamePanel.GetComponentInChildren<Text>().rectTransform.anchoredPosition = new Vector2(0, -9);

        Show3DButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/3dIcon");
        HomeButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/homeIcon");
        ZhkDetailsButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/homeInfoIconColored");

        ZhkImageSlider.SetActive(false);
        ZhkDetailsPanel.SetActive(true);

        BackToMenuButton.gameObject.SetActive(false);

        StartCoroutine(ResizePanels(0.01f));
    }

    private void ZhkDetailsHide()
    {
        ZhkDetailsPanel.SetActive(false);

        ZhkDetailsPanel.GetComponent<ScrollRect>().content.transform.localPosition = ZhkDetailsPanelContentInitialPosition.transform.localPosition;

        if (Image3DMaketOn)
        {
            MoveMaketButton.gameObject.SetActive(true);
            Show3DButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/3dIconColored");
        }
        else
        {
            HomeButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/homeIconColored");
        }

        ZhkDetailsButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/homeInfoIcon");
    }

    private void ShowZhkPhotoAlbum()
    {
        if (ZhkImageSlider.GetComponent<ImageSwiper>().panelMoved)
        {
            return;
        }

        if (Image3DMaketOn)
        {
            ShowHome();

            MoveMaketButton.gameObject.SetActive(false);
            RotateMaketButton.gameObject.SetActive(false);
        }

        if (ZhkDetailsPanel.activeSelf)
        {
            ZhkDetailsHide();
        }

        if (ZhkImageInfoPanel.activeSelf)
        {
            HideInfo();
        }

        ZhkNamePanel.GetComponentInChildren<Text>().alignment = TextAnchor.LowerCenter;
        ZhkNamePanel.GetComponentInChildren<Text>().rectTransform.anchoredPosition = new Vector2(0, -9);

        Show3DButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/3dIcon");
        HomeButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/homeIcon");
        ZhkPhotoAlbumButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/galleryIconColored");

        ZhkImageSlider.SetActive(false);
        ZhkPhotoAlbumPanel.SetActive(true);

        BackToMenuButton.gameObject.SetActive(false);
    }

    private void HideZhkPhotoAlbum()
    {
        ZhkPhotoAlbumPanel.SetActive(false);

        ZhkPhotoAlbumPanel.GetComponent<ScrollRect>().content.transform.localPosition = ZhkPhotoAlbumContentSample.transform.localPosition;

        if (Image3DMaketOn)
        {
            MoveMaketButton.gameObject.SetActive(true);
            Show3DButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/3dIconColored");
        }
        else
        {
            HomeButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/homeIconColored");
        }

        ZhkPhotoAlbumButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/galleryIcon");
    }

    private void OpenModel()
    {
        uiButtonPressed = true;

        if (lastSelectedObject == null)
        {
            lastSelectedObject = modelsInScene[modelsInScene.Count - 1];
        }

        modelYPosition = 0;
        //disable all models except selected
        foreach (PlacementObject model in modelsInScene)
        {
            if (model == lastSelectedObject)
            {
                lastOpenedModel = lastSelectedObject;

                lastSelectedObject = Instantiate(lastSelectedObject.gameObject,
                    new Vector3 (arCamera.transform.position.x, modelYPosition, arCamera.transform.position.z),
                    lastSelectedObject.gameObject.transform.rotation).GetComponent<PlacementObject>();
            }

            model.gameObject.SetActive(false);
        }

        SkyboxCube.transform.position = new Vector3(arCamera.transform.position.x, modelYPosition, arCamera.transform.position.z);

        //check and save arPlaneManeger active status
        if (scannerEnabled)
        {
            ARPlane lowestPlane = lastOpenedModel.plane;

            foreach (ARPlane plane in aRPlaneManager.trackables)
            {
                if (lowestPlane.gameObject.transform.position.y > plane.gameObject.transform.position.y)
                {
                    lowestPlane = plane;
                }
            }

            lowestPlaneInitialYPosition = lowestPlane.transform.position.y;

            lowestPlane.transform.position = new Vector3(lowestPlane.transform.position.x, arCamera.transform.position.y - 1.5f, lowestPlane.transform.position.z);

            lastSelectedObject.plane = lowestPlane;

            modelYPosition = lowestPlane.transform.position.y;
        }

        //set arPlaneManager inActive and disable all planes
        aRPlaneManager.enabled = false;

        foreach (ARPlane plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }


        //enlarge selected object
        lastSelectedObject.transform.localScale = lastSelectedObject.initialPrefabScale * 10;


        cameraCollider.enabled = true;

        modelJustOpened = true;

        ModelOpenningSound.Play();

        //change status
        modelIsOpen = true;


        if (PlacerGreen.activeSelf)
        {
            PlacerGreen.SetActive(false);
        }

        if (PlacerGreenOne !=null)
        {
            Destroy(PlacerGreenOne);
        }

        if (PlacerRed.activeSelf)
        {
            PlacerRed.SetActive(false);
        }

        if (!rotateCameraShowedOnce)
        {
            rotateCameraShowedOnce = true;
            rotateCamera.SetActive(true);
            rotateCameraText.SetActive(true);
        }

        if (AdditionalPanel.activeSelf)
        {
            ToggleAdditionalPanel();
        }

        if (WallsTransparencyOn)
        {
            ToggleWalls();
        }

        StartCoroutine(EnableMusic(1));
        if (!MusicMuted)
        {
            ChangeMusicButton.gameObject.transform.parent.gameObject.SetActive(true);
        }

        ZhkImageSlider.SetActive(false);
        ZhkImageNavigationPanel.SetActive(false);
        ZhkNamePanel.SetActive(false);
        MapPointerArrow.SetActive(true);
        OpenModelButton.gameObject.SetActive(false);
        NavigationPanelOpenedModel.gameObject.SetActive(true);
        SwitchCamerasButton.gameObject.SetActive(true);
        NavigationPanel.SetActive(false);
        BackToMenuButton.gameObject.SetActive(false);
        topCamera.gameObject.SetActive(true);
        PressToSelect.SetActive(false);
        AdditionalPanelButton.gameObject.SetActive(false);
        Joystick.gameObject.SetActive(true);
        JoystickHandleInitialPosition = Joystick.transform.position;
        SkyboxCube.SetActive(true);
        CentraliseModelButton.gameObject.transform.parent.gameObject.SetActive(true);
        ToggleMuteMusicButton.gameObject.transform.parent.gameObject.SetActive(true);
    }

    private void CloseModel()
    {
        if (tour3D)
        {
            lastSelectedObject.gameObject.SetActive(false);
            Destroy(lastSelectedObject.gameObject);
            lastSelectedObject = null;

            foreach (PlacementObject model in modelsInScene)
            {
                model.gameObject.SetActive(true);
            }

            cameraCollider.enabled = false;

            arCameraPreveousPosition = Vector3.zero;

            ModelClosingSound.Play();

            //change status
            modelIsOpen = false;

            tour3D = false;
            ZhkImageSlider.SetActive(true);
            ZhkImageNavigationPanel.SetActive(true);
            rotateCamera.SetActive(false);
            rotateCameraText.SetActive(false);
            ZhkNamePanel.SetActive(true);
            MapPointerArrow.SetActive(false);
            NavigationPanelOpenedModel.gameObject.SetActive(false);
            SwitchCamerasButton.gameObject.SetActive(false);
            NavigationPanelMain.SetActive(true);
            ZhkDetailsButton.gameObject.SetActive(true);
            BackToMenuButton.gameObject.SetActive(true);
            SwitchCamerasToNormal();
            topCamera.gameObject.SetActive(false);
            PressToSelect.SetActive(true);
            Joystick.gameObject.SetActive(false);
            SkyboxCube.SetActive(false);
            CentraliseModelButton.gameObject.transform.parent.gameObject.SetActive(false);
            ToggleMuteMusicButton.gameObject.transform.parent.gameObject.SetActive(false);
            ChangeMusicButton.gameObject.transform.parent.gameObject.SetActive(false);

            if (zhkModelsArray.Length > 1)
            {
                ZhkImageLeftRightImages.SetActive(true);
            }

            if (Image3DMaketWasOn)
            {
                Image3DMaketWasOn = false;

                Show3D();
            }
            else
            {
                ZhkNamePanel.GetComponentInChildren<Text>().alignment = TextAnchor.LowerLeft;
                ZhkNamePanel.GetComponentInChildren<Text>().rectTransform.anchoredPosition = ZhkNameTextPosition;
            }

            MusicEnabled = false;
            BackgroundMusic.Stop();

            return;
        }



        uiButtonPressed = true;

        if (scannerEnabled) {
            foreach (ARPlane plane in aRPlaneManager.trackables) {
                plane.gameObject.SetActive(true);
            }

            lastSelectedObject.plane.transform.position = new Vector3(lastSelectedObject.plane.transform.position.x, lowestPlaneInitialYPosition, lastSelectedObject.plane.transform.position.z);
        }

        lastSelectedObject.gameObject.SetActive(false);
        Destroy(lastSelectedObject.gameObject);

        foreach (PlacementObject model in modelsInScene)
        {
            model.gameObject.SetActive(true);
        }

        lastSelectedObject = lastOpenedModel;

        cameraCollider.enabled = false;

        arCameraPreveousPosition = Vector3.zero;

        ModelClosingSound.Play();

        //change status
        modelIsOpen = false;

        rotateCamera.SetActive(false);
        rotateCameraText.SetActive(false);
        ZhkNamePanel.SetActive(true);
        MapPointerArrow.SetActive(false);
        OpenModelButton.gameObject.SetActive(true);
        NavigationPanelOpenedModel.gameObject.SetActive(false);
        SwitchCamerasButton.gameObject.SetActive(false);
        NavigationPanel.SetActive(true);
        SwitchCamerasToNormal();
        topCamera.gameObject.SetActive(false);
        PressToSelect.SetActive(true);
        AdditionalPanelButton.gameObject.SetActive(true);
        Joystick.gameObject.SetActive(false);
        SkyboxCube.SetActive(false);
        CentraliseModelButton.gameObject.transform.parent.gameObject.SetActive(false);
        ToggleMuteMusicButton.gameObject.transform.parent.gameObject.SetActive(false);
        ChangeMusicButton.gameObject.transform.parent.gameObject.SetActive(false);

        MusicEnabled = false;
        BackgroundMusic.Stop();
    }

    private String getBetween(string strSource, string strStart, string strEnd)
    {
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            int Start, End;
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);

            return strSource.Substring(Start, End - Start);
        }

        return "";
    }

    private String RemovePhoneAndSite(String text)
    {
        if (text.Contains("phoneStart"))
        {
            int Start;
            Start = text.IndexOf("phoneStart", 0);

            return text.Remove(Start).TrimEnd();
        }

        return text;
    }

    private IEnumerator ResizePanels(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (ZhkDetailsPanel.activeSelf)
        {
            ZhkDetailsPanel.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta =
                new Vector2(ZhkDetailsPanel.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.x,
                ZhkDetailsPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.y + 32);
        }

        if (ZhkImageInfoPanel.activeSelf)
        {
            ZhkImageInfoPanel.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta =
                new Vector2(ZhkImageInfoPanel.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.x,
                ZhkImageInfoPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta.y + 32);
        }
    }

    private IEnumerator EnableMusic(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (modelIsOpen)
        {
            MusicEnabled = true;
        }
    }


    /////////////////////////////////////ADDITIONAL BUTTONS/////////////////////////////////////

    private void MoveRotateMaket()
    {
        TapSound.Play();

        if (MoveMaketButton.gameObject.activeSelf)
        {
            MoveMaketButton.gameObject.SetActive(false);
            RotateMaketButton.gameObject.SetActive(true);
        }
        else if(RotateMaketButton.gameObject.activeSelf)
        {
            RotateMaketButton.gameObject.SetActive(false);
            MoveMaketButton.gameObject.SetActive(true);
        }
    }

    private void TogglePlaneDetection()
    {
        uiButtonPressed = true;

        scannerEnabled = !scannerEnabled;
        aRPlaneManager.enabled = scannerEnabled;

        if (!scannerEnabled)
        {
            phoneInHand.SetActive(false);
            phoneInHandText.SetActive(false);
        }
        else
        {
            phoneInHandShowedOnce = false;
        }

        placedPrefab.GetComponent<Lean.Touch.LeanDragTranslate>().enabled = !scannerEnabled;

        foreach (PlacementObject modelInScene in modelsInScene)
        {
            modelInScene.GetComponent<Lean.Touch.LeanDragTranslate>().enabled = !scannerEnabled;
        }


        TogglePlaneDetectionButton.GetComponentInChildren<Text>().text = scannerEnabled ? "Отвязать" : "Привязать";

        TapSound.Play();

        foreach (ARPlane plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(scannerEnabled);
        }

        ToggleAdditionalPanel();
    }

    private void ResetScene()
    {
        uiButtonPressed = true;

        RestartSound.Play();

        if (!scannerEnabled)
        {
            scannerEnabled = true;
            aRPlaneManager.enabled = true;

            TogglePlaneDetectionButton.GetComponentInChildren<Text>().text = "Отвязать";

            foreach (ARPlane plane in aRPlaneManager.trackables)
            {
                plane.gameObject.SetActive(true);
            }
        }


        foreach (PlacementObject model in modelsInScene)
        {
            model.gameObject.SetActive(false);
        }

        modelsInScene.Clear();

        arSession.Reset();

        modelChanged = true;
        lastSelectedObject = null;

        OpenModelButton.enabled = false;

        ToggleAdditionalPanel();
    }

    public void ToggleAdditionalPanel()
    {
        uiButtonPressed = true;
        TapSound.Play();

        if (!AdditionalPanel.activeSelf)
        {
            AdditionalPanel.SetActive(true);

            if (ModelsPanel.activeSelf)
            {
                ToggleModelPanel();
            }

            AdditionalPanelButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/AdditionalMenuIconColored");
        }

        else
        {
            AdditionalPanel.SetActive(false);

            AdditionalPanelButton.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/AdditionalMenuIcon");
        }
    }

    public void ToggleModelPanel()
    {
        uiButtonPressed = true;

        if (!ModelsPanel.activeSelf)
        {
            ModelsPanel.SetActive(true);

            OpenModelButton.enabled = false;
            DeleteModelButton.enabled = false;

            if (AdditionalPanel.activeSelf)
            {
                ToggleAdditionalPanel();
            }

            ToggleToolsButton.gameObject.SetActive(false);

            phoneInHand.SetActive(false);
            phoneInHandText.SetActive(false);
            fingerTap.SetActive(false);
            fingerTapText.SetActive(false);

            PlacerGreen.SetActive(false);
            PlacerRed.SetActive(false);

            ShowModelsPanelButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/PlusIconColored");
        }

        else
        {
            ModelsPanel.SetActive(false);

            OpenModelButton.enabled = true;
            DeleteModelButton.enabled = true;

            ToggleToolsButton.gameObject.SetActive(true);

            ShowModelsPanelButton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/PlusIcon");
        }
    }

    public void ChangeModel(int i)
    {
        uiButtonPressed = true;
        modelChanged = true;

        placedPrefab = zhkModelsArray[i];


        lastSelectedObject = null;

        ToggleModelPanel();
    }

    private void DeleteModel()
    {
        uiButtonPressed = true;

        foreach (PlacementObject modelInScene in modelsInScene)
        {
            if (modelInScene.Selected)
            {
                modelInScene.gameObject.SetActive(false);
                modelsInScene.Remove(modelInScene);
                Destroy(modelInScene);
                ModelDeletingSound.Play();

                if (modelsInScene.Count < 1)
                {
                    lastSelectedObject = null;

                    OpenModelButton.gameObject.SetActive(false);
                    DeleteModelButton.gameObject.SetActive(false);
                }
                else
                {
                    lastSelectedObject = modelsInScene[modelsInScene.Count - 1];
                    modelsInScene[modelsInScene.Count - 1].Selected = true;
                }
            }
        }
    }

    private void SwitchCameras()
    {
        ChangeMapViewSound.Play();

        if (normalView)
        {
            arCamera.gameObject.GetComponent<Camera>().enabled = false;

            topCamera.rect = new Rect(0f, 0f, 1f, 1f);
            arCamera.rect = new Rect(0.7f, 0.7f, 1f, 1f);

            arCamera.gameObject.GetComponent<Camera>().enabled = true;

            normalView = false;
        }

        else
        {
            topCamera.gameObject.GetComponent<Camera>().enabled = false;

            arCamera.rect = new Rect(0f, 0f, 1f, 1f);
            topCamera.rect = new Rect(0.7f, 0.7f, 1f, 1f);

            topCamera.gameObject.GetComponent<Camera>().enabled = true;

            normalView = true;
        }
    }

    private void SwitchCamerasToNormal()
    {
        topCamera.gameObject.SetActive(false);

        arCamera.rect = new Rect(0f, 0f, 1f, 1f);
        topCamera.rect = new Rect(0.7f, 0.7f, 1f, 1f);

        topCamera.gameObject.SetActive(true);

        normalView = true;
    }

    private void OpenSite()
    {
        String url = "https://" + siteAddress;

        if (url.Length > 8)
        {
            Application.OpenURL(url);
        }

        StartCoroutine(UpdateSiteClicksNumber());
    }

    private void MakeCall()
    {
        String url = "tel://" + phoneNumber;

        if (url.Length > 6)
        {
            Application.OpenURL(url);
        }

        StartCoroutine(UpdateCallClicksNumber());
    }

    private void OpenPolicy()
    {
        Application.OpenURL("https://www.novostroi3d.kz/index.php/privacy-policy");
    }

    private IEnumerator UpdateSiteClicksNumber()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://novostroi3d.kz/downloads/clicksSite/" + lastUsedZhkName + ".php");

        yield return www.SendWebRequest();
    }

    private IEnumerator UpdateCallClicksNumber()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://novostroi3d.kz/downloads/clicksCall/" + lastUsedZhkName + ".php");

        yield return www.SendWebRequest();
    }


    /////////////////////////////////////MODEL BUTTONS/////////////////////////////////////

    private void ToggleTools()
    {
        uiButtonPressed = true;

        StartCoroutine(ToolsOpenAndClose());
    }

    private void ToggleLights()
    {
        uiButtonPressed = true;
        SwitchLightsSound.Play();

        if (!LightsOn)
        {
            LightsOn = true;

            ToggleLightsButton.GetComponentInChildren<Image>().color = new Color(1, 1, 1);

            if (rulerOn)
            {
                ChangeFurnitureTransparantColor(furnitureBlack);
            }

            mainCamera.GetComponent<Camera>().backgroundColor = new Color32(150, 200 ,255, 0);
            topCamera.GetComponent<Camera>().backgroundColor = new Color32(150, 200, 255, 0);

            SkyboxCube.GetComponent<MeshRenderer>().material.SetFloat("_Exposure", 0.7f);

            RenderSettings.skybox.SetFloat("_Exposure", 1f);
            DynamicGI.UpdateEnvironment();
        }

        else
        {
            LightsOn = false;

            ToggleLightsButton.GetComponentInChildren<Image>().color = new Color(0, 0, 0);

            if (rulerOn)
            {
                ChangeFurnitureTransparantColor(furnitureWhite);
            }

            mainCamera.GetComponent<Camera>().backgroundColor = new Color32(20, 60, 100, 0);
            topCamera.GetComponent<Camera>().backgroundColor = new Color32(20, 60, 100, 0);

            SkyboxCube.GetComponent<MeshRenderer>().material.SetFloat("_Exposure", 0.2f);

            RenderSettings.skybox.SetFloat("_Exposure", 0.4f);
            DynamicGI.UpdateEnvironment();
        }
    }

    private void ChangeFurnitureTransparantColor(Material material)
    {
        if (modelIsOpen)
        {
            for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
            {
                if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                {
                    if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                    {
                        for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                        {
                            if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                            {
                                lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(material);
                            }
                        }
                    }
                }
            }
        }

        else if (Image3DMaketOn)
        {
            for (int childIndex = 0; childIndex < Image3DMaket.transform.childCount; childIndex++)
            {
                if (!(Image3DMaket.transform.GetChild(childIndex) == null))
                {
                    if (Image3DMaket.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                    {
                        for (int i = 0; i < Image3DMaket.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                        {
                            if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                            {
                                Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(material);
                            }
                        }
                    }
                }
            }
        }

        else
        {
            foreach (PlacementObject modelInScene in modelsInScene)
            {
                for (int childIndex = 0; childIndex < modelInScene.gameObject.transform.childCount; childIndex++)
                {
                    if (!(modelInScene.gameObject.transform.GetChild(childIndex) == null))
                    {
                        if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                                {
                                    modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(material);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void ToggleRuler()
    {
        uiButtonPressed = true;

        if (!TapSound.isPlaying && !ModelPlacingSound.isPlaying)
        {
            TapSound.Play();
        }

        if (WallsTransparencyOn)
        {
            ToggleWalls();
        }

        if (rulerOn)
        {
            rulerOn = false;

            ChangeRulerMaterials(furnitureBlack);

            ToggleRulerButton.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Icons/rulerBlack");
        }
        else
        {
            rulerOn = true;

            if (!LightsOn)
            {
                ChangeRulerMaterials(furnitureWhite);
            }
            else
            {
                ChangeRulerMaterials(furnitureBlack);
            }

            ToggleRulerButton.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Icons/rulerWhite");
        }
    }

    private void ChangeRulerMaterials(Material material)
    {
        if (rulerOn)
        {
            if (modelIsOpen)
            {
                for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
                {
                    if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                    {
                        if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall")
                                    || lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell")
                                    || lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Floor"))
                                {
                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.
                                        CopyPropertiesFromMaterial(placedPrefab.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);
                                }

                                else if(lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                                {
                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(material);
                                }
                            }
                        }

                        else if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Scheme"))
                        {
                            lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.SetActive(true);
                        }
                    }
                }
            }

            else if (Image3DMaketOn)
            {
                for (int childIndex = 0; childIndex < Image3DMaket.transform.childCount; childIndex++)
                {
                    if (!(Image3DMaket.transform.GetChild(childIndex) == null))
                    {
                        if (Image3DMaket.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < Image3DMaket.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall")
                                    || Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell")
                                    || Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Floor"))
                                {
                                    Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.
                                        CopyPropertiesFromMaterial(placedPrefab.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);
                                }

                                else if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                                {
                                    Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(material);
                                }
                            }
                        }

                        else if (Image3DMaket.transform.GetChild(childIndex).gameObject.CompareTag("Scheme"))
                        {
                            Image3DMaket.transform.GetChild(childIndex).gameObject.SetActive(true);
                        }
                    }
                }
            }

            else
            {
                foreach (PlacementObject modelInScene in modelsInScene)
                {
                    for (int childIndex = 0; childIndex < modelInScene.gameObject.transform.childCount; childIndex++)
                    {
                        if (modelInScene.gameObject.transform.GetChild(childIndex) != null)
                        {
                            if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                            {
                                for (int i = 0; i < modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                                {
                                    if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall"))
                                    {
                                        modelInScene.lastUsedWallMaterialForRuller.CopyPropertiesFromMaterial(modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);

                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.
                                            CopyPropertiesFromMaterial(modelInScene.initialPrefab.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);
                                    }

                                    else if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                                    {
                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.
                                            CopyPropertiesFromMaterial(modelInScene.initialPrefab.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);
                                    }

                                    else if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Floor"))
                                    {
                                        modelInScene.lastUsedFloorMaterial.CopyPropertiesFromMaterial(modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);

                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.
                                            CopyPropertiesFromMaterial(modelInScene.initialPrefab.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);
                                    }

                                    else if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                                    {
                                        modelInScene.initialPrefab.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.
                                            CopyPropertiesFromMaterial(modelInScene.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);

                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(material);
                                    }
                                }
                            }

                            else if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Scheme"))
                            {
                                modelInScene.gameObject.transform.GetChild(childIndex).gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }

        else
        {
            if (modelIsOpen)
            {
                for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
                {
                    if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                    {
                        if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall"))
                                {
                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[wallMaterialIndex]);
                                }
                                else if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Floor"))
                                {
                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(floorMaterials[floorMaterialIndex]);
                                }
                                else if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                                {
                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[0]);
                                }
                                else if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                                {
                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.
                                        CopyPropertiesFromMaterial(placedPrefab.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);
                                }
                            }
                        }

                        else if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Scheme"))
                        {
                            lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.SetActive(false);
                        }
                    }
                }
            }

            else if (Image3DMaketOn)
            {
                for (int childIndex = 0; childIndex < Image3DMaket.transform.childCount; childIndex++)
                {
                    if (!(Image3DMaket.transform.GetChild(childIndex) == null))
                    {
                        if (Image3DMaket.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < Image3DMaket.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall"))
                                {
                                    Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[wallMaterialIndex]);
                                }
                                else if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Floor"))
                                {
                                    Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(floorMaterials[floorMaterialIndex]);
                                }
                                else if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                                {
                                    Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[0]);
                                }
                                else if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                                {
                                    Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.
                                        CopyPropertiesFromMaterial(placedPrefab.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);
                                }
                            }
                        }

                        else if (Image3DMaket.transform.GetChild(childIndex).gameObject.CompareTag("Scheme"))
                        {
                            Image3DMaket.transform.GetChild(childIndex).gameObject.SetActive(false);
                        }
                    }
                }
            }

            else
            {
                foreach (PlacementObject modelInScene in modelsInScene)
                {
                    for (int childIndex = 0; childIndex < modelInScene.gameObject.transform.childCount; childIndex++)
                    {
                        if (!(modelInScene.gameObject.transform.GetChild(childIndex) == null))
                        {
                            if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                            {
                                for (int i = 0; i < modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                                {
                                    if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall"))
                                    {
                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(modelInScene.lastUsedWallMaterialForRuller);
                                    }
                                    else if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Floor"))
                                    {
                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(modelInScene.lastUsedFloorMaterial);
                                    }
                                    else if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                                    {
                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[0]);
                                    }
                                    else if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                                    {
                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.
                                            CopyPropertiesFromMaterial(modelInScene.initialPrefab.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);
                                    }
                                }
                            }
                            else if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Scheme"))
                            {
                                modelInScene.gameObject.transform.GetChild(childIndex).gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    private void ToggleFurniture()
    {
        uiButtonPressed = true;

        TapSound.Play();

        if (furnitureOn)
        {
            furnitureOn = false;

            ToggleFurnitureButton.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Icons/FurnitureBlack");

            OnOffFurniture();
        }
        else
        {
            furnitureOn = true;

            ToggleFurnitureButton.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Icons/FurnitureWhite");

            OnOffFurniture();
        }
    }

    private void OnOffFurniture()
    {
        if (modelIsOpen)
        {
            for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
            {
                if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                {
                    if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                    {
                        for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                        {
                            if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                            {
                                lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.SetActive(furnitureOn);
                            }
                        }
                    }
                }
            }
        }

        else if (Image3DMaketOn)
        {
            for (int childIndex = 0; childIndex < Image3DMaket.transform.childCount; childIndex++)
            {
                if (!(Image3DMaket.transform.GetChild(childIndex) == null))
                {
                    if (Image3DMaket.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                    {
                        for (int i = 0; i < Image3DMaket.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                        {
                            if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                            {
                                Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.SetActive(furnitureOn);
                            }
                        }
                    }
                }
            }
        }

        else
        {
            foreach (PlacementObject modelInScene in modelsInScene)
            {
                for (int childIndex = 0; childIndex < modelInScene.gameObject.transform.childCount; childIndex++)
                {
                    if (!(modelInScene.gameObject.transform.GetChild(childIndex) == null))
                    {
                        if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Furniture"))
                                {
                                    modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.SetActive(furnitureOn);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void ChangeWalls()
    {
        uiButtonPressed = true;

        TapSound.Play();

        wallMaterialIndex++;
        if (wallMaterialIndex >= wallMaterials.Length)
        {
            wallMaterialIndex = 0;
        }

        if (WallsTransparencyOn)
        {
            ToggleWalls();
        }

        ChangeWallsMaterial(wallMaterialIndex);
    }

    private void ChangeWallsMaterial(int wallMaterialIndex)
    {
        if (modelIsOpen)
        {
            for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
            {
                if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                {
                    if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                    {
                        for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                        {
                            if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall"))
                            {
                                lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[wallMaterialIndex]);
                            }
                        }
                    }
                }
            }
        }

        else if (Image3DMaketOn)
        {
            for (int childIndex = 0; childIndex < Image3DMaket.transform.childCount; childIndex++)
            {
                if (!(Image3DMaket.transform.GetChild(childIndex) == null))
                {
                    if (Image3DMaket.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                    {
                        for (int i = 0; i < Image3DMaket.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                        {
                            if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall"))
                            {
                                Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[wallMaterialIndex]);
                            }
                        }
                    }
                }
            }
        }

        else
        {
            if (!(lastSelectedObject == null))
            {
                for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
                {
                    if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                    {
                        if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall"))
                                {
                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[wallMaterialIndex]);

                                    lastSelectedObject.lastUsedWallMaterialForRuller.CopyPropertiesFromMaterial(wallMaterials[wallMaterialIndex]);
                                    lastSelectedObject.lastUsedWallMaterialForWallTrasparency.CopyPropertiesFromMaterial(wallMaterials[wallMaterialIndex]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void ChangeFloors()
    {
        uiButtonPressed = true;

        TapSound.Play();

        floorMaterialIndex++;
        if (floorMaterialIndex >= floorMaterials.Length)
        {
            floorMaterialIndex = 0;
        }

        ChangeFloorsMaterial(floorMaterialIndex);
    }

    private void ChangeFloorsMaterial(int floorMaterialIndex)
    {
        if (modelIsOpen)
        {
            for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
            {
                if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                {
                    if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                    {
                        for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                        {
                            if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Floor"))
                            {
                                lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(floorMaterials[floorMaterialIndex]);
                            }
                        }
                    }
                }
            }
        }

        else if (Image3DMaketOn)
        {
            for (int childIndex = 0; childIndex < Image3DMaket.transform.childCount; childIndex++)
            {
                if (!(Image3DMaket.transform.GetChild(childIndex) == null))
                {
                    if (Image3DMaket.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                    {
                        for (int i = 0; i < Image3DMaket.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                        {
                            if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Floor"))
                            {
                                Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(floorMaterials[floorMaterialIndex]);
                            }
                        }
                    }
                }
            }
        }

        else
        {
            if (lastSelectedObject != null)
            {
                for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
                {
                    if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                    {
                        if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Floor"))
                                {
                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(floorMaterials[floorMaterialIndex]);
                                    lastSelectedObject.lastUsedFloorMaterial.CopyPropertiesFromMaterial(floorMaterials[floorMaterialIndex]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void SetShellMaterial()
    {
        if (modelIsOpen)
        {
            for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
            {
                if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                {
                    if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                    {
                        for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                        {
                            if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                            {
                                lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[0]);
                            }
                        }
                    }
                }
            }
        }

        else if (Image3DMaketOn)
        {
            for (int childIndex = 0; childIndex < Image3DMaket.transform.childCount; childIndex++)
            {
                if (!(Image3DMaket.transform.GetChild(childIndex) == null))
                {
                    if (Image3DMaket.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                    {
                        for (int i = 0; i < Image3DMaket.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                        {
                            if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                            {
                                Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[0]);
                            }
                        }
                    }
                }
            }
        }

        else
        {
            if (lastSelectedObject != null)
            {
                for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
                {
                    if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                    {
                        if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                                {
                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[0]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void ToggleWalls()
    {
        uiButtonPressed = true;

        if (!TapSound.isPlaying && !ModelPlacingSound.isPlaying)
        {
            TapSound.Play();
        }

        if (WallsTransparencyOn)
        {
            WallsTransparencyOn = false;

            ToggleWallsButton.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Icons/WallWhite");

            OnOffWalls();
        }
        else
        {
            WallsTransparencyOn = true;

            ToggleWallsButton.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Icons/WallBlack");

            OnOffWalls();
        }
    }

    private void OnOffWalls()
    {
        if (WallsTransparencyOn)
        {
            if (modelIsOpen)
            {
                for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
                {
                    if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                    {
                        if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall")
                                    || lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                                {
                                    lastSelectedObject.lastUsedWallMaterialForWallTrasparency.CopyPropertiesFromMaterial(lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);

                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallTransparent);
                                }
                            }
                        }
                    }
                }
            }

            else if (Image3DMaketOn)
            {
                for (int childIndex = 0; childIndex < Image3DMaket.transform.childCount; childIndex++)
                {
                    if (!(Image3DMaket.transform.GetChild(childIndex) == null))
                    {
                        if (Image3DMaket.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < Image3DMaket.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall")
                                    || Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                                {
                                    Image3DMaket.GetComponent<PlacementObject>().lastUsedWallMaterialForWallTrasparency.CopyPropertiesFromMaterial(Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);

                                    Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallTransparent);
                                }
                            }
                        }
                    }
                }
            }

            else
            {
                foreach (PlacementObject modelInScene in modelsInScene)
                {
                    for (int childIndex = 0; childIndex < modelInScene.gameObject.transform.childCount; childIndex++)
                    {
                        if (!(modelInScene.gameObject.transform.GetChild(childIndex) == null))
                        {
                            if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                            {
                                for (int i = 0; i < modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                                {
                                    if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall"))
                                    {
                                        modelInScene.lastUsedWallMaterialForWallTrasparency.CopyPropertiesFromMaterial(modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial);

                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallTransparent);
                                    }

                                    else if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                                    {
                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallTransparent);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        else
        {
            if (modelIsOpen)
            {
                for (int childIndex = 0; childIndex < lastSelectedObject.gameObject.transform.childCount; childIndex++)
                {
                    if (!(lastSelectedObject.gameObject.transform.GetChild(childIndex) == null))
                    {
                        if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall"))
                                {
                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(lastSelectedObject.lastUsedWallMaterialForWallTrasparency);
                                }
                                else if (lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                                {
                                    lastSelectedObject.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[0]);
                                }
                            }
                        }
                    }
                }
            }

            else if (Image3DMaketOn)
            {
                for (int childIndex = 0; childIndex < Image3DMaket.transform.childCount; childIndex++)
                {
                    if (!(Image3DMaket.transform.GetChild(childIndex) == null))
                    {
                        if (Image3DMaket.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                        {
                            for (int i = 0; i < Image3DMaket.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                            {
                                if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall"))
                                {
                                    Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(Image3DMaket.GetComponent<PlacementObject>().lastUsedWallMaterialForWallTrasparency);
                                }
                                else if (Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                                {
                                    Image3DMaket.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[0]);
                                }
                            }
                        }
                    }
                }
            }

            else
            {
                foreach (PlacementObject modelInScene in modelsInScene)
                {
                    for (int childIndex = 0; childIndex < modelInScene.gameObject.transform.childCount; childIndex++)
                    {
                        if (!(modelInScene.gameObject.transform.GetChild(childIndex) == null))
                        {
                            if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.CompareTag("Model"))
                            {
                                for (int i = 0; i < modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.childCount; i++)
                                {
                                    if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Wall"))
                                    {
                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(modelInScene.lastUsedWallMaterialForWallTrasparency);
                                    }
                                    else if (modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.CompareTag("Shell"))
                                    {
                                        modelInScene.gameObject.transform.GetChild(childIndex).gameObject.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(wallMaterials[0]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void CentraliseModel()
    {
        lastSelectedObject.transform.position = new Vector3(arCamera.transform.position.x, modelYPosition, arCamera.transform.position.z);

        SkyboxCube.transform.position = new Vector3(arCamera.transform.position.x, modelYPosition, arCamera.transform.position.z);
    }

    private void ToggleMuteMusic()
    {
        if (MusicMuted)
        {
            MusicMuted = false;

            ChangeMusicButton.gameObject.transform.parent.gameObject.SetActive(true);

            ToggleMuteMusicButton.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/MusicPlay");
        }

        else
        {
            MusicMuted = true;
            BackgroundMusic.Stop();

            ChangeMusicButton.gameObject.transform.parent.gameObject.SetActive(false);

            ToggleMuteMusicButton.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Icons/MusicMute");
        }
    }

    private void ChangeMusic()
    {
        soundtrackNumber++;
        if (soundtrackNumber == allSoundtracksNumber)
        {
            soundtrackNumber = 0;
        }

        BackgroundMusic.Stop();
        BackgroundMusic.clip = Resources.Load<AudioClip>("Music/" + soundtrackNumber);
        BackgroundMusic.Play();
    }


    /////////////////////////////////////PAGE TRANSITION BUTTONS/////////////////////////////////////

    public void EnterCompany(String companyName)
    {
        if (ZhksPanels.transform.Find(companyName) != null)
        {
            ZhksPanels.transform.Find(companyName).gameObject.SetActive(true);

            TopPanel.GetComponentInChildren<Text>().text = "Жилые Комплексы";
            ZhksPanels.SetActive(true);
            BackToCompaniesListButton.gameObject.SetActive(true);
            CompaniesPanel.SetActive(false);
        }
    }

    public void EnterZhk(String zhkName, int version)
    {
        zhkIsAlreadyDownloaded = false;

        if (zhkModelsArraysList != null)
        {
            foreach (ZhkModelsArray zhkModelsArrayGameObject in zhkModelsArraysList)
            {
                if (zhkName.Equals(zhkModelsArrayGameObject.getZhkModelsArrayName()))
                {
                    zhkModelsArray = zhkModelsArrayGameObject.getZhkModelsArray();
                    ZhkDetailsPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = zhkModelsArrayGameObject.getZhkDetailsText().text;

                    String sourceText = ZhkDetailsPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;
                    phoneNumber = getBetween(sourceText, "phoneStart[", "]phoneEnd");
                    siteAddress = getBetween(sourceText, "siteStart[", "]siteEnd");
                    ZhkDetailsPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = RemovePhoneAndSite(sourceText);
                    lastUsedZhkName = zhkName;

                    if (phoneNumber.Equals(""))
                    {
                        CallButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        CallButton.gameObject.SetActive(true);
                    }

                    if (siteAddress.Equals(""))
                    {
                        SiteButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        SiteButton.gameObject.SetActive(true);
                    }


                    foreach (Transform content in ModelsPanel.transform)
                    {
                        content.gameObject.SetActive(false);
                    }

                    zhkModelsArrayGameObject.getModelsPanelContent().SetActive(true);
                    ModelsPanel.GetComponent<ScrollRect>().content = zhkModelsArrayGameObject.getModelsPanelContent().GetComponent<RectTransform>();

                    foreach (Transform content in ZhkPhotoAlbumPanel.transform)
                    {
                        content.gameObject.SetActive(false);
                    }

                    zhkModelsArrayGameObject.getZhkPhotoAlbumContent().SetActive(true);
                    ZhkPhotoAlbumPanel.GetComponent<ScrollRect>().content = zhkModelsArrayGameObject.getZhkPhotoAlbumContent().GetComponent<RectTransform>();


                    for (int i = 0; i < ZhkImageSlider.transform.childCount; i++)
                    {
                        ZhkImageSlider.transform.GetChild(i).gameObject.SetActive(false);
                    }


                    ZhkImageSlider.GetComponent<ImageSwiper>().totalPages = zhkModelsArrayGameObject.getZhkImagesArray().Length;
                    for (int i = 0; i < zhkModelsArrayGameObject.getZhkImagesArray().Length; i++)
                    {
                        zhkModelsArrayGameObject.getZhkImagesArray()[i].gameObject.SetActive(true);
                    }

                    ZhkImageInfoPanelTextsArray = zhkModelsArrayGameObject.getZhkImagesInfoTextsArray();

                    if (zhkModelsArrayGameObject.getZhkImagesArray().Length > 1)
                    {
                        LeftSwipeImage.SetActive(false);
                        RightSwipeImage.SetActive(true);

                        ZhkImageLeftRightImages.SetActive(true);
                    }

                    loadingImage.SetActive(false);
                    ZhkImageSlider.SetActive(true);
                    ZhkImageNavigationPanel.SetActive(true);
                    ZhkNamePanel.GetComponentInChildren<Text>().text = zhkName;
                    ZhkNamePanel.SetActive(true);
                    NavigationPanelMain.SetActive(true);
                    ZhkDetailsButton.gameObject.SetActive(true);
                    Menu.SetActive(false);
                    BackToCompaniesListButton.gameObject.SetActive(false);
                    BackToMenuButton.gameObject.SetActive(true);

                    zhkIsAlreadyDownloaded = true;
                    break;
                }
            }
        }

        if (!zhkIsAlreadyDownloaded)
        {
            StartCoroutine(DownloadPromo(zhkName, version));
        }
    }

    private void BackToCompanies()
    {
        TopPanel.GetComponentInChildren<Text>().text = "Список Компаний";
        BackToCompaniesListButton.gameObject.SetActive(false);

        foreach (Transform zhksPanel in ZhksPanels.transform)
        {
            zhksPanel.gameObject.SetActive(false);
        }

        CompaniesPanel.SetActive(true);
    }

    private void BackToMenu()
    {
        if (ZhkImageSlider.GetComponent<ImageSwiper>().panelMoved)
        {
            return;
        }

        if (ZhkDetailsPanel.activeSelf)
        {
            ZhkDetailsHide();
        }

        if (Image3DMaketOn)
        {
            ShowHome();
        }

        if (ZhkImageInfoPanel.activeSelf)
        {
            HideInfo();
        }

        if (ZhkPhotoAlbumPanel.activeSelf)
        {
            HideZhkPhotoAlbum();
        }

        ZhkImageSlider.GetComponent<ImageSwiper>().SetPanelInitialLocation();

        ZhkNamePanel.GetComponentInChildren<Text>().text = "";
        ZhkNamePanel.SetActive(false);

        Menu.SetActive(true);
        ZhkImageLeftRightImages.SetActive(false);
        Show3DButton.gameObject.SetActive(true);
        NavigationPanelMain.SetActive(false);
        ZhkDetailsButton.gameObject.SetActive(false);
        ZhkImageSlider.SetActive(false);
        ZhkImageNavigationPanel.SetActive(false);
        BackToCompaniesListButton.gameObject.SetActive(true);
        BackToMenuButton.gameObject.SetActive(false);
        SwitchCamerasButton.gameObject.SetActive(false);
        ModelsPanel.SetActive(false);
        NavigationPanelOpenedModel.gameObject.SetActive(false);
        NavigationPanel.SetActive(false);
        AdditionalPanelButton.gameObject.SetActive(false);
        phoneInHand.SetActive(false);
        phoneInHandText.SetActive(false);
        fingerTap.SetActive(false);
        fingerTapText.SetActive(false);

        if (AdditionalPanel.activeSelf)
        {
            ToggleAdditionalPanel();
        }
    }

    private void BackToImages()
    {
        MaketsPageOpen = false;

        ZhkImageSlider.SetActive(true);
        ZhkImageNavigationPanel.SetActive(true);
        NavigationPanelMain.SetActive(true);
        ZhkDetailsButton.gameObject.SetActive(true);
        NavigationPanel.SetActive(false);
        AdditionalPanelButton.gameObject.SetActive(false);
        ZhkPhotoAlbumButton.gameObject.SetActive(true);
        BackToMenuButton.gameObject.SetActive(true);
        BackToImagesButton.gameObject.SetActive(false);
        ToggleToolsButton.gameObject.SetActive(false);

        phoneInHand.SetActive(false);
        phoneInHandText.SetActive(false);
        fingerTap.SetActive(false);
        fingerTapText.SetActive(false);

        PlacerGreen.SetActive(false);
        PlacerRed.SetActive(false);

        ModelsPanel.SetActive(false);

        if (zhkModelsArray.Length > 1)
        {
            ZhkImageLeftRightImages.SetActive(true);
        }

        if (AdditionalPanel.activeSelf)
        {
            ToggleAdditionalPanel();
        }

        if (Image3DMaketWasOn)
        {
            Image3DMaketWasOn = false;

            Show3D();
        }
        else
        {
            ZhkNamePanel.GetComponentInChildren<Text>().alignment = TextAnchor.LowerLeft;
            ZhkNamePanel.GetComponentInChildren<Text>().rectTransform.anchoredPosition = ZhkNameTextPosition;
        }

        aRPlaneManager.enabled = false;
        foreach (ARPlane plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }

        foreach (PlacementObject model in modelsInScene)
        {
            model.gameObject.SetActive(false);
        }
    }


    /////////////////////////////////////DOWNLOAD METHODS/////////////////////////////////////

    private IEnumerator DownloadMenu()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            PrivacyPolicyButton.gameObject.SetActive(false);

            url = UnityWebRequestAssetBundle.GetAssetBundle("https://novostroi3d.kz/downloads/ios/menu");
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            url = UnityWebRequestAssetBundle.GetAssetBundle("https://novostroi3d.kz/downloads/android/menu");
        }


        yield return url.SendWebRequest();


        if (url.result != UnityWebRequest.Result.Success)
        {
            StartCoroutine(ConnectionErrorMessageOff(5));
            StartCoroutine(RetryDownload());
        }
        else
        {
            ConnectionErrorMessage.SetActive(false);

            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(url);

            Sprite[] companiesLogosArray = bundle.LoadAllAssets<Sprite>();

            GameObject companySample = CompaniesList.transform.GetChild(0).gameObject;
            GameObject ZhksPanelSample = ZhksPanels.transform.GetChild(0).gameObject;


            if (companiesLogosArray != null)
            {
                RectTransform CompaniesListRectTransform = (RectTransform)CompaniesList.transform;
                CompaniesListRectTransform.sizeDelta = new Vector2(CompaniesListRectTransform.sizeDelta.x, 80 * companiesLogosArray.Length);

                if (companiesLogosArray.Length > 0)
                {
                    foreach (Sprite logo in companiesLogosArray)
                    {
                        GameObject company = Instantiate(companySample);
                        company.transform.SetParent(CompaniesList.transform, false);
                        company.name = logo.name;
                        company.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = logo;
                        company.GetComponent<Button>().onClick.AddListener(delegate { EnterCompany(logo.name); });
                        company.SetActive(true);
                        if (company.name.Contains("Soon"))
                        {
                            company.transform.GetChild(1).gameObject.SetActive(true);
                        }
                        else
                        {
                            GameObject ZhksPanel = Instantiate(ZhksPanelSample);
                            ZhksPanel.transform.SetParent(ZhksPanels.transform, false);
                            ZhksPanel.name = logo.name;

                            GameObject ZhksList = ZhksPanel.transform.GetChild(0).gameObject;
                            GameObject Zhk = ZhksList.transform.GetChild(0).gameObject;

                            TextAsset CompaniesZhksFile = bundle.LoadAsset<TextAsset>(logo.name);
                            String[] zhkNames = CompaniesZhksFile.text.Split('\n');

                            RectTransform ZhksListRectTransform = (RectTransform)ZhksList.transform;
                            ZhksListRectTransform.sizeDelta = new Vector2(ZhksListRectTransform.sizeDelta.x, 60 * zhkNames.Length);

                            for (int i = 0; i < zhkNames.Length; i++)
                            {
                                GameObject createdZhkRow = Instantiate(Zhk);
                                createdZhkRow.transform.SetParent(ZhksList.transform, false);
                                createdZhkRow.GetComponentInChildren<Text>().text = zhkNames[i];
                                createdZhkRow.name = zhkNames[i];
                                createdZhkRow.SetActive(true);

                                if (createdZhkRow.name.Contains("Soon"))
                                {
                                    createdZhkRow.transform.GetChild(1).gameObject.SetActive(true);
                                    createdZhkRow.GetComponentInChildren<Text>().text = createdZhkRow.GetComponentInChildren<Text>().text.Remove(createdZhkRow.GetComponentInChildren<Text>().text.Length - 7);
                                }
                                else if (createdZhkRow.name.Contains("version"))
                                {
                                    int version = int.Parse(getBetween(createdZhkRow.name, "version[", "]version"));
                                    createdZhkRow.GetComponentInChildren<Text>().text = createdZhkRow.GetComponentInChildren<Text>().text.Remove(createdZhkRow.GetComponentInChildren<Text>().text.Length - 20);
                                    createdZhkRow.name = createdZhkRow.GetComponentInChildren<Text>().text;
                                    createdZhkRow.GetComponent<Button>().onClick.AddListener(delegate { EnterZhk(createdZhkRow.name, version); });
                                }
                                else
                                {
                                    createdZhkRow.GetComponent<Button>().onClick.AddListener(delegate { EnterZhk(createdZhkRow.name, 0); });
                                }
                            }
                        }
                    }

                    MenuPlaceholder.SetActive(false);
                }
            }
        }
    }

    private IEnumerator DownloadPromo(String zhkName, int version)
    {
        List<Hash128> listOfCachedVersions = new List<Hash128>();
        Caching.GetCachedVersions(zhkName + "Promo", listOfCachedVersions);

        if (!listOfCachedVersions.Contains(Hash128.Compute(version)))
        {
            loadingImage.SetActive(true);
        }

        promoFinished = false;
        zhkFinishedDownload = false;

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            url = UnityWebRequestAssetBundle.GetAssetBundle("https://novostroi3d.kz/downloads/ios/" + zhkName + "Promo", Hash128.Compute(version), 0);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            url = UnityWebRequestAssetBundle.GetAssetBundle("https://novostroi3d.kz/downloads/android/" + zhkName + "Promo", Hash128.Compute(version), 0);
        }

        yield return url.SendWebRequest();

        if (Caching.ready)
        {
            CachedAssetBundle cachedZhkPromo = new CachedAssetBundle();
            cachedZhkPromo.name = zhkName + "Promo";
            cachedZhkPromo.hash = Hash128.Compute(version);
            Caching.ClearOtherCachedVersions(cachedZhkPromo.name, cachedZhkPromo.hash);
        }


        if (url.result != UnityWebRequest.Result.Success)
        {
            promoFinished = true;
            StartCoroutine(DownloadZhkModels(zhkName, version));
        }
        else
        {
            promoBundle = DownloadHandlerAssetBundle.GetContent(url);

            VideoClip[] zhkPromoVideo = promoBundle.LoadAllAssets<VideoClip>();

            if (zhkPromoVideo != null)
            {
                arCamera.gameObject.SetActive(false);
                promoCamera.gameObject.SetActive(true);

                PromoVideoPlayer.clip = zhkPromoVideo[0];
                PromoVideoPlayer.gameObject.SetActive(true);
                PromoVideoPlayer.Play();

                myCanvas.gameObject.SetActive(false);

                loadingImage.SetActive(false);

                promoFinished = false;
                zhkFinishedDownload = false;

                StartCoroutine(DownloadZhkModels(zhkName, version));
            }
        }
    }

    private IEnumerator DownloadZhkModels(String zhkName, int version)
    {
        loadingImage.SetActive(true);

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            url = UnityWebRequestAssetBundle.GetAssetBundle("https://novostroi3d.kz/downloads/ios/" + zhkName, Hash128.Compute(version), 0);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            url = UnityWebRequestAssetBundle.GetAssetBundle("https://novostroi3d.kz/downloads/android/" + zhkName, Hash128.Compute(version), 0);
        }

        yield return url.SendWebRequest();

        if (Caching.ready)
        {
            CachedAssetBundle cachedZhk = new CachedAssetBundle();
            cachedZhk.name = zhkName;
            cachedZhk.hash = Hash128.Compute(version);
            Caching.ClearOtherCachedVersions(cachedZhk.name, cachedZhk.hash);
        }
        

        if (url.result != UnityWebRequest.Result.Success)
        {
            loadingImage.SetActive(false);

            StartCoroutine(ConnectionErrorMessageOff(5));
        }
        else
        {
            zhkBundle = DownloadHandlerAssetBundle.GetContent(url);

            zhkFinishedDownload = true;

            lastUsedZhkName = zhkName;

            if (promoFinished)
            {
                SetZhkWindows();
            }
        }
    }

    private IEnumerator ConnectionErrorMessageOff(float waitTime)
    {
        ConnectionErrorMessage.SetActive(true);

        yield return new WaitForSeconds(waitTime);

        ConnectionErrorMessage.SetActive(false);
    }

    private IEnumerator ToolsOpenAndClose()
    {
        if (!ToolsPanel.GetComponent<Animation>().isPlaying)
        {
            if (ToolsPanel.activeSelf)
            {
                ToolsPanel.GetComponent<Animation>().Play("toolsClose");
                yield return new WaitForSeconds(1);
                ToolsPanel.SetActive(false);

                ToggleToolsButton.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Icons/ToolsBlack");
            }

            else
            {
                ToolsPanel.SetActive(true);
                ToolsPanel.GetComponent<Animation>().Play("toolsOpen");
                yield return new WaitForSeconds(1);

                ToggleToolsButton.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Icons/ToolsWhite");
            }
        }
    }

    private IEnumerator RetryDownload()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(DownloadMenu());
    }

    private void SetZhkWindows()
    {
        GameObject[] zhkModelsArrayLocal = zhkBundle.LoadAllAssets<GameObject>();

        if (zhkModelsArrayLocal.Length != 0)
        {
            TextAsset zhkDetailsText = new TextAsset();
            if (zhkBundle.LoadAsset<TextAsset>("Details") != null)
            {
                zhkDetailsText = zhkBundle.LoadAsset<TextAsset>("Details");
            }
            else
            {
                zhkDetailsText = new TextAsset(" ");
            }

            ZhkDetailsPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = zhkDetailsText.text;

            String sourceText = ZhkDetailsPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text;
            phoneNumber = getBetween(sourceText, "phoneStart[", "]phoneEnd");
            siteAddress = getBetween(sourceText, "siteStart[", "]siteEnd");
            ZhkDetailsPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = RemovePhoneAndSite(sourceText);

            if (phoneNumber.Equals(""))
            {
                CallButton.gameObject.SetActive(false);
            }
            else
            {
                CallButton.gameObject.SetActive(true);
            }
            if (siteAddress.Equals(""))
            {
                SiteButton.gameObject.SetActive(false);
            }
            else
            {
                SiteButton.gameObject.SetActive(true);
            }


            foreach (Transform content in ModelsPanel.transform)
            {
                content.gameObject.SetActive(false);
            }

            foreach (Transform content in ZhkPhotoAlbumPanel.transform)
            {
                content.gameObject.SetActive(false);
            }

            ModelsPanelContent = Instantiate(ModelsPanelContentSample);
            ModelsPanelContent.transform.SetParent(ModelsPanel.transform, false);
            RectTransform ModelsPanelContentRectTransform = (RectTransform) ModelsPanelContent.transform;
            ModelsPanelContentRectTransform.sizeDelta = new Vector2(ModelsPanelContentRectTransform.sizeDelta.x, 0);
            ModelsPanelContent.SetActive(true);
            ModelsPanel.GetComponent<ScrollRect>().content = ModelsPanelContent.GetComponent<RectTransform>();

            ZhkPhotoAlbumContent = Instantiate(ZhkPhotoAlbumContentSample);
            ZhkPhotoAlbumContent.transform.SetParent(ZhkPhotoAlbumPanel.transform, false);
            RectTransform ZhkPhotoAlbumContentRectTransform = (RectTransform) ZhkPhotoAlbumContent.transform;
            ZhkPhotoAlbumContentRectTransform.sizeDelta = new Vector2(ZhkPhotoAlbumContentRectTransform.sizeDelta.x, 0);
            ZhkPhotoAlbumContent.SetActive(true);
            ZhkPhotoAlbumPanel.GetComponent<ScrollRect>().content = ZhkPhotoAlbumContent.GetComponent<RectTransform>();


            GameObject ModelsPanelContentRoundImageSample = ModelsPanelContent.transform.GetChild(0).gameObject;
            GameObject ZhkPhotoAlbumContentRoundImageSample = ZhkPhotoAlbumContent.transform.GetChild(0).gameObject;


            Sprite[] zhkAllImagesAndPhotos = zhkBundle.LoadAllAssets<Sprite>();

            if (zhkAllImagesAndPhotos != null)
            {
                float photoPosition = 0;

                foreach (Sprite photo in zhkAllImagesAndPhotos)
                {
                    if (photo.name.Contains("Photo"))
                    {
                        GameObject ZhkPhotoAlbumContentRoundImage = Instantiate(ZhkPhotoAlbumContentRoundImageSample);
                        ZhkPhotoAlbumContentRoundImage.transform.SetParent(ZhkPhotoAlbumContent.transform, false);

                        float photoRatio = photo.rect.height / photo.rect.width;
                        RectTransform imageRectTransform = (RectTransform) ZhkPhotoAlbumContentRoundImage.transform;
                        imageRectTransform.sizeDelta = new Vector2(Screen.width / myCanvas.scaleFactor, (Screen.width / myCanvas.scaleFactor) * photoRatio);

                        ZhkPhotoAlbumContentRoundImage.transform.localPosition = new Vector3(ZhkPhotoAlbumContentRoundImage.transform.localPosition.x, photoPosition - imageRectTransform.sizeDelta.y / 2);
                        photoPosition -= imageRectTransform.sizeDelta.y;

                        GameObject ZhkPhotoAlbumContentRoundImageItem = ZhkPhotoAlbumContentRoundImage.transform.GetChild(0).gameObject;
                        ZhkPhotoAlbumContentRoundImageItem.GetComponent<Image>().sprite = photo;

                        ZhkPhotoAlbumContentRoundImage.SetActive(true);

                        ZhkPhotoAlbumContentRectTransform.sizeDelta = new Vector2(ZhkPhotoAlbumContentRectTransform.sizeDelta.x, ZhkPhotoAlbumContentRectTransform.sizeDelta.y + imageRectTransform.sizeDelta.y);
                    }
                }
            }

            ModelsPanelContentRoundImageButtons = new GameObject[zhkModelsArrayLocal.Length];
            GameObject[] zhkImagesArray = new GameObject[zhkModelsArrayLocal.Length];
            TextAsset[] ZhkImageInfoPanelTextsArrayLocal = new TextAsset[zhkModelsArrayLocal.Length];

            for (int i = 0; i < ZhkImageSlider.transform.childCount; i++)
            {
                ZhkImageSlider.transform.GetChild(i).gameObject.SetActive(false);
            }

            ZhkImageSlider.GetComponent<ImageSwiper>().totalPages = zhkModelsArrayLocal.Length;

            float imagePosition = 0;

            for (int i = 0; i < zhkModelsArrayLocal.Length; i++)
            {
                GameObject ZhkImagePanel = Instantiate(ZhkImagePanelSample);
                ZhkImagePanel.transform.SetParent(ZhkImageSlider.transform, false);
                ZhkImagePanel.transform.position = new Vector3(Screen.width * i + ZhkImagePanel.transform.position.x, ZhkImagePanel.transform.position.y, ZhkImagePanel.transform.position.z);
                ZhkImagePanel.GetComponent<Image>().sprite = zhkBundle.LoadAsset<Sprite>(zhkModelsArrayLocal[i].name + "image");
                ZhkImagePanel.SetActive(true);
                zhkImagesArray[i] = ZhkImagePanel;


                TextAsset zhkImageInfoPanelText = new TextAsset();

                if (zhkBundle.LoadAsset<TextAsset>(zhkModelsArrayLocal[i].name + "info") != null)
                {
                    zhkImageInfoPanelText = zhkBundle.LoadAsset<TextAsset>(zhkModelsArrayLocal[i].name + "info");
                }
                else
                {
                    zhkImageInfoPanelText = new TextAsset(" ");
                }

                ZhkImageInfoPanelTextsArrayLocal[i] = zhkImageInfoPanelText;


                GameObject ModelsPanelContentRoundImage = Instantiate(ModelsPanelContentRoundImageSample);

                GameObject ModelsPanelContentRoundImageButton = ModelsPanelContentRoundImage.transform.GetChild(0).gameObject;
                Sprite photo = zhkBundle.LoadAsset<Sprite>(zhkModelsArrayLocal[i].name + "image");
                ModelsPanelContentRoundImageButton.GetComponent<Image>().sprite = photo;

                float photoRatio = photo.rect.height / photo.rect.width;

                ModelsPanelContentRoundImageButtons[i] = ModelsPanelContentRoundImageButton;
                int modelsIndex = i;
                ModelsPanelContentRoundImageButtons[i].GetComponent<Button>().onClick.AddListener(delegate { ChangeModel(modelsIndex); });

                ModelsPanelContentRoundImage.transform.SetParent(ModelsPanelContent.transform, false);

                RectTransform imageRectTransform = (RectTransform)ModelsPanelContentRoundImage.transform;
                imageRectTransform.sizeDelta = new Vector2(imageRectTransform.sizeDelta.x, imageRectTransform.sizeDelta.x * photoRatio);
                ModelsPanelContentRoundImage.transform.localPosition = new Vector3(ModelsPanelContentRoundImage.transform.localPosition.x, imagePosition - imageRectTransform.sizeDelta.y / 2);
                imagePosition -= imageRectTransform.sizeDelta.y;
                ModelsPanelContentRoundImage.SetActive(true);

                ModelsPanelContentRectTransform.sizeDelta = new Vector2(ModelsPanelContentRectTransform.sizeDelta.x, ModelsPanelContentRectTransform.sizeDelta.y + imageRectTransform.sizeDelta.y);

                ModelsPanelContentRoundImage.SetActive(true);
            }

            ZhkImageSlider.GetComponent<ImageSwiper>().SetImagesInitialPositions();
            if (zhkModelsArrayLocal.Length == 1)
            {
                ZhkImageLeftRightImages.SetActive(false);
            }
            else if (zhkModelsArrayLocal.Length > 1)
            {
                LeftSwipeImage.SetActive(false);
                RightSwipeImage.SetActive(true);

                ZhkImageLeftRightImages.SetActive(true);
            }


            ZhkImagePanelSample.SetActive(false);
            Destroy(ModelsPanelContentRoundImageSample);
            Destroy(ZhkPhotoAlbumContentRoundImageSample);

            placedPrefab = zhkModelsArrayLocal[0];


            zhkModelsArray = zhkModelsArrayLocal;
            ZhkImageInfoPanelTextsArray = ZhkImageInfoPanelTextsArrayLocal;


            ZhkModelsArray zhksModelsObject = new ZhkModelsArray();
            zhksModelsObject.setZhkModelsArray(zhkModelsArrayLocal);
            zhksModelsObject.setModelsPanelContent(ModelsPanelContent);
            zhksModelsObject.setZhkModelsArrayName(lastUsedZhkName);
            zhksModelsObject.setZhkImagesArray(zhkImagesArray);
            zhksModelsObject.setZhkDetailsText(zhkDetailsText);
            zhksModelsObject.setZhkImagesInfoTextsArray(ZhkImageInfoPanelTextsArrayLocal);
            zhksModelsObject.setZhkPhotoAlbumContent(ZhkPhotoAlbumContent);

            zhkModelsArraysList.Add(zhksModelsObject);
        }

        loadingImage.SetActive(false);
        ZhkImageSlider.SetActive(true);
        ZhkImageNavigationPanel.SetActive(true);
        ZhkNamePanel.GetComponentInChildren<Text>().text = lastUsedZhkName;
        ZhkNamePanel.SetActive(true);
        NavigationPanelMain.SetActive(true);
        ZhkDetailsButton.gameObject.SetActive(true);
        Menu.SetActive(false);
        BackToCompaniesListButton.gameObject.SetActive(false);
        BackToMenuButton.gameObject.SetActive(true);
    }

    private void ShowCanvas(VideoPlayer videoPlayer)
    {
        promoBundle.Unload(false);

        promoCamera.gameObject.SetActive(false);
        arCamera.gameObject.SetActive(true);

        myCanvas.gameObject.SetActive(true);
        PromoVideoPlayer.gameObject.SetActive(false);

        promoFinished = true;

        if (zhkFinishedDownload)
        {
            SetZhkWindows();
        }
    }

    private void skipVideo()
    {
        ShowCanvas(PromoVideoPlayer);
    }


    /////////////////////////////////////ROOM LIGHT ESTIMATION/////////////////////////////////////

    private void OnEnable()
    {
        aRCameraManager.frameReceived += FrameUpdated;
    }

    private void OnDisable()
    {
        aRCameraManager.frameReceived -= FrameUpdated;
    }

    private void FrameUpdated(ARCameraFrameEventArgs args)
    {
        if (MaketsPageOpen && !RoomLightsOkButtonPressed)
        {
            if (args.lightEstimation.averageBrightness.HasValue)
            {
                if (args.lightEstimation.averageBrightness.Value > 0.3f)
                {
                    RoomLightsOffPanel.SetActive(false);
                }

                else
                {
                    RoomLightsOffPanel.SetActive(true);
                }
            }
        }
    }

    private void RoomLightsOffPanelOk()
    {
        RoomLightsOkButtonPressed = true;

        RoomLightsOffPanel.SetActive(false);
    }
}