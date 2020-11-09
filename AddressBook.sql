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
--Create table Contact_Info
create table Contact_Info
(
ContactId int identity(1,1) not null primary key,
FirstName varchar(25) not null,
LastName varchar(25) not null,
PhoneNumber varchar(12) not null,
Email varchar(20) not null
);
--Insert into Contact_Info
insert into Contact_Info values
('Bill','Jones','9856985696','billjones@gmail.com'),
('Leena','Thomas','7458632156','leena@gmail.com'),
('Terrisa','Gates','8425693856','gates@gmail.com'),
('Priyanka','Chopra','9589657485','priyanka@gmail.com'),
('Karishma','Khanna','9424787845','karishma@gmail.com'),
('Rakhi','Saraf','8596785425','rakhi@gmail.com');

--ContactType table added
create table Contact_Type
(
ContactId int foreign key references Contact_Info(ContactId) on delete cascade,
Contact_Type varchar(20) not null
);
--Add enteries to contact_type
insert into Contact_Type values
(1,'Friends'),
(2,'Family'),
(3,'Friends'),
(4,'Friends'),
(5,'Family'),
(6,'Professional');
--create Address table
create table Address
(
ContactId int foreign key references Contact_Info(ContactId) on delete cascade,
Address varchar(60) not null,
City varchar(15) not null,
State varchar(20) not null,
Zipcode varchar(6) not null
);
--Insert into Address table
insert into Address values
(1,'Street 4','Mumbai','Maharashtra','452856'),
(2,'New Market','Bhopal','Madhya Pradesh','852147'),
(3,'Malviya Nagar','Ajmer','Rajasthan','547856'),
(4,'Gopal Vihar','Bhopal','Madhya Pradesh','658927'),
(5,'Manik Nagar','Ajmer','Rajasthan','125463');
--View Contact_type
select * from Contact_Type
--Join contact_info and contact_type and Address
select * from (Contact_Info contact inner join Contact_Type type
on (contact.ContactId = type.ContactId)) inner join Address address on address.ContactId = contact.ContactId
--Count contact by type
select type.Contact_Type, COUNT(contact.FirstName) from Contact_Info contact inner join Contact_Type type
on (contact.ContactId = type.ContactId)
Group by type.Contact_Type;
--Add DateAdded field to the contact_info
Alter table Contact_Info
Add DateAdded datetime
--Add DateAdded
Update Contact_Info set 
DateAdded = '2019-05-31' where ContactId = 1
Update Contact_Info set 
DateAdded = '2020-05-06' where ContactId = 2
Update Contact_Info set 
DateAdded = '2019-04-01' where ContactId = 3
Update Contact_Info set 
DateAdded = '2018-03-09' where ContactId = 4
Update Contact_Info set 
DateAdded = '2019-08-07' where ContactId = 5
Update Contact_Info set 
DateAdded = '2020-09-30' where ContactId = 6
--Stored Procedure to add a new contact
create procedure SpAddContactDetails
(
@FirstName varchar(255),
@LastName varchar(255),
@PhoneNumber varchar(255),
@Email varchar(255),
@DateAdded DateTime,
@Contact_Type varchar(255),
@Address varchar(255),
@City varchar(255),
@State varchar(255),
@Zipcode varchar(255)
)
as
begin
insert into Contact_Info values
(
@FirstName,@LastName, @PhoneNumber, @Email, @DateAdded
);
insert into Contact_Type values
(
@@IDENTITY, @Contact_Type
)
Declare @CustId int
select @CustId = ContactId from Contact_Info where Email = @Email
insert into Address values
(
@CustId, @Address, @City, @State, @Zipcode
)
end