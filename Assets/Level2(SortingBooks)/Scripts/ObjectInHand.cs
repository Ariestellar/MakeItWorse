using UnityEngine;

//Компонент вешается на объекты которые можно подбирать
//Содержит поле обозначающее тип цвета для сравнения логики проигрыша/выйгрыша
public class ObjectInHand : MonoBehaviour
{
    [SerializeField] private ColorType _colorType;
    public ColorType ColorType { get => _colorType; }
}
