using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private Transform _trainView;
    [SerializeField] private Transform _cenitalView;

    public void TrainView()
    {
        iTween.MoveTo(_parent, _trainView.position, 2f);
        iTween.RotateTo(this.gameObject, _trainView.rotation.eulerAngles, 2f);
    }

    public void CenitalView()
    {
        iTween.MoveTo(_parent, _cenitalView.position, 2f);
        iTween.RotateTo(this.gameObject, _cenitalView.rotation.eulerAngles, 2f);
    }
}
