﻿using HotelClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelClassLibrary.Utilities
{
    // Класс Утилиты
    public static class Utils
    {
        #region Утилиты

        public static Random rand = new Random();


        // генерация вещественного числа (min, max]
        public static double GetRand(double min, double max)
        {
            // если диапазон не корректен
            if (min.CompareTo(max) > 0 || max.CompareTo(min) < 0)
                throw new Exception("Utils.GetRand(double min, double max): минимум не может быть больше максимума");

            // число
            double num;

            // генерация числа
            do
            {
                num = rand.Next((int)min, (int)max - 1) + rand.NextDouble();
            } while (num.CompareTo(min) < 0 || num.CompareTo(max) > 0);

            return num;
        }


        // генерация целого числа (min, max]
        public static int GetRand(int min, int max)
        {
            // если диапазон не корректен
            if (min.CompareTo(max) > 0 || max.CompareTo(min) < 0)
                throw new Exception("Utils.GetRand(int min, int max): минимум не может быть больше максимума");

            return rand.Next(min, max);
        }

        // коллекция персон
        public static (string Surname, string Name, string Patronymic)[] persons = new[]{
            ("Сергеева",            "Полина",           "Артемьевна"),
            ("Миронов",             "Василий",          "Владиславович"),
            ("Новиков",             "Егор",             "Леонидович"),
            ("Прохорова",           "Мария",            "Дмитриевна"),
            ("Ефимова",             "Софья",            "Артёмовна"),
            ("Никитин",             "Александр",        "Александрович"),
            ("Семенов",             "Павел",            "Никитич"),
            ("Карпов",              "Максим",           "Иванович"),
            ("Воробьева",           "Кристина",         "Яновна"),
            ("Королева",            "Вера",             "Родионовна"),
            ("Денисова",            "Ева",              "Дмитриевна"),
            ("Алешина",             "Милана",           "Максимовна"),
            ("Громов",              "Артём",            "Артёмович"),
            ("Филатова",            "Ева",              "Максимовна"),
            ("Титов",               "Илья",             "Кириллович"),
            ("Фролов",              "Константин",       "Егорович"),
            ("Зубов",               "Артём",            "Даниилович"),
            ("Гришин",              "Андрей",           "Матвеевич"),
            ("Гришин",              "Мирон",            "Родионович"),
            ("Петрова",             "Василиса",         "Ильинична"),
            ("Соколов",             "Семён",            "Сергеевич"),
            ("Макаров",             "Максим",           "Ильич"),
            ("Соколов",             "Михаил",           "Матвеевич"),
            ("Волков",              "Данила",           "Артёмович"),
            ("Иванов",              "Николай",          "Никитич"),
            ("Савельева",           "Адель",            "Маратовна"),
            ("Данилова",            "Валерия",          "Львовна"),
            ("Сотникова",           "Кира",             "Владимировна"),
            ("Белоусова",           "Анна",             "Захаровна"),
            ("Зыков",               "Семён",            "Тимурович"),
            ("Климова",             "Маргарита",        "Владимировна"),
            ("Одинцов",             "Илья",             "Всеволодович"),
            ("Воронков",            "Михаил",           "Михайлович"),
            ("Алексеева",           "Ксения",           "Ярославовна"),
            ("Алексеева",           "Полина",           "Викторовна"),
            ("Леонова",             "Софья",            "Мироновна"),
            ("Фомин",               "Василий",          "Андреевич"),
            ("Власова",             "Анна",             "Алексеевна"),
            ("Завьялов",            "Николай",          "Никитич"),
            ("Кондратьева",         "Аиша",             "Семёновна"),
            ("Рябова",              "Александра",       "Дмитриевна"),
            ("Соколов",             "Виктор",           "Сергеевич"),
            ("Андреева",            "Злата",            "Игоревна"),
            ("Шаповалова",          "Виктория",         "Максимовна"),
            ("Васильев",            "Вадим",            "Дмитриевич"),
            ("Блохина",             "Евгения",          "Максимовна"),
            ("Кузнецова",           "Полина",           "Филипповна"),
            ("Алексеева",           "Николь",           "Романовна"),
            ("Широков",             "Григорий",         "Дмитриевич"),
            ("Кузнецова",           "Варвара",          "Александровна"),
            ("Жуков",               "Ярослав",          "Максимович"),
            ("Емельянова",          "Арина",            "Кирилловна"),
            ("Уварова",             "Аделина",          "Марковна"),
            ("Попова",              "Анастасия",        "Дмитриевна"),
            ("Королев",             "Никита",           "Романович"),
            ("Воронина",            "Виктория",         "Максимовна"),
            ("Рябова",              "Дарья",            "Львовна"),
            ("Королева",            "Майя",             "Максимовна"),
            ("Петрова",             "Ева",              "Фёдоровна"),
            ("Евсеева",             "Амелия",           "Сергеевна"),
            ("Шестаков",            "Василий",          "Николаевич"),
            ("Иванов",              "Михаил",           "Миронович"),
            ("Никитин",             "Данила",           "Максимович"),
            ("Коновалова",          "Дарья",            "Саввична"),
            ("Соколов",             "Матвей",           "Георгиевич"),
            ("Осипов",              "Арсен",            "Тимофеевич"),
            ("Макаров",             "Иван",             "Захарович"),
            ("Кузнецов",            "Степан",           "Артёмович"),
            ("Никитин",             "Али",              "Викторович"),
            ("Фролова",             "Ульяна",           "Викторовна"),
            ("Дубова",              "Екатерина",        "Никитична"),
            ("Громов",              "Михаил",           "Николаевич"),
            ("Иванов",              "Даниил",           "Романович"),
            ("Романов",             "Илья",             "Макарович"),
            ("Фролов",              "Станислав",        "Артёмович"),
            ("Моисеев",             "Дмитрий",          "Львович"),
            ("Столярова",           "Алиса",            "Андреевна"),
            ("Зорина",              "Елена",            "Данииловна"),
            ("Егорова",             "Кристина",         "Николаевна"),
            ("Ермолаев",            "Артём",            "Александрович"),
            ("Щербакова",           "Камила",           "Михайловна"),
            ("Савельева",           "Екатерина",        "Матвеевна"),
            ("Дубровин",            "Александр",        "Дмитриевич"),
            ("Зверева",             "София",            "Арсентьевна"),
            ("Смирнова",            "Серафима",         "Леонидовна"),
            ("Левин",               "Елисей",           "Павлович"),
            ("Беляева",             "Варвара",          "Максимовна"),
            ("Селезнева",           "Мария",            "Львовна"),
            ("Васильева",           "Ева",              "Алексеевна"),
            ("Нефедова",            "Анна",             "Максимовна"),
            ("Семенова",            "Мария",            "Ивановна"),
            ("Денисова",            "Стефания",         "Львовна"),
            ("Коровин",             "Владимир",         "Максимович"),
            ("Виноградова",         "Кира",             "Кирилловна"),
            ("Игнатова",            "Марина",           "Михайловна"),
            ("Толкачева",           "Софья",            "Ильинична"),
            ("Лобанов",             "Демид",            "Данилович"),
            ("Николаева",           "Виктория",         "Романовна"),
            ("Петров",              "Даниил",           "Артёмович"),
            ("Лапшин",              "Алексей" ,         "Артёмович")
        };

        // получить данные персоны
        public static Person GetPerson()
        {
            var person = persons[GetRand(0, persons.Length)];

            return new Person
            {
                Surname = person.Surname,
                Name = person.Name,
                Patronymic = person.Patronymic
            };
        }


        // генерация паспортных данных
        public static string GetPassport() 
        { 
            StringBuilder sb = new StringBuilder();

            Enumerable.Repeat(0, 9).Select(n => GetRand(0, 10)).ToList().ForEach(n => sb.Append($"{n}"));

            return sb.ToString();
        }



        // типы номеров
        public static TypeHotelRoom[] TypesHotelRoom = new[] { 
            new TypeHotelRoom { Name = "Одноместный", CountRooms = 1, Price = 3_500 },
            new TypeHotelRoom { Name = "Двухместный", CountRooms = 2, Price = 4_300 }, 
            new TypeHotelRoom { Name = "Трехместный", CountRooms = 3, Price = 5_500 }
        };


        // города
        public static string[] Cities = new[] { "Киев", "Харьков", "Одесса", "Днепр", "Донецк",
                                                "Запорожье", "Львов", "Кривой Рог" };


        // дни недели
        public static string[] DaysOfWeek = new[] { "Воскресенье", "Понедельник", "Вторник", "Среда",
                                                    "Четверг", "Пятница", "Суббота"
                                                  };

        #endregion

    }
}
