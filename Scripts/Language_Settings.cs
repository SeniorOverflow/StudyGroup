using System;
using System.Collections.Generic;
namespace  StudyGroup.Script
{
     class    Language_Settings
    {
       private static List<List<string>> words_array = new  List<List<string>>();
       
       public static void Initialization()
       {
            words_array.Add(new List<string>{"Sing In","Войти"} );                                  //0
            words_array.Add(new List<string>{"Download","Скачать"} );                               //1
            words_array.Add(new List<string>{"Search","Поиск"} );                                   //2
            words_array.Add(new List<string>{"Open","Открыть"} );                                   //3
            words_array.Add(new List<string>{"Registration","Регистрация"} );                       //4
            words_array.Add(new List<string>{"Log Out","Выйти"} );                                  //5
            words_array.Add(new List<string>{"Admin Tools","Инструменты Админа"} );                 //6
            words_array.Add(new List<string>{"Your Company","Ваша компания"} );                     //7
            words_array.Add(new List<string>{"Management","Управление"} );                          //8
            words_array.Add(new List<string>{"Categories ","Категории"} );                          //9
            words_array.Add(new List<string>{"Developers","Разработчики"} );                        //10
            words_array.Add(new List<string>{"Products","Товары"} );                                //11
            words_array.Add(new List<string>{"Roles","Роли"} );                                     //12
            words_array.Add(new List<string>{"Subscriptions","Подписки"} );                         //13
            words_array.Add(new List<string>{"Transactions","Сделки"} );                            //14
            words_array.Add(new List<string>{"Users","Пользователи"} );                             //15
            words_array.Add(new List<string>{"Reviews ","Отзывы"} );                                //16
            words_array.Add(new List<string>{"Profile"," Профиль"} );                               //17
            words_array.Add(new List<string>{"Cart","Корзина"} );                                   //18
            words_array.Add(new List<string>{"Picture","Изображение"} );                            //19
            words_array.Add(new List<string>{"Labels","Метки"} );                                   //20
            words_array.Add(new List<string>{"Events","События"} );                                 //21
            words_array.Add(new List<string>{"Fisrst and second Name","Имя и Фамилия "} );          //22
            words_array.Add(new List<string>{"Description","Описание"} );                           //23
            words_array.Add(new List<string>{"First name","Имя"} );                                 //24
            words_array.Add(new List<string>{"Second name","Фамилия"} );                            //25
            words_array.Add(new List<string>{"Add Review","Добавить отзыв"} );                      //26
            words_array.Add(new List<string>{"Delete","Удалить"} );                                 //27
            words_array.Add(new List<string>{"Login","Логин"} );                                    //28
            words_array.Add(new List<string>{"Password","Пароль"} );                                //29
            words_array.Add(new List<string>{"E-mail","Электронная почта"} );                       //30
            words_array.Add(new List<string>{"Create","Создать"} );                                 //31
            words_array.Add(new List<string>{"We'll never share your login with anyone else",
                                             "Мы никогда не передадим ваш логин кому-либо еще"} );  //32
            words_array.Add(new List<string>{"Editing Sub.","Редак. подписок"} );                   //33    
            words_array.Add(new List<string>{"Add","Добавить"} );                                   //34  
            words_array.Add(new List<string>{"Price","Цена"} );                                     //35  
            words_array.Add(new List<string>{"Add to Cart","Добавить в корзину"} );                 //36      
            words_array.Add(new List<string>{"Add Review","Добавить отзыв"} );                      //37 
            words_array.Add(new List<string>{"Like","Понравилось"} );                               //38  
            words_array.Add(new List<string>{"Remove Like","Удалить Лайк"} );                       //39  
            words_array.Add(new List<string>{"Category","Категория"} );                             //40  
            words_array.Add(new List<string>{"Labels","Метки"} );                                   //41  
            words_array.Add(new List<string>{"Events","События"} );                                 //42  
            words_array.Add(new List<string>{"Date event","Дата события"} );                        //43  
            words_array.Add(new List<string>{"Show","Показать"} );                                  //44   
            words_array.Add(new List<string>{"Assessment","Оценка"} );                              //45 
            words_array.Add(new List<string>{"Terrible","Ужастно"} );                               //46
            words_array.Add(new List<string>{"Bad","Плохо"} );                                      //47 
            words_array.Add(new List<string>{" Not Bad","Не плохо"} );                              //48 
            words_array.Add(new List<string>{"Good","Хорошо"} );                                    //49 
            words_array.Add(new List<string>{"Cool","Замечательно"} );                              //50 
            words_array.Add(new List<string>{"Write your review","Напишите ваш отзыв"} );           //51 
            words_array.Add(new List<string>{"Your Score","Ваш счет"} );                            //52 
            words_array.Add(new List<string>{"Your Bonus Score","Ваш бонусный счет"} );             //53 
            words_array.Add(new List<string>{"Your LvL","Ваш уровень"} );                           //54 
            words_array.Add(new List<string>{"Edit your data","Редактировать ваши данные"} );       //55 
            words_array.Add(new List<string>{"Create your company","Создать компанию"} );           //56 
            words_array.Add(new List<string>{"Your Item","Инвентарь "} );                           //57
            words_array.Add(new List<string>{"Library","Библиотека "} );                            //58
            words_array.Add(new List<string>{"Items","Предметы "} );                                //59
            words_array.Add(new List<string>{"Your friend","Ваши друзья "} );                       //60
            words_array.Add(new List<string>{"Friends","Друзья "} );                                //61
            words_array.Add(new List<string>{"Product reviews","Отзывы на продукт"} );              //62
            words_array.Add(new List<string>{"Count","Кол-во"} );                                   //63
            words_array.Add(new List<string>{"Buy","Купить "} );                                    //64
            words_array.Add(new List<string>{"Company","Компания"} );                               //65
            words_array.Add(new List<string>{"Date of deal","Дата сделки"} );                       //66
            words_array.Add(new List<string>{"Add to Lib","Добавить"} );                            //67
            words_array.Add(new List<string>{"Give friend",""} );                                   //68
            words_array.Add(new List<string>{"Edit your team","Редактировать компанию"} );          //69
            words_array.Add(new List<string>{"Your products","Ваши продукты"} );                    //70
            words_array.Add(new List<string>{"Delete your Company","Удалить компанию"} );           //71
            words_array.Add(new List<string>{"Count products","Кол-во продуктов"} );                //72
            words_array.Add(new List<string>{"Edit","Редактировать"} );                             //73
            words_array.Add(new List<string>{"Phone","Телефон"} );                                  //74
            words_array.Add(new List<string>{"Score","Счет"} );                                     //75
            words_array.Add(new List<string>{"Give","Назначить"} );                                 //76
            words_array.Add(new List<string>{"Add New Role"," Добавить роль"} );                    //77
            words_array.Add(new List<string>{"User Login","Логин"} );                               //78
            words_array.Add(new List<string>{"LvL","Уровень"} );                                    //79
            words_array.Add(new List<string>{"Bonus Score"," Бонусный счет"} );                     //80
            words_array.Add(new List<string>{"Block","Заблокировать"} );                            //81
            words_array.Add(new List<string>{"Type of Subscription"," Типы подписок"} );            //82
            words_array.Add(new List<string>{"Add Subscription","Добавить подписку"} );             //83
            words_array.Add(new List<string>{"Subscribe","Подписаться"} );                          //84
            words_array.Add(new List<string>{"Name of sub","Название подписки"} );                  //85
            words_array.Add(new List<string>{"Date End"," Дата окончания"} );                       //86
            words_array.Add(new List<string>{"Count days","кол-во дней"} );                         //87
            words_array.Add(new List<string>{"",""} );                                              //88
            words_array.Add(new List<string>{"",""} );                                              //89
                                             
       }

       public static  List<string> GetWords(int id_language = 0)
       {
            List<string> words = new List<string>();
            foreach(List<string> a in words_array)
            {
                 words.Add(a[id_language]);
            }
            return words;
       }
       

    }
}