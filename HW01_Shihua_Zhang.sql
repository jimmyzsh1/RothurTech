USE AdventureWorks2019;
GO
Select * From Production.Product

--1.Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, with no filter

Select ProductID, Name, Color, ListPrice From Production.Product

--2.Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, excludes the rows that ListPrice is 0

Select ProductID, Name, Color, ListPrice From Production.Product where ListPrice <> 0

--3.Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, the rows that are NULL for the Color column

Select ProductID, Name, Color, ListPrice From Production.Product where Color is NULL

--4.Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, the rows that are not NULL for the Color column.

Select ProductID, Name, Color, ListPrice From Production.Product where Color is NOT NULL

--5.Write a query that retrieves the columns ProductID, Name, Color and ListPrice from the Production.Product table, the rows that are not NULL for the column Color, 
--and the column ListPrice has a value greater than zero

Select ProductID, Name, Color, ListPrice From Production.Product where Color is NOT NULL and ListPrice > 0

--6.Write a query that concatenates the columns Name and Color like 'LL Crankarm: Black' from the Production.Product table by excluding the rows that are null for color.

Select Name + ': ' + Color as 'Name : Color' From Production.Product where color is NOT NULL

--7.Write a query that generates the following result set  from Production.Product:
<single column>
NAME: LL Crankarm -- COLOR: Black
NAME: ML Crankarm -- COLOR: Black
NAME: HL Crankarm -- COLOR: Black
NAME: Chainring Bolts -- COLOR: Silver
NAME: Chainring Nut -- COLOR: Silver
NAME: Chainring -- COLOR: Black
NAME: Freewheel -- COLOR: Silver
NAME: Front Derailleur Cage -- COLOR: Silver
...


Select 'Name: ' + Name + ' __ COLOR: ' + Color AS 'single column' From Production.Product where color IN ('Black', 'Silver')

--8.Write a query to retrieve the columns ProductID and Name from the Production.Product table filtered by ProductID from 400 to 500

Select ProductID, Name From Production.Product where ProductID between 400 and 500

--9.Write a query to retrieve the to the columns  ProductID, Name and color from the Production.Product table restricted to the colors black and blue

Select ProductID, Name, Color From Production.Product where color IN ('Black', 'Blue')

--10.Write a query to get a result set on products that begins with the letter S. 

Select * From Production.Product where Name like 'S%'

--11.Write a query that retrieves the columns Name and ListPrice from the Production.Product table. 
--Order the result set by the Name column.

Select Name, ListPrice from Production.Product Order by Name

--12. Write a query that retrieves the columns Name and ListPrice from the Production.Product table. 
--Order the result set by the Name column. The products name should start with either 'A' or 'S'

Select Name, ListPrice from Production.Product where Name like 'A%' or Name like 'S%' Order by Name

--13.Write a query so you retrieve rows that have a Name that begins with the letters SPO, but is then not followed by the letter K. 
--After this zero or more letters can exists. Order the result set by the Name column.

Select * From Production.Product where Name like 'SPO%' and Name not like 'SPOK%' order by Name

--14.Write a query that retrieves unique colors from the table Production.Product. Order the results in descending  manner

Select Distinct Color From Production.Product order by color desc

--15.Write a query that retrieves the unique combination of columns ProductSubcategoryID and Color from the Production.Product table. 
--We do not want any rows that are NULL.in any of the two columns in the result.

Select Distinct ProductSubcategoryID, Color From Production.Product where ProductSubcategoryID is NOT NULL and Color is NOT NULL