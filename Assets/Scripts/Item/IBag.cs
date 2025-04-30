using UnityEngine;

public interface IBag
{
    void PutOn(GameObject obj, Vector3 pos);
    void TakeOff(GameObject obj, Vector3 left, Vector3 right);
}
