using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Balls;
    [SerializeField] private GameObject FreezeBackground;
    [SerializeField] private GameObject LevelStartPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject LevelWinPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject GameOverTrigger;
    [SerializeField] private GameObject HouseRed;
    [SerializeField] private GameObject HouseBlue;
    [SerializeField] private GameObject SliderParent;
    [SerializeField] private GameObject CoinTop;
    [SerializeField] private GameObject RubyTop;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI CreaturesLeftText;
    [SerializeField] private TextMeshProUGUI RubysText;
    [SerializeField] private TextMeshProUGUI CoinsText;
    [SerializeField] private TextMeshProUGUI OperationMadeText;
    [SerializeField] private Button RetryButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button RubysButton;
    [SerializeField] private Button CoinsButton;
    [SerializeField] private Button FreezeTimeButton;
    [SerializeField] private Button StopCountingButton;
    [SerializeField] private Button PowerBallsButton;
    [SerializeField] private Slider Slider;
    [SerializeField] private Transform BottomPanel;
    [SerializeField] private Transform Scene;
    [SerializeField] private Transform SliderPosition;

    public static GameObject CoinTopStatic;
    public static GameObject RubyTopStatic;
    public static List<GameObject> psPlayingBotsList = new();
    public static int redCreaturesLeft;
    public static bool levelWin;
    public static bool redHouseAttacked;
    public static bool stopCounting;
    public static bool freezing;
    public static bool powerBalls;
    public static bool hookEnabled;
    public static bool adsInitialized = false;
    public static int allBotBluesTotalLifes;
    public static int allBotRedsTotalLifes;
    public static int freezingTime;
    public static Vector2 houseRedPosition;
    public static Vector2 houseBluePosition;
    public static TextMeshProUGUI OperationMadeTextStatic;
    public static TextMeshProUGUI RubysTextStatic;
    public static TextMeshProUGUI CoinsTextStatic;

    private static float sliderNewValue;
    private static int totalCreaturesAndLifes;

    
    private void Start()
    {
        redHouseAttacked = false;
        stopCounting = false;
        freezing = false;
        powerBalls = false;
        levelWin = false;
        hookEnabled = true;
        freezingTime = 5;
        CreaturesLeftText.text = redCreaturesLeft + "";
        LevelText.text = "Level - " + Levels.level;
        houseRedPosition = HouseRed.transform.position;
        houseBluePosition = HouseBlue.transform.position;
        HouseRed.GetComponent<BoxCollider2D>().size = new Vector2(0.74f * HouseRed.GetComponent<RectTransform>().rect.size.y * HouseRed.GetComponent<Image>().sprite.rect.width / HouseRed.GetComponent<Image>().sprite.rect.height, HouseRed.GetComponent<RectTransform>().rect.size.y);
        HouseBlue.GetComponent<BoxCollider2D>().size = new Vector2(0.74f * HouseBlue.GetComponent<RectTransform>().rect.size.y * HouseBlue.GetComponent<Image>().sprite.rect.width / HouseBlue.GetComponent<Image>().sprite.rect.height, HouseBlue.GetComponent<RectTransform>().rect.size.y);
        psPlayingBotsList.Clear();

        OperationMadeTextStatic = OperationMadeText;
        CoinTopStatic = CoinTop;
        RubyTopStatic = RubyTop;

        UpdateSliderValue();
        Slider.value = sliderNewValue;

        RubysText.text = PlayerPrefs.GetInt("Rubys", 0).ToString();
        CoinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        RubysTextStatic = RubysText;
        CoinsTextStatic = CoinsText;


        if (StartUI.adsOn && Creatures.levelRow > 2)
        {
            if (Creatures.levelRow > 20)
            {
                StartCoroutine(DisplayBannerWithDelay());
            }

            if (StartUI.gamePlayed % StartUI.adsInterval == 0)
            {
                AdsManager.Instance.interstitialAds.ShowInterstitialAd();

                StartUI.gamePlayed = 0;
                StartUI.adsInterval--;

                if (StartUI.adsInterval < 2)
                {
                    StartUI.adsInterval = 5;
                }
            }

            StartUI.gamePlayed++;
        }


        if (RetryButton != null)
        {
            RetryButton.onClick.AddListener(RetryGame);
        }

        if (PauseButton != null)
        {
            PauseButton.onClick.AddListener(PauseGame);
        }

        if (RubysButton != null)
        {
            RubysButton.onClick.AddListener(OpenShop);
        }

        if (CoinsButton != null)
        {
            CoinsButton.onClick.AddListener(OpenShop);
        }

        if (FreezeTimeButton != null)
        {
            FreezeTimeButton.transform.parent.localScale = FreezeTimeButton.transform.parent.localScale / 2.16f * Screen.height / Screen.width;
            FreezeTimeButton.onClick.AddListener(() => StartCoroutine(FreezeTime()));
        }

        if (StopCountingButton != null)
        {
            StopCountingButton.transform.parent.localScale = StopCountingButton.transform.parent.localScale / 2.16f * Screen.height / Screen.width;
            StopCountingButton.onClick.AddListener(() => StartCoroutine(StopCounting()));
        }

        if (PowerBallsButton != null)
        {
            PowerBallsButton.transform.parent.localScale = PowerBallsButton.transform.parent.localScale / 2.16f * Screen.height / Screen.width;
            PowerBallsButton.onClick.AddListener(() => StartCoroutine(PowerBalls()));
        }
    }

    private void FixedUpdate()
    {
        Slider.value = Mathf.MoveTowards(Slider.value, sliderNewValue, 200 * Time.deltaTime);
    }


    public void UpdateUI(int number, string sign, bool tooClose)
    {
        if (!stopCounting)
        {
            if (!tooClose)
            {
                if (sign.Equals("-"))
                {
                    redCreaturesLeft -= number;
                }
                else if(sign.Equals("+"))
                {
                    redCreaturesLeft += number;
                }
                else if (sign.Equals("/"))
                {
                    redCreaturesLeft /= number;
                }
                else if (sign.Equals("x"))
                {
                    redCreaturesLeft *= number;
                }
                else if (sign.Equals("√x"))
                {
                    if (redCreaturesLeft > 0)
                    {
                        redCreaturesLeft = (int)Mathf.Floor(Mathf.Sqrt(redCreaturesLeft));
                    }
                    //else if (redCreaturesLeft < 0 && Creatures.levelRow > 36)
                    //{
                    //    redCreaturesLeft *= redCreaturesLeft;
                    //    redCreaturesLeft = -Mathf.Abs(redCreaturesLeft);
                    //}
                }
                else if (sign.Equals("x²"))
                {
                    redCreaturesLeft *= redCreaturesLeft;
                    redCreaturesLeft = Mathf.Abs(redCreaturesLeft);
                }


                for (int i = 5; i > 1; i--)
                {
                    if (redCreaturesLeft >= (int)Mathf.Pow(10, i))
                    {
                        if (Creatures.botRedLevel < i)
                        {
                            Creatures.botRedLevel = i;
                            break;
                        }
                    }
                }

                if (redCreaturesLeft > 100000)
                {
                    redCreaturesLeft = 100000;
                }
                else if (redCreaturesLeft < -100000)
                {
                    redCreaturesLeft = -100000;
                }

                CreaturesLeftText.text = redCreaturesLeft + "";


                if (redCreaturesLeft < 0 && Creatures.levelRow > 16)
                {
                    Creatures.movePortals = true;
                }
            }
        }
    }

    public static void UpdateSliderValue()
    {
        totalCreaturesAndLifes = 0;

        allBotBluesTotalLifes = 0;
        allBotRedsTotalLifes = 0;

        foreach (GameObject botBlue in Creatures.botBlueList)
        {
            allBotBluesTotalLifes += botBlue.GetComponent<BotBlue>().currentLife;
        }

        foreach (GameObject botRed in Creatures.botRedList)
        {
            allBotRedsTotalLifes += botRed.GetComponent<BotRed>().currentLife;
        }

        totalCreaturesAndLifes = allBotBluesTotalLifes + allBotRedsTotalLifes;

        if (Creatures.blueCreaturesTotal > 0)
        {
            totalCreaturesAndLifes += Creatures.blueCreaturesTotal;
        }

        if (redCreaturesLeft > 0)
        {
            totalCreaturesAndLifes += redCreaturesLeft;
        }

        if (totalCreaturesAndLifes != 0)
        {
            sliderNewValue = (Creatures.blueCreaturesTotal + allBotBluesTotalLifes) * 1000 / totalCreaturesAndLifes;
        }
    }

    public void GameOver()
    {
        GameOverTrigger.GetComponent<GameOver>().ShowGameOverPanel();
    }

    public void LevelWin()
    {
        if (!levelWin)
        {
            levelWin = true;
            LevelWinPanel.GetComponent<LevelWin>().ShowLevelWinPanel();
        }
    }

    public void UpdateCreaturesLeftText()
    {
        CreaturesLeftText.text = redCreaturesLeft + "";
    }

    private void RetryGame()
    {
        if (Sound.SoundEnabled)
        {
            Sound.Tap.Play();
        }

        SceneManager.LoadScene("Main");
    }

    private void PauseGame()
    {
        if (Sound.SoundEnabled)
        {
            Sound.Tap.Play();
        }

        PausePanel.SetActive(true);
    }

    private void OpenShop()
    {
        if (Sound.SoundEnabled)
        {
            Sound.Tap.Play();
        }

        PauseGame();

        StartUI.ShopOpenedIngame = true;
        StartUI.ShowStartUI();
        StartUI.ShopPanelStatic.SetActive(true);
    }

    private IEnumerator StopCounting()
    {
        if (PlayerPrefs.GetInt("Rubys", 0) >= 20)
        {
            if (Sound.SoundEnabled)
            {
                Sound.Tap.Play();
            }

            stopCounting = true;
            StopCountingButton.interactable = false;

            foreach (GameObject creature in Creatures.creaturesBodiesList)
            {
                if (creature != null)
                {
                    creature.transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            OperationMadeText.text = "";

            PlayerPrefs.SetInt("Rubys", PlayerPrefs.GetInt("Rubys", 0) - 20);
            PlayerPrefs.Save(); 
            RubysText.text = PlayerPrefs.GetInt("Rubys", 0).ToString();

            for (int i = 5; i > 0; i--)
            {
                StopCountingButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
                StopCountingButton.GetComponent<Animation>().Play();

                yield return new WaitForSeconds(1f);
            }

            StopCountingButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

            stopCounting = false;

            foreach (GameObject creature in Creatures.creaturesBodiesList)
            {
                if (creature != null)
                {
                    creature.transform.GetChild(0).gameObject.SetActive(true);
                }
            }

            StopCountingButton.interactable = true;
        }
        else
        {
            OpenShop();
        }
    }

    private IEnumerator FreezeTime()
    {
        if (PlayerPrefs.GetInt("Rubys", 0) >= 20)
        {
            if (Sound.SoundEnabled)
            {
                Sound.Tap.Play();
            }

            freezing = true;
            FreezeBackground.SetActive(true);
            FreezeTimeButton.interactable = false;
            FreezeTimeButton.GetComponent<Animation>().Play();

            PlayerPrefs.SetInt("Rubys", PlayerPrefs.GetInt("Rubys", 0) - 20);
            PlayerPrefs.Save();
            RubysText.text = PlayerPrefs.GetInt("Rubys", 0).ToString();

            for (int i = freezingTime; i > 0; i--)
            {
                FreezeTimeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
                yield return new WaitForSeconds(1f);
            }

            FreezeTimeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

            freezing = false;
            FreezeBackground.SetActive(false);
            FreezeTimeButton.interactable = true;
        }
        else
        {
            OpenShop();
        }
    }

    private IEnumerator PowerBalls()
    {
        if (PlayerPrefs.GetInt("Rubys", 0) >= 20)
        {
            if (Sound.SoundEnabled)
            {
                Sound.Tap.Play();
            }

            powerBalls = true;
            PowerBallsButton.interactable = false;
            Shooter.FirstBallSpriteStatic.GetComponent<Image>().color = Color.blue;
            PowerBallsButton.GetComponent<Animation>().Play("PowerBallTurnBlue");

            foreach (Transform ball in Balls.transform)
            {
                ball.GetComponent<Ball>().ballPower = 4;
                ball.GetComponent<Image>().color = Color.blue;
            }

            PlayerPrefs.SetInt("Rubys", PlayerPrefs.GetInt("Rubys", 0) - 20);
            PlayerPrefs.Save();
            RubysText.text = PlayerPrefs.GetInt("Rubys", 0).ToString();

            yield return new WaitForSeconds(0.6f);

            for (int i = 10; i > 0; i--)
            {
                PowerBallsButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
                yield return new WaitForSeconds(1f);
            }

            PowerBallsButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

            powerBalls = false;
            PowerBallsButton.interactable = true;
            Shooter.FirstBallSpriteStatic.GetComponent<Image>().color = Color.red;
            PowerBallsButton.GetComponent<Animation>().Play("PowerBallTurnRed");

            foreach (Transform ball in Balls.transform)
            {
                ball.GetComponent<Ball>().ballPower = 1;
                ball.GetComponent<Image>().color = Color.red;
            }
        }
        else
        {
            OpenShop();
        }
    }

    private static IEnumerator DisplayBannerWithDelay()
    {
        yield return new WaitForSeconds(5f);
        AdsManager.Instance.bannerAds.ShowBannerAd();
    }

    private void OnApplicationPause(bool appInBackground)
    {
        if (appInBackground)
        {
            if (!GameOverPanel.activeSelf && !LevelWinPanel.activeSelf && !LevelStartPanel.activeSelf && !PausePanel.activeSelf)
            {
                PauseGame();
            }
        }
    }
}
