using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using WpfApp2.Models;

namespace WpfApp2;

public partial class MainWindow : Window
{
    public List<Person> people = new List<Person>();
    public MainWindow()
    {
        InitializeComponent();
        List.ItemsSource = people;
    }

    private void Submit_Click(object sender, RoutedEventArgs e)
    {

        string g = "";
        foreach (var item in GStack.Children)
        {
            var gender = item as RadioButton;
            if (gender != null)
            {
                if (gender.IsChecked == true)
                    g = gender.Content.ToString();
            }
        }
        Person p = new Person()
        {
            Name = NameBox.Text,
            Surname = SurnameBox.Text,
            City = ComboBox.Text,
            Gender = g,
            HasStep = CheckBox.IsChecked
        };
        people.Add(p);
        List.Items.Refresh();
        NameBox.Text = string.Empty;
        SurnameBox.Text = string.Empty;
        CheckBox.IsChecked = false;
        ComboBox.Text = string.Empty;
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
        foreach (var item in List.SelectedItems)
        {
            var person = item as Person;
            people.Remove(person);
        }
        List.Items.Refresh();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        var p = List.Items[0] as Person;
        var filename = p.Name;
        var options = new JsonSerializerOptions()
        { WriteIndented = true };
        string json = JsonSerializer.Serialize(List.Items, options);

        File.WriteAllText(filename, json);
        people.Clear();
        List.Items.Refresh();
    }

    private void Load_Click(object sender, RoutedEventArgs e)
    {
        if (!File.Exists(LoadBox.Text))
        {
            MessageBox.Show("This File Doesnt exists");
            LoadBox.Text = "";
            return;
        }
        label.Visibility = Visibility.Hidden;
        string json = File.ReadAllText(LoadBox.Text);
        var op = new JsonSerializerOptions() { WriteIndented = true };
        List<Person> p = JsonSerializer.Deserialize<List<Person>>(json, op);
        people = p;
        List.ItemsSource = null;
        List.ItemsSource = p;
        LoadBox.Text = "";
    }

    private void LoadBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
    {
        label.Visibility = Visibility.Hidden;
    }

    private void label_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
    {
        label.Visibility = Visibility.Visible;
    }
}
