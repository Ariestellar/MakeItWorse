using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BookSorter : MonoBehaviour, GameLogic
{
    [SerializeField] private Book[] _books;
    //Расстояние между книгами
    private float _distanceBetweenBooks = 0.75f;
    private Book _booksInHand;    
    [SerializeField] private float [] _correctOrderBooksInHeight;
    private Action<StatusGame> _getResultsGame;

    private void Awake()
    {
        _books = GetComponentsInChildren<Book>();
        _correctOrderBooksInHeight = new float[_books.Length];

        for (int i = 0; i < _books.Length; i++)
        {            
            _books[i].SetNumber(i);
            _books[i].ChangeOfPosition += Sort;
            _correctOrderBooksInHeight[i] = _books[i].transform.localScale.y;
        }
        //Запустить для быстрой расстановки книг потом скопировать из эдитора
        /*float positionOffsetX = 0;
        for (int i = 0; i < _books.Length; i++)
        {
            if (i != 0)
            {
                positionOffsetX += _distanceBetweenBooks;
            }
            _books[i].transform.localPosition = new Vector3(positionOffsetX, _books[i].transform.localPosition.y, _books[i].transform.localPosition.z);
        }*/
    }

    public IEnumerator PostponeStartingCounting()
    {
        yield return new WaitForSeconds(1);
        CheckOrderBooks();
    }

    public void SetActionResultsGame(Action<StatusGame> action)
    {
        _getResultsGame = action;
    }

    public void SetBookInHand(Book booksInHand)
    {
        _booksInHand = booksInHand;
    }

    /*
     *Сортировка
     *Пареметр: номер книги на которую указывают
     */
    public void Sort(int number)
    {
        //Если указатель на книге:
        //-меньше чем номер книги в руке то двигаем порядок в право
        if (number < _booksInHand.GetNumber())
        {
            for (int i = number; i < _books.Length; i++)
            {
                if (_books[i].IsHand() == false)//Все книги которые не в руке изменяют порядковый номер
                {
                    _books[i].SetNumber(i + 1);
                }
                else//книга которая в руке занимаетномер той на которую указанно
                {
                    _books[i].SetNumber(number);
                    break;
                }
                
            }
        }
        else//если больше то в лево:
        {
            for (int i = number; i >= 0; i--)
            {
                if (_books[i].IsHand() == false)
                {
                    _books[i].SetNumber(i - 1);
                }
                else
                {
                    _books[i].SetNumber(number);
                    break;
                }
            }
        }

        //Отсортировать массив по порядку:
        _books = _books.OrderBy(Book => Book.GetNumber()).ToArray();

        float positionOffsetX = 0;
        for (int i = 0; i < _books.Length; i++)
        {
            if (i != 0)
            {
                positionOffsetX += _distanceBetweenBooks;
            }
            //Распределить по порядку расположение(Учесть выключенную книгу)
            _books[i].transform.localPosition = new Vector3(positionOffsetX, _books[i].transform.localPosition.y, _books[i].transform.localPosition.z);            
        }
        
    }

    private void CheckOrderBooks()
    {
        int numberNonMatches = 0;
        for (int i = 0; i < _books.Length; i++)
        {
            if (_books[i].transform.localScale.y != _correctOrderBooksInHeight[i])
            {
                numberNonMatches += 1;
            }
        }

        if (numberNonMatches == 0)
        {
            _getResultsGame?.Invoke(StatusGame.DEFEAT);
        }
        else
        {
            _getResultsGame?.Invoke(StatusGame.VICTORY);
        }
    }    
}
