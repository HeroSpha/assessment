-- Create the Stored Procedure
CREATE PROCEDURE pr_GetOrderSummary
AS
BEGIN
   
    DECLARE @TotalOrders INT,
            @TotalSales MONEY,
            @AverageOrderAmount MONEY,
            @HighestOrderAmount MONEY,
            @LowestOrderAmount MONEY;

    
    SELECT @TotalOrders = COUNT(OrderID)
    FROM Orders;

    
    SELECT @TotalSales = SUM([Order Details].UnitPrice * [Order Details].Quantity)
    FROM Orders
    INNER JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID;

    
    SELECT @AverageOrderAmount = @TotalSales / NULLIF(@TotalOrders, 0);

    
    SELECT TOP 1 @HighestOrderAmount = [Order Details].UnitPrice * [Order Details].Quantity
    FROM Orders
    INNER JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID
    ORDER BY ([Order Details].UnitPrice * [Order Details].Quantity) DESC;

    
    SELECT TOP 1 @LowestOrderAmount = [Order Details].UnitPrice * [Order Details].Quantity
    FROM Orders
    INNER JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID
    ORDER BY ([Order Details].UnitPrice * [Order Details].Quantity) ASC;

   
    SELECT 
        TotalOrders = @TotalOrders,
        TotalSales = @TotalSales,
        AverageOrderAmount = @AverageOrderAmount,
        HighestOrderAmount = @HighestOrderAmount,
        LowestOrderAmount = @LowestOrderAmount;
END;
