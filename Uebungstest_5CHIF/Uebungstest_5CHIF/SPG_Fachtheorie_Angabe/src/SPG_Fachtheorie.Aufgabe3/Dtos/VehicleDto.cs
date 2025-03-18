namespace SPG_Fachtheorie.Aufgabe3.Dtos
{
    public record VehicleDto(string numberplate, string customerFirstname, string customerLastname, VehicleType vehicleType ,List<StickerDto> sticker);
}
