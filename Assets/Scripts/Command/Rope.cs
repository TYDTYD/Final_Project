using UnityEngine;
using System.Collections;
public class Rope : ICommand
{
    GameObject Anchor;
    GameObject rope;

    Vector3 offset = new Vector3(0, 0.687f);
    Player GetPlayer;
    
    public Rope(Player player,GameObject obj)
    {
        GetPlayer = player;
        Anchor = obj;
    }

    IEnumerator MoveToTarget(Transform start, Vector3 destination, float time)
    {
        Vector3 startPosition = start.position;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            float t = Mathf.SmoothStep(0, 1, elapsedTime / time);
            start.position = Vector3.Lerp(startPosition, destination, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        start.position = destination;
        GameObject parent = start.GetChild(1).gameObject;
        yield return CreateRope(parent.transform.position, parent, start.GetComponent<BoxCollider2D>());
    }

    IEnumerator CreateRope(Vector3 startPos, GameObject parent, BoxCollider2D anchor)
    {
        Vector3 pos = startPos;
        Vector2 sizeOffset = new Vector2(0, 0.275f);
        while (pos.y > GetPlayer.transform.position.y + 1f)
        {
            pos -= offset;
            GameObject obj = Object.Instantiate(parent, rope.transform);
            anchor.size += 2 * sizeOffset;
            anchor.offset -= sizeOffset;
            obj.transform.position -= offset;
            HingeJoint2D hinge = obj.GetComponent<HingeJoint2D>();
            hinge.connectedBody = parent.GetComponent<Rigidbody2D>();
            hinge.anchor = new Vector2(1.11f, 0);
            hinge.connectedAnchor = Vector2.zero;
            parent = obj;
            yield return null;
        }
    }

    public void Execute()
    {
        int layerMask = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(GetPlayer.transform.position, Vector2.up, 10f, layerMask);

        if (hit.collider == null)
            return;

        if (Stage_UI_Presenter.Instance.bomb.Value <= 0)
            return;

        Stage_UI_Presenter.Instance.bomb.Value--;
        Vector3 pos = new Vector3(Mathf.Round(hit.point.x), hit.point.y);
        rope = Object.Instantiate(Anchor);
        rope.transform.position = GetPlayer.transform.position;
        GetPlayer.StartCoroutine(MoveToTarget(rope.transform, pos, 0.2f));
    }
}