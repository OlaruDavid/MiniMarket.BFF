namespace MiniMarket.Models;

public class CartItem
{
    public int Id{get;set;}
    public int CardId{get;set;}
    public int ProductId{get;set;}
    public int Quantity{get;set;}=1;
}