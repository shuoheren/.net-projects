USE NORTHWIND
GO


--4--
CREATE VIEW view_product_order_REN
AS
SELECT p.ProductName, SUM(od.Quantity) "Total ordered quantity"
FROM Products p INNER JOIN [Order Details] od ON p.ProductID = od.ProductID
GROUP BY p.ProductName
GO

--5--
CREATE PROC sp_product_order_quantity_REN
@pid int,
@total int out
AS
BEGIN
SELECT @total = SUM(Quantity)
FROM [Order Details]
WHERE ProductID = @pid
END
GO

BEGIN
declare @total int
EXEC sp_product_order_quantity_REN 30, @total out
print @total
END
GO

-- 6
CREATE PROC sp_product_order_city_REN
@pname nvarchar(40)
AS
BEGIN
SELECT TOP 5 o.ShipCity, SUM(od.Quantity) "Total quantity"
FROM Orders o INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
WHERE od.ProductID = (SELECT ProductID FROM Products WHERE ProductName = @pname)
GROUP BY O.ShipCity
ORDER BY SUM(OD.Quantity) DESC
END
GO

sp_product_order_city_REN Chai
GO

--8--
CREATE TRIGGER tr_employee_perritories_REN ON EmployeeTerritories
FOR INSERT, UPDATE
AS
DECLARE @tid int
SELECT @tid = TerritoryID FROM Territories WHERE TerritoryDescription = 'Stevens Point'
IF((SELECT COUNT(EmployeeID) FROM EmployeeTerritories WHERE TerritoryID = @tid) > 100)
	BEGIN
	UPDATE EmployeeTerritories
	SET TerritoryID = (SELECT TerritoryID FROM Territories WHERE TerritoryDescription = 'Troy')
	WHERE TerritoryID = @tid
	END
GO

DROP TRIGGER tr_employee_perritories_REN
GO

--9--
CREATE TABLE people_REN
(
	Id int,
	"Name" varchar(20),
	City int
)
GO

CREATE TABLE city_REN
(
	Id int,
	City varchar(20)
)
GO

INSERT INTO people_REN VALUES(1, 'Aaron Rodgers', 2);
INSERT INTO people_REN VALUES(2, 'Russell Wilson', 1);
INSERT INTO people_REN VALUES(3, 'Jody Nelson', 2);

INSERT INTO city_REN VALUES(1, 'Seattle');
INSERT INTO city_REN VALUES(2, 'Green Bay');
GO

CREATE VIEW Packers_REN
AS
	SELECT Name
	FROM people_REN
	WHERE City = (SELECT Id FROM city_REN WHERE City = 'Green Bay')
GO

DROP TABLE people_REN
DROP TABLE city_REN
DROP VIEW Packers_REN
GO


--10--
CREATE PROC sp_birthday_employees_REN
AS
BEGIN
	CREATE TABLE birthday_employees_REN
	(
		Eid int,
		Efirstname nvarchar(10),
		Elastname nvarchar(20)
	)
	INSERT INTO birthday_employees_REN
	SELECT EmployeeID, FirstName, LastName
	FROM Employees
	WHERE MONTH(BirthDate) = 2
END
GO

sp_birthday_employees_REN
GO



--11--
CREATE PROC sp_REN_1
AS
BEGIN
SELECT dt.City
FROM (SELECT c.City, c.CustomerID
FROM Orders o JOIN [Order Details] od ON o.OrderID = od.OrderID RIGHT JOIN Customers c ON o.CustomerID = c.CustomerID
GROUP BY c.City, c.CustomerID
HAVING COUNT(ProductID) <= 1) dt
GROUP BY dt.City
HAVING COUNT(dt.CustomerID) >= 2
END
GO

CREATE PROC sp_REN_2
AS
BEGIN
EXEC sp_REN_1
END
GO

