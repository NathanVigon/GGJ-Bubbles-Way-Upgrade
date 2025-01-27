using TMPro;
using UnityEngine;

public class DropDownToVisibility : MonoBehaviour {
    private TMP_Dropdown dropdown;
    public bool forVisibility;

    void Start() {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    void OnDropdownValueChanged(int index) {
        if (forVisibility) {
            GridManager.Instance.ChangeVisibility((Visibility)index);
        } else {
            GridManager.Instance.ChangeAlpha((AlphaGrid)index);
        }
    }
}

public enum Visibility {
    Visible,
    EditMode,
    Hidden
}

public enum AlphaGrid {
    Full,
    Middle,
    Little
}
