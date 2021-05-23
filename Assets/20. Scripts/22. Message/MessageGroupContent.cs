using UnityEngine;
public class MessageGroupContent : MonoBehaviour, IContentUIControl<MessageData>
{
    // 0: Manager, 1: Me, 2: Others
    [SerializeField] private CanvasGroup[] CanvasGroups;


    public void InitData()
    {
        foreach (CanvasGroup _cg in CanvasGroups)
        {
            _cg.alpha = 0;
        }
    }

    public void SetData(MessageData _t)
    {
        for (int i = 0; i < CanvasGroups.Length; i++)
        {
            if (i == _t.checker)
            {
                if (i == 0)
                {
                    CanvasGroups[i].GetComponent<CanvasGroup>().alpha = 1;
                    CanvasGroups[i].GetComponent<MessageContent>().SetData(_t.message);
                }
                else
                {
                    CanvasGroups[i].GetComponent<CanvasGroup>().alpha = 1;
                    CanvasGroups[i].GetComponent<MessageContent>().SetData(_t.message, _t.name, _t.time);
                }
            }
            else
            {
                CanvasGroups[i].GetComponent<CanvasGroup>().alpha = 0;
            }
        }
    }

}
