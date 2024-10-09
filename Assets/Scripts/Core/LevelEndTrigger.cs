using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private int nextLevelIndex; // ������ ���������� ������ � Build Settings

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���������� ��������� ������ ����� ��������� ���������� ������
            collision.gameObject.GetComponent<Health>().Reset();
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1; // ��������� �������
        GameDataManager.SaveLocationData(nextLevelIndex, ""); // ��������� ������ ������ ��� ����������� �����
        LoadingManager.instance.LoadDefineLevel(nextLevelIndex);
        //SceneManager.LoadScene(nextLevelIndex, LoadSceneMode.Single);
    }

}
