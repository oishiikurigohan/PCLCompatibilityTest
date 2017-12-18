using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;
using OxyPlot;
using PCLCompatibilityTest.Services;

namespace PCLCompatibilityTest.ViewModels
{
    public class MainPageViewModel : INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        public ICommand UpdateContentsCommand { get; private set; }
        public PlotModel BarChart { get; private set; }

        public MainPageViewModel(INavigationService navigationService, OxyPlotModel oxyPlotModel,AnimationModel animationModel)
        {
            this.BarChart = oxyPlotModel.Model;
            this.UpdateContentsCommand = new Command<double>(newValue => {
                oxyPlotModel.Update(newValue);
                animationModel.Update(newValue);
            });
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters) { }
        public virtual void OnNavigatedTo(NavigationParameters parameters) { }
        public virtual void OnNavigatingTo(NavigationParameters parameters) { }
        public virtual void Destroy() { }
    }
}
