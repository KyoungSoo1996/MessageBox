using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public abstract class ScrollViewControl_Horizontal<TUI, TData> : MonoBehaviour where TUI : IContentUIControl<TData> where TData : class
{
    [Header("hide objects")]
    [SerializeField] int marginCount;

    [Header("Content Object Prefabs")]
    [SerializeField] private Transform transformContentPrefabs = null;

    [Header("scrollView Content")]
    [SerializeField] protected Transform transformView = null;

    [Header("scrollView Viewport")]
    [SerializeField] private Transform transformViewport = null;

    private RectTransform rectViewport = null;
    private RectTransform rectScrollView = null;
    private RectTransform rectContentPrefabs = null;

    // List View Index
    private int startIndex, endIndex;
    private int viewStartIndex, viewEndIndex, viewPrevIndex;
    private int intervalIndex;

    private int contentsCount, dataCount;
    private float contentWidthSize;
    private float spacing;

    protected List<Transform> transformContents = new List<Transform>();

    private List<TData> contentDatas { get; set; }

    public void Init()
    {
        InitScrollView();
    }

    public void SetData(List<TData> _Tdata)
    {
        ResetList();
        setContentData(_Tdata);
        SetScrollView();
    }

    public void OnDragScrollView()
    {
        viewStartIndex = (int)((Mathf.Abs(rectScrollView.localPosition.x + (rectViewport.rect.width * 0.5f)) / (rectContentPrefabs.rect.width + spacing)));

        viewEndIndex = viewStartIndex + intervalIndex;
        if (viewStartIndex != viewPrevIndex)
        {
            DragScrollViewControl();
        }
    }

    private void ResetList()
    {
        for (int i = 0; i < transformContents.Count; i++)
        {
            Destroy(transformContents[i].gameObject);
        }
        transformContents.Clear();
    }

    private void setContentData(List<TData> _Tdata)
    {
        contentDatas = _Tdata;
        dataCount = contentDatas.Count;
    }

    private void InitScrollView()
    {
        rectContentPrefabs = transformContentPrefabs.GetComponent<RectTransform>();
        rectScrollView = transformView.GetComponent<RectTransform>();
        rectViewport = transformViewport.GetComponent<RectTransform>();
        intervalIndex = (int)(rectViewport.rect.width / rectContentPrefabs.rect.width) + 1;
        contentsCount = intervalIndex + marginCount * 2;
        spacing = transformView.GetComponent<HorizontalLayoutGroup>().spacing;
        contentWidthSize = spacing + rectContentPrefabs.rect.width;
        rectScrollView.anchoredPosition = new Vector2(0, 0);
    }

    private void SetScrollView()
    {
        endIndex = contentsCount > dataCount ? dataCount : contentsCount;
        for (int i = 0; i < endIndex; i++)
        {
            transformContents.Add(MonoBehaviour.Instantiate(rectContentPrefabs, transformView));
        }
        rectScrollView.sizeDelta = new Vector2((contentWidthSize * dataCount) + spacing, rectScrollView.sizeDelta.y);
        for (int i = 0; i < endIndex; i++)
        {
            transformContents[i % contentsCount].GetComponent<TUI>().InitData();
            transformContents[i % endIndex].GetComponent<TUI>().SetData(contentDatas[i]);
        }
    }

    private void DragScrollViewControl()
    {

        for (int i = startIndex; i < endIndex; i++)
        {
            if (viewStartIndex <= i && i <= viewEndIndex)
            {
                transformContents[i % contentsCount].GetComponent<TUI>().InitData();
                transformContents[i % contentsCount].GetComponent<TUI>().SetData(contentDatas[i]);
            }
            RectTransform _rect = transformContents[i % contentsCount].GetComponent<RectTransform>();
            _rect.anchoredPosition = new Vector2((i * contentWidthSize) + spacing, _rect.anchoredPosition.y);
        }
        if (viewPrevIndex <= viewStartIndex)
        {
            startIndex = Mathf.Clamp(viewStartIndex + 1, 0, dataCount);
            endIndex = Mathf.Clamp(viewEndIndex + 1, 0, dataCount);
            viewPrevIndex = viewStartIndex;
        }
        else
        {
            startIndex = Mathf.Clamp(viewStartIndex - 1, 0, dataCount);
            endIndex = Mathf.Clamp(viewEndIndex - 1, 0, dataCount);
            viewPrevIndex = viewStartIndex;
        }
    }
}
