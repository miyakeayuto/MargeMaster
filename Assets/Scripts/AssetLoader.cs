using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AssetLoader : MonoBehaviour
{
    [SerializeField] Slider loadingSlider;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loading());
    }

    IEnumerator loading()
    {
        //カタログ更新処理
        var handle = Addressables.UpdateCatalogs();     //最新のカタログ(json)を取得
        yield return handle;

        //ダウンロード実行
        AsyncOperationHandle downloadHandle =
            Addressables.DownloadDependenciesAsync("default", false);
        //ダウンロード完了するまでスライダーのUIを更新
        while(downloadHandle.Status == AsyncOperationStatus.None)
        {
            loadingSlider.value = downloadHandle.GetDownloadStatus().Percent * 100;
            yield return null;
        }
        loadingSlider.value = 100;
        Addressables.Release(downloadHandle);
        Addressables.Release(handle);

        //次のシーンに移動
        SceneManager.LoadScene("StageSelect");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
