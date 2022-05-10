-- -----------------------------------------
-- PI1 PROJECT
-- TATIANA BARBOSA GUIMARAES
-- Bank ATM Management System
-- -----------------------------------------

USE BankATM
GO


-- ---------------------
-- Populate Bank table
-- ---------------------

INSERT INTO Bank (bank_code, bank_balance, bank_status)
VALUES('1', '1000', 'O');


-- ---------------------
-- Populate Clients table
-- ---------------------

INSERT INTO Clients (client_code, client_fullname, client_phone, client_email, client_pin, client_situation, client_attempts)
VALUES('000', 'Admin', '(123)000-0000', 'admin@hotmail.com', '0000', 'U', '0');

INSERT INTO Clients (client_code, client_fullname, client_phone, client_email, client_pin, client_situation, client_attempts)
VALUES('111', 'Donald Trump', '(123)111-1111', 'donaldtrump@hotmail.com', '1234', 'U', '0');

INSERT INTO Clients (client_code, client_fullname, client_phone, client_email, client_pin, client_situation, client_attempts)
VALUES('222', 'Barack Obama', '(123)222-2222', 'barackobama@hotmail.com', '1234', 'U', '0');

INSERT INTO Clients (client_code, client_fullname, client_phone, client_email, client_pin, client_situation, client_attempts)
VALUES('333', 'George Bush', '(123)333-3333', 'georgebush@hotmail.com', '1234', 'U', '0');

INSERT INTO Clients (client_code, client_fullname, client_phone, client_email, client_pin, client_situation, client_attempts)
VALUES('444', 'Justin Trudeau', '(123)444-4444', 'justintrudeau@hotmail.com', '1234', 'B', '0');

INSERT INTO Clients (client_code, client_fullname, client_phone, client_email, client_pin, client_situation, client_attempts)
VALUES('555', 'François Legault', '(123)555-5555', 'francoislegault@hotmail.com', '1234', 'B', '0');



-- ---------------------
-- Populate AccountTypes table
-- ---------------------


INSERT INTO AccountTypes (accounttype_code, accounttype_description)
VALUES('AC', 'Checking');

INSERT INTO AccountTypes (accounttype_code, accounttype_description)
VALUES('AS', 'Savings');

INSERT INTO AccountTypes (accounttype_code, accounttype_description)
VALUES('AM', 'Mortgage');

INSERT INTO AccountTypes (accounttype_code, accounttype_description)
VALUES('AL', 'Line of Credit');


-- ------------------------
-- Populate TransactionTypes table
-- ------------------------

INSERT INTO TransactionTypes (transactiontype_code, transactiontype_description)
VALUES('TD', 'Deposit');

INSERT INTO TransactionTypes (transactiontype_code, transactiontype_description)
VALUES('TW', 'Withdraw');

INSERT INTO TransactionTypes (transactiontype_code, transactiontype_description)
VALUES('TT', 'Transfer');

INSERT INTO TransactionTypes (transactiontype_code, transactiontype_description)
VALUES('TB', 'Bill');




-- ------------------------
-- Populate ClientsAccounts table
-- ------------------------

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('111AC', '111', 'AC', '1000', 'Checking');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('111AS', '111', 'AS', '10000', 'Savings');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('111AM', '111', 'AM', '100000', 'Mortgage');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('111AL', '111', 'AL', '100000', 'Line of Credit');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('222AC', '222', 'AC', '2000', 'Checking');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('222AS', '222', 'AS', '20000', 'Savings');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('222AL', '222', 'AL', '200000', 'Line of Credit');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('333AC', '333', 'AC', '3000', 'Checking');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('333AS', '333', 'AS', '30000', 'Savings');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('333AL', '333', 'AL', '30000', 'Line of Credit');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('444AC', '444', 'AC', '4000', 'Checking');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('444AS', '444', 'AS', '40000', 'Savings');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('444AM', '444', 'AM', '400000', 'Mortgage');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('444AL', '444', 'AL', '44000', 'Line of Credit');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('555AC', '555', 'AC', '5000', 'Checking');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('555AC2', '555', 'AC', '1000', 'Checking');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('555AL', '555', 'AL', '50000', 'Line of Credit');

INSERT INTO ClientsAccounts (clientaccount_id, client_code, accounttype_code, account_balance, accounttype_description)
VALUES('555AM', '555', 'AM', '50000', 'Mortgage');



  -- ------------------------
-- Populate TransactionsHistory table
-- ------------------------

INSERT INTO TransactionsHistory(clientaccount_id, client_code, accounttype_description, transactiontype_description, transaction_amount, transaction_date)
VALUES('555AC', '555', 'Checking', 'Deposit', '100', '2022-02-15 15:26:36');

INSERT INTO TransactionsHistory(clientaccount_id, client_code, accounttype_description, transactiontype_description, transaction_amount, transaction_date)
VALUES('555AC', '555', 'Checking', 'Deposit', '200', '2022-02-24 15:00:40');

