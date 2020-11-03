--Create AddressBook database
create database Address_Book_Service;
--Use Address_Book_Service database
use Address_Book_Service;
--View Database Name
select DB_NAME()
--Creating AddressBook table
create table Address_Book
(
FirstName varchar(25) not null,
LastName varchar(25) not null,
Address varchar(60) not null,
City varchar(15) not null,
State varchar(20) not null,
Zipcode varchar(6) not null,
PhoneNumber varchar(12) not null,
Email varchar(20) not null
);
--View table details
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'Address_Book';
--Add Contacts
insert into Address_Book values
('Bill','Jones','Street 4','Mumbai','Maharashtra','452856','9856985696','billjones@gmail.com'),
('Leena','Thomas','New Market','Bhopal','Madhya Pradesh','852147','7458632156','leena@gmail.com'),
('Terrisa','Gates','Sector 10','Bhopal','Madhya Pradesh','875489','8425693856','gates@gmail.com'),
('Priyanka','Chopra','Malviya Nagar','Ajmer','Rajasthan','547856','9589657485','priyanka@gmail.com'),
('Karishma','Khanna','Gopal Vihar','Bhopal','Madhya Pradesh','658927','9424787845','karishma@gmail.com'),
('Rakhi','Saraf','Manik Nagar','Ajmer','Rajasthan','125463','8596785425','rakhi@gmail.com');
--View AddressBook
select* from Address_Book;
--Update existing contact
update Address_Book
set Address = 'street 10' where FirstName = 'Bill';
select* from Address_Book;
--delete contact using person's name
delete Address_Book
where FirstName = 'Rakhi';
select* from Address_Book;
--Retrieve Data City and state wise
select * from Address_Book
where City = 'Bhopal' or State = 'Madhya Pradesh';
--Count contacts by city and state
select COUNT(City), City, State from Address_Book
group by State, City;
--Sort contacts alphabetically for a city
select * from Address_Book
where City = 'Bhopal'
order by FirstName asc;
--Add Contact_Type attribute to address_book
alter table Address_Book
add Contact_Type varchar(20)
--Update Contacts for contact_type
update Address_Book set Contact_Type ='Friends' where FirstName in ('Bill', 'Terrisa', 'Priyanka');
update Address_Book set Contact_Type ='Family' where FirstName in ('Leena','Karishma');
update Address_Book set Contact_Type ='Professional' where FirstName='Rakhi';
select * from Address_Book;
--Add Book_Name attribute to address_book
alter table Address_Book
add Book_Name varchar(20)
--Update Contacts for book_name
update Address_Book set  Book_Name ='Office' where FirstName in ('Bill', 'Terrisa', 'Priyanka', 'Rakhi');
update Address_Book set  Book_Name ='Home' where FirstName in ('Leena','Karishma');
select * from Address_Book;
--Count contacts by contact type
select Contact_Type, COUNT(Contact_Type) from Address_Book
group by Contact_Type
--Adding contact bill to family also
insert into Address_Book values
('Bill','Jones','Street 4','Mumbai','Maharashtra','452856','9856985696','billjones@gmail.com','Family','Home');
