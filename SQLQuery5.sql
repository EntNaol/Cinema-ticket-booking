drop database dbMyOnlineMovie;
go
create database dbMyOnlineMovie;
go
use dbMyOnlineMovie;
go


Create table movie_catagory(
    CategoryId int identity primary key,
    CategoryName varchar(60),
    IsActive bit null,
    IsDelete bit null    
);

create table customer(
    cust_id int identity primary key,
    cust_FirstName varchar(20),
    cust_LastName varchar(20),
    cust_Email varchar(50) unique,
    cut_Password varchar(50),
    IsActive bit null,
    IsDelete bit null,
    createOn datetime null,
    ModifiedOn datetime null,
    movie_id int,
    foreign key(movie_id) references movie(MovieId)
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
    rating decimal,
    duration decimal,  
    MovieImage varchar(max) 
    foreign key (CategoryId) references movie_catagory(CategoryId)
);

create table schedule(
    schedule_id int identity primary key,
    date_time datetime null
);

create table seats(
    seat_id int identity primary key,
    seat_num varchar(10),
    price decimal,
    cat_id int,
    schedule_id int,
    foreign key(cat_id) references seatCategory(cat_id),
    foreign key(schedule_id) references Schedule(Schedule_id)
);

create table booking(
    booking_id int identity primary key,
    amount_paid decimal,
    payment_Type varchar(50),
    cust_id int,
    seat_id int,
    foreign key(cust_id) references customer(cust_id),
    foreign key(seat_id) references seats(seat_id)    
);

create table seatCategory(
    cat_id int identity primary key,
    cat_name varchar(50)
);

create table slideImage(
    SlideId integer identity primary key,
    SlideTitle varchar(50),
    SlideImage varchar(50)
);