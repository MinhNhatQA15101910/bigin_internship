namespace FacilityService.Core.Domain.Entities;

public class Location
{
    public string Type { get; set; } = null!;
    public double[] Coordinates { get; set; } = [];
}
