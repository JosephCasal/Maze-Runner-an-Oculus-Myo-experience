using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class ObstacleController : MonoBehaviour {

    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;

    // Materials to change to when poses are made.
    //public Material waveInMaterial;
    //public Material waveOutMaterial;
    //public Material doubleTapMaterial;

    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;

    public float speed;

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

        // Check if the pose has changed since last update.
        // The ThalmicMyo component of a Myo game object has a pose property that is set to the
        // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
        // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
        // is not on a user's arm, pose will be set to Pose.Unknown.
        if (thalmicMyo.pose != _lastPose)
        {
            if((GameObject.Find("FPSController").transform.position.z >= 1.7) && (GameObject.Find("FPSController").transform.position.z <= 4.7))
            {
                if ((GameObject.Find("FPSController").transform.position.x <= 0) && (GameObject.Find("FPSController").transform.position.x >= -1))
                {
                    //if ((GameObject.Find("FPSController").transform.position.z >= -90))
                    //{
                    _lastPose = thalmicMyo.pose;

                    // Vibrate the Myo armband when a fist is made.
                    //if (thalmicMyo.pose == Pose.Fist)
                    //{
                        //thalmicMyo.Vibrate(VibrationType.Medium);

                        //ExtendUnlockAndNotifyUserAction(thalmicMyo);

                        // Change material when wave in, wave out or double tap poses are made.
                    //}
                    if (thalmicMyo.pose == Pose.WaveIn)
                    {
                        //GetComponent<Renderer>().material = waveInMaterial;

                        //Vector3 movement = new Vector3(10f, 0.0f, 0.0f);

                        //rb.AddForce(movement * speed);

                        //thalmicMyo.Vibrate(VibrationType.Medium);

                        float translation = speed;
                        translation *= Time.deltaTime;
                        transform.Translate(0, 0, translation);

                        ExtendUnlockAndNotifyUserAction(thalmicMyo);
                    }
                    else if (thalmicMyo.pose == Pose.WaveOut)
                    {
                        //GetComponent<Renderer>().material = waveOutMaterial;

                        //Vector3 movement = new Vector3(-10f, 0.0f, 0.0f);

                        //rb.AddForce(movement * speed);

                        float translation = speed;
                        translation *= Time.deltaTime;
                        transform.Translate(0, 0, (-1 * translation));

                        ExtendUnlockAndNotifyUserAction(thalmicMyo);
                    }
                    //else if (thalmicMyo.pose == Pose.DoubleTap)
                    //{
                    //GetComponent<Renderer>().material = doubleTapMaterial;

                    // ExtendUnlockAndNotifyUserAction(thalmicMyo);
                    //}
                    //}
                }

            }
            //if (thalmicMyo.pose == Pose.DoubleTap)
            //{
                //thalmicMyo.Vibrate(VibrationType.Medium);

                //UnityEngine.SceneManagement.SceneManager.LoadScene(0);

                //Application.LoadLevel(Application.loadedLevel);
            //}
        }
    }

    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }
}
