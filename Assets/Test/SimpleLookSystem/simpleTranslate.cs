using UnityEngine;

namespace Test
{
    public class simpleTranslate : MonoBehaviour
    {
        public int speed = 2;

        void Update()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("A is pressed");
                //Player moves left
                gameObject.transform.Translate(Vector3.left * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("D is pressed");
                //Player moves right
                gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("W is pressed");
                //Player moves forward
                gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("S is pressed");
                //Player moves back
                gameObject.transform.Translate(Vector3.back * Time.deltaTime * speed);
            }
        }
    }
}
