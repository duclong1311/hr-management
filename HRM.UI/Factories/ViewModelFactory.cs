using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terp.UI.Defines;
using Terp.UI.ViewModels;
using Terp.UI.ViewModels.Abstract;


namespace Terp.UI.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<RegisterViewModel> _createRegisterViewModel;
        private readonly CreateViewModel<CurrencyViewModel> _createCurrencyViewModel;
        private readonly CreateViewModel<MainContentViewModel> _createMainContentViewModel;
        private readonly CreateViewModel<UnitViewModel> _createUnitViewModel;
        private readonly CreateViewModel<AlternativeProductInformationsViewModel> _createAlternativeProductInformationsViewModel;
        private readonly CreateViewModel<ProviderViewModel> _createProviderViewModel;
        private readonly CreateViewModel<ProductViewModel> _createProductViewModel;
        private readonly CreateViewModel<PriceInformationViewModel> _createPriceInformationViewModel;
        private readonly CreateViewModel<SearchPriceInformationViewModel> _createSearchPriceInformationViewModel;

        public ViewModelFactory(CreateViewModel<LoginViewModel> createLoginViewModel,
            CreateViewModel<RegisterViewModel> createRegisterViewModel,
            CreateViewModel<CurrencyViewModel> createCurrencyViewModel,
            CreateViewModel<MainContentViewModel> createMainContentViewModel,
            CreateViewModel<UnitViewModel> createUnitViewModel,
            CreateViewModel<AlternativeProductInformationsViewModel> createAlternativeProductInformationsViewModel,
            CreateViewModel<ProviderViewModel> createProviderViewModel,
            CreateViewModel<ProductViewModel> createProductViewModel,
            CreateViewModel<PriceInformationViewModel> createPriceInformationViewModel,
            CreateViewModel<SearchPriceInformationViewModel> createSearchPriceInformationViewModel)
        {
            _createLoginViewModel = createLoginViewModel;
            _createRegisterViewModel = createRegisterViewModel;
            _createCurrencyViewModel = createCurrencyViewModel;
            _createMainContentViewModel = createMainContentViewModel;
            _createUnitViewModel = createUnitViewModel;
            _createAlternativeProductInformationsViewModel = createAlternativeProductInformationsViewModel;
            _createProviderViewModel = createProviderViewModel;
            _createProductViewModel = createProductViewModel;
            _createProductViewModel = createProductViewModel;
            _createPriceInformationViewModel = createPriceInformationViewModel;
            _createSearchPriceInformationViewModel = createSearchPriceInformationViewModel;
        }
        public BaseViewModel CreateViewModel(EViewTypes viewType)
        {
            switch (viewType)
            {
                case EViewTypes.Login:
                    return _createLoginViewModel();
                case EViewTypes.Register:
                    return _createRegisterViewModel();
                case EViewTypes.Currency:
                    return _createCurrencyViewModel();
                case EViewTypes.MainContent:
                    return _createMainContentViewModel();
                case EViewTypes.Unit:
                    return _createUnitViewModel();
                case EViewTypes.Customer:
                    return _createAlternativeProductInformationsViewModel();
                case EViewTypes.Provider:
                    return _createProviderViewModel();
                case EViewTypes.Product:
                    return _createProductViewModel();
                case EViewTypes.PriceInformation:
                    return _createPriceInformationViewModel();
                case EViewTypes.SearchPriceInformation:
                    return _createSearchPriceInformationViewModel();
                default:
                    throw new ArgumentException();
            }
        }
    }
}
