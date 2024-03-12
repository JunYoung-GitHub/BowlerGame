using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class GameManager : MonoBehaviour {



    PlayerController playerController;

    private Pin[] _currentPins = new Pin[0];
    private Ball _currentBall;

    //For reseting pins
    [SerializeField] private Transform _pinsetSpawnPosition;
    [SerializeField] private GameObject _pintSetPrefab;

    private bool _throwStarted;
    private int _throwNumber;

    //References to the UI
    [Header("UI Texts Fields")]
    [SerializeField] private TMP_Text _frameNumber;
    [SerializeField] private TMP_Text _firstThrowScore;
    [SerializeField] private TMP_Text _secondThrowScore;
    [SerializeField] private TMP_Text _frameTotalScore;

    [SerializeField] AudioSource audioSource;
    private int _currentFrameScore;
    private int _currentThrowScore;
    private int _currentFrame;
    private int _totalScore;
    private float remainingTimeout;
    private float throwTimeout = 10f;

    // Start is called before the first frame update
    void Start() {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        Invoke(nameof(SetupFrame), 1);
    }

    // Update is called once per frame
    void Update() {
        if (!_throwStarted || !playerController.wasBallThrown)
            return;

        remainingTimeout -= Time.deltaTime;
        if(remainingTimeout <= 0 || CheckIfPiecesAreStatic())
            FinishThrow();
    }
    //Updates the UI in the game 
    public void UpdateScoreUI() {
        if (_throwNumber == 0) {

            if (_currentFrameScore == 10) {
                _secondThrowScore.text = "X";
            }

            else
                _firstThrowScore.text = _currentFrameScore.ToString();
        }
        else
            _secondThrowScore.text = _currentFrameScore == 10 ? "/" : _currentThrowScore.ToString();
    }

    private void ResetScoreUI() {
        _frameNumber.text = _currentFrame.ToString();
        _firstThrowScore.text = "";
        _secondThrowScore.text = "";
        _frameTotalScore.text = _totalScore.ToString();
    }
    //called when each pin is hit and pin head comes in contact with floor called from Pin script when ball comes in contact with pin
    public void PinKnockedDown() {
        //Increase score when pins are knocked down
        _currentFrameScore++;
        _currentThrowScore++;
        Debug.Log("Current Score is: " + _currentFrameScore);
    }

    //Called when ball gets into the pit
    public void BallKnockedDown() {
        _currentBall = null;
    }

    //Reference ball that is thrown 
    public void BallThrown(Ball ball) {
        _currentBall = ball;
    }

    //checks for pin movement
    private bool CheckIfPiecesAreStatic() {
        foreach(var pin in _currentPins) {
            if(pin != null && pin.DidPinMove()) {
                return false;
            }
        }
        //check for ball (boolean)
        var ballStatus = _currentBall == null || !_currentBall.DidBallMove();
        return ballStatus;
    }

    //Used to setup frame/pin formation
    private void SetupFrame() {
        _throwNumber  = 0;
        DisposeLastFrame();
        Instantiate(_pintSetPrefab, _pinsetSpawnPosition.position, _pinsetSpawnPosition.rotation);
        _currentPins = FindObjectsOfType<Pin>();

        _currentFrame++;
        ResetScoreUI();
        SetupThrow();
    }

    //Called when we complete a throw
    private void FinishThrow() {
        _throwStarted = false;
        foreach(var pin in _currentPins) {
            if(pin != null && pin.DidPinFall) {
                _currentFrameScore++;
                _currentThrowScore++;
                Destroy(pin.gameObject);
            }
        }

        _totalScore += _currentThrowScore;
        UpdateScoreUI();

        //Make delay 
        if(_throwNumber == 0 && _currentFrameScore < 10) {
            Invoke(nameof(SetupThrow), 1); //Methodname as String (but use nameof for consistency) then seconds in floats
            _throwNumber++;
            return;
        }
        if(_currentFrame >= 10) {
            Debug.Log("Last Frame");
            Invoke(nameof(FinishGame), 5);
            return;
        }
        Invoke(nameof(SetupFrame), 1); //Inovke delays method calls
    }

    public void FinishGame() {
        SceneManager.LoadScene("MainMenu");
    }

    //Creates initial conditions to allow player throw ball
    public void SetupThrow() {
        _currentThrowScore = 0;
        foreach(var pin in _currentPins) {
            if(pin != null ) {
                pin.ResetPosition();
            }
        }
        if (_currentBall != null) Destroy(_currentBall.gameObject);

        playerController.StartAiming();
        _throwStarted = true;
        remainingTimeout = throwTimeout;
    }
    
    //Erases all pins on court
    public void DisposeLastFrame() {
        foreach(var pin in _currentPins) {
            if(pin != null) 
                Destroy(pin.gameObject);
        }
        
    }

    public void PlayStrikeSoundEffect() {
        //Make sure to play sound once not for all pins
        if(!audioSource.isPlaying) {
            audioSource.Play();
        }
        
    }

    //ASSIGNMENT 8
    //Receives the score from when pins are knocked down
    private void ResetGame(int score) {
        if(score > 5) {
            SceneManager.LoadScene("BowlBallAlley");
        }
    }
    
}
