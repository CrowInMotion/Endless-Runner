using System.Collections.Generic;
using UnityEngine;


//===================================================================================
//setup: place one image as the first child of this gameobject
//       at runtime, two copies are created (left and right)
//optional single image mode: used for unique images that do not need to be repeated
//===================================================================================

public class SimpleParallax : MonoBehaviour
{
    public Vector2 parallaxSpeed;
    public Vector2 autoScrollSpeed;
    public float imageOverlap;
    public bool oneImage;
    //ability to keep track fo all images needed to parallax
    private List<Transform> imageTransforms = new();
    //width of sprite after accounting for overlap
    private float imageDistance;
    //reference to camera
    private Transform cameraTransform;
    //camera position during the previous frame
    //used to calculate how far the camera moved
    private Vector2 lastCamPos;

    Transform originalTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalTransform = transform.GetChild(0);
        cameraTransform = Camera.main.transform;

        lastCamPos = cameraTransform.position;

        //Determine how far apart repeated images should be
        getImageDistance();

        //Create left and right as long as oneImage is not true
        //One image is a bool used for unique images in the scene
        if (!oneImage)
        {
            createSideImages();
        }

        //get reference to all images in this layer
        fillImageList();
    }

    //calculate spacing between repeated images
    //we can adjust this by using the overlap value
    //the sprite width is reduced by the imageOverlap to eliminate seams
    void getImageDistance()
    {
        imageDistance = originalTransform.GetComponent<SpriteRenderer>().bounds.size.x - imageOverlap;
    }

    //Creates one copy to the left and one copy to the right, so there are three images in one continous strip
    void createSideImages()
    {
        Transform leftImage = Instantiate(originalTransform, transform);
        Transform rightImage = Instantiate(originalTransform, transform);

        //Set position of each image
        leftImage.position = new Vector3(originalTransform.position.x - imageDistance, originalTransform.position.y, originalTransform.position.z);
        rightImage.position = new Vector3(originalTransform.position.x + imageDistance, originalTransform.position.y, originalTransform.position.z);
    }

    //Stores references to every child image
    void fillImageList()
    {
        foreach (Transform t in GetComponentInChildren<Transform>())
        {
            if (t != transform)
            {
                imageTransforms.Add(t);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move every image according to the camera
        scrollImage();

        //only recycle the images when repeating is enabled
        if (!oneImage)
        {
            shiftImages();
        }
    }

    //Moves every image based on:
    //1. Camera movement
    //2. Constant scroll
    //The farther the camera moves, the farther this layer shifts according to the parallax speed
    void scrollImage()
    {
        //Determine how far the camera moved from last frame
        Vector2 parallaxScroll = new Vector2((lastCamPos.x - cameraTransform.position.x) * parallaxSpeed.x, (lastCamPos.y - cameraTransform.position.y) * parallaxSpeed.y);

        foreach (Transform t in imageTransforms)
        {
            Vector3 curPos = t.position;

            float newXPos = curPos.x + autoScrollSpeed.x * Time.deltaTime - parallaxScroll.x;
            float newYPos = curPos.y + autoScrollSpeed.y * Time.deltaTime - parallaxScroll.y;

            t.position = new Vector3(newXPos, newYPos, curPos.z);
        }

        //cache the camera position for next frame
        lastCamPos = cameraTransform.position;
    }

    //instead of destroying and recreating sprites we will move images to the other side
    //This will create the illusion of the endless background
    //while not killing our memory
    void shiftImages()
    {
        //images outside of these limits will be recycled
        float leftThreshold = cameraTransform.position.x - imageDistance * 1.6f;
        float rightThreshold = cameraTransform.position.x + imageDistance * 1.6f;

        foreach (Transform t in imageTransforms)
        {
            //image is completetly left of view move to right
            if (t.position.x < leftThreshold)
            {
                t.position = new Vector3(t.position.x + imageDistance * 3, t.position.y, t.position.z);
            }

            //image is completetly right of view move to right
            if (t.position.x > rightThreshold)
            {
                t.position = new Vector3(t.position.x - imageDistance * 3, t.position.y, t.position.z);
            }
        }
    }
}
