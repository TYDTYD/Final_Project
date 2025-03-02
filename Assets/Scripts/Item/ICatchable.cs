using UnityEngine;
public interface ICatchable
{
    public void Grap(GameObject obj, Vector3 pos);
    public void Throw(GameObject obj, Vector3 left, Vector3 right);
}