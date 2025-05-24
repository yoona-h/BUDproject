using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEditor.ProjectWindowCallback;

public class MakeCSVfile
{
    // CSV 내용
    /*------------------------------------------------------------------------------------------------------------------------------
     *id : 대사에 붙은 식별id이다. 대사가 끝난 이후에 나올 대사를 가리키거나 선택지에 인한 다른 대사를 가리킬 때 사용한다.
     *type : dialogue와 choice 두가지가 있다. type에 따라 출력될 형식이나 파일을 쓰는 방식이 달라진다.
     *speaker : 화자다. type이 choice라면 무조건 '주인공'이다.
     *text : 실제로 출력될 내용이다. type이 choice라면 선택지로 선택할 내용을 선택지 개수에 따라 ';'로 구분한다.
     *next_id : 다음에 오는 대사의 id이다. 선택지에 따라 불러올 대사는 선택지 개수에 따라 ';'로 구분한다.
     *stat : 해당 대사가 끝나고 난 뒤에 능력치의 변화다. '능력치의 인덱스번호''+또는-''변화할 양' 을 띄어쓰기 없이 적는다.
     *      만약 type이 choice라면 선택한 선택지에 따라 ';'로 구분한다.
     *      마지막 대화에는 -1을 써서 끝났음을 표시한다.
     *event : 해당 대사가 출력될 때 실행시킬 이벤트를 적는다. 사운드 출력, (표정 변경, 애니메이션 등)
     */
    private const string TEMPLATE =
@"id,type,speaker,text,next_id,stat,event
1,dialogue,손님,안녕하세요.,2,,
2,choice,주인공,어서오세요.;영업끝났습니다.,3;4,3+1;3-1,
3,dialogue,손님,(.....),5,,
4,dialogue,손님,그렇군요...죄송합니다.,5,,
5,dialogue,주인공,혹시 고민이 있으신가요?,6,,
6,dialogue,손님,네...,-1,,";

    [MenuItem("Assets/Create/TalkingData CSV", false, 80)]
    public static void CreateCSV()
    {
        string path = GetSelectedFolderPath();
        string defaultName = "NewTalkingData.csv";
        string fullPath = Path.Combine(path, defaultName);

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            ScriptableObject.CreateInstance<CSVNameEditAction>(),
            fullPath,
            EditorGUIUtility.IconContent("TextAsset Icon")?.image as Texture2D,
            null
        );
    }

    private static string GetSelectedFolderPath()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(path)) return "Assets";
        if (Directory.Exists(path)) return path;
        return Path.GetDirectoryName(path);
    }

    private class CSVNameEditAction : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            File.WriteAllText(pathName, TEMPLATE, new UTF8Encoding(false));
            AssetDatabase.ImportAsset(pathName);
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(pathName);
            ProjectWindowUtil.ShowCreatedAsset(obj);
        }
    }
}