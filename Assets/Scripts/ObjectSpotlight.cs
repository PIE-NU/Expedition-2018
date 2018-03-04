using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpotlight : MonoBehaviour {
    //public PhysicsTD directionComponent;
    public GameObject AttachedObject;

    public Vector3 upPosition;
    public Vector3 downPosition;
    public Vector3 leftPosition;
    public Vector3 rightPosition;

    public bool LightCanRotate = false;
    public Direction PermanentDirection = Direction.LEFT;

    private Quaternion upRotation, downRotation, leftRotation, rightRotation;

    private Direction dir;

    // Use this for initialization
    void Start()
    {
        //if (directionComponent == null)
        //  directionComponent = GetComponent<PhysicsTD>();

        if (AttachedObject == null)
            AttachedObject = transform.parent.gameObject;

        upRotation = Quaternion.Euler(-15f, 0f, 0f);
        downRotation = Quaternion.Euler(105f, 0f, 0f);
        leftRotation = Quaternion.Euler(20f, -70f, 90f);
        rightRotation = Quaternion.Euler(20f, 70f, -90f);


        //dir = directionComponent.Dir;
        dir = PermanentDirection;
        ChooseDirection();
    }
    void OnEnable()
    {
        if(LightCanRotate)
            ChooseDirection();
    }
    // Update is called once per frame
    void Update()
    {

        //Direction d = directionComponent.Dir;
        Direction d = PermanentDirection;
        if (LightCanRotate && d != dir)
        {
            ChooseDirection();
        }
    }
    /// <summary>
    /// ChooseDirection changes the direction of the spotlight based on the parent object's Direction.
    /// If this is to be automatic, ensure LightCanRotate is true.
    /// </summary>
    void ChooseDirection()
    {
        //Direction d = directionComponent.Dir;
        Direction d = PermanentDirection;
        switch (d)
        {
            //Add position to parent object pos
            //Quaternion rotation to current direction
            case Direction.UP:
                transform.position = AttachedObject.transform.position + upPosition;
                transform.rotation = upRotation;
                break;
            case Direction.DOWN:
                transform.position = AttachedObject.transform.position + downPosition;
                transform.rotation = downRotation;
                break;
            case Direction.LEFT:
                transform.position = AttachedObject.transform.position + leftPosition;
                transform.rotation = leftRotation;
                break;
            case Direction.RIGHT:
                transform.position = AttachedObject.transform.position + rightPosition;
                transform.rotation = rightRotation;
                break;
        }
        dir = d;
    }
    /// <summary>
    /// ChangeDirection takes a Direction from an outside source and changes the spotlight direction
    /// </summary>
    /// <param name="d"></param>
    public void ChangeDirection(Direction d)
    {
        switch (d)
        {
            //Add position to parent object pos
            //Quaternion rotation to current direction
            case Direction.UP:
                transform.position = AttachedObject.transform.position + upPosition;
                transform.rotation = upRotation;
                break;
            case Direction.DOWN:
                transform.position = AttachedObject.transform.position + downPosition;
                transform.rotation = downRotation;
                break;
            case Direction.LEFT:
                transform.position = AttachedObject.transform.position + leftPosition;
                transform.rotation = leftRotation;
                break;
            case Direction.RIGHT:
                transform.position = AttachedObject.transform.position + rightPosition;
                transform.rotation = rightRotation;
                break;
        }
        dir = d;
    }
}
