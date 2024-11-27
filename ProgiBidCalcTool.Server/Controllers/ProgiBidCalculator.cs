using System.Numerics;
using Microsoft.AspNetCore.Mvc;


namespace ProgiBidCalcTool.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgiBidCalculatorController : ControllerBase
    {
        private decimal basicBuyerFee { get; set; }
        private decimal specialFee { get; set; }

        private readonly ILogger<ProgiBidCalculatorController> _logger;

        private IConfiguration _configuration;
        public ProgiBidCalculatorController(IConfiguration configuration, ILogger<ProgiBidCalculatorController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [Route("CalculateFees")]
        // Requirement - Backend (called from site using axios)
        // params (vehiclePrice , vehicleTypeId )
        public JsonResult CalculateFees(decimal vehiclePrice, int vehicleTypeId)
        {
            decimal basicFee = CalculateBasicBuyerFee(vehiclePrice, vehicleTypeId);
            decimal specialFee = CalculateSellerSpecialFee(vehiclePrice, vehicleTypeId);
            decimal associationFee = CalculateAssociationFee(vehiclePrice);
            decimal storageFee = 100;

            VehicleCostBidModel mdl = new VehicleCostBidModel
            {
                VehiclePrice = vehiclePrice.ToString("#.00"),
                VehicleTypeId = vehicleTypeId,
                VehicleType = vehicleTypeId == 1 ? "Common" : "Luxury",
                BasicFee = basicFee.ToString("#.00"),
                SpecialFee = specialFee.ToString("#.00"),
                AssociationFee = associationFee.ToString("#.00"),
                StorageFee = storageFee.ToString("#.00"),
                TotalPrice = (vehiclePrice + basicFee + specialFee + associationFee + storageFee).ToString("#.00")
            };

            return new JsonResult(mdl);
        }

        // Requirement - Basic buyer fee: 10% of the price of the vehicle
        //               Common: minimum $10 and maximum $50
        //               Luxury: minimum 25$ and maximum 200$
        // params (vehiclePrice , vehicleTypeId )
        private decimal CalculateBasicBuyerFee(decimal vehiclePrice, int vehicleTypeId)
        {
            decimal basicBuyerFee = vehiclePrice / 10;

            switch (vehicleTypeId)
            {
                case 1: // common
                    basicBuyerFee = basicBuyerFee < 10 ? 10 : basicBuyerFee;
                    basicBuyerFee = basicBuyerFee > 50 ? 50 : basicBuyerFee;
                    break;

                case 2: // luxury
                    basicBuyerFee = basicBuyerFee < 25 ? 25 : basicBuyerFee;
                    basicBuyerFee = basicBuyerFee > 200 ? 200 : basicBuyerFee;
                    break;
            }

            return basicBuyerFee;
        }

        // Requirement - The seller's special fee:
        //              Common: 2% of the vehicle price
        //              Luxury: 4% of the vehicle price
        // params (vehiclePrice , vehicleTypeId )
        private decimal CalculateSellerSpecialFee(decimal vehiclePrice, int vehicleTypeId)
        {
            decimal sellerSpecialFee = 0;

            switch (vehicleTypeId)
            {
                case 1: // common
                    sellerSpecialFee = (decimal).02 * vehiclePrice;
                    break;

                case 2: // luxury
                    sellerSpecialFee = (decimal).04 * vehiclePrice;
                    break;
            }

            return sellerSpecialFee;
        }

        // Requirement -The added costs for the association based on the price of the vehicle:
        //              $5 for an amount between $1 and $500
        //              $10 for an amount greater than $500 up to $1000
        //              $15 for an amount greater than $1000 up to $3000
        //              $20 for an amount over $3000
        // params (vehiclePrice)
        private int CalculateAssociationFee(decimal vehiclePrice)
        {
            int associationFee = 0;

            switch (vehiclePrice)
            {
                case < 500:
                    associationFee = 5;
                    break;

                case >= 500 and <= 1000:
                    associationFee = 10;
                    break;

                case > 1000 and <= 3000:
                    associationFee = 15;
                    break;

                default:
                    associationFee = 20;
                    break;
            }

            return associationFee;
        }

    }
}