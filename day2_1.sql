use AdventureWorks2019
go


/*	
1. How many products can you find in the Production.Product table?
504
*/
select count (*)
from production.product

/*	
2.Write a query that retrieves the number of products in the Production.Product table that are included in a subcategory. 
The rows that have NULL in column ProductSubcategoryID are considered to not be a part of any subcategory.
*/
select count(ProductSubcategoryID)
from production.product
where ProductSubcategoryID is NOT null
/*
3. How many Products reside in each SubCategory? Write a query to display the results with the following titles.
*/

SELECT    ProductSubcategoryID, Count(*) CountedProducts
FROM Production.Product
WHERE ProductSubcategoryID is NOT NULL
GROUP BY ProductSubcategoryID


/*	
4. How many products that do not have a product subcategory. 
*/

SELECT COUNT(*)
FROM Production.Product
WHERE ProductSubcategoryID is NULL

/*	
5.Write a query to list the sum of products quantity in the Production.ProductInventory table.
*/

SELECT SUM(Quantity)
FROM Production.ProductInventory  p

/*
6.  Write a query to list the sum of products in the Production.ProductInventory table
and LocationID set to 40 and limit the result to include just summarized quantities less than 100.
*/

SELECT ProductID, SUM(Quantity) TheSum
FROM Production.ProductInventory
WHERE LocationID = 40
GROUP BY ProductID
HAVING SUM(Quantity) < 100

/*	
7. Write a query to list the sum of products with the shelf information in the Production.ProductInventory table
and LocationID set to 40 and limit the result to include just summarized quantities less than 100
*/
SELECT Shelf, ProductID, SUM(Quantity) TheSum
FROM Production.ProductInventory
WHERE LocationID = 40
GROUP BY Shelf, ProductID
HAVING SUM(Quantity) < 100


/*
8.Write the query to list the average quantity for products where column LocationID has the value of 10 from the table Production.ProductInventory table.
*/
SELECT AVG(Quantity) Average
FROM Production.ProductInventory
WHERE LocationID = 10

/*
9.Write query  to see the average quantity  of  products by shelf  from the table Production.ProductInventory
*/
SELECT ProductID, Shelf, AVG(Quantity) TheAvg
FROM Production.ProductInventory
GROUP BY ProductID, Shelf


/* 
10.Write query  to see the average quantity  of  products by shelf excluding rows that has the value of N/A in the column Shelf from the table Production.ProductInventory
*/
SELECT ProductID, Shelf, AVG(Quantity) TheAvg
FROM Production.ProductInventory
WHERE Shelf <> 'N/A'
GROUP BY ProductID, Shelf

/*
11.List the members (rows) and average list price in the Production.Product table. 
This should be grouped independently over the Color and the Class column. Exclude the rows where Color or Class are null.
*/
SELECT Color, Class, COUNT(*) TheCount, AVG(ListPrice) AvgPrice
FROM Production.Product
WHERE Color is NOT NULL AND Class is NOT NULL
GROUP BY Color, Class

/*
12.Write a query that lists the country and province names from person. CountryRegion and person. StateProvince tables. Join them and produce a result set similar to the following. 
*/
SELECT c.Name Country, s.Name Province
FROM Person.CountryRegion c INNER JOIN Person.StateProvince s ON c.CountryRegionCode = s.CountryRegionCode
ORDER BY 1
GO

/*
13.Write a query that lists the country and province names from person. CountryRegion and person. StateProvince tables 
and list the countries filter them by Germany and Canada. Join them and produce a result set similar to the following.
*/
SELECT c.Name Country, s.Name Province
FROM Person.CountryRegion c INNER JOIN Person.StateProvince s ON c.CountryRegionCode = s.CountryRegionCode
WHERE c.Name IN ('Germany ', 'Canada')
ORDER BY 1
GO

