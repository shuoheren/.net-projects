USE Northwind;
go

/*
northwind database

14 List all Products that has been sold at least once in last 25 years.
*/
SELECT DISTINCT ProductID
FROM [Order Details] 
WHERE OrderID IN (
SELECT OrderID
FROM Orders
WHERE YEAR(GETDATE()) - YEAR(OrderDate) < 25
)
ORDER BY ProductID

/*
15. List top 5 locations (Zip Code) where the products sold most.
*/

SELECT TOP 5 ShipPostalCode "Top5"
FROM Orders
WHERE ShipPostalCode is NOT NULL
GROUP BY ShipPostalCode
ORDER BY COUNT(*) DESC
GO

/*
16 List top 5 locations (Zip Code) where the products sold most in last 25 years.
*/
SELECT TOP 5 ShipPostalCode "Top 5 Locations"
FROM Orders
WHERE ShipPostalCode is NOT NULL AND OrderID IN (
SELECT OrderID
FROM Orders
WHERE YEAR(GETDATE()) - YEAR(OrderDate) < 25
)
GROUP BY ShipPostalCode
ORDER BY COUNT(*) DESC

/* 
17. List all city names and number of customers in that city.    
*/
SELECT ShipCity, COUNT(DISTINCT CustomerID) "Number of customers"
FROM Orders
GROUP BY ShipCity
GO

/*
18.  List city names which have more than 2 customers, and number of customers in that city 
*/
SELECT ShipCity, COUNT(DISTINCT CustomerID) AS "Number of customers"
FROM Orders
GROUP BY ShipCity
HAVING COUNT(DISTINCT CustomerID) > 2
GO

/*
19. List the names of customers who placed orders after 1/1/98 with order date.
*/

SELECT ContactName
FROM Customers
WHERE CustomerID IN(
	SELECT DISTINCT CustomerID
	FROM Orders
	WHERE OrderDate > '1998-01-01'
)
ORDER BY 1

/*
20. List the names of all customers with most recent order dates 
*/
SELECT ContactName, dt.MostRecent "Most recent order dates"
FROM Customers c INNER JOIN (
	SELECT CustomerID, MAX(OrderDate) MostRecent
	FROM Orders
	GROUP BY CustomerID
) dt ON c.CustomerID = dt.CustomerID
ORDER BY 1


/*
21 Display the names of all customers  along with the  count of products they bought 
*/
SELECT ContactName, dt.OrderCounts
FROM Customers c INNER JOIN (
	SELECT CustomerID, COUNT(*) OrderCounts
	FROM Orders
	GROUP BY CustomerID
) dt ON c.CustomerID = dt.CustomerID
ORDER BY 1

/*
22.Display the customer ids who bought more than 100 Products with count of products.
*/
SELECT CustomerID
FROM Orders o INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY CustomerID
HAVING COUNT(od.Quantity) > 100


/*
23 List all of the possible ways that suppliers can ship their products. Display the results as below
*/
SELECT DISTINCT sup.CompanyName "Supplier Company Name", ship.CompanyName "Shipping Company Name" 
FROM Orders o INNER JOIN [Order Details] od ON o.OrderID = od.OrderID 
	INNER JOIN Products p ON od.ProductID = p.ProductID 
	INNER JOIN Suppliers sup ON p.SupplierID = sup.SupplierID
	INNER JOIN Shippers ship ON o.ShipVia = ship.ShipperID
ORDER BY 1
/*
24   Display the products order each day. Show Order date and Product Name.
*/
SELECT DISTINCT o.OrderDate, p.ProductName
FROM Orders o INNER JOIN [Order Details] od ON o.OrderID = od.OrderID 
		INNER JOIN Products p ON p.ProductID = od.ProductID
ORDER BY 1 DESC
/*
25 Displays pairs of employees who have the same job title.
*/
SELECT e1.FirstName + e1.LastName, e2.FirstName + e2.LastName
FROM Employees e1 INNER JOIN Employees e2 ON e1.Title = e2.Title
WHERE e1.EmployeeID > e2.EmployeeID 
/*
26 Display all the Managers who have more than 2 employees reporting to them.
*/
SELECT m.FirstName + ' ' + m.LastName "Manager Name"
FROM Employees e INNER JOIN Employees m ON e.ReportsTo = m.EmployeeID
GROUP BY m.FirstName, m.LastName
HAVING COUNT(e.EmployeeID) > 2

/*
27 Display the customers and suppliers by city. The results should have the following columns
*/
SELECT City, CompanyName "NAME", ContactName "Contact Name", 'Customer' "Type"
FROM Customers
UNION ALL
SELECT City, CompanyName, ContactName, 'Supplier'
FROM Suppliers
ORDER BY City

