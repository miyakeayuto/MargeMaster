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
        //�J�^���O�X�V����
        var handle = Addressables.UpdateCatalogs();     //�ŐV�̃J�^���O(json)���擾
        yield return handle;

        //�_�E�����[�h���s
        AsyncOperationHandle downloadHandle =
            Addressables.DownloadDependenciesAsync("default", false);
        //�_�E�����[�h��������܂ŃX���C�_�[��UI���X�V
        while(downloadHandle.Status == AsyncOperationStatus.None)
        {
            loadingSlider.value = downloadHandle.GetDownloadStatus().Percent * 100;
            yield return null;
        }
        loadingSlider.value = 100;
        Addressables.Release(downloadHandle);
        Addressables.Release(handle);

        //���̃V�[���Ɉړ�
        SceneManager.LoadScene("StageSelect");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
