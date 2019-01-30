using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private string horizontalInputName, verticalInputName;
    [SerializeField] private float movementSpeed;
    private CharacterController charController;

    public GameObject character;
    public Text text;
    public CatAnimation animation;
    
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        animation = GetComponent<CatAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (text != null)
            text.text = HouseInteraction.wrongInteractions + "/2";
        //Debug.Log("houseinteratcions: " + HouseInteraction.wrongInteractions);
        PlayerMovement(movementSpeed);
    }

    private void PlayerMovement(float speed)
    {
        float verInput = Input.GetAxis(verticalInputName) * speed;
        float horizInput = Input.GetAxis(horizontalInputName) * speed;

        Vector3 forwardMovement = transform.forward * verInput;
        Vector3 rigthMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rigthMovement);
        animation.Progress((forwardMovement + rigthMovement).magnitude * Time.deltaTime * 4);

        float movAngle = Mathf.Atan2(verInput, horizInput) / Mathf.PI * 180f; // between -180 and 180
        const float ANGLE_TOLERANCE = 30; // degrees
        
        // down
        if (forwardMovement.z < 0/* && movAngle > -90 - ANGLE_TOLERANCE && movAngle < -90 + ANGLE_TOLERANCE*/)
        {
            if (Mathf.Approximately(character.transform.eulerAngles.y, 0))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y + 180, character.transform.eulerAngles.z);
            }
            else if (Mathf.Approximately(character.transform.eulerAngles.y, 90))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y + 90, character.transform.eulerAngles.z);
            }
            else if (Mathf.Approximately(character.transform.eulerAngles.y, 270))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y - 90, character.transform.eulerAngles.z);
            }
        }
        
        // up
        if (forwardMovement.z > 0/* && movAngle > 90 - ANGLE_TOLERANCE && movAngle < 90 + ANGLE_TOLERANCE*/)
        {
            if (Mathf.Approximately(character.transform.eulerAngles.y, 180))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y - 180, character.transform.eulerAngles.z);
            }
            else if (Mathf.Approximately(character.transform.eulerAngles.y, 90))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y - 90, character.transform.eulerAngles.z);
            }
            else if (Mathf.Approximately(character.transform.eulerAngles.y, 270))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y - 270, character.transform.eulerAngles.z);
            }
        }
        
        // right
        if (rigthMovement.x > 0 && movAngle > -ANGLE_TOLERANCE && movAngle < ANGLE_TOLERANCE)
        {
            if (Mathf.Approximately(character.transform.eulerAngles.y, 0))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y + 90, character.transform.eulerAngles.z);
            }
            else if (Mathf.Approximately(character.transform.eulerAngles.y, 270))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y - 180, character.transform.eulerAngles.z);
            }
            else if (Mathf.Approximately(character.transform.eulerAngles.y, 180))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y - 90, character.transform.eulerAngles.z);
            }
        }
        
        // left
        if (rigthMovement.x < 0 && (movAngle > 180 - ANGLE_TOLERANCE || movAngle < -180 + ANGLE_TOLERANCE))
        {
            if (Mathf.Approximately(character.transform.eulerAngles.y, 0))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y + 270, character.transform.eulerAngles.z);
            }
            else if (Mathf.Approximately(character.transform.eulerAngles.y, 90))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y + 180, character.transform.eulerAngles.z);
            }
            else if (Mathf.Approximately(character.transform.eulerAngles.y, 180))
            {
                character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, character.transform.eulerAngles.y + 90, character.transform.eulerAngles.z);
            }
        }
        
        // Debug.LogFormat(this, "( {0:0.00} ; {1:0.00} )", rigthMovement.x, forwardMovement.z);
        // if (Input.GetButtonDown("Horizontal") && rigthMovement.x > 0) print("RIGHT");
        // if (Input.GetButtonDown("Horizontal") && rigthMovement.x < 0) print("LEFT");
        // if (Input.GetButtonDown("Vertical") && forwardMovement.z > 0) print("UP");
        // if (Input.GetButtonDown("Vertical") && forwardMovement.z < 0) print("DOWN");
    }


    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.CompareTag("Enemies"))
            SceneManager.LoadScene("CitySceneTestAgents");
        */
        if (collision.gameObject.CompareTag("Enemies") && !FinalCutsceneAnimation.instance.triggered) 
            gameOver();
    }


    public static void gameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
