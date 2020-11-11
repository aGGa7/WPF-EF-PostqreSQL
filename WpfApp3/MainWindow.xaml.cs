using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<User> UserUpdateList; // Чтобы включить отслеживание изменений коллекции, необходимо использовать коллекцию, реализующую интерфейс INotifyCollectionChanged
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void LoadDB (object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                UserUpdateList = new ObservableCollection<User>(db.Users); //коллекция необходима т.к. элемент datagrid хранит все изменения в привязанной коллекции, в эту коллекцию сначала загружаем данные из контекста данных
                                                                                                         //db.Users является отложенным запросом LINQ, Метод ToList() указывает запросу то, что он должен быть вызван сразу же и полученные данные должны 
                                                                                                         //быть преобразованы в коллекцию C#, реализующую интерфейс IList<T>. Теперь запрос к базе данных будет выполнен один раз.
                datalist.ItemsSource = UserUpdateList; //а здесь эту коллекцию привязываем к элементу датагрид (привязка столбцов к свойствам DbSET выполнена в XAML, также там отключена автогенерация столбцов для того чтобы данные повторно не отображались)
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e) //добавление записи
        {  
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (User user in UserUpdateList) //для одновления/добавления пользователя идем ссылкой по коллекции
                {
                    db.Update(user);//обновляем данные

                    if (!db.Users.Contains(user)) //если в контексте денных такой пользователь отсутвует
                        db.Users.Add(user); //добавляем объект в контекстданных
                }
                db.SaveChanges(); //сохраняем изменения в бд
            }    

        }

        private void Delete_Click(object sender, RoutedEventArgs e) //удаление записи
        {
            User deleteuser = (User) datalist.SelectedItem; //по выделению в DataGrid получаем объект для удаления, производя явное приведение
            if (deleteuser != null)
            {
                UserUpdateList.Remove(deleteuser); //сначала удаляем в коллекции чтобы данные в  DataGRID также удалились
                using (ApplicationContext db = new ApplicationContext())
                { 
                    db.Users.Remove(deleteuser); //удаляем в контексте
                    db.SaveChanges(); // сохраняем изменения
                }
            }
        }
    }
}
