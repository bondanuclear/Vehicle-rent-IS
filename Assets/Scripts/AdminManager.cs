using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminManager : MonoBehaviour
{
    UserInfo userInfo;
    ClientForm clientForm;
    private void Awake() {
        clientForm = GetComponent<ClientForm>();
        userInfo = GetComponent<UserInfo>();
    }

    public void CloseUserInfo()
    {
        userInfo.ProcessUserInfo(false);
    }
    public void EvaluateAndDelete()
    {
        StartCoroutine(ProcessDeleteClient());
    }
    IEnumerator ProcessDeleteClient()
    {
        CloseUserInfo();
        yield return null;
        clientForm.DeleteClient();
    }
}
