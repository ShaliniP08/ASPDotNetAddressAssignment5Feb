	--Create table for Country
	CREATE TABLE TblCountry(
    CountryId int not null PRIMARY KEY,
	CountryName varchar(30))
	
	INSERT into TblCountry values(1,'India')
	INSERT into TblCountry values(2,'United States of America')

	select * from TblCountry

	--Create table for State
	CREATE table TblState(
	CountryId int FOREIGN KEY references TblCountry(CountryId),
	StateId int NOT NULL PRIMARY KEY,
	StateName varchar(20) NOT NULL)
	
	INSERT into TblState values(1,1,'Andhra Pradesh')
	INSERT into TblState values(1,2,'Jharkhand')
	INSERT into TblState values(1,3,'Karnataka')
	INSERT into TblState values(1,4,'Maharashtra')

	INSERT into TblState values(2,5,'California')
	INSERT into TblState values(2,6,'Nevada')
	INSERT into TblState values(2,7,'New York')
	INSERT into TblState values(2,8,'Pennsylvania')

	SELECT * from TblState

--Create table for City
	CREATE TABLE TblCity(
	StateId int FOREIGN KEY references TblState(StateId),
	CityId int NOT NULL PRIMARY KEY,
	CityName varchar(20) NOT NULL)

	--INDIA 
	--STATES
	--ANDHRA PRADESH
	INSERT INTO TblCity VALUES(1,1,'Kakinada')
	INSERT INTO TblCity VALUES(1,2,'Tirupati')
	--JHARKHAND
	INSERT INTO TblCity VALUES(2,3,'Jamshedpur')
	INSERT INTO TblCity VALUES(2,4,'Ranchi')
	--KARNATAKA
	INSERT INTO TblCity VALUES(3,5,'Bengaluru')
	INSERT INTO TblCity VALUES(3,6,'Mangalore')
	--MAHARASHTRA
	INSERT INTO TblCity VALUES(4,7,'Kalyan')
	INSERT INTO TblCity VALUES(4,8,'Pune')

	--USA
	--STATES
	--CALIFORNIA
	INSERT INTO TblCity VALUES(5,9,'Los Angeles')
	INSERT INTO TblCity VALUES(5,10,'San Francisco')
	--NEVADA
	INSERT INTO TblCity VALUES(6,11,'Las Vegas')
	INSERT INTO TblCity VALUES(6,12,'Reno')
	--NEW YORK
	INSERT INTO TblCity VALUES(7,13,'Manhattan')
	INSERT INTO TblCity VALUES(7,14,'New York City')
	--PENNSYLVANIA
	INSERT INTO TblCity VALUES(8,15,'Pittsburgh')
	INSERT INTO TblCity VALUES(8,16,'Scranton')

	SELECT * from TblCity

	--Stored Procedures
	CREATE proc sp_ShowSelectedState
	@countryid int
	AS
	BEGIN
	SELECT StateName FROM TblState WHERE CountryId=@countryid
	END

	EXECUTE sp_ShowSelectedState 1

	CREATE proc sp_ShowSelectedCity
	@stateid int
	AS
	BEGIN
	SELECT CityName FROM TblCity WHERE StateId=@stateid
	END

	EXECUTE sp_ShowSelectedCity 2