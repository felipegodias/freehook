using System;
using System.Collections.Generic;
using System.Reflection;

using UnityEditor;

[CustomEditor(typeof(Achievement), true)]
public class AchievementEditor : Editor
{

    private SerializedProperty m_achievementKeyProperty;

    private int m_index;
    private Dictionary<string, string> m_achievements;
    private List<string> m_achievementsList;

    private void OnEnable()
    {
        m_achievements = new Dictionary<string, string>();
        m_achievementsList = new List<string>();
        m_achievementKeyProperty = serializedObject.FindProperty("m_achievement");
        Type type = typeof(GPGSIds);
        FieldInfo[] fields = type.GetFields();
        foreach (FieldInfo field in fields)
        {
            if (!field.Name.StartsWith("achievement"))
            {
                continue;
            }

            m_achievements.Add(field.Name, (string) field.GetValue(null));
            m_achievementsList.Add(field.Name);
        }

        string currentValue = m_achievementKeyProperty.stringValue;
        foreach (KeyValuePair<string, string> keyValuePair in m_achievements)
        {
            if (keyValuePair.Value == currentValue)
            {
                m_index = m_achievementsList.IndexOf(keyValuePair.Key);
            }
        }
    }

    public override void OnInspectorGUI()
    {
        SerializedProperty ii = serializedObject.GetIterator();
        while (true)
        {
            if (ii.name != "Base")
            {
                if (ii.name != "m_achievement")
                {
                    EditorGUILayout.PropertyField(ii);
                }
                else
                {
                    int index = EditorGUILayout.Popup(ii.displayName, m_index, m_achievementsList.ToArray());
                    if (m_index != index)
                    {
                        m_achievementKeyProperty.stringValue = m_achievements[m_achievementsList[index]];
                        m_index = index;
                    }
                }
            }

            bool hasNext = ii.NextVisible(true);
            if (!hasNext)
            {
                break;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

}