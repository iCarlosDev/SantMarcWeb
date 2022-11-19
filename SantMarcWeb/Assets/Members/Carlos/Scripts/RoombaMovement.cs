using UnityEngine;
namespace Members.Carlos.Scripts
{
    public class RoombaMovement : MonoBehaviour
    {
        //Variables
        [SerializeField] private Transform[] points;
        [SerializeField] private int current;
        [SerializeField] private float speed;

        private void Start()
        {
            current = 0;
        }

        private void Update()
        {
            if (transform.position != points[current].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);
                transform.LookAt(points[current].transform);
            }
            else
            {
                current = (current + 1) % points.Length;
            }
        }
    }
}
