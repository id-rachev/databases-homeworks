CREATE DATABASE IF NOT EXISTS Supermarket;

USE Supermarket;

CREATE TABLE IF NOT EXISTS Vendors(
VendorId int AUTO_INCREMENT,
`Vendor Name` varchar(100),
CONSTRAINT PK_VendorId PRIMARY KEY (VendorId)
);

CREATE TABLE IF NOT EXISTS Measures(
MeasureId int AUTO_INCREMENT,
`Measure Name` varchar(100),
CONSTRAINT PK_MeasureId PRIMARY KEY (MeasureId)
);

CREATE TABLE IF NOT EXISTS Products(
ProductId int AUTO_INCREMENT,
VendorId int,
`Product Name` varchar(100),
MeasureId int,
`Base Price` decimal(10,2),
CONSTRAINT PK_ProductId PRIMARY KEY (ProductId),
CONSTRAINT FK_ProductsVendors FOREIGN KEY (VendorId)
REFERENCES Vendors(VendorId),
CONSTRAINT FK_ProductsMeasures FOREIGN KEY (MeasureId)
REFERENCES Measures(MeasureId)
);

INSERT INTO Vendors(`Vendor Name`) VALUES ('7-Eleven');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Ahold');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Aldi');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Auchan');
INSERT INTO Vendors(`Vendor Name`) VALUES ('bauMax');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Billa');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Carrefour');
INSERT INTO Vendors(`Vendor Name`) VALUES ('CBA');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Cencosud');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Chedraui');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Conad');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Coop');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Cora');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Costco');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Crai');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Delhaize Group');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Dunnes');
INSERT INTO Vendors(`Vendor Name`) VALUES ('E-mart');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Eurospin');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Falabella');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Famila');
INSERT INTO Vendors(`Vendor Name`) VALUES ('Metro');

INSERT INTO Measures(`Measure Name`) VALUES ('killogram');
INSERT INTO Measures(`Measure Name`) VALUES ('litter');
INSERT INTO Measures(`Measure Name`) VALUES ('pownd');
INSERT INTO Measures(`Measure Name`) VALUES ('grammes');
INSERT INTO Measures(`Measure Name`) VALUES ('count');

INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (1, 'Apple Pie', 3, 72.25);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (8, 'Chicken', 3, 61);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (21, 'Sandwichnd', 2, 49.83);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (4, 'Tuna Casserole ', 4, 38.88);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (7, 'Bagels', 3, 13);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (5, 'Salmon', 4, 94.5);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (20, 'Sweet Potato Pie', 4, 92.67);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (4, 'Hot Dog', 3, 109.5);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (3, 'Beef', 3, 394);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (10, 'Pop Tarts', 4, 36.5);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (21, 'Strawberry Syrup', 4, 156);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (9, 'Lobster', 1, 81.33);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (3, 'Cake', 3, 113);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (21, 'Strawberry Jam', 1, 116.5);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (3, 'Muffin', 3, 140.25);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (3, 'Cookie', 2, 37.33);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (3, 'Toast', 1, 112);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (2, 'Cereal', 1, 137.5);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (11, 'Bacon', 3, 68.33);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (12, 'Buffalo', 2, 37);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (13, 'Hash Browns', 1, 96.17);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (4, 'Lobster', 3, 18.62);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (9, 'Bread Rolls', 3, 16.43);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (8, 'Brownies', 3, 76);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (17, 'Steak', 1, 60.5);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (11, 'Reuben Sandwiches', 3, 271);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (21, 'Lemon Squares', 1, 17.75);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (21, 'Burgers', 4, 96.43);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (9, 'Griddle', 3, 110.38);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (20, 'Strawberry Shortcake', 2, 33);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (1, 'Steak Fajitas ', 3, 43.25);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (21, 'Fruit Cocktail ', 4, 8.71);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (7, 'Pancakes', 3, 140.5);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (5, 'Subs', 1, 42.75);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (20, 'Chocolate Chip Cookies', 3, 88.5);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (13, 'Pear', 3, 238.75);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (20, 'BBQ Chicken', 2, 67.83);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (16, 'Green Beans', 2, 193);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (20, 'Eggs', 3, 27.62);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (11, 'Fruit Salad', 4, 29);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (3, 'Orange Juice', 1, 39.33);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (8, 'Cantaloupe ', 1, 181.67);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (8, 'Doughnuts', 4, 324.33);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (16, 'Mac & Cheese', 1, 291.33);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (15, 'Superman Ice Cream', 3, 97.67);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (8, 'Fried Chicken', 2, 14.25);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (3, 'Grapes', 1, 116);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (17, 'BBQ Ribs', 2, 215);
INSERT INTO Products(VendorID, `Product Name`, MeasureID, `Base Price`) VALUES (11, 'Cinnamon Rolls', 3, 219);



