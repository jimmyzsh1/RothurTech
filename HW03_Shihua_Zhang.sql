USE Northwnd;

--1.List all cities that have both Employees and Customers.

SELECT TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME LIKE '%City%'

select * from Employees
select * from Customers

SELECT DISTINCT City FROM Employees
INTERSECT
SELECT DISTINCT City FROM Customers;

-- or

SELECT DISTINCT e.City
FROM Employees e
JOIN Customers c ON e.City = c.City;


--2List all cities that have Customers but no Employee.
--a.      Use sub-query

select DISTINCT City from Customers
where City not IN (
	select DISTINCT City from Employees
	)

--b.      Do not use sub-query

select DISTINCT c.City from Customers c
LEFT JOIN Employees e ON c.City = e.City
where e.City is null

--3.  List all products and their total order quantities throughout all orders.

SELECT TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME LIKE '%order%'

select * from Products
select * from Orders
select * from [Order Details]

select p.ProductName, Sum(od.Quantity) 
from Products p
JOIN [Order Details] od ON p.ProductID = od.ProductID
group by p.ProductName
order by Sum(od.Quantity) desc


--4.  List all Customer Cities and total products ordered by that city.

select * from Customers
select * from Orders
select * from [Order Details]

select c.City, Sum(od.Quantity) as TotalProductOrdered
from Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
JOIN [Order Details] od ON od.OrderID = o.OrderID
group by c.City
order by Sum(od.Quantity) desc


--5. List all Customer Cities that have at least two customers.
--a.      Use union


SELECT City, STRING_AGG(ContactName, ', ') AS CustomerNames
FROM Customers
GROUP BY City
HAVING COUNT(CustomerID) = 2

UNION

SELECT City, STRING_AGG(ContactName, ', ') AS CustomerNames
FROM Customers
GROUP BY City
HAVING COUNT(CustomerID) > 2



--b.      Use sub-query and no union

SELECT 
    City, 
    STRING_AGG(ContactName, ', ') AS CustomerNames
FROM 
    Customers
WHERE 
    City IN (
        SELECT City
        FROM Customers
        GROUP BY City
        HAVING COUNT(CustomerID) >= 2
    )
GROUP BY 
    City


--6.List all Customer Cities that have ordered at least two different kinds of products.


select * from Customers
select * from Orders
select * from [Order Details]

select c.City, COUNT(DISTINCT od.ProductID) as 'Kinds of Products Ordered'
from Customers c
JOIN Orders o ON o.CustomerID = c.CustomerID
JOIN [Order Details] od ON od.OrderID = o.OrderID
group by c.City
having COUNT(DISTINCT od.ProductID) >= 2
order by COUNT(DISTINCT od.ProductID) desc

--7. List all Customers who have ordered products, but have the ‘ship city’ on the order different from their own customer cities.


select * from Customers
select * from Orders
select * from [Order Details]
select * from Products

select c.ContactName as Customer, o.ShipCity as [Ship City], c.City as [Customer City]
from Customers c
JOIN Orders o ON o.CustomerID = c.CustomerID
where o.ShipCity <> c.City




--8. List 5 most popular products, their average price, and the customer city that ordered most quantity of it.


WITH ProductPopularity AS (
    SELECT 
        od.ProductID,
        p.ProductName,
        SUM(od.Quantity) AS TotalQuantity,
        SUM(od.Quantity * od.UnitPrice) * 1.0 / SUM(od.Quantity) AS AveragePrice
    FROM [Order Details] od
    JOIN Products p ON p.ProductID = od.ProductID
    GROUP BY od.ProductID, p.ProductName
),
Top5Products AS (
    SELECT TOP 5 *
    FROM ProductPopularity
    ORDER BY TotalQuantity DESC
),
CityTopBuyers AS (
    SELECT 
        od.ProductID,
        c.City,
        SUM(od.Quantity) AS CityQuantity,
        RANK() OVER (PARTITION BY od.ProductID ORDER BY SUM(od.Quantity) DESC) AS CityRank
    FROM [Order Details] od
    JOIN Orders o ON o.OrderID = od.OrderID
    JOIN Customers c ON c.CustomerID = o.CustomerID
    WHERE od.ProductID IN (SELECT ProductID FROM Top5Products)
    GROUP BY od.ProductID, c.City
)
SELECT 
    t.ProductID,
    t.ProductName,
    t.AveragePrice,
    cb.City AS TopCity
FROM Top5Products t
JOIN CityTopBuyers cb ON t.ProductID = cb.ProductID
WHERE cb.CityRank = 1;


--9.List all cities that have never ordered something but we have employees there.
--a.      Use sub-query

select * from Customers
select * from Orders
select * from Employees

select DISTINCT e.City
from Employees e
where e.City NOT IN (
    select DISTINCT c.City
    from Orders o
    JOIN Customers c ON c.CustomerID = o.CustomerID
)


--b.      Do not use sub-query

SELECT DISTINCT e.City
FROM Employees e
LEFT JOIN (
    SELECT DISTINCT c.City
    FROM Orders o
    JOIN Customers c ON o.CustomerID = c.CustomerID
) AS OrderedCities
ON e.City = OrderedCities.City
WHERE OrderedCities.City IS NULL


--10.List one city, if exists, that is the city from where the employee sold most orders (not the product quantity) is, and also the city of most total quantity of products ordered from. (tip: join  sub-query)
select * from Customers
select * from Orders
select * from [Order Details]
select * from Products
select * from Employees


SELECT TOP 1 e.City, COUNT(*) AS NumOrders
FROM Orders o
JOIN Employees e ON o.EmployeeID = e.EmployeeID
GROUP BY e.City
ORDER BY COUNT(*) DESC;


SELECT TOP 1 c.City, SUM(od.Quantity) AS TotalQty
FROM [Order Details] od
JOIN Orders o ON od.OrderID = o.OrderID
JOIN Customers c ON o.CustomerID = c.CustomerID
GROUP BY c.City
ORDER BY SUM(od.Quantity) DESC;

-- 可以看见，这两个subquery的结果不同，最终应该是空集

WITH MostOrdersByEmployeeCity AS (
    SELECT TOP 1 e.City
    FROM Orders o
    JOIN Employees e ON o.EmployeeID = e.EmployeeID
    GROUP BY e.City
    ORDER BY COUNT(*) DESC
),

MostProductsOrderedCity AS (
    SELECT TOP 1 c.City
    FROM [Order Details] od
    JOIN Orders o ON od.OrderID = o.OrderID
    JOIN Customers c ON o.CustomerID = c.CustomerID
    GROUP BY c.City
    ORDER BY SUM(od.Quantity) DESC
)


SELECT e.City
FROM MostOrdersByEmployeeCity e
JOIN MostProductsOrderedCity p ON e.City = p.City;

--结果为空




--11.How do you remove the duplicates record of a table?

--To remove duplicate records from a table, we can use a Common Table Expression (CTE) with the ROW_NUMBER() window function. 
--This allows us to identify duplicates by assigning a unique row number to each record within a group of duplicates based on specific columns. 
--Then, we can delete all rows where the row number is greater than 1.