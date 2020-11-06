using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfOrganizer : MonoBehaviour
{
    [SerializeField] private GameObject _prefabsBook;
    [SerializeField] private int _numbersBooks;
    [SerializeField] private int _distanceBetweenBooks;
    
    private void Awake()
    {       
        //Запустить для быстрой расстановки книг потом скопировать из эдитора
        float positionOffsetX = 0;
        for (int i = 0; i < _numbersBooks; i++)
        {
            if (i != 0)
            {
                positionOffsetX += _distanceBetweenBooks;
            }
            GameObject currentBook = Instantiate(_prefabsBook, this.transform);
            currentBook.transform.localPosition = new Vector3(positionOffsetX, currentBook.transform.localPosition.y, currentBook.transform.localPosition.z);
        }
    }
}
