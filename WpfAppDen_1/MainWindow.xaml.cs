using System;
using System.Collections.Generic;
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
using Library;

namespace WpfAppDen_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Zatrat zatrat = new Zatrat(); // Создание объекта библиотеки 

        private int wages; // Переменная wages для хранения начисления на зар. плату
        private int office_eq; // Переменная office_eq для хранения затрат оргтехники
        private int electr; // Переменная electr для хранения затрат на электроэнергию
        private int deprec; // Переменная deprec для хранения амортизаии 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_1_Click(object sender, RoutedEventArgs e) // Кнопка для вычисления начисления зар заработной платы
        {
            int time_spent; // Время, затраченное на разработку проекта
            if (!int.TryParse(textBox_timeRaz.Text, out time_spent)) // Проверка на введенные символы
            {
                MessageBox.Show("Время разработки должна принимать число"); 
                textBox_timeRaz.Clear(); // Очистка поля time_spent
                return;
            }

            if (time_spent < 1300)
            {
                MessageBox.Show("Время разработки продукта превышает его норму");
                textBox_timeRaz.Clear();
                return;
            }

            int salary; // Оклад разработчика
            if (!int.TryParse(textBox_OkladRaz.Text, out salary))
            {
                MessageBox.Show("Оклад разработчика должен принимать число"); 
                textBox_OkladRaz.Clear(); // Очистка поля salary
                return;
            }

            if (salary < 200000)
            {
                MessageBox.Show("Оклад разработчика превышает норму");
                textBox_OkladRaz.Clear(); 
                return;
            }

            wages = zatrat.wages(time_spent, salary); //Метод для расчета заработной платы
            listboxRes.Items.Clear();
            listboxRes.Items.Add($"Начисления на заработную плату: {wages} руб."); // Добавления wages в listbox
        }

        private void button_2_Click(object sender, RoutedEventArgs e) // Кнопка для вычисления использования оргтехники
        {
            listboxRes.Items.Clear();
            int T; // Машинное время работы
            if (!int.TryParse(textBox_MashinTime.Text, out T))
            {
                MessageBox.Show("Машинное время должно принимать число"); 
                textBox_MashinTime.Clear(); // Очистка поля T
                return;
            }

            if (T < 1300)
            {
                MessageBox.Show("Машинное время должно соответствовать норме");
                textBox_MashinTime.Clear(); // Очистка поля T
                return;
            }

            office_eq = zatrat.office_equipment(T); //Метод для расчета использования оргтехники
            listboxRes.Items.Clear();
            listboxRes.Items.Add($"Затраты с использованием оргтехники {office_eq} руб."); // Добавления office_eq в listbox
        }

        private void button_3_Click(object sender, RoutedEventArgs e) // Кнопка для вычисления затраты на электроэнергии
        {
            listboxRes.Items.Clear();
            int T; // Машинное время работы
            if (!int.TryParse(textBox_MashiTimeEl.Text, out T))
            {
                MessageBox.Show("Машинное время должно принимать число"); 
                textBox_MashiTimeEl.Clear(); // Очистка поля T
                return;
            }

            if (T < 1300)
            {
                MessageBox.Show("Машинное время должно соответствовать норме");
                textBox_MashiTimeEl.Clear(); // Очистка поля T
                return;
            }

            double rate; // Тариф электроэнергии
            if (!double.TryParse(textBox_TarifElectro.Text, out rate))
            {
                MessageBox.Show("Тариф электроэнергии должен принимать число");
                textBox_TarifElectro.Clear(); // Очистка тарифа электроэнергии
                return;
            }

            if (rate < 10)
            {
                MessageBox.Show("Тариф электроэнергии не должен превышать норму");
                textBox_TarifElectro.Clear(); // Очистка тарифа электроэнергии
                return;
            }

            electr = zatrat.eletricity(T, rate); // Метод для расчета затраты на электроэнергию
            listboxRes.Items.Clear();
            listboxRes.Items.Add($"Затраты на электроэнергию: {electr} руб."); // Добавления electr в listbox
        }

        private void button_4_Click(object sender, RoutedEventArgs e) // Кнопка для вычисления амиортизации
        {
            listboxRes.Items.Clear();
            int S; // Первоначальная стоимость ПК
            if (!int.TryParse(textBox_PervonaStoim.Text, out S))
            {
                MessageBox.Show("Первоначальная стоимость должна принимать число");
                textBox_PervonaStoim.Clear();
                return;
            }

            if (S < 100000)
            {
                MessageBox.Show("Первоначальная стоимость не должна быть больше установленной");
                textBox_PervonaStoim.Clear();
                return;
            }

            deprec = zatrat.depreciation(S); // Метод для расчета амортизации
            listboxRes.Items.Clear();
            listboxRes.Items.Add($"Амортизация: {deprec}  руб."); // Добавления deprec в listbox
        }

        private void button_5_Click(object sender, RoutedEventArgs e) // Кнопка для производимых вычисления
        {
            listboxRes.Items.Clear();
            int Itog = zatrat.cost_calculation(wages, office_eq, electr, deprec, wages);
            string Output = $"1.Начисления на заработную плату: {wages} руб.\n2.Затраты с использованием оргтехники {office_eq} руб.\n3.Затраты на электроэнергию: {electr} руб.\n4.Амортизация: {deprec}  руб.\n5.Результат выполненных рассчетов: {Itog}  руб.";
            listboxRes.Items.Add(Output); // Добавления Itog в listbox
        }
    }
}
