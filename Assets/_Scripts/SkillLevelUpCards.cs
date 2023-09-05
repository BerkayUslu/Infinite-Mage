using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillLevelUpCards : MonoBehaviour
{
    private List<PlayerSkillSettings> _skillList;
    private List<Button> _buttonGameObjectList;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        // Loop through all child transforms
        for (int i = 0; i < _transform.childCount; i++)
        {
            _buttonGameObjectList.Add(_transform.GetChild(i).GetComponent<Button>());
        }
    }
}
