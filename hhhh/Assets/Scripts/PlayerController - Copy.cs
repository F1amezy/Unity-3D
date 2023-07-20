using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {

        public Slider CooldownSlider;
        float cooldownStartTime;

        bool isCooldownActive = false;
        bool isShiftPressed = false;
        float shiftPressTime = 0f;
        public float sprintDuration = 10f;
        public float cooldownDuration = 10f;

        public AudioSource cooldownAudioSource;

        [Header("PlayerController")]
        [SerializeField] public Transform Camera;
        [SerializeField] public ItemChange Items;
        [SerializeField, Range(1, 10)] float walkingSpeed = 3.0f;
        [Range(0.1f, 5)] public float CroughSpeed = 1.0f;
        [SerializeField, Range(2, 20)] float RuningSpeed = 4.0f;
        [SerializeField, Range(0, 20)] float jumpSpeed = 6.0f;
        [SerializeField, Range(0.5f, 10)] float lookSpeed = 2.0f;
        [SerializeField, Range(10, 120)] float lookXLimit = 80.0f;
        [Space(20)]
        [Header("Advance")]
        [SerializeField] float RunningFOV = 65.0f;
        [SerializeField] float SpeedToFOV = 4.0f;
        [SerializeField] float CroughHeight = 1.0f;
        [SerializeField] float gravity = 20.0f;
        [SerializeField] float timeToRunning = 2.0f;
        [HideInInspector] public bool canMove = true;
        [HideInInspector] public bool CanRunning = true;

        [Space(20)]
        [Header("Climbing")]
        [SerializeField] bool CanClimbing = true;
        [SerializeField, Range(1, 25)] float Speed = 2f;
        bool isClimbing = false;

        [Space(20)]
        [Header("HandsHide")]
        [SerializeField] bool CanHideDistanceWall = true;
        [SerializeField, Range(0.1f, 5)] float HideDistance = 1.5f;
        [SerializeField] int LayerMaskInt = 1;

        [Space(20)]
        [Header("Input")]
        [SerializeField] KeyCode CroughKey = KeyCode.LeftControl;


        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Vector3 moveDirection = Vector3.zero;
        bool isCrough = false;
        float InstallCroughHeight;
        float rotationX = 0;
        [HideInInspector] public bool isRunning = false;
        Vector3 InstallCameraMovement;
        float InstallFOV;
        Camera cam;
        [HideInInspector] public bool Moving;
        [HideInInspector] public float vertical;
        [HideInInspector] public float horizontal;
        [HideInInspector] public float Lookvertical;
        [HideInInspector] public float Lookhorizontal;
        float RunningValue;
        float installGravity;
        bool WallDistance;
        [HideInInspector] public float WalkingValue;
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            if (Items == null && GetComponent<ItemChange>()) Items = GetComponent<ItemChange>();
            cam = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            InstallCroughHeight = characterController.height;
            InstallCameraMovement = Camera.localPosition;
            InstallFOV = cam.fieldOfView;
            RunningValue = RuningSpeed;
            installGravity = gravity;
            WalkingValue = walkingSpeed;

            CooldownSlider.value = 1f;
            CooldownSlider.minValue = 0f; // Set the minimum value of the slider to 0
            CooldownSlider.maxValue = 1f; // Set the maximum value of the slider to 1

        }

        void Update()
        {
            RaycastHit CroughCheck;
            RaycastHit ObjectCheck;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!isShiftPressed)
                {
                    // The player just started sprinting
                    isShiftPressed = true;
                    shiftPressTime = Time.time;
                    isCooldownActive = false; // Reset cooldown when sprinting starts
                    CooldownSlider.value = 1f; // Reset slider value to 1 when sprinting starts
                }

                // Check if the sprint time threshold is reached
                if (!isCooldownActive && Time.time - shiftPressTime >= sprintDuration)
                {
                    // Start the cooldown and stop the character controller movement
                    isCooldownActive = true;
                    cooldownStartTime = Time.time; // Record the start time of the cooldown
                    cooldownAudioSource.Play();
                    StartCoroutine(StopMovementCoroutine());
                }
            }
            else
            {
                // The player released left shift, reset the flags
                isShiftPressed = false;
            }

            // Update the UI Slider value
            if (isShiftPressed && !isCooldownActive)
            {
                float elapsedTime = Time.time - shiftPressTime;
                float sliderValue = 1f - (elapsedTime / sprintDuration); // Calculate the slider value while sprinting
                CooldownSlider.value = Mathf.Clamp01(sliderValue); // Clamp the slider value between 0 and 1
            }
            else if (isCooldownActive)
            {
                float elapsedCooldownTime = Time.time - cooldownStartTime;
                float sliderValue = elapsedCooldownTime / cooldownDuration; // Calculate the slider value during cooldown
                CooldownSlider.value = Mathf.Clamp01(sliderValue); // Clamp the slider value between 0 and 1

                // Check if the cooldown is over, then reset the cooldown flag and slider value
                if (elapsedCooldownTime >= cooldownDuration)
                {
                    isCooldownActive = false;
                    CooldownSlider.value = 1f;
                }
            }
            else
            {
                // Reset the slider value when cooldown is active or player is not sprinting
                CooldownSlider.value = 1f;
            }


            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!isShiftPressed)
                {
                    // The player just pressed shift, start the timer
                    isShiftPressed = true;
                    shiftPressTime = Time.time;
                }

                // Check if the sprint time threshold is reached
                if (!isCooldownActive && Time.time - shiftPressTime >= sprintDuration)
                {
                    // Start the cooldown and stop the character controller movement
                    isCooldownActive = true;
                    cooldownAudioSource.Play();
                    StartCoroutine(StopMovementCoroutine());
                }
            }
            else
            {
                // The player released left shift, reset the flags
                isShiftPressed = false;
            }

            if (!characterController.isGrounded && !isClimbing)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            isRunning = !isCrough ? CanRunning ? Input.GetKey(KeyCode.LeftShift) : false : false;
            vertical = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Vertical") : 0;
            horizontal = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Horizontal") : 0;
            if (isRunning) RunningValue = Mathf.Lerp(RunningValue, RuningSpeed, timeToRunning * Time.deltaTime);
            else RunningValue = WalkingValue;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * vertical) + (right * horizontal);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !isClimbing)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }
            characterController.Move(moveDirection * Time.deltaTime);
            Moving = horizontal < 0 || vertical < 0 || horizontal > 0 || vertical > 0 ? true : false;

            if (Cursor.lockState == CursorLockMode.Locked && canMove)
            {
                Lookvertical = -Input.GetAxis("Mouse Y");
                Lookhorizontal = Input.GetAxis("Mouse X");

                rotationX += Lookvertical * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);

                if (isRunning && Moving) cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, RunningFOV, SpeedToFOV * Time.deltaTime);
                else cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, SpeedToFOV * Time.deltaTime);
            }

            if (Input.GetKey(CroughKey))
            {
                isCrough = true;
                float Height = Mathf.Lerp(characterController.height, CroughHeight, 5 * Time.deltaTime);
                characterController.height = Height;
                WalkingValue = Mathf.Lerp(WalkingValue, CroughSpeed, 6 * Time.deltaTime);

            }
            else if (!Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.up), out CroughCheck, 0.8f, 1))
            {
                if (characterController.height != InstallCroughHeight)
                {
                    isCrough = false;
                    float Height = Mathf.Lerp(characterController.height, InstallCroughHeight, 6 * Time.deltaTime);
                    characterController.height = Height;
                    WalkingValue = Mathf.Lerp(WalkingValue, walkingSpeed, 4 * Time.deltaTime);
                }
            }

            if(WallDistance != Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.forward), out ObjectCheck, HideDistance, LayerMaskInt) && CanHideDistanceWall)
            {
                WallDistance = Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.forward), out ObjectCheck, HideDistance, LayerMaskInt);
                Items.ani.SetBool("Hide", WallDistance);
                Items.DefiniteHide = WallDistance;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Ladder" && CanClimbing)
            { 
                CanRunning = false;
                isClimbing = true;
                WalkingValue /= 2;
                Items.Hide(true);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Ladder" && CanClimbing)
            {
                moveDirection = new Vector3(0, Input.GetAxis("Vertical") * Speed * (-Camera.localRotation.x / 1.7f), 0);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Ladder" && CanClimbing)
            {
                CanRunning = true;
                isClimbing = false;
                WalkingValue *= 2;
                Items.ani.SetBool("Hide", false);
                Items.Hide(false);
            }
        }
        private System.Collections.IEnumerator StopMovementCoroutine()
        {
            canMove = false;
            characterController.Move(Vector3.zero); // Stop the player's movement
            yield return new WaitForSeconds(cooldownDuration);
            canMove = true;
            isCooldownActive = false;
        }
    }
}