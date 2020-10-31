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
