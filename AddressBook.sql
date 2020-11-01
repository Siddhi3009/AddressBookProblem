/*Create AddressBook database*/
create database Address_Book_Service;
/*Use Address_Book_Service database*/
use Address_Book_Service;
/*View Database Name*/
select DB_NAME()
/*Creating AddressBook table*/
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
/*View table details*/
select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'Address_Book';
/*Add Contacts*/
insert into Address_Book values
('Bill','Jones','Street 4','Mumbai','Maharashtra','452856','9856985696','billjones@gmail.com'),
('Leena','Thomas','New Market','Bhopal','Madhya Pradesh','852147','7458632156','leena@gmail.com'),
('Terrisa','Gates','Sector 10','Bhopal','Madhya Pradesh','875489','8425693856','gates@gmail.com'),
('Priyanka','Chopra','Malviya Nagar','Ajmer','Rajasthan','547856','9589657485','priyanka@gmail.com'),
('Karishma','Khanna','Gopal Vihar','Bhopal','Madhya Pradesh','658927','9424787845','karishma@gmail.com'),
('Rakhi','Saraf','Manik Nagar','Ajmer','Rajasthan','125463','8596785425','rakhi@gmail.com');
/*View AddressBook*/
select* from Address_Book;
/*Update existing contact*/
update Address_Book
set Address = 'street 10' where FirstName = 'Bill';
select* from Address_Book;
/*delete contact using person's name*/
delete Address_Book
where FirstName = 'Rakhi';
select* from Address_Book;
/*Retrieve Data City and state wise*/
select * from Address_Book
where City = 'Bhopal' or State = 'Madhya Pradesh';
/*Count contacts by city and state*/
select COUNT(City), City, State from Address_Book
group by State, City;
/*Sort contacts alphabetically for a city*/
select * from Address_Book
where City = 'Bhopal'
order by FirstName asc;