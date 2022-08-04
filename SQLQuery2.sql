drop database dbMyOnlineMovie2;
go
create database dbMyOnlineMovie2;
go
use dbMyOnlineMovie2;
go

Create table movie_catagory(
    CategoryId int identity primary key,
    CategoryName varchar(60),
    IsActive bit null,
    IsDelete bit null
);

create table movie(
    MovieId int identity primary key,
    MovieName varchar(50),
    CategoryId int,
    IsActive bit null,
    IsDelete bit null,
    CreatedDate datetime null,
    ModifiedDate datetime null,
    Description varchar(500),
    rating float,
    duration float,  
    MovieImage varchar(max) 
    foreign key (CategoryId) references movie_catagory(CategoryId) 
);

create table members(
    MemberId int identity primary key,
    FirstName varchar(20),
    LastName varchar(20),
    Email varchar(50) unique,
    Password varchar(50),
    IsActive bit null,
    IsDelete bit null,
    createOn datetime null,
    ModifiedOn datetime null,
    movie_id int,
    foreign key(movie_id) references movie(MovieId)
);

create table movieDetail(
    MovieDetailId int identity primary key,
    MemberId int,
    BookingId int,
    AmountPaid float,
    PaymentType varchar(50)
);

create table cartStatus(
    CartStatusId int identity primary key,
    CartStatus varchar(100)
);

create table cart(
    CartId integer identity primary key,
    MovieId integer,
    MemberId integer,
    CartStatusId integer
);

create table roles(
    RoleId integer identity primary key,
    RoleName varchar(50)
);

create table memberRole(
    MemberRoleId integer identity primary key,
    MemberId integer,
    RoleId integer
);

create table slideImage(
    SlideId integer identity primary key,
    SlideTitle varchar(50),
    SlideImage varchar(50)
);