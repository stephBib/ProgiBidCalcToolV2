namespace ProgiBidCalcTool.Server
{
    public class VehicleCostBidModel
    {
        public string? VehiclePrice {get;set;}
        public int VehicleTypeId { get;set;}
        public string? VehicleType { get;set;}
        public string? BasicFee { get;set;}
        public string? SpecialFee {get;set;}
        public string? AssociationFee {get;set;}
        public string? StorageFee  {get;set;}
        public string? TotalPrice {get;set;}
    }
}
