using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 25f, strafSpeed = 8f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafSpeed, activeHoverSpeed;
    private float ForwardAcceleration = 2.5f, StrafAcceleration = 2f, HoverSpeedAcceleration = 2f;

    public float lookRateSpeed = 90f;
    public Vector2 lookInput, screenCenter, mouseDistance;

    bool boosting = false;
    public float boostingSpeed = 2f;
    public float boostingAcceleration = 10f;

    //public GameObject windVFX;
    public ParticleSystem xyz;
    public ParticleSystem.MinMaxCurve abc;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;
    public Transform spawnPoint;
    public float shootForce = 10;
    [SerializeField]
    private Camera mainCamera;

    public GameObject bullet;

    public Transform trailParent;
    private TrailRenderer[] trails;

    public Texture2D currsor;
    //public Vector2 cursorOffset;

    void Start()
    {
        trails = trailParent.GetComponentsInChildren<TrailRenderer>();

        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        xyz.Play();
        //abc = xyz.emission.rateOverTime;
        abc = 0f;
        

        Cursor.lockState = CursorLockMode.Confined;
        Vector2 hotSpot = new Vector2(currsor.width / 2f, currsor.height / 2f);
        Cursor.SetCursor(currsor, hotSpot, CursorMode.Auto);
        //Cursor.SetCursor(currsor, new Vector2(cursorOffset.x,cursorOffset.y), CursorMode.ForceSoftware);
    }

    void Update()
    {

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
        

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        if(boosting)
        {
            //abc = Mathf.Lerp(0f, 200f, 1f);
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed ,  forwardSpeed * boostingSpeed * Time.deltaTime, (ForwardAcceleration+boostingAcceleration) * Time.deltaTime);
        }
        else
        {
            xyz.Play();
            //abc = Mathf.Lerp(200f, 0f, 1f);
            activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed * Time.deltaTime, ForwardAcceleration * Time.deltaTime);
        }

        activeStrafSpeed = Mathf.Lerp(activeStrafSpeed, Input.GetAxisRaw("Horizontal") * strafSpeed * Time.deltaTime, StrafAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed,Input.GetAxisRaw("Hover") * hoverSpeed * Time.deltaTime, HoverSpeedAcceleration * Time.deltaTime);

        transform.position += (transform.forward * activeForwardSpeed)+(transform.right * activeStrafSpeed) + (transform.up * activeHoverSpeed) ;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            boosting = true;
        }
        else
        {
            boosting = false;
        }

        ModifyTrail();

    }

    void ModifyTrail()
    {
        foreach (TrailRenderer tr in trails)
        {
            tr.time = (activeForwardSpeed+1) * .35f;
        }
    }
}
