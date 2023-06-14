using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AILogic_Handler))]
public class AILogic_HandlerEditor : Editor
{
    AILogic_Handler _handler;
    List<AI_State> _states = new List<AI_State>();

    List<System.Type> _actionTypes;
    string[] _actionTypeOptions;

    void OnEnable()
    {
        _handler = target as AILogic_Handler;
        _handler.GetComponentsInChildren(_states);

        System.Type baseActionType = typeof(AITakeAction);
        _actionTypes = new List<System.Type>(baseActionType.Assembly.GetTypes());
        _actionTypes.RemoveAll(a => a == baseActionType || !baseActionType.IsAssignableFrom(a));

        _actionTypeOptions = new string[_actionTypes.Count + 1];
        _actionTypeOptions[0] = "Add Action";

        for(int i = 0; i < _actionTypes.Count; ++i)
            _actionTypeOptions[i + 1] = _actionTypes[i].Name;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AI_State markedForDeletion = null;

        EditorGUILayout.LabelField("States:");
        
        foreach(var state in _states)
        {
            if(Application.isPlaying && _handler.currentState == state)
                GUI.color = Color.green;

            if(Application.isPlaying)
            {
                EditorGUILayout.ObjectField(state, typeof(AI_State), true);
            }
            else
            {
                using(var horiz = new GUILayout.HorizontalScope())
                {
                    string stateName = EditorGUILayout.TextField(state.name);
                    if(stateName != state.name)
                    {
                        state.name = stateName;
                        EditorUtility.SetDirty(state);
                    }

                    if(GUILayout.Button("X"))
                    {
                        //Don't delete immediately since we're in the middle of iterating over the states
                        markedForDeletion = state;
                    }
                }
            }

            EditorGUI.indentLevel++;
            {
                EditorGUILayout.LabelField("Actions:");
                
                EditorGUI.indentLevel++;
                {
                    foreach(var action in state._takeActions)
                    {
                        EditorGUILayout.ObjectField(action, typeof(AITakeAction), true);
                    }

                    if(!Application.isPlaying)
                    {
                        int addIndex = EditorGUILayout.Popup(0, _actionTypeOptions);

                        if(addIndex != 0)
                        {
                            var newAction = state.gameObject.AddComponent(_actionTypes[addIndex -1]) as AITakeAction;
                            state._takeActions.Add(newAction);
                            EditorUtility.SetDirty(state);
                        }
                    }
                }
                EditorGUI.indentLevel--;

                EditorGUILayout.LabelField("Transitions:");
                
                EditorGUI.indentLevel++;
                {
                    foreach(var transition in state._transitions)
                    {
                        Color oldColor = GUI.color;

                        if(Application.isPlaying && _handler.lastState == state && state.lastTransition == transition)
                            GUI.color = Color.red;

                        EditorGUILayout.ObjectField(transition, typeof(AITransitionState), true);
                        GUI.color = oldColor;

                        EditorGUI.indentLevel++;
                        {
                            foreach(var decision in transition.decisionsConsidered)
                                EditorGUILayout.ObjectField(decision, typeof(AIMakeDecisions), true);
                            
                            if(transition.positiveOutput)
                                EditorGUILayout.ObjectField("Positive -> ", transition.positiveOutput, typeof(AI_State), true);
                            if(transition.negativeOutput)
                                EditorGUILayout.ObjectField("Negative ->", transition.negativeOutput, typeof(AI_State), true);
                        }
                        EditorGUI.indentLevel--;
                    }                
                }
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;

            GUI.color = Color.white;
        }

        if(!Application.isPlaying)
        {
            if(GUILayout.Button("Add State"))
            {
                GameObject go = new GameObject();
                go.name = "New State";
                //This will only hold up if we already have a state, so we'd want a better default place to add states
                go.transform.parent = _states[0].transform.parent;

                _states.Add(go.AddComponent<AI_State>());
            }
        }

        if(markedForDeletion)
        {
            _states.Remove(markedForDeletion);
            DestroyImmediate(markedForDeletion.gameObject);
        }
    }
}
