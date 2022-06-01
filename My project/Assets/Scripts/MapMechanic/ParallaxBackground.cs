using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVerticle;
    [SerializeField] private Transform cameraTrans;
    [SerializeField] private float autoScrollSpeed;
    private Vector3 lastCampos;
    private float textureUnitSizeX;
    private float textureUnitSizeY;
    // Start is called before the first frame update
    void Start()
    {
        lastCampos = cameraTrans.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
        textureUnitSizeY = (texture.height / sprite.pixelsPerUnit) * transform.localScale.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTrans.position - lastCampos;
        lastCampos = cameraTrans.position;
        transform.position += new Vector3((deltaMovement.x + autoScrollSpeed/1000) * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        if (infiniteHorizontal)
            if(Mathf.Abs(cameraTrans.position.x - transform.position.x) >= textureUnitSizeX) {
                float offsetPositionX = (cameraTrans.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(cameraTrans.position.x + offsetPositionX, transform.position.y);
            }
        if (infiniteVerticle)
            if(Mathf.Abs(cameraTrans.position.y - transform.position.y) >= textureUnitSizeY) {
                float offsetPositionY = (cameraTrans.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(transform.position.x , cameraTrans.position.y + offsetPositionY);
            }
    }
}
