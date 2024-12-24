using UnityEngine;

public class LoopingEnvironment : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        // Check if the player exits the cube walls
        if (other.CompareTag("Player"))
        {
            Vector3 playerPos = other.transform.position;
            Vector3 cubeCenter = transform.position;
            Vector3 cubeSize = transform.localScale / 2f; // Half the size of the cube

            // Loop the player back inside the cube if they exit
            if (Mathf.Abs(playerPos.x - cubeCenter.x) > cubeSize.x)
            {
                playerPos.x = -playerPos.x;
            }
            if (Mathf.Abs(playerPos.z - cubeCenter.z) > cubeSize.z)
            {
                playerPos.z = -playerPos.z;
            }
            if (Mathf.Abs(playerPos.y - cubeCenter.x) > cubeSize.x)
            {
                playerPos.y = -playerPos.y;
            }

            // Set the player's position back inside the cube
            other.transform.position = playerPos;
        }
    }
}
