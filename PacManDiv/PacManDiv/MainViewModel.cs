using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace PacManDiv
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string reportText;
        private int placeX;
        private int placeY;
        private string selectedDirection;

        public List<PacManStackLayout> AllStacks { get; set; }
        public ICommand StackTapCommand { get; set; }
        public ICommand TurnLeftCommand { get; set; }
        public ICommand TurnRightCommand { get; set; }
        public ICommand MovePacManCommand { get; set; }
        public ICommand PlaceCommand { get; set; }
        public bool PacManPlaced { get; set; }


        public int PlaceX
        {
            get => placeX; set
            {
                placeX = value;
                OnPropertyChanged(nameof(PlaceX));

            }
        }
        public int PlaceY
        {
            get => placeY; set
            {
                placeY = value;
                OnPropertyChanged(nameof(PlaceY));
            }
        }

        public string SelectedDirection
        {
            get => selectedDirection; set
            {
                selectedDirection = value;
                OnPropertyChanged(nameof(SelectedDirection));
            }
        }

        public string ReportText
        {
            get => reportText; set
            {
                reportText = value;
                OnPropertyChanged(nameof(ReportText));
            }
        }

        public int PlacedRow { get; set; }
        public int PlaceCol { get; set; }
        public double CurrentAngle { get; set; }
        public Image PacManImage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            AllStacks = new List<PacManStackLayout>();
            StackTapCommand = new Command(StackTapAction);
            TurnLeftCommand = new Command(TurnLeftAction);
            TurnRightCommand = new Command(TurnRightAction);
            MovePacManCommand = new Command(MovePacManAction);
            PlaceCommand = new Command(PlaceAction);
            PacManImage = new Image
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Source = ImageSource.FromResource("PacManDiv.Images.pacman.png"),
                Scale = 2
            };
        }

        /// <summary>
        /// When user manully places robot
        /// </summary>
        /// <param name="obj"></param>
        public void PlaceAction(object obj)
        {
            PacManImage.Rotation = 0;
            PacManPlaced = true;
            switch (SelectedDirection)
            {
                case "North":
                    TurnLeftCommand?.Execute(null);
                    break;
                case "South":
                    TurnRightCommand?.Execute(null);
                    break;
                case "East":
                    //TurnLeftCommand?.Execute(null);
                    break;
                case "West":
                    TurnLeftCommand?.Execute(null);
                    TurnLeftCommand?.Execute(null);
                    break;
                default:
                    //TurnLeftCommand?.Execute(null);
                    break;
            }
            //Get stack from list with new row and col
            var stack = AllStacks?.FirstOrDefault(f => f.Row == placeX && f.Col == placeY);
            if (stack != null && StackTapCommand.CanExecute(stack))
            {
                StackTapCommand.Execute(stack);
            }
        }


        /// <summary>
        /// When use clicks on move button
        /// </summary>
        /// <param name="obj"></param>
        public void MovePacManAction(object obj)
        {
            if (!Validate())
                return;

            int currentRow = PlacedRow;
            int currentCol = PlaceCol;

            switch (CurrentAngle)
            {
                case 0.0:
                    currentCol++;
                    break;
                case 90.0:
                case -270.0:
                    currentRow++;
                    break;
                case -90.0:
                case 270.0:
                    currentRow--;
                    break;
                case 180.0:
                case -180.0:
                    currentCol--;
                    break;
                default:
                    break;
            }

            //Get stack from list with new row and col
            var stack = AllStacks?.FirstOrDefault(f => f.Row == currentRow && f.Col == currentCol);
            if (stack != null && StackTapCommand.CanExecute(stack))
            {
                StackTapCommand.Execute(stack);
            }
        }

        /// <summary>
        /// When user clicks on right button image turns right side by 90 degrees
        /// </summary>
        /// <param name="obj"></param>
        public void TurnRightAction(object obj)
        {
            if (!Validate())
                return;

            var rotationAngle = PacManImage.Rotation;
            if (rotationAngle >= 270)
            {
                PacManImage.Rotation = 0;
            }
            else
            {
                PacManImage.Rotation += 90.0;
            }

            CurrentAngle = PacManImage.Rotation;
            ReportPacMan();
        }

        /// <summary>
        /// Checks if use placed robot already before executing any commands
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            if (!PacManPlaced)
            {
                UserDialogs.Instance.Alert("Place Pacman, first", "Info");
                return false;
            }
            return true;
        }

        /// <summary>
        /// When user clicks on left button image turns right side by 90 degrees
        /// </summary>
        /// <param name="obj"></param>
        public void TurnLeftAction(object obj)
        {
            if (!Validate())
                return;

            var rotationAngle = PacManImage.Rotation;
            if (rotationAngle <= -270)
            {
                PacManImage.Rotation = 0;
            }
            else
            {
                PacManImage.Rotation -= 90.0;
            }
            CurrentAngle = PacManImage.Rotation;
            ReportPacMan();

        }

        /// <summary>
        /// When user taps on the green tabs on screen
        /// </summary>
        /// <param name="obj"></param>
        public void StackTapAction(object obj)
        {
            var pacStack = obj as PacManStackLayout;
            if (pacStack == null)
                return;
            PlacedRow = pacStack.Row;
            PlaceCol = pacStack.Col;

            pacStack.Children.Add(PacManImage);

            PacManPlaced = true;
            ReportPacMan();
        }

        /// <summary>
        /// Reports on UI about the location of the robot
        /// </summary>
        public void ReportPacMan()
        {
            string report = PlacedRow + ", " + PlaceCol + ", ";

            switch (CurrentAngle)
            {
                case 0.0:
                case 360.0:
                    report += "East";
                    break;
                case 90.0:
                case -270.0:
                    report += "South";
                    break;
                case 180.0:
                case -180.0:
                    report += "West";
                    break;
                case 270.0:
                case -90.0:
                    report += "North";
                    break;
                default:
                    break;
            }
            ReportText = "Output: " + report;
        }
    }
}
