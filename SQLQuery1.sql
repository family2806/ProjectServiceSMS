--create database SMSGateway;
go
use SMSGateway;
go
create table QueueService
(
	Id int primary key identity(1,1),
	[Source] nvarchar(50),
	Dest nvarchar(50),
	TransID nvarchar(50),
	TransTime DateTime not null,
	[User] nvarchar(50),
	[Password] nvarchar(50),
	ProcessingCode nvarchar(50),
	[Priority] int not null,
	Content ntext,
	Receiver nvarchar(50),
	DateCreate DateTime not null
)
go
create table MT
(
	Id int primary key identity(1,1),
	[Source] nvarchar(50),
	Dest nvarchar(50),
	TransID nvarchar(50),
	TransTime DateTime not null,
	[User] nvarchar(50),
	[Password] nvarchar(50),
	ProcessingCode nvarchar(50),
	[Priority] int not null,
	Content ntext,
	Receiver nvarchar(50),
	DateCreate DateTime not null,
	TimeSend DateTime not null,
	[Status] int not null
)