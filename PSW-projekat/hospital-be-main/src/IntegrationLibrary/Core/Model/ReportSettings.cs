using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model
{
    public class ReportSettings : EntityClass
    {
        private DateTime _startDeliveryDate;
        private int _deliveryYears;
        private int _deliveryMonths;
        private int _deliveryDays;
        private int _calculationYears;
        private int _calculationMonths;
        private int _calculationDays;

        public ReportSettings()
        {
        }

        public ReportSettings(DateTime startDeliveryDate, int deliveryYears, int deliveryMonths, 
            int deliveryDays, int calculationYears, int calculationMonths, int calculationDays)
        {
            _startDeliveryDate = startDeliveryDate;
            _deliveryYears = deliveryYears;
            _deliveryMonths = deliveryMonths;
            _deliveryDays = deliveryDays;
            _calculationYears = calculationYears;
            _calculationMonths = calculationMonths;
            _calculationDays = calculationDays;
        }

        [Required]
        public DateTime StartDeliveryDate { get => _startDeliveryDate; set => _startDeliveryDate = value; }
        [Required]
        [Range(0, Double.PositiveInfinity)]
        public int DeliveryYears { get => _deliveryYears; set => _deliveryYears = value; }
        [Required]
        [Range(0, 11)]
        public int DeliveryMonths { get => _deliveryMonths; set => _deliveryMonths = value; }
        [Required]
        [Range(0, 31)]
        public int DeliveryDays { get => _deliveryDays; set => _deliveryDays = value; }
        [Required]
        [Range(0, Double.PositiveInfinity)]
        public int CalculationYears { get => _calculationYears; set => _calculationYears = value; }
        [Required]
        [Range(0, 11)]
        public int CalculationMonths { get => _calculationMonths; set => _calculationMonths = value; }
        [Required]
        [Range(0, 31)]
        public int CalculationDays { get => _calculationDays; set => _calculationDays = value; }

        public DateTime NextDeliveryDate 
        {
            get
            {
                DateTime deliveryDate = DateTime.Now;
                deliveryDate = deliveryDate.AddDays(DeliveryDays);
                deliveryDate = deliveryDate.AddMonths(DeliveryMonths);
                deliveryDate = deliveryDate.AddYears(DeliveryYears);
                return deliveryDate;
            }
        }

        public DateTime StartDateCalculation
        {
            get
            {
                DateTime calculationDate = DateTime.Now;
                calculationDate = calculationDate.AddDays(-CalculationDays);
                calculationDate = calculationDate.AddMonths(-CalculationMonths);
                calculationDate = calculationDate.AddYears(-CalculationYears);
                return calculationDate;
            }
        }
    }
}
