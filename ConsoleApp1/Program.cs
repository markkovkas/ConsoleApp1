using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Notebook
{

    public class Notebook
    {
        public Dictionary<int, Note> allNotes = new Dictionary<int, Note>();
        private List<string> properties = new List<string>()
        {
            "Id", "Surname", "Name", "SecondName", "Phone", "Country", "DateOfBirth", "Organization", "Position", "Remark"
        };

        public static void Main(string[] args)
        {
            Notebook nb = new Notebook();
            nb.Action();
        }

        private void CreateNote()
        {
            Note note = new Note();
            int id = allNotes.Count();
            note.Surname = ReadUntilValidationPass(properties[1]);
            note.Name = ReadUntilValidationPass(properties[2]);
            note.SecondName = ReadUntilValidationPass(properties[3]);
            if (string.IsNullOrWhiteSpace(note.SecondName)) note.SecondName = null;
            note.Phone = ReadUntilValidationPass(properties[4]);
            note.Country = ReadUntilValidationPass(properties[5]);
            if (string.IsNullOrWhiteSpace(note.Country)) note.Country = null;
            note.DateOfBirth = ReadUntilValidationPass(properties[6]);
            if (string.IsNullOrWhiteSpace(note.DateOfBirth)) note.DateOfBirth = null;
            note.Organization = ReadUntilValidationPass(properties[7]);
            if (string.IsNullOrWhiteSpace(note.Organization)) note.Organization = null;
            note.Position = ReadUntilValidationPass(properties[8]);
            if (string.IsNullOrWhiteSpace(note.Position)) note.Position = null;
            note.Remark = ReadUntilValidationPass(properties[9]);
            if (string.IsNullOrWhiteSpace(note.Remark)) note.Remark = null;
            id++;
            note.Id = id;
            allNotes.Add(id, note);
        }

        private void ReadNote()
        {
            Console.Write("Введите Id записи: ");
            bool isNum = int.TryParse(Console.ReadLine(), out int id);
            if (isNum)
            {
                if (allNotes.ContainsKey(id))
                {
                    Console.WriteLine(allNotes[id]);
                }
                else
                {
                    Console.WriteLine("Данной записи не найдено!");
                }
            }
            else
            {
                Console.WriteLine("Введен некорректный идентификатор!");
            }
        }

        private void UpdateNote()
        {
            Console.Write("Укажите ID записи для редактирования: ");
            bool inputIsCorrect = int.TryParse(Console.ReadLine(), out int id);
            while (true)
            {
                if (!inputIsCorrect)
                {
                    Console.WriteLine("Введен некорректный идентификатор!");
                    return;
                }
                else
                {
                    if (!allNotes.ContainsKey(id))
                    {
                        Console.WriteLine("Данной записи не найдено!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine(allNotes[id]);
                        Console.WriteLine("Какое поле необходимо отредактировать?\n\t1 - Фамилия\n\t2 - Имя\n\t3 - Отчество\n\t4 - Телефон\n\t5 - Страна\n\t6 - Дата рождения\n\t7 - Организация\n\t8 - Должность\n\t9 - Примечание");
                        Console.Write("Введите цифру для выбора или cancel для завершения редактирования: ");
                        string s = Console.ReadLine();
                        if (s == "cancel")
                        {
                            return;
                        }
                        inputIsCorrect = int.TryParse(s, out int option);
                        while (!inputIsCorrect || option < 1 || option > 9)
                        {
                            Console.Write("Команда не найдена! Введите ещё раз: ");
                            inputIsCorrect = int.TryParse(Console.ReadLine(), out option);
                        }
                        var optionProperty = new Dictionary<int, string>(){
                        {1, allNotes[id].Surname },
                        {2, allNotes[id].Name },
                        {3, allNotes[id].SecondName },
                        {4, allNotes[id].Phone },
                        {5, allNotes[id].Country },
                        {6, allNotes[id].DateOfBirth },
                        {7, allNotes[id].Organization },
                        {8, allNotes[id].Position },
                        {9, allNotes[id].Remark }
                        };
                        optionProperty[option] = ReadUntilValidationPass(properties[option]);
                        allNotes[id] = new Note(optionProperty[1], optionProperty[2], optionProperty[3], optionProperty[4], optionProperty[5], optionProperty[6], optionProperty[7], optionProperty[8], optionProperty[9], id);
                    }
                }
                Console.Write("Поле изменено! Продолжить редактирование записи? (yes/no): ");
                string input;
                while (true)
                {
                    input = Console.ReadLine();
                    if (input == "yes")
                    {
                        Console.Clear();
                        break;
                    }
                    else if (input == "no")
                    {
                        Console.Clear();
                        return;
                    }
                    else
                    {
                        Console.Write("Пожалуйста введите yes или no: ");
                    }
                }
            }
        }

        private void DeleteNote()
        {
            Console.Write("Введите Id записи для удаления: ");
            bool isNum = int.TryParse(Console.ReadLine(), out int id);
            if (isNum)
            {
                if (allNotes.ContainsKey(id))
                {
                    Console.WriteLine($"Запись {id} удалена!");
                    allNotes.Remove(id);
                }
                else
                {
                    Console.WriteLine("Данной записи не найдено!");
                }
            }
            else
            {
                Console.WriteLine("Введен некорректный идентификатор!");
            }
        }

        private void ShowAllNotes()
        {
            foreach (var note in allNotes)
            {
                Console.WriteLine(note.Value.ToShortString());
            }
        }

        private string ReadUntilValidationPass(string propertyName)
        {
            Console.Write($"Введите {propertyName}: ");
            Validation validation = Note.fieldsValidation[propertyName];
            while (true)
            {
                string input = Console.ReadLine();
                bool inputIsValid = validation.TryValidate(input, out string validationResult);
                if (inputIsValid)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine(validationResult);
                }
            }
        }

        private static void Greetings()
        {
            Console.WriteLine("Добро пожаловать в нашу записную книжку!");
            Console.WriteLine("\t- для создания новой записи введите команду: create.");
            Console.WriteLine("\t- для просмотра записи введите команду: show.");
            Console.WriteLine("\t- для редактирования записи введите команду: edit.");
            Console.WriteLine("\t- для удаления записи введите команду: del.");
            Console.WriteLine("\t- для просмотра списка всех записей введите команду: all.");
            Console.WriteLine("\t- для выхода из программы введите команду: exit.");
        }

        private void Action()
        {
            Greetings();
            bool commandIsCorrect = false;
            string command;
            while (!commandIsCorrect)
            {
                Console.Write("Введите команду: ");
                command = Console.ReadLine();
                switch (command)
                {
                    case "create":
                        //Console.WriteLine("Создание");
                        CreateNote();
                        break;
                    case "show":
                        //Console.WriteLine("Показ");
                        ReadNote();
                        break;
                    case "edit":
                        //Console.WriteLine("Редактирование");
                        UpdateNote();
                        break;
                    case "del":
                        //Console.WriteLine("Удаление");
                        DeleteNote();
                        break;
                    case "all":
                        //Console.WriteLine("Всё");
                        ShowAllNotes();
                        break;
                    case "exit":
                        //Console.WriteLine("Выход");
                        Console.WriteLine("Пока-пока!");
                        return;
                    default:
                        Console.Clear();
                        Console.Write("Данной команды не найдено! Попробуйте ещё раз: \n");
                        break;
                }
            }
        }
    }

    public class Validation
    {
        public bool Required { set; get; }

        public int MinLength { set; get; }

        public int MaxLength { set; get; }

        public char[] ValidSymbols { set; get; }

        public Validation(bool required, int minLength, int maxLength, char[] validSymbols)
        {
            Required = required;
            MinLength = minLength;
            MaxLength = maxLength;
            ValidSymbols = validSymbols;
        }

        public bool TryValidate(string str, out string validationResult)
        {
            validationResult = null;
            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str))
            {
                if (Required)
                {
                    validationResult = "Это поле является обязательным!";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (str.Length < MinLength)
            {
                validationResult = $"Минимальная длина: {MinLength}!";
                return false;
            }
            else if (str.Length > MaxLength)
            {
                validationResult = $"Максимальная длина: {MaxLength}!";
                return false;
            }
            else
            {
                foreach (char ch in str)
                {
                    if (!ValidSymbols.Contains(ch))
                    {
                        validationResult = $"Используйте только: {new string(ValidSymbols)}!";
                        return false;
                    }
                }

                return true;
            }
        }
    }   
    public class Note
    {
        public string Surname { set; get; }

        public string Name { set; get; }

        public string SecondName { set; get; }

        public string Phone { set; get; }

        public string Country { set; get; }

        public string DateOfBirth { set; get; }

        public string Organization { set; get; }

        public string Position { set; get; }

        public string Remark { set; get; }

        public int Id { set; get; }

        public static Dictionary<string, Validation> fieldsValidation;

        static Note()
        {
            string russianSymbols = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string numbers = "0123456789";
            string special = ",.?!()\\+-=№@'\" &$;:^ ";
            char[] russianSpecial = (russianSymbols + special).ToCharArray();
            fieldsValidation = new Dictionary<string, Validation>();
            fieldsValidation.Add("Id", new Validation(true, 1, 10, numbers.ToCharArray()));
            fieldsValidation.Add("Name", new Validation(true, 1, 20, (russianSymbols + " -").ToCharArray()));
            fieldsValidation.Add("Surname", new Validation(true, 1, 20, (russianSymbols + " -").ToCharArray()));
            fieldsValidation.Add("SecondName", new Validation(false, 0, 20, (russianSymbols + " -").ToCharArray()));
            fieldsValidation.Add("Phone", new Validation(true, 5, 11, numbers.ToCharArray()));
            fieldsValidation.Add("Country", new Validation(false, 0, 20, russianSpecial));
            fieldsValidation.Add("DateOfBirth", new Validation(false, 10, 10, (numbers + ".").ToCharArray()));
            fieldsValidation.Add("Organization", new Validation(false, 0, 20, russianSpecial));
            fieldsValidation.Add("Position", new Validation(false, 0, 20, russianSpecial));
            fieldsValidation.Add("Remark", new Validation(false, 0, 200, (russianSymbols + numbers + special).ToCharArray()));
        }

        public Note()
        {
        }

        public Note(string surname, string name, string secondName, string phone, string country, string dateOfBirth, string organization, string position, string remark, int id)
        {
            Surname = surname;
            Name = name;
            SecondName = secondName;
            Phone = phone;
            Country = country;
            DateOfBirth = dateOfBirth;
            Organization = organization;
            Position = position;
            Remark = remark;
            Id = id;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"\n\tID: {this.Id}\n");
            sb.Append($"\tФамилия: {this.Surname}\n");
            sb.Append($"\tИмя: {this.Name}\n");
            sb.Append($"\tОтчество: {this.SecondName}\n");
            sb.Append($"\tНомер телефона: {this.Phone}\n");
            sb.Append($"\tСтрана: {this.Country}\n");
            sb.Append($"\tДата рождения: {this.DateOfBirth}\n");
            sb.Append($"\tОрганизация: {this.Organization}\n");
            sb.Append($"\tДолжность: {this.Position}\n");
            sb.Append($"\tПримечание: {this.Remark}");

            return sb.ToString();
        }

        public string ToShortString()
        {
            return $"{Id} {Surname} {Name} {Phone}";
        }
    }
}
