using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

public class UIPlayingView : BaseMonoBehaviour<UIPlayingView>
{
    public Text textPause;

    public void OnClickPause()
    {
        GameManager.Instance.paused = !GameManager.Instance.paused;
        GameManager.Instance.NotifyGameStatusChange();

        if (GameManager.Instance.paused)
        {
            // 游戏暂停
            textPause.text = ">";
        }
        else
        {
            textPause.text = "||";
        }
    }

    public void OnClickAccelerate()
    {
        GameManager.Instance.accelerateSkip = !GameManager.Instance.accelerateSkip;
        GameManager.Instance.CurrentAccelerateSeed = GameManager.Instance.accelerateSkip ? GameSetting.BATTLE_ACCELERATE_SEED : 1f;
        GameManager.Instance.NotifyBallAccelerateChange();
    }
}
