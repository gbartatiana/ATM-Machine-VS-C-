-- -----------------------------------------
-- PI1 PROJECT
-- TATIANA BARBOSA GUIMARAES
-- Bank ATM Management System
-- -----------------------------------------

CREATE DATABASE BankATM
GO

USE BankATM
GO

-- ---------------------
-- Create Bank table
-- ---------------------
CREATE TABLE Bank
(
  bank_code    				int					NOT NULL,
  bank_balance				decimal(8,2)		NOT NULL,
  bank_status				char(1)				NOT NULL
 
);

-- ---------------------
-- Create Clients table
-- ---------------------
CREATE TABLE Clients
(
  client_code    			int					NOT NULL,
  client_fullname			nvarchar(50)		NOT NULL,
  client_phone				nvarchar(15)		NOT NULL,
  client_email				nvarchar(50)		NOT NULL,
  client_pin				int					NOT NULL,
  client_situation			char(1)				NOT NULL,
  client_attempts			int					NOT NULL
);


-- ---------------------
-- Create AccountTypes table
-- ---------------------
CREATE TABLE AccountTypes
(
accounttype_code				char(2)		NOT NULL,
accounttype_description			char(20)	NOT NULL

);


-- ----------------------
-- Create TransactionTypes table
-- ----------------------
CREATE TABLE TransactionTypes
(
  transactiontype_code      		char(2)		NOT NULL,
  transactiontype_description	   	char(20)	NOT NULL
  );


-- ----------------------
-- Create ClientsAccounts table  
-- ----------------------
CREATE TABLE ClientsAccounts
(
	clientaccount_id		nvarchar(10)	NOT NULL,
    client_code    			int				NOT NULL,
	accounttype_code		char(2)			NOT NULL,
	account_balance			decimal(8,2)	NOT NULL,
	accounttype_description		char(20)	NOT NULL,
	clientaccount_id_transferto		nvarchar(10)	
);


-- -------------------
-- Create TransactionsHistory table
-- -------------------
CREATE TABLE TransactionsHistory
(
	clientaccount_id			nvarchar(10)	NOT NULL,
    client_code    				int				NOT NULL,
   	accounttype_description		char(20)		NOT NULL,
	transactiontype_description	  char(20)		NOT NULL,
	transaction_amount			decimal(8,2)	NOT NULL,
	transaction_date			datetime		NOT NULL,
	clientaccount_id_transferto		nvarchar(10)
);


-- -------------------
-- Define primary keys
-- -------------------
ALTER TABLE Bank
ADD CONSTRAINT PK_bank_code
PRIMARY KEY (bank_code);

ALTER TABLE Clients
ADD CONSTRAINT PK_clients
PRIMARY KEY (client_code);

ALTER TABLE AccountTypes
ADD CONSTRAINT PK_accounttypes
PRIMARY KEY (accounttype_code);

ALTER TABLE TransactionTypes
ADD CONSTRAINT PK_transactiontypes
PRIMARY KEY (transactiontype_code);

ALTER TABLE ClientsAccounts
ADD CONSTRAINT PK_clientsaccounts
PRIMARY KEY (clientaccount_id);



-- -------------------
-- Add UNIQUE AND CHECK Constraints
-- -------------------

ALTER TABLE AccountTypes
ADD CONSTRAINT CHK_accountcode
CHECK (accounttype_code IN ('AC', 'AS', 'AM', 'AL'));

ALTER TABLE TransactionTypes
ADD CONSTRAINT CHK_transactionscode
CHECK (transactiontype_code IN ('TD', 'TW', 'TT', 'TB'));

ALTER TABLE Clients
ADD CONSTRAINT CHK_clientsituation
CHECK (client_situation IN ('B', 'U'));

ALTER TABLE Clients
ADD CONSTRAINT UN_clientcode
UNIQUE (client_code);

ALTER TABLE Bank
ADD CONSTRAINT CHK_bankstatus
CHECK (bank_status IN ('C', 'O'));



