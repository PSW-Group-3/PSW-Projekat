USE [IntegrationDb]
GO
TRUNCATE TABLE [dbo].[BloodRequests];
TRUNCATE TABLE [dbo].[ReportSettings];
TRUNCATE TABLE [dbo].[BloodBanks];
TRUNCATE TABLE [dbo].[Newses];
TRUNCATE TABLE [dbo].[EmergencyBloodRequests];
TRUNCATE TABLE [dbo].[ScheduledOrders];

--Don't change order

TRUNCATE TABLE [dbo].[Bids];
ALTER TABLE [dbo].[Bids]
	DROP CONSTRAINT [FK_Bids_Tenders_TenderId];
TRUNCATE TABLE [dbo].[Tenders];

ALTER TABLE [dbo].[Bids]
	ADD CONSTRAINT [FK_Bids_Tenders_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [dbo].[Tenders]([Id])

SET IDENTITY_INSERT [dbo].[Tenders] ON
	INSERT INTO [dbo].[Tenders] ([Id], [DueDate], [State], [Demands]) VALUES (1, N'2023-01-02 15:19:01', 2, N'[{"BloodType":1,"Quantity":5},{"BloodType":5,"Quantity":9}]')
	INSERT INTO [dbo].[Tenders] ([Id], [DueDate], [State], [Demands]) VALUES (2, N'2022-12-11 15:19:02', 0, N'[{"BloodType":3,"Quantity":3},{"BloodType":1,"Quantity":7}]')
	INSERT INTO [dbo].[Tenders] ([Id], [DueDate], [State], [Demands]) VALUES (3, N'2023-01-20 15:19:01', 0, N'[{"BloodType":3,"Quantity":3},{"BloodType":1,"Quantity":7}, {"BloodType":4,"Quantity":7}, {"BloodType":5,"Quantity":7}]')
	INSERT INTO [dbo].[Tenders] ([Id], [DueDate], [State], [Demands]) VALUES (4, N'2023-01-15 15:19:01', 0, N'[{"BloodType":3,"Quantity":15},{"BloodType":1,"Quantity":13}, {"BloodType":4,"Quantity":20}, {"BloodType":5,"Quantity":50}]')
	INSERT INTO [dbo].[Tenders] ([Id], [DueDate], [State], [Demands]) VALUES (5, N'2023-01-02 15:19:01', 2, N'[{"BloodType":6,"Quantity":5},{"BloodType":7,"Quantity":9}]')
	INSERT INTO [dbo].[Tenders] ([Id], [DueDate], [State], [Demands]) VALUES (6, N'2023-01-02 15:19:01', 2, N'[{"BloodType":2,"Quantity":2},{"BloodType":4,"Quantity":1}, {"BloodType":0,"Quantity":2}, {"BloodType":6,"Quantity":3}]')
	INSERT INTO [dbo].[Tenders] ([Id], [DueDate], [State], [Demands]) VALUES (7, N'2023-01-02 15:19:01', 2, N'[{"BloodType":1,"Quantity":1},{"BloodType":3,"Quantity":6}, {"BloodType":2,"Quantity":3}, {"BloodType":7,"Quantity":2}]')

SET IDENTITY_INSERT [dbo].[Tenders] OFF

SET IDENTITY_INSERT [dbo].[Bids] ON
	INSERT INTO [dbo].[Bids] ([Id], [DeliveryDate], [Price], [BloodBankId], [Status], [TenderId]) VALUES (1, N'2023-01-31 23:59:59', 1000, 1, 1, 1)
	INSERT INTO [dbo].[Bids] ([Id], [DeliveryDate], [Price], [BloodBankId], [Status], [TenderId]) VALUES (2, N'2023-02-15 15:19:01', 1000, 2, 2, 1)
	INSERT INTO [dbo].[Bids] ([Id], [DeliveryDate], [Price], [BloodBankId], [Status], [TenderId]) VALUES (3, N'2023-01-11 15:19:01', 2000, 1, 0, 2)
	INSERT INTO [dbo].[Bids] ([Id], [DeliveryDate], [Price], [BloodBankId], [Status], [TenderId]) VALUES (4, N'2023-01-15 15:19:01', 1500, 2, 0, 2)
	INSERT INTO [dbo].[Bids] ([Id], [DeliveryDate], [Price], [BloodBankId], [Status], [TenderId]) VALUES (5, N'2023-02-15 15:19:01', 10000, 1, 0, 3)
	INSERT INTO [dbo].[Bids] ([Id], [DeliveryDate], [Price], [BloodBankId], [Status], [TenderId]) VALUES (6, N'2023-01-10 15:19:01', 10000, 1, 1, 5)
	INSERT INTO [dbo].[Bids] ([Id], [DeliveryDate], [Price], [BloodBankId], [Status], [TenderId]) VALUES (7, N'2023-01-10 15:19:01', 10000, 2, 1, 6)
	INSERT INTO [dbo].[Bids] ([Id], [DeliveryDate], [Price], [BloodBankId], [Status], [TenderId]) VALUES (8, N'2023-01-10 15:19:01', 10000, 3, 1, 7)
SET IDENTITY_INSERT [dbo].[Bids] OFF

-------------------------------------------------------


SET IDENTITY_INSERT [dbo].[BloodRequests] ON 
	INSERT [dbo].[BloodRequests] ([Id], [RequiredForDate], [BloodQuantity_Value], [Reason], [DoctorId], [RequestState], [BloodType], [Comment], [BloodBankId]) VALUES (1, '2023-02-11 11:30:00', 3, 'Need it for heart operation', 5, 0, 0, null, 0)
	INSERT [dbo].[BloodRequests] ([Id], [RequiredForDate], [BloodQuantity_Value], [Reason], [DoctorId], [RequestState], [BloodType], [Comment], [BloodBankId]) VALUES (2, '2023-02-12 11:30:00', 5, 'Need it for brain surgery', 5, 2, 1, 'Your asking for too much blood', 0)
	INSERT [dbo].[BloodRequests] ([Id], [RequiredForDate], [BloodQuantity_Value], [Reason], [DoctorId], [RequestState], [BloodType], [Comment], [BloodBankId]) VALUES (3, '2023-02-13 11:30:00', 2, 'Need it for operation on the leg', 5, 3, 2, null, 0)
	INSERT [dbo].[BloodRequests] ([Id], [RequiredForDate], [BloodQuantity_Value], [Reason], [DoctorId], [RequestState], [BloodType], [Comment], [BloodBankId]) VALUES (4, '2023-01-20 11:30:00', 3, 'Need it for heart operation', 3, 0, 3, null, 2)
	INSERT [dbo].[BloodRequests] ([Id], [RequiredForDate], [BloodQuantity_Value], [Reason], [DoctorId], [RequestState], [BloodType], [Comment], [BloodBankId]) VALUES (5, '2023-01-21 11:30:00', 4, 'Need it for brain surgery', 3, 2, 4, 'Your asking for too much blood', 3)
	INSERT [dbo].[BloodRequests] ([Id], [RequiredForDate], [BloodQuantity_Value], [Reason], [DoctorId], [RequestState], [BloodType], [Comment], [BloodBankId]) VALUES (6, '2023-01-22 11:30:00', 5, 'Need it for operation on the leg', 3, 3, 5, null, 1)
	INSERT [dbo].[BloodRequests] ([Id], [RequiredForDate], [BloodQuantity_Value], [Reason], [DoctorId], [RequestState], [BloodType], [Comment], [BloodBankId]) VALUES (7, '2023-01-25 11:30:00', 6, 'Need it for heart operation', 4, 0, 0, null, 2)
	INSERT [dbo].[BloodRequests] ([Id], [RequiredForDate], [BloodQuantity_Value], [Reason], [DoctorId], [RequestState], [BloodType], [Comment], [BloodBankId]) VALUES (8, '2023-01-26 11:30:00', 7, 'Need it for brain surgery', 4, 2, 4, 'Your asking for too much blood', 0)
	INSERT [dbo].[BloodRequests] ([Id], [RequiredForDate], [BloodQuantity_Value], [Reason], [DoctorId], [RequestState], [BloodType], [Comment], [BloodBankId]) VALUES (9, '2023-01-27 11:30:00', 2, 'Need it for operation on the leg', 4, 3, 6, null, 2)
SET IDENTITY_INSERT [dbo].[BloodRequests] OFF

GO
SET IDENTITY_INSERT [dbo].[ReportSettings] ON 
	INSERT [dbo].[ReportSettings] ([Id], [StartDeliveryDate], [CalculationDays], [CalculationMonths], [CalculationYears], [DeliveryDays], [DeliveryMonths], [DeliveryYears]) VALUES (1, '2022-11-16 11:30:00', 0, 0, 1, 0, 1, 0)
SET IDENTITY_INSERT [dbo].[ReportSettings] OFF

GO
SET IDENTITY_INSERT [dbo].[BloodBanks] ON 
	INSERT [dbo].[BloodBanks] ([Id], [AccountStatus], [ApiKey], [Email], [Name], [Password], [PasswordResetKey], [ServerAddress], [GRPCServerAddress]) VALUES (1, 1, '4rijtG2K/XHcaesRU2gLHS5LC0QJoqMmKGKQrj4OmNLoxQjsjwUc+w60BFi3fR+0pO/BtrSma8yEJ1+bwAoQHQ==', '{"LocalPart": "bloodymary", "DomainName": "gmail.com"}', 'Bloody Mary', '123', '5lSxho6OJIAMKE2CzURTLcpOPIVeCp5becYHSgj26BIpLcE5DXARRiJFUtPupqpNWdIFqY1EhEFNx1QQsTFxbj', 'http://localhost:8086/', 'localhost:12689')
	INSERT [dbo].[BloodBanks] ([Id], [AccountStatus], [ApiKey], [Email], [Name], [Password], [PasswordResetKey], [ServerAddress], [GRPCServerAddress]) VALUES (2, 1, 'W3QW1vpKFX9NUJ94HF0klON+lRaPBaSgx7mwAgV0b0ml4uVu7t2+2FoNDLqweFKzw4drCuaf0mPRbylQaja3Nw==', '{"LocalPart": "bloodyhell", "DomainName": "gmail.com"}', 'Bloody Hell', '123', '4UHf68tCV7GfsjAyYtCnackqmphU28PzwH7AFNHARy3PnMLuDbFD5Ec3Q2nBX8JW2rkXnQSTCvfeklOOgqwhH7', 'http://localhost:8086/', null)
	INSERT [dbo].[BloodBanks] ([Id], [AccountStatus], [ApiKey], [Email], [Name], [Password], [PasswordResetKey], [ServerAddress], [GRPCServerAddress]) VALUES (3, 1, 'tLeiJ6w79JdrILqI34F6kYM3UAWENV8RjeZvi0LVtJochrXWJ7mpt0Cdedka8lVWUPnCFLZOhJbcS8ao9VFgwQ==', '{"LocalPart": "newlife", "DomainName": "gmail.com"}', 'New Life', '123', '4UHf68tCV7GfsjAyYtCnackqmphU28PzwH7AFNHARy3PnMLuDbFD5Ec3Q2nBX8JW2rkXnQSTCvfeklOOgqwhH7', 'http://localhost:8086/', 'localhost:12689')

SET IDENTITY_INSERT [dbo].[BloodBanks] OFF

GO
SET IDENTITY_INSERT [dbo].[Newses] ON 
	INSERT [dbo].[Newses] ([Id], [Title], [Text], [Status], [DateTime], [BloodBankId], [Image]) VALUES (1, 'Blood donations', 'Donating blood helps save lives. Donate now!', 0, '2022-11-16 11:30:00', 1,'.//src//assets//images//krv.png')
	INSERT [dbo].[Newses] ([Id], [Title], [Text], [Status], [DateTime], [BloodBankId], [Image]) VALUES (2, 'Scheduling promotion', 'Scheduling appointments in the next month is 50% off!', 0, '2022-11-16 11:30:00', 1,'.//src//assets//images//schedule.png')
	INSERT [dbo].[Newses] ([Id], [Title], [Text], [Status], [DateTime], [BloodBankId], [Image]) VALUES (3, 'New hospital', 'We have oppened a new clinic in Novi Sad!', 0, '2022-11-16 11:30:00', 1,'.//src//assets//images//ns.png')
SET IDENTITY_INSERT [dbo].[Newses] OFF
