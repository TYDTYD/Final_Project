using UnityEngine;
using System.Collections;
public class Rope : ICommand
{
    GameObject Anchor;
    GameObject rope;
    Player GetPlayer;
    Vector3 offset = new Vector3(0, 5.5f);
    public Rope(Player player,GameObject obj)
    {
        GetPlayer = player;
        Anchor = obj;
    }

    IEnumerator MoveToTarget(Transform start, Vector3 destination, float time)
    {
        Vector3 startPosition = start.position;
        float elapsedTime = 0f;
        Debug.Log(startPosition);
        while (elapsedTime < time)
        {
            float t = Mathf.SmoothStep(0, 1, elapsedTime / time);
            start.position = Vector3.Lerp(startPosition, destination, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        start.position = destination; // 최종 위치 보정
    }
    public void Execute()
    {
        int layerMask = LayerMask.GetMask("Ground");

        Debug.DrawRay(GetPlayer.transform.position, Vector2.up * 100f, Color.red, 1f);
        RaycastHit2D hit = Physics2D.Raycast(GetPlayer.transform.position, Vector2.up, 10f, layerMask);

        if (hit.collider == null)
            return;

        Vector3 pos = new Vector3(Mathf.Round(hit.point.x), hit.point.y) - offset;
        rope = Object.Instantiate(Anchor);
        rope.transform.position = GetPlayer.transform.position - offset;
        Debug.Log(GetPlayer.transform.position);
        GetPlayer.StartCoroutine(MoveToTarget(rope.transform, pos, 0.2f));
    }
}