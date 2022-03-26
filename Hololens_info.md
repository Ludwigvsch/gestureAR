# Hololens dev project 🥽 🖥️

Glossary 

MRTK - allows you to consume inputs from various input sources such as 6DOF, controllers, articualated hands or speech.  MRTK is the API to the hardware signals.

## **Unity in-editor input simulation**

The Unity in-editor input simulation allows you to test holographic object behavior when using hand or eye interaction.

How to move around in the scene:

- Use **W/A/S/D** keys to move the camera forward/left/back/right.
- Use **Q/E** to move the camera vertically.
- Press and hold the **right mouse button** to rotate the camera.

How to simulate hand input:

- Press and hold the **space bar** to enable the right hand.
- While holding the space bar, move your mouse to move the hand.
- Use the **mouse scroll wheel** to adjust the depth of the hand.
- Click the **left mouse button** to simulate the pinch gesture.
- Use **T/Y** keys to make the hand persistent in the view.
- Hold the **CTRL** key and move the mouse to rotate the hand.
- Press and hold the **left shift key** to enable the left hand.

Scene setup

- Mixed Reality ⇒ toolkit ⇒ add scenes and configure
- For transform of hologram object
    - position [x,y,z]: x is to the left or right of center.  y is up or down from center and z is how near or far from center.
        - In this vector3, we want z to be at least 50cm or set to 0.5.
- To interact with the hologram object you need
    - a collider
    - object manipulator MRTK script
    - NearInteractionGrabbable MRTK script

Three primary interaction models 

1. Hands and motion controllers model - brings virtual into physical world
    1. Use case
        1. interacting with 2D UI screens
        2. interacting with 3D objects in reality.
    2. Modalities are
        1. direct manipulation with hands
        2. point and commit with hands
        3. motion controllers
2. Hands-free model 
    1. for when hands are busy
    2. Modalities are
        1. voice input
        2. gaze and dwell
3. Gaze and commit
    1. modality
        1. look at the object and then a commit action which can include voice command, button press or hand gesture.

        