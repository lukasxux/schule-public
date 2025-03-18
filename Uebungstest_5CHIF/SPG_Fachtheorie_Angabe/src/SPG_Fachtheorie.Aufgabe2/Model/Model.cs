using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public enum VehicleType
{
    PassengerCar,
    Motorcycle
}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class Customer
{
    protected Customer() { }

    public Customer(string firstname, string lastname, string email, DateTime registrationDate)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        RegistrationDate = registrationDate;
    }
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public List<Vehicle> Vehicles { get; } = new();
}
[Index(nameof(Numberplate), IsUnique = true)]
public class Vehicle
{
    protected Vehicle() { }

    public Vehicle(string numberplate, Customer customer, VehicleType vehicleType, DateOnly motValidUntil)
    {
        Numberplate = numberplate;
        Customer = customer;
        VehicleType = vehicleType;
        MotValidUntil = motValidUntil;
    }

    public int Id { get; set; }
    public string Numberplate { get; set; }
    public Customer Customer { get; set; }
    public VehicleType VehicleType { get; set; }
    public DateOnly MotValidUntil { get; set; }
    public List<Sticker> Stickers { get; } = new();
    public List<TollPayment> TollPayments { get; } = new();
}

public class Sticker
{
    protected Sticker() { }

    public Sticker(Vehicle vehicle, DateTime purchaseDate, DateOnly validFrom, int daysValid, decimal price)
    {
        Vehicle = vehicle;
        PurchaseDate = purchaseDate;
        ValidFrom = validFrom;
        DaysValid = daysValid;
        Price = price;
    }
    public int Id { get; set; }
    public Vehicle Vehicle { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateOnly ValidFrom { get; set; }
    public int DaysValid { get; set; }
    public decimal Price { get; set; }
}

[Index(nameof(Name), IsUnique = true)]
public class TollStation
{
    protected TollStation() { }

    public TollStation(string name)
    {
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; }
}

public class TollPayment
{
    protected TollPayment() { }

    public TollPayment(Vehicle vehicle, TollStation tollStation, decimal price, DateTime timestamp)
    {
        Vehicle = vehicle;
        TollStation = tollStation;
        Price = price;
        Timestamp = timestamp;
    }
    public int Id { get; set; }
    public Vehicle Vehicle { get; set; }
    public TollStation TollStation { get; set; }
    public decimal Price { get; set; }
    public DateTime Timestamp { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
