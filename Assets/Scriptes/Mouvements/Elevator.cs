using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour {
    public float hauteur;
    public float duration;
    public float leavingJumpPower;
    public GameObject elevator;

    private void OnTriggerEnter(Collider other) {
        Movement movement = other.GetComponent<Movement>();
        if (movement != null) {
            StartCoroutine(GetAnElevator(movement));
        }
    }

    private IEnumerator GetAnElevator(Movement movement) {
        movement.Elevator(false);
        GameObject obj = Instantiate(elevator, movement.transform.position, Quaternion.identity);
        obj.transform.parent = movement.transform;
        Vector3 target = new Vector3(transform.position.x, transform.position.y + hauteur, transform.position.z);
        
        float elapsedTime = 0;
        while (elapsedTime < duration) {
            movement.transform.position = Vector3.Lerp(transform.position, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(obj);
        movement.Elevator(true);
        movement.GetComponent<Rigidbody>().AddForce(new Vector3(0, leavingJumpPower, 0), ForceMode.Impulse);
        
    }
}