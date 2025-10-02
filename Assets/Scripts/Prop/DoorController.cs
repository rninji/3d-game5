using System;
using System.Collections;
using UnityEngine;
using static Constants;

public class DoorController : MonoBehaviour
{
    [SerializeField] private ESceneName sceneName;
    [SerializeField] private GameObject door;
    [SerializeField] private float openDuration;

    private bool _isOpen;

    private void OnTriggerEnter(Collider other)
    {
        // 문 열기
        if (other.CompareTag("Player"))
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        float distance = 3f;
        Vector3 startPosition = door.transform.position;
        Vector3 endPosition = startPosition + Vector3.up * distance;
        float elapsedTime = 0f;

        while (elapsedTime < openDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / openDuration;
            door.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        door.transform.position = endPosition;
        
        GameManager.Instance.LoadScene(sceneName);
    }
}
