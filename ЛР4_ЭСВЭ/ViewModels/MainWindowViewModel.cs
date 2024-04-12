using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace ЛР4_ЭСВЭ.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Навигация по корусели

        // Свойства
        private int _carouselIndex = 0;
        public int CarouselIndex
        {
            get => _carouselIndex;
            set => this.RaiseAndSetIfChanged(ref _carouselIndex, value);
        }

        // Объявление команд
        private ReactiveCommand<Unit, Unit>? _nextCommand;
        private ReactiveCommand<Unit, Unit>? _previousCommand;

        // Инициализация команд
        public ReactiveCommand<Unit, Unit> NextCommand => _nextCommand ??= ReactiveCommand.Create(Next);
        public ReactiveCommand<Unit, Unit> PreviousCommand => _previousCommand ??= ReactiveCommand.Create(Previous);

        // Методы
        private void Next()
        {
            if (CarouselIndex == 1) Result = GetResult();
            else if (CarouselIndex == 2 && Password != "Конспект составлен") return;
            else if (CarouselIndex == 3) SaveAndClear();
            CarouselIndex++;
        }
        private void Previous() => CarouselIndex--;

        #endregion

        private readonly ObservableAsPropertyHelper<string> _nextButtonTitle;
        public string NextButtonTitle => _nextButtonTitle.Value;
        public MainWindowViewModel()
        {
            _nextButtonTitle = this.WhenAnyValue(vm => vm.CarouselIndex).Select(x => x switch {
                1 => "Готово",
                2 => "Результат",
                3 => "Завершить",
                _ => "Вперед"
            }).ToProperty(this, vm => vm.NextButtonTitle);
        }
       

        private string? _fullName;
        private string? _phoneNumber;
        private string? _date;
        private string? _result;
        private string? _password;
        private bool _education;
        private bool _degree;
        private bool _discovery;
        private int? _experience;
        private double? _averageGrade;

        public string? FullName
        {
            get => _fullName;
            set => this.RaiseAndSetIfChanged(ref _fullName, value);
        }
        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => this.RaiseAndSetIfChanged(ref _phoneNumber, value);
        }
        public string? Date
        {
            get => _date;
            set => this.RaiseAndSetIfChanged(ref _date, value);
        }
        public string? Result
        {
            get => _result;
            set => this.RaiseAndSetIfChanged(ref _result, value);
        }
        public string? Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }
        public bool Education
        {
            get => _education;
            set => this.RaiseAndSetIfChanged(ref _education, value);
        }
        public bool Degree
        {
            get => _degree;
            set => this.RaiseAndSetIfChanged(ref _degree, value);
        }
        public bool Discovery
        {
            get => _discovery;
            set => this.RaiseAndSetIfChanged(ref _discovery, value);
        }
        public int? Experience
        {
            get => _experience;
            set => this.RaiseAndSetIfChanged(ref _experience, value);
        }
        public double? AverageGrade
        {
            get => _averageGrade;
            set => this.RaiseAndSetIfChanged(ref _averageGrade, value);
        }

        public void SaveAndClear() // вместо сохранения просто в консоль вывожу по фану
        {
            FullName = null;
            PhoneNumber = null;
            Date = null;
            Result = null;
            Password = null;
            Education = false;
            Degree = false;
            Discovery = false;
            AverageGrade = 0;
            Experience = 0;
        }
        public string GetResult()
        {
            if (!Education) return "Максимально доступная должность: Ассистент";
            if (Degree)
            {
                if (Experience >= 2 && AverageGrade >= 3.5)
                {
                    if (Discovery) return "Рекомендуется на должноссть: Научный сотрудник";
                    return "Рекомендуется на должноссть: Старший преподаватель";
                }
                return "Рекомендуется на должноссть: Инженер-конструктор";
            }
            return "Рекомендуется на должноссть: Лаборант";
        }
    }
}
