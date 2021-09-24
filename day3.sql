use northwind
go

--1--
SELECT City
FROM Employees
INTERSECT
SELECT City
FROM Customers
GO

--2a--
SELECT DISTINCT City
FROM Customers
WHERE City NOT IN  
	(SELECT City
	FROM Employees)
--2b--
SELECT City
FROM Customers
	EXCEPT
	SELECT City
	FROM Employees

--3--
SELECT p.ProductID, SumQuantity
FROM Products p INNER JOIN 
(
SELECT ProductID, SUM(Quantity) SumQuantity FROM [Order Details] GROUP BY ProductID
) od ON p.ProductID = od.ProductID
ORDER BY 1
GO

-- 4 - -
SELECT c.City, SUM(od.Quantity) "Total Products"
FROM Customers c LEFT JOIN Orders o ON c.CustomerID = o.CustomerID INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY c.City


--5--
SELECT p.ProductID, SumQuantity
FROM Products p INNER JOIN 
(
SELECT ProductID, SUM(Quantity) SumQuantity FROM [Order Details] GROUP BY ProductID
) od ON p.ProductID = od.ProductID
ORDER BY 1


-- 6 --
SELECT c.City, SUM(od.Quantity) "Total Products"
FROM Customers c LEFT JOIN Orders o ON c.CustomerID = o.CustomerID INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY c.City


--7--

SELECT DISTINCT o.CustomerID, c.ContactName
FROM Orders o INNER JOIN Customers c ON o.CustomerID = c.CustomerID AND o.ShipCity <> c.City

-- 8--
SELECT t1.pid, p.ProductName "Product Name", 
(
SELECT TOP 1 c.city
FROM Orders o 
INNER JOIN [Order Details] od ON o.OrderID = od.OrderID 
INNER JOIN Customers c ON o.CustomerID = c.CustomerID
WHERE od.ProductId = t1.pid
GROUP BY c.City
ORDER BY SUM(od.Quantity) DESC 
) "Most Popular in"
FROM 
(SELECT TOP 5 od.ProductID pid
FROM [Order Details] od
GROUP BY od.ProductID
ORDER BY COUNT(od.OrderID) DESC
) t1 
INNER JOIN Products p ON t1.pid = p.ProductID
GO

-- 9a
SELECT City
FROM Employees
WHERE City NOT IN(SELECT DISTINCT ShipCity FROM Orders)

-- 9b
SELECT City
FROM Employees emp LEFT JOIN Orders o ON emp.City = o.ShipCity
WHERE o.OrderID IS NULL

-- 10
SELECT TOP 1 emp.City
FROM Orders o INNER JOIN Employees emp ON o.EmployeeID = emp.EmployeeID
GROUP BY emp.City
ORDER BY COUNT(o.OrderID) DESC
GO

SELECT TOP 1 emp.City
FROM Orders o INNER JOIN Employees emp ON o.EmployeeID = emp.EmployeeID INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY emp.City
ORDER BY SUM(od.Quantity) DESC
GO

