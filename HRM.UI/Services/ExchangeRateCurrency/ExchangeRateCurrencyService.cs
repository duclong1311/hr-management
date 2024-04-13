using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using Terp.Core.Repositories;
using Terp.Core.UnitOfWorks;
using Terp.Domain.Models;
using Terp.UI.Services.ExchangeRateCurrency;

namespace Terp.UI.Services
{
    public class ExchangeRateCurrencyService : IExchangeRateCurrencyService
    {
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExchangeRateCurrencyService(IRepository<Currency> currencyRepository, IUnitOfWork unitOfWork)
        {
            _currencyRepository = currencyRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Update(string currencyCodeBase)
        {
            try
            {
                var client = new RestClient("https://v6.exchangerate-api.com/v6/af129794b7103ee8dfcf0991/latest/" + currencyCodeBase);

                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("apikey", "af129794b7103ee8dfcf0991");
                IRestResponse response = client.Execute(request);

                ExchangRateAPI exchangRateAPI = JsonConvert.DeserializeObject<ExchangRateAPI>(response.Content);
                if (exchangRateAPI == null)
                {
                    MessageBox.Show("No internet", "Warning");
                }
                else
                {
                    await _unitOfWork.BeginTransactionAsync();
                    try
                    {
                        var query = _currencyRepository.AsQueryable().Where(x => !x.IsDelete).ToList();
                        if (query != null)
                        {
                            foreach (var currency in _currencyRepository.AsQueryable().Where(x => !x.IsDelete).ToList())
                            {
                                if (exchangRateAPI.Conversion_rates.GetType().GetProperties().FirstOrDefault(p => p.Name.ToLower() == currency.Code.ToLower()) != null)
                                {
                                    currency.ExchangeRate = (double)exchangRateAPI.Conversion_rates.GetType().GetProperties().FirstOrDefault(p => p.Name.ToLower() == currency.Code.ToLower()).GetValue(exchangRateAPI.Conversion_rates);
                                    await _currencyRepository.UpdateAsync(currency);
                                    _unitOfWork.CommitAsync();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await _unitOfWork.RollbackAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No internet", "Warning");
            }
        }
    }
}
