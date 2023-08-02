using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.FPS.Game
{
    public class GameFlowManager : MonoBehaviour
    {
        [Header("Parameters")]
        [Tooltip("Duration of the fade-to-black at the end of the game")]
        public float EndSceneLoadDelay = 3f;
        [Tooltip("Duration of the fade-to-black when death")]
        public float RestartSceneLoadDelay = 7f;

        [Tooltip("The canvas group of the fade-to-black screen")]
        public CanvasGroup EndGameFadeCanvasGroup;

        [Header("Win")]
        [Tooltip("This string has to be the name of the scene you want to load when winning")]
        public string WinSceneName = "WinScene";

        [Tooltip("Duration of delay before the fade-to-black, if winning")]
        public float DelayBeforeFadeToBlack = 2f;

        [Tooltip("Win game message")]
        public string WinGameMessage;
        [Tooltip("Restart game message")]
        public string RestartGameMessage;
        [Tooltip("Duration of delay before the win message")]
        public float DelayBeforeWinMessage = 2f;

        [Tooltip("Sound played on win")] public AudioClip VictorySound;

        [Header("Lose")]
        [Tooltip("This string has to be the name of the scene you want to load when losing")]
        public string LoseSceneName = "LoseScene";

        [Header("TimeLimit")]
        [Tooltip("The time limit for losing the game. Unit: Sec")]
        public float TimeLimit = 300;
        public bool GameIsEnding { get; private set; }

        float m_TimeLoadEndGameScene;
        string m_SceneToLoad;

        static float timeBefore = 0f;
        private float timer;
        static float totalTime = 0f;
        static int deathCount;

        private bool gameStarted = false;

        void Awake()
        {
            EventManager.AddListener<AllObjectivesCompletedEvent>(OnAllObjectivesCompleted);
            EventManager.AddListener<PlayerDeathEvent>(OnPlayerDeath);
            GameIsEnding = false;
        }

        void Start()
        {
            AudioUtility.SetMasterVolume(1);
        }
       
        void Update()
        {
            if (!gameStarted)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartGame();
                }
            }
            else
            {
                if (GameIsEnding)
                {
                    float timeRatio = 1 - (m_TimeLoadEndGameScene - Time.time) / EndSceneLoadDelay;
                    EndGameFadeCanvasGroup.alpha = timeRatio;

                    AudioUtility.SetMasterVolume(1 - timeRatio);

                    // See if it's time to load the end scene (after the delay)
                    if (Time.time >= m_TimeLoadEndGameScene)
                    {
                        SceneManager.LoadScene(m_SceneToLoad);
                        GameIsEnding = false;
                    }
                }
                else
                {
                    timer += Time.deltaTime;
                    totalTime = timer + timeBefore;
                    if (totalTime > TimeLimit)
                    {
                        EndGame(false, true);// Set GameIsEnding to true
                    }
                }
            }
        }


        void StartGame()
        {
            gameStarted = true;
            EndGameFadeCanvasGroup.gameObject.SetActive(false); 
            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false; 
        }

        public float GetTimer()
        {
            return TimeLimit - totalTime;
        }

        public static float GetTotalTime()
        {
            return totalTime;
        }
        public static int GetDeathCount()
        {
            return deathCount;
        }

        void OnAllObjectivesCompleted(AllObjectivesCompletedEvent evt) => EndGame(true, false);
        void OnPlayerDeath(PlayerDeathEvent evt) => EndGame(false, false);

        void EndGame(bool win, bool timeOut)
        {  
            EndGameFadeCanvasGroup.gameObject.SetActive(true);
            m_TimeLoadEndGameScene = Time.time + EndSceneLoadDelay + (win ? DelayBeforeFadeToBlack : 0);

            if (win || timeOut)
            {
                // unlocks the cursor before leaving the scene, to be able to click buttons
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                GameIsEnding = true;
                // Remember that we need to load the appropriate end scene after a delay
                m_SceneToLoad = win ? WinSceneName : LoseSceneName;
                m_TimeLoadEndGameScene = Time.time + EndSceneLoadDelay + (win ? DelayBeforeFadeToBlack : 0);

                if (win)
                {
                    // play a sound on win
                    var audioSource = gameObject.AddComponent<AudioSource>();
                    audioSource.clip = VictorySound;
                    audioSource.playOnAwake = false;
                    audioSource.outputAudioMixerGroup = AudioUtility.GetAudioGroup(AudioUtility.AudioGroups.HUDVictory);
                    audioSource.PlayScheduled(AudioSettings.dspTime + DelayBeforeWinMessage);

                    DisplayMessageEvent displayMessage = Events.DisplayMessageEvent;
                    displayMessage.Message = WinGameMessage;
                    displayMessage.DelayBeforeDisplay = DelayBeforeWinMessage;
                    EventManager.Broadcast(displayMessage);
                }
            }
            else
            {
                timeBefore += timer;
                deathCount++;
                GameIsEnding = true;
                m_SceneToLoad = SceneManager.GetActiveScene().name;
                m_TimeLoadEndGameScene = Time.time + EndSceneLoadDelay;

                DisplayMessageEvent displayMessage = Events.DisplayMessageEvent;
                displayMessage.Message = RestartGameMessage;
                EventManager.Broadcast(displayMessage);
            }
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<AllObjectivesCompletedEvent>(OnAllObjectivesCompleted);
            EventManager.RemoveListener<PlayerDeathEvent>(OnPlayerDeath);
        }
    }
}