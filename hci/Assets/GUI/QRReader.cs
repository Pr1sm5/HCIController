using System;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class QRReader : MonoBehaviour
{
    [SerializeField] private RawImage _rawImageBackground;
    [SerializeField] private AspectRatioFitter _aspectRatioFitter;
    [SerializeField] private TextMeshProUGUI _textOut;
    [SerializeField] private RectTransform _scanZone;

    private bool _isCamAvailable;
    private WebCamTexture _cameraTexture;
    IPData _ipData = IPData.Instance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUpCamera();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            _isCamAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                _cameraTexture = new WebCamTexture(devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);
            }
        }

        _cameraTexture.Play();
        _rawImageBackground.texture = _cameraTexture;
        _isCamAvailable = true;
    }

    private void UpdateCameraRender()
    {
        if (_isCamAvailable == false) return;
        float ratio = (float)_cameraTexture.width / (float)_cameraTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = -_cameraTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
        
    }
    
    

    private void Scan()
    {
        try
        {
            byte[] pixelData = new byte[_cameraTexture.width * _cameraTexture.height * 4];
            Color32[] color32Data = _cameraTexture.GetPixels32();

            for (int i = 0; i < color32Data.Length; i++)
            {
                pixelData[i * 4] = color32Data[i].r;    // Red channel
                pixelData[i * 4 + 1] = color32Data[i].g; // Green channel
                pixelData[i * 4 + 2] = color32Data[i].b; // Blue channel
                pixelData[i * 4 + 3] = color32Data[i].a; // Alpha channel
            }
            
            
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(pixelData, _cameraTexture.width, _cameraTexture.height, RGBLuminanceSource.BitmapFormat.RGBA32);

            if (result != null)
            {
                _textOut.text = result.Text;
                SplitQRString();
            }
            else
            {
                _textOut.text = "Failed to Read code1";
            }
        }
        catch
        {
            _textOut.text = "Failed to Scan QR code2";
        }
    }

    //Split the QR code and load it into the IPData Instance
    private void SplitQRString()
    {
        try
        {
            // Ensure _textOut is not null
            if (_textOut == null)
            {
                Debug.LogError("_textOut is not assigned.");
                return;
            }

            // Ensure IPData.Instance is not null
            if (IPData.Instance == null)
            {
                Debug.LogError("IPData instance is null.");
                return;
            }

            string ip;
            int port;

            // Split the text by comma
            string[] splitText = _textOut.text.Split(',');

            // Ensure there are exactly two parts in the split text
            if (splitText.Length != 2)
            {
                Debug.LogError("Invalid QR code format. Expected 'ip,port'.");
                return;
            }

            ip = splitText[0].Trim();
            string portStr = splitText[1].Trim();

            // Check if the IP is non-empty
            if (string.IsNullOrEmpty(ip))
            {
                Debug.LogError("IP address is empty.");
                return;
            }

            // Try to parse the port string into an integer
            if (!int.TryParse(portStr, out port))
            {
                Debug.LogError("Invalid port number: " + portStr);
                return;
            }

            // Set the IP and port in the IPData singleton instance
            IPData.Instance.ip = ip;
            IPData.Instance.port = port;
        }
        catch (Exception e)
        {
            Debug.LogError("Error while splitting QR string: " + e.Message);
        }
    }

    public void OnClickScan()
    {
        Scan();
    }
}
