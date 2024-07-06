using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace MoralisSDK.Tools
{
    public class ImageDownloader
    {
        public static async UniTask<Texture2D> DownloadImage(string url)
        {
            if (url.Contains("ipfs://"))
            {
                url = url.Replace("ipfs://", "https://ipfs.io/ipfs/");
            }
            if (url.Contains("data:image/svg"))
            {
                Debug.LogWarning("ImageDownloader does not support SVG");
                return null;
            }
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

            // Send the web request
            var operation = request.SendWebRequest();
            await UniTask.WaitUntil(() => operation.isDone);

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                return null;
            }
            else
            {
                return ((DownloadHandlerTexture)request.downloadHandler).texture;

            }
        }
    }
}
