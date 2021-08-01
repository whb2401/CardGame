using UnityEngine;

public class AddBall : MonoBehaviour
{

    private ExtraBallManager _extraBallManager;

    // Use this for initialization
    private void Start()
    {
        _extraBallManager = FindObjectOfType<ExtraBallManager>();
    }

    // Update is called once per frame
    private void Update()
    { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(GameSetting.TAGBALL) &&
            !collision.gameObject.CompareTag(GameSetting.TAGEXTRABALL)) return;
        // add an extra ball to the count
        _extraBallManager.numberOfExtraBalls++;
        gameObject.SetActive(false);
    }

}
