using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject Ball;
    [SerializeField] private GameObject Balls;
    [SerializeField] private GameObject FirstBallSprite;
    [SerializeField] private GameObject TopWall;
    [SerializeField] private GameObject GameOverTrigger;
    [SerializeField] private TextMeshProUGUI CurrentBallCountText;
    [SerializeField] private Slider Slider;
    [SerializeField] private Button ResetBallsButton;

    public static bool shooting;
    public static int totalBallCount;
    public static TextMeshProUGUI CurrentBallCountTextStatic;
    public static GameObject FirstBallSpriteStatic;

    private static List<GameObject> ballInstancesList = new();

    private bool sliderIsPressed;
    private static bool stopShooting;
    public static bool shooterRotationUp;
    private float force = 800;
    private float angle;
    private float angleMin = 10;
    private float angleMax = 170;
    private int currentBallCount;


    private void Start()
    {
        shooting = false;
        sliderIsPressed = false;
        shooterRotationUp = false;

        FirstBallSpriteStatic = FirstBallSprite;
        CurrentBallCountTextStatic = CurrentBallCountText;

        totalBallCount = (int)Mathf.Floor(Mathf.Sqrt(800 + 10 * Levels.level));
        CurrentBallCountTextStatic.text = totalBallCount + "x";

        TopWall.GetComponent<BoxCollider2D>().size = TopWall.GetComponent<RectTransform>().rect.size;
        TopWall.GetComponent<BoxCollider2D>().offset = new Vector2(0, -TopWall.GetComponent<RectTransform>().rect.size.y / 2);

        transform.parent.localScale = transform.parent.localScale / 2.16f * Screen.height / Screen.width;
        transform.parent.localPosition = new Vector2(0, 0.16f * StartUI.canvasHeight);

        Slider.transform.parent.DOMove(transform.GetChild(2).position, 1f);


        if (ResetBallsButton != null)
        {
            ResetBallsButton.transform.localScale = ResetBallsButton.transform.localScale / 2.16f * Screen.height / Screen.width;
            ResetBallsButton.onClick.AddListener(ResetBalls);
        }
    }

    private void FixedUpdate()
    {
        if (!shooterRotationUp && Balls.transform.childCount == 0 && !sliderIsPressed && !Hooker.isHooking)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(90, Vector3.forward), 100 * Time.deltaTime);
            Slider.value = 180 - transform.rotation.eulerAngles.z;

            if (Quaternion.Angle(transform.rotation, Quaternion.AngleAxis(90, Vector3.forward)) < 0.01f)
            {
                ResetBallsButton.interactable = false;
                shooterRotationUp = true;
            }
        }

        if (((Input.GetMouseButton(0) || sliderIsPressed) && !GameManager.hookEnabled))
        {
            if (!LevelStart.touched || Input.GetMouseButton(0) && !sliderIsPressed && (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > TopWall.transform.position.y || Camera.main.ScreenToWorldPoint(Input.mousePosition).y < GameOverTrigger.transform.position.y))
            {
                return;
            }

            if (sliderIsPressed)
            {
                angle = 180 - Slider.value;
            }
            else
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 dir = Input.mousePosition - pos;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            }

            if (angle > angleMin && angle < angleMax)
            {
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                if (!sliderIsPressed)
                {
                    Slider.value = 180 - angle;
                }
            }
            else
            {
                if (angle <= angleMin && angle >= -90 && !Input.GetMouseButtonDown(0))
                {
                    transform.rotation = Quaternion.AngleAxis(angleMin, Vector3.forward);

                    if (!sliderIsPressed)
                    {
                        Slider.value = 180 - angle;
                    }
                }
                else if ((angleMax <= angle || (angle <= -90 && angle >= -180)) && !Input.GetMouseButtonDown(0))
                {
                    transform.rotation = Quaternion.AngleAxis(angleMax, Vector3.forward);

                    if (!sliderIsPressed)
                    {
                        if (angleMax <= angle)
                        {
                            Slider.value = 180 - angle;
                        }
                        else if (angle <= -90 && angle >= -180)
                        {
                            Slider.value = 0;
                        }
                    }
                }
            }


            if (!shooting)
            {
                shooting = true;
                StartCoroutine(ShootBall());
            }
        }


        if (!Input.GetMouseButton(0))
        {
            stopShooting = true;
        }
    }


    private IEnumerator ShootBall()
    {
        currentBallCount = totalBallCount;

        if (currentBallCount == 0)
        {
            shooting = false;
            FirstBallSpriteStatic.SetActive(false);
        }

        else
        {
            stopShooting = false;

            for (int i = 0; i < currentBallCount; i++)
            {
                if (stopShooting)
                {
                    break;
                }

                yield return new WaitForFixedUpdate();

                GameObject ballInstance = Instantiate(Ball, transform.position, Quaternion.identity, Balls.transform);
                ballInstancesList.Add(ballInstance);

                if (GameManager.powerBalls)
                {
                    ballInstance.GetComponent<Ball>().ballPower = 4;
                    ballInstance.GetComponent<Image>().color = Color.blue;
                }

                ballInstance.GetComponent<Rigidbody2D>().AddForce(transform.right * force);

                totalBallCount--;
                CurrentBallCountTextStatic.text = totalBallCount + "x";

                if (!ResetBallsButton.interactable)
                {
                    ResetBallsButton.interactable = true;
                    shooterRotationUp = false;
                }

                if (totalBallCount <= 0)
                {
                    FirstBallSpriteStatic.SetActive(false);
                }
            }

            shooting = false;
        }
    }

    public static void ResetBalls()
    {
        if (Sound.SoundEnabled)
        {
            Sound.Tap.Play();
        }

        stopShooting = true;
        shooting = false;

        foreach (GameObject ballInstance in ballInstancesList)
        {
            if (ballInstance != null)
            {
                ballInstance.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                ballInstance.GetComponent<Ball>().returnBall = true;
            }
        }

        ballInstancesList.Clear();
    }

    public void SetSliderIsPressed(bool isPressed)
    {
        sliderIsPressed = isPressed;
    }
}
