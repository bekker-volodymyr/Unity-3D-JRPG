using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject iconPrefab;

    List<Marker> markers = new List<Marker>();

    public RawImage compassImage;
    public Transform _camera;
    public Transform player;

    private float compassUnit;

    public float maxDistance = 200f;

    public Marker ghosts;

    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;
        AddMarker(ghosts);
    }

    private void Update()
    {
        compassImage.uvRect = new Rect(_camera.localEulerAngles.y / 360f, 0f, 1f, 1f);

        foreach(Marker marker in markers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

            float dst = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), marker.position);
            float scale = dst < maxDistance ? 1f - (dst / maxDistance) : 0f;

            marker.image.rectTransform.localScale = Vector3.one * scale;
        }
    }

    public void AddMarker(Marker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        markers.Add(marker);
    }

    private Vector2 GetPosOnCompass(Marker marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 cameraFwd = new Vector2(_camera.transform.forward.x, _camera.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, cameraFwd);

        return new Vector2(compassUnit * angle, 0f);
    }
}
