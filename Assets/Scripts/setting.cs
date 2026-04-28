using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;

    private Slider volumeSlider;
    private TMP_Dropdown resolutionDropdown;
    private Toggle fullscreenToggle;
    private Resolution[] resolutions;

    void Awake()
    {
        SetupPanel();
        BuildUI();
    }

    void SetupPanel()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = rt.offsetMax = Vector2.zero;
        Image bg = GetComponent<Image>() ?? gameObject.AddComponent<Image>();
        bg.color = new Color(0f, 0f, 0f, 0.88f);

        Canvas parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas != null)
        {
            CanvasScaler scaler = parentCanvas.GetComponent<CanvasScaler>();
            if (scaler == null) scaler = parentCanvas.gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;
        }
    }

    void BuildUI()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        GameObject box = CreateBox(transform, Vector2.zero, new Vector2(700, 500), new Color(0.06f, 0.03f, 0.01f, 0.97f));

        // Title
        MakeText(box.transform, "SETTINGS", 0, 210, 600, 65, 48, new Color(0.95f, 0.78f, 0.3f), true, TextAlignmentOptions.Center);
        MakeDivider(box.transform, 0, 175);

        // Volume
        MakeText(box.transform, "Volume", -150, 110, 180, 40, 28, Color.white, false, TextAlignmentOptions.Right);
        volumeSlider = MakeSlider(box.transform, 110, 110, 300, 30);

        // Resolution
        MakeText(box.transform, "Resolution", -150, 30, 180, 40, 28, Color.white, false, TextAlignmentOptions.Right);
        resolutionDropdown = MakeDropdown(box.transform, 110, 30, 300, 42);

        // Fullscreen
        MakeText(box.transform, "Fullscreen", -150, -55, 180, 40, 28, Color.white, false, TextAlignmentOptions.Right);
        fullscreenToggle = MakeToggle(box.transform, 110, -55);

        MakeDivider(box.transform, 0, -115);
        MakeCloseButton(box.transform, 0, -185);

        SetupValues();
    }

    void SetupValues()
    {
        float savedVol = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVol;
        if (audioMixer != null)
            audioMixer.SetFloat("Volume", Mathf.Log10(Mathf.Max(savedVol, 0.0001f)) * 20);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        resolutions = Screen.resolutions;
        var options = new List<string>();
        int currentIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                currentIndex = i;
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetVolume(float value)
    {
        if (audioMixer != null)
            audioMixer.SetFloat("Volume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool value)
    {
        Screen.fullScreen = value;
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }

    GameObject CreateBox(Transform parent, Vector2 pos, Vector2 size, Color color)
    {
        GameObject obj = new GameObject("Box");
        obj.transform.SetParent(parent, false);
        RectTransform rt = obj.AddComponent<RectTransform>();
        rt.anchoredPosition = pos;
        rt.sizeDelta = size;
        obj.AddComponent<Image>().color = color;
        return obj;
    }

    void MakeText(Transform parent, string text, float x, float y, float w, float h, float size, Color color, bool bold, TextAlignmentOptions align)
    {
        GameObject obj = new GameObject(text + "_txt");
        obj.transform.SetParent(parent, false);
        RectTransform rt = obj.AddComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, y);
        rt.sizeDelta = new Vector2(w, h);
        var tmp = obj.AddComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = size;
        tmp.color = color;
        tmp.fontStyle = bold ? FontStyles.Bold : FontStyles.Normal;
        tmp.alignment = align;
        tmp.enableWordWrapping = false;
        tmp.overflowMode = TextOverflowModes.Overflow;
    }

    void MakeDivider(Transform parent, float x, float y)
    {
        GameObject obj = new GameObject("Divider");
        obj.transform.SetParent(parent, false);
        RectTransform rt = obj.AddComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, y);
        rt.sizeDelta = new Vector2(620, 2);
        obj.AddComponent<Image>().color = new Color(0.75f, 0.55f, 0.15f, 0.7f);
    }

    Slider MakeSlider(Transform parent, float x, float y, float w, float h)
    {
        GameObject root = new GameObject("Slider");
        root.transform.SetParent(parent, false);
        RectTransform rootRt = root.AddComponent<RectTransform>();
        rootRt.anchoredPosition = new Vector2(x, y);
        rootRt.sizeDelta = new Vector2(w, h);

        GameObject track = new GameObject("Track");
        track.transform.SetParent(root.transform, false);
        RectTransform trackRt = track.AddComponent<RectTransform>();
        trackRt.anchorMin = new Vector2(0, 0.3f);
        trackRt.anchorMax = new Vector2(1, 0.7f);
        trackRt.offsetMin = trackRt.offsetMax = Vector2.zero;
        track.AddComponent<Image>().color = new Color(0.2f, 0.12f, 0.04f);

        GameObject fillArea = new GameObject("Fill Area");
        fillArea.transform.SetParent(root.transform, false);
        RectTransform faRt = fillArea.AddComponent<RectTransform>();
        faRt.anchorMin = new Vector2(0, 0.3f);
        faRt.anchorMax = new Vector2(1, 0.7f);
        faRt.offsetMin = new Vector2(5, 0);
        faRt.offsetMax = new Vector2(-15, 0);

        GameObject fill = new GameObject("Fill");
        fill.transform.SetParent(fillArea.transform, false);
        RectTransform fillRt = fill.AddComponent<RectTransform>();
        fillRt.anchorMin = Vector2.zero;
        fillRt.anchorMax = new Vector2(0, 1);
        fillRt.sizeDelta = new Vector2(10, 0);
        fill.AddComponent<Image>().color = new Color(0.9f, 0.65f, 0.1f);

        GameObject ha = new GameObject("Handle Area");
        ha.transform.SetParent(root.transform, false);
        RectTransform haRt = ha.AddComponent<RectTransform>();
        haRt.anchorMin = Vector2.zero;
        haRt.anchorMax = Vector2.one;
        haRt.offsetMin = new Vector2(10, 0);
        haRt.offsetMax = new Vector2(-10, 0);

        GameObject handle = new GameObject("Handle");
        handle.transform.SetParent(ha.transform, false);
        RectTransform hRt = handle.AddComponent<RectTransform>();
        hRt.sizeDelta = new Vector2(24, 24);
        Image hImg = handle.AddComponent<Image>();
        hImg.color = new Color(0.95f, 0.8f, 0.2f);

        Slider slider = root.AddComponent<Slider>();
        slider.fillRect = fill.GetComponent<RectTransform>();
        slider.handleRect = hRt;
        slider.targetGraphic = hImg;
        slider.minValue = 0.001f;
        slider.maxValue = 1f;
        slider.value = 1f;
        return slider;
    }

    TMP_Dropdown MakeDropdown(Transform parent, float x, float y, float w, float h)
    {
        GameObject root = new GameObject("Dropdown");
        root.transform.SetParent(parent, false);
        RectTransform rootRt = root.AddComponent<RectTransform>();
        rootRt.anchoredPosition = new Vector2(x, y);
        rootRt.sizeDelta = new Vector2(w, h);
        Image rootImg = root.AddComponent<Image>();
        rootImg.color = new Color(0.15f, 0.08f, 0.02f);
        TMP_Dropdown dd = root.AddComponent<TMP_Dropdown>();
        dd.targetGraphic = rootImg;

        GameObject label = new GameObject("Label");
        label.transform.SetParent(root.transform, false);
        RectTransform lRt = label.AddComponent<RectTransform>();
        lRt.anchorMin = Vector2.zero;
        lRt.anchorMax = Vector2.one;
        lRt.offsetMin = new Vector2(12, 2);
        lRt.offsetMax = new Vector2(-30, -2);
        TextMeshProUGUI lTmp = label.AddComponent<TextMeshProUGUI>();
        lTmp.fontSize = 18;
        lTmp.color = Color.white;
        lTmp.alignment = TextAlignmentOptions.Left;
        lTmp.enableWordWrapping = false;
        dd.captionText = lTmp;

        GameObject arrow = new GameObject("Arrow");
        arrow.transform.SetParent(root.transform, false);
        RectTransform aRt = arrow.AddComponent<RectTransform>();
        aRt.anchorMin = new Vector2(1, 0.5f);
        aRt.anchorMax = new Vector2(1, 0.5f);
        aRt.anchoredPosition = new Vector2(-16, 0);
        aRt.sizeDelta = new Vector2(20, 20);
        TextMeshProUGUI aTmp = arrow.AddComponent<TextMeshProUGUI>();
        aTmp.text = "▼";
        aTmp.fontSize = 14;
        aTmp.color = Color.white;
        aTmp.alignment = TextAlignmentOptions.Center;

        GameObject template = new GameObject("Template");
        template.transform.SetParent(root.transform, false);
        RectTransform tRt = template.AddComponent<RectTransform>();
        tRt.anchorMin = new Vector2(0, 0);
        tRt.anchorMax = new Vector2(1, 0);
        tRt.pivot = new Vector2(0.5f, 1f);
        tRt.anchoredPosition = new Vector2(0, -2);
        tRt.sizeDelta = new Vector2(0, 200);
        Image tImg = template.AddComponent<Image>();
        tImg.color = new Color(0.08f, 0.04f, 0.01f, 1f);
        ScrollRect sr = template.AddComponent<ScrollRect>();
        template.AddComponent<Mask>().showMaskGraphic = true;

        GameObject viewport = new GameObject("Viewport");
        viewport.transform.SetParent(template.transform, false);
        RectTransform vpRt = viewport.AddComponent<RectTransform>();
        vpRt.anchorMin = Vector2.zero;
        vpRt.anchorMax = Vector2.one;
        vpRt.offsetMin = vpRt.offsetMax = Vector2.zero;
        viewport.AddComponent<Image>().color = Color.clear;
        viewport.AddComponent<Mask>().showMaskGraphic = false;
        sr.viewport = vpRt;

        GameObject content = new GameObject("Content");
        content.transform.SetParent(viewport.transform, false);
        RectTransform cRt = content.AddComponent<RectTransform>();
        cRt.anchorMin = new Vector2(0, 1);
        cRt.anchorMax = new Vector2(1, 1);
        cRt.pivot = new Vector2(0.5f, 1f);
        cRt.anchoredPosition = Vector2.zero;
        cRt.sizeDelta = new Vector2(0, 32);
        sr.content = cRt;

        GameObject item = new GameObject("Item");
        item.transform.SetParent(content.transform, false);
        RectTransform iRt = item.AddComponent<RectTransform>();
        iRt.anchorMin = new Vector2(0, 0.5f);
        iRt.anchorMax = new Vector2(1, 0.5f);
        iRt.sizeDelta = new Vector2(0, 32);
        Toggle iToggle = item.AddComponent<Toggle>();

        GameObject itemBg = new GameObject("Item Background");
        itemBg.transform.SetParent(item.transform, false);
        RectTransform ibRt = itemBg.AddComponent<RectTransform>();
        ibRt.anchorMin = Vector2.zero;
        ibRt.anchorMax = Vector2.one;
        ibRt.offsetMin = ibRt.offsetMax = Vector2.zero;
        Image ibImg = itemBg.AddComponent<Image>();
        ibImg.color = new Color(0.12f, 0.07f, 0.02f, 1f);

        GameObject itemCheck = new GameObject("Item Checkmark");
        itemCheck.transform.SetParent(item.transform, false);
        RectTransform icRt = itemCheck.AddComponent<RectTransform>();
        icRt.anchorMin = new Vector2(0, 0.5f);
        icRt.anchorMax = new Vector2(0, 0.5f);
        icRt.sizeDelta = new Vector2(20, 20);
        icRt.anchoredPosition = new Vector2(14, 0);
        Image icImg = itemCheck.AddComponent<Image>();
        icImg.color = new Color(0.9f, 0.65f, 0.1f);

        GameObject itemLabel = new GameObject("Item Label");
        itemLabel.transform.SetParent(item.transform, false);
        RectTransform ilRt = itemLabel.AddComponent<RectTransform>();
        ilRt.anchorMin = Vector2.zero;
        ilRt.anchorMax = Vector2.one;
        ilRt.offsetMin = new Vector2(35, 2);
        ilRt.offsetMax = new Vector2(-5, -2);
        TextMeshProUGUI ilTmp = itemLabel.AddComponent<TextMeshProUGUI>();
        ilTmp.fontSize = 17;
        ilTmp.color = Color.white;
        ilTmp.alignment = TextAlignmentOptions.Left;
        ilTmp.enableWordWrapping = false;

        iToggle.targetGraphic = ibImg;
        iToggle.graphic = icImg;
        iToggle.isOn = false;
        dd.itemText = ilTmp;
        dd.template = tRt;
        template.SetActive(false);

        return dd;
    }

    Toggle MakeToggle(Transform parent, float x, float y)
    {
        GameObject root = new GameObject("Toggle");
        root.transform.SetParent(parent, false);
        RectTransform rt = root.AddComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, y);
        rt.sizeDelta = new Vector2(56, 30);
        Image bgImg = root.AddComponent<Image>();
        bgImg.color = new Color(0.2f, 0.12f, 0.04f);

        GameObject check = new GameObject("Checkmark");
        check.transform.SetParent(root.transform, false);
        RectTransform cRt = check.AddComponent<RectTransform>();
        cRt.anchorMin = new Vector2(0.1f, 0.1f);
        cRt.anchorMax = new Vector2(0.9f, 0.9f);
        cRt.offsetMin = cRt.offsetMax = Vector2.zero;
        Image cImg = check.AddComponent<Image>();
        cImg.color = new Color(0.9f, 0.65f, 0.1f);

        Toggle toggle = root.AddComponent<Toggle>();
        toggle.targetGraphic = bgImg;
        toggle.graphic = cImg;
        return toggle;
    }

    void MakeCloseButton(Transform parent, float x, float y)
    {
        GameObject root = new GameObject("CloseBtn");
        root.transform.SetParent(parent, false);
        RectTransform rt = root.AddComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, y);
        rt.sizeDelta = new Vector2(260, 55);
        Image img = root.AddComponent<Image>();
        img.color = new Color(0.45f, 0.08f, 0.03f);

        GameObject label = new GameObject("Text");
        label.transform.SetParent(root.transform, false);
        RectTransform lRt = label.AddComponent<RectTransform>();
        lRt.anchorMin = Vector2.zero;
        lRt.anchorMax = Vector2.one;
        lRt.offsetMin = lRt.offsetMax = Vector2.zero;
        TextMeshProUGUI tmp = label.AddComponent<TextMeshProUGUI>();
        tmp.text = "CLOSE";
        tmp.fontSize = 24;
        tmp.fontStyle = FontStyles.Bold;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = new Color(0.95f, 0.82f, 0.45f);

        Button btn = root.AddComponent<Button>();
        btn.targetGraphic = img;
        btn.onClick.AddListener(CloseSettings);
    }
}