USE [HospitalDb]
GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

INSERT [dbo].[Addresses] ([Id], [Street], [Number], [City], [Township], [PostCode], [Deleted]) VALUES (5, N'A', N'1a', N'A', N'A', N'21000', 0)
INSERT [dbo].[Addresses] ([Id], [Street], [Number], [City], [Township], [PostCode], [Deleted]) VALUES (6, N'A', N'1a', N'A', N'A', N'21000', 0)
INSERT [dbo].[Addresses] ([Id], [Street], [Number], [City], [Township], [PostCode], [Deleted]) VALUES (7, N'A', N'1a', N'A', N'A', N'21000', 0)
INSERT [dbo].[Addresses] ([Id], [Street], [Number], [City], [Township], [PostCode], [Deleted]) VALUES (8, N'A', N'1a', N'A', N'A', N'21000', 0)
INSERT [dbo].[Addresses] ([Id], [Street], [Number], [City], [Township], [PostCode], [Deleted]) VALUES (9, N'A', N'1a', N'A', N'A', N'21000', 0)
INSERT [dbo].[Addresses] ([Id], [Street], [Number], [City], [Township], [PostCode], [Deleted]) VALUES (10, N'A', N'1a', N'A', N'A', N'21000', 0)
INSERT [dbo].[Addresses] ([Id], [Street], [Number], [City], [Township], [PostCode], [Deleted]) VALUES (11, N'A', N'1a', N'A', N'A', N'21000', 0)
INSERT [dbo].[Addresses] ([Id], [Street], [Number], [City], [Township], [PostCode], [Deleted]) VALUES (12, N'A', N'2', N'Aa', N'Aa', N'21000', 0)
INSERT [dbo].[Addresses] ([Id], [Street], [Number], [City], [Township], [PostCode], [Deleted]) VALUES (13, N'A', N'2', N'Aa', N'Aa', N'21000', 0)
INSERT [dbo].[Addresses] ([Id], [Street], [Number], [City], [Township], [PostCode], [Deleted]) VALUES (14, N'A', N'2', N'Aa', N'Aa', N'21000', 0)
SET IDENTITY_INSERT [dbo].[Addresses] OFF
GO
SET IDENTITY_INSERT [dbo].[Persons] ON 

INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (1, N'Pera', N'Peric', N'pera@gmail.com', 5, 0, CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), 2, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (2, N'Marko', N'Markovic', N'marko@gmail.com', 6, 0, CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), 0, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (3, N'Janko', N'Jankovic', N'janko@gmail.com', 7, 0, CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), 0, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (4, N'Milan', N'Milankovic', N'milan@gmail.com', 8, 0, CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), 0, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (5, N'Milica', N'Micic', N'milica@gmail.com', 9, 1, CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), 0, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (6, N'Dragana', N'Dragic', N'dragana@gmail.com', 10, 1, CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), 0, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (7, N'Jovana', N'Jovanovic', N'jovana@gmail.com', 11, 1, CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), 0, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (8, N'Ana', N'Anic', N'ana@gmail.com', 12, 1, CAST(N'2022-08-09T00:00:00.0000000' AS DateTime2), 1, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (9, N'Nevena', N'Nevic', N'nevena@gmail.com', 13, 1, CAST(N'2022-08-09T00:00:00.0000000' AS DateTime2), 1, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (10, N'Milos', N'Milic', N'milos@gmail.com', 14, 0, CAST(N'2022-08-09T00:00:00.0000000' AS DateTime2), 1, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (11, N'Nada', N'Nadic', N'nada@gmail.com', 10, 1, CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), 1, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (12, N'Mikica', N'Mikicovic', N'mikica@gmail.com', 11, 0, CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), 1, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (13, N'Tara', N'Taric', N'tara@gmail.com', 12, 1, CAST(N'2022-08-09T00:00:00.0000000' AS DateTime2), 1, 0)
INSERT [dbo].[Persons] ([Id], [Name], [Surname], [Email_Adress], [AddressId], [Gender], [BirthDate], [Role], [Deleted]) VALUES (14, N'Novica', N'Novicic', N'novica@gmail.com', 13, 0, CAST(N'2022-08-09T00:00:00.0000000' AS DateTime2), 1, 0)

SET IDENTITY_INSERT [dbo].[Persons] OFF
GO
SET IDENTITY_INSERT [dbo].[Allergies] ON 

INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (1, N'Polen', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (2, N'Prasina', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (3, N'Pas', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (4, N'Macka', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (5, N'Pcela', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (6, N'Ambrozija', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (7, N'Kikiriki', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (8, N'Gluten', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (9, N'Laktoza', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (10, N'Alergija10', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (11, N'Alergija11', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (12, N'Alergija12', 0)
INSERT [dbo].[Allergies] ([Id], [Name], [Deleted]) VALUES (13, N'Alergija13', 0)
SET IDENTITY_INSERT [dbo].[Allergies] OFF
GO
SET IDENTITY_INSERT [dbo].[Doctors] ON 

INSERT [dbo].[Doctors] ([Id], [Specialization], [PersonId], [Deleted]) VALUES (1, 0, 2, 0)
INSERT [dbo].[Doctors] ([Id], [Specialization], [PersonId], [Deleted]) VALUES (2, 0, 3, 0)
INSERT [dbo].[Doctors] ([Id], [Specialization], [PersonId], [Deleted]) VALUES (3, 0, 4, 0)
INSERT [dbo].[Doctors] ([Id], [Specialization], [PersonId], [Deleted]) VALUES (4, 0, 5, 0)
INSERT [dbo].[Doctors] ([Id], [Specialization], [PersonId], [Deleted]) VALUES (5, 0, 6, 0)
INSERT [dbo].[Doctors] ([Id], [Specialization], [PersonId], [Deleted]) VALUES (6, 0, 7, 0)
SET IDENTITY_INSERT [dbo].[Doctors] OFF
GO
SET IDENTITY_INSERT [dbo].[Patients] ON 

INSERT [dbo].[Patients] ([Id], [BloodType], [PersonId], [DoctorId], [Deleted]) VALUES (1, 0, 8, 1, 0)
INSERT [dbo].[Patients] ([Id], [BloodType], [PersonId], [DoctorId], [Deleted]) VALUES (2, 0, 9, 1, 0)
INSERT [dbo].[Patients] ([Id], [BloodType], [PersonId], [DoctorId], [Deleted]) VALUES (3, 0, 10, 2, 0)
INSERT [dbo].[Patients] ([Id], [BloodType], [PersonId], [DoctorId], [Deleted]) VALUES (4, 1, 11, 1, 0)
INSERT [dbo].[Patients] ([Id], [BloodType], [PersonId], [DoctorId], [Deleted]) VALUES (5, 2, 12, 1, 0)
INSERT [dbo].[Patients] ([Id], [BloodType], [PersonId], [DoctorId], [Deleted]) VALUES (6, 3, 13, 2, 0)
INSERT [dbo].[Patients] ([Id], [BloodType], [PersonId], [DoctorId], [Deleted]) VALUES (7, 0, 14, 2, 0)

SET IDENTITY_INSERT [dbo].[Patients] OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'58f7fb5f-09dc-4bee-813f-4e47940650c4', N'Manager', N'MANAGER', N'76feb459-393f-45d5-be10-46df582dd946')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'93000374-46d9-44ef-ac96-6db416a28770', N'Doctor', N'DOCTOR', N'9a23c8cc-acc0-4b5a-a247-3f5298277b76')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'd20bd231-b1fe-477b-be40-e4e38be41762', N'Patient', N'PATIENT', N'bb721e26-c59c-4ab5-be3e-63940807d492')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'16630510-68af-400b-918b-8d72e0a1fcab', N'milica', N'MILICA', N'milica@gmail.com', N'MILICA@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEOv41QzJaDJ5FjhdPJsX7H9tBw+6IuQyC6QOcfbOWzMQ4eq5sUKB1ycSBjWPMDj21A==', N'VCMAUXWI2NIUUED5XFA3BZSKI3PTEJFA', N'0deb28c5-88b3-4bcb-aa97-9d4be97c9f01', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'28138a9e-fefc-49da-93d2-d71e02216e8c', N'dragana', N'DRAGANA', N'dragana@gmail.com', N'DRAGANA@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEPtgIoIqeWwde8LEfknpuKQ5wa5JGsEqkLIjHkuw1gMnTezpa4lELzXY/rfgViU7mA==', N'GWLENLN75TXBR2LHEXELFSM4G6LU6H3T', N'63493bcd-67c2-4f10-8369-84631fef0dc0', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'364f540b-b357-4bbd-808f-7f429c76031f', N'janko', N'JANKO', N'janko@gmail.com', N'JANKO@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAELwzSZlXeoxO15fKW6nL+WuGTfghG4UwdTeNRo8nW9r/q3IpPWjcFOZOkdUF+MPEhw==', N'LEJRYF6F6LOICMY75PQQPH56GCDKZFHU', N'bb97593c-2676-4a15-8f87-bd939e2edf64', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'78309e36-da31-41ee-b9a4-bfa8351ceac6', N'milos', N'MILOS', N'milos@gmail.com', N'MILOS@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEAlG5cVyFG2Bi9NwvqhmwnfZWRzw3Kh+aoH8rhVrNWG6TK8DAVgvhdsZ1n1wzLzQbA==', N'EWBMLHK44TLBW675KO3A4CIMLAZYQCTU', N'1f607f87-1ae8-4fbe-aa41-be97e1b915e4', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'84c97761-e58a-4fdc-ab43-c2c3fd099066', N'milan', N'MILAN', N'milan@gmail.com', N'MILAN@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEOMN52pRJzxDxqlhMrYz8iYa9FlPRciBOQd/ukFTSwBsZdZ9uQ590CpZo/S5cul0fg==', N'O3ZVE5MGGUNGRX3ZWSIBAGHCZ5A2OYMP', N'a2ae57be-9fc0-4cde-96e8-61e7151aabc0', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b9eb173b-ab50-434e-88ac-75950696ecaf', N'jovana', N'JOVANA', N'jovana@gmail.com', N'JOVANA@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEJFuC8cEIz3crGPQ4dACeiodrti0xyk05Q9luniTkaqfhwul/aOqkq92guAvbnN57g==', N'YJAGTM2BXJ7EYS76ZXFYKZXZQ5JJIV5I', N'66c44794-6fee-4bb8-a675-abc45c5504e7', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'bcdd59c6-9ec3-42b3-a196-6ccdda4f06cc', N'pera', N'PERA', N'pera@gmail.com', N'PERA@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEHMCuQqkPmsjAzSOnkRVcihtbPTUUZL7Y0jz8o4flDycU60eAMVfUP+SxPe8xZQDJQ==', N'W3EUS2BHBTUOUMHZX2HASSY4LMR6HPSJ', N'4b5ed517-ba39-4540-98aa-5a7d49f03f6d', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'dff84bd5-c5dc-4205-99f7-0fb4e174e2ab', N'nevena', N'NEVENA', N'nevena@gmail.com', N'NEVENA@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEC9/DhP55BxYyGUWuAW5q4uf3nRrARqZIXmUNqZLi2NlVFAjcPfl5VxMvmK1exRqZQ==', N'B6CE6L7XTNT3U53JGTDJANTMRQTG6TM7', N'9ecfa6c4-38b6-481b-a9ff-4ca6064b3e1f', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e75222d6-a345-4d3f-a394-56fcab7a9eef', N'marko', N'MARKO', N'marko@gmail.com', N'MARKO@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEEapcOaqmCyvIxxTEU38/dg5aKlNTtwRDNdMrllGDoAKQrd3pCeQr4UXFvDDpd/cRw==', N'IC27RMZ2HRRGYNTBCYXFDIOGM34KQDJM', N'8abd6e3f-0de8-445f-9c39-b543a42e5702', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ff7017b3-2807-4004-bf9d-8342ecd0c774', N'ana', N'ANA', N'ana@gmail.com', N'ANA@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEOhRhhUyOLs1WuvXjzIyzQqn2pprBcoAI/d/+i/tKvs5/LvyAoaX9SuQWFf/3kjNNw==', N'BJX6MGB7DMKTK54GDK7RJXMSTUJTCPBU', N'fff8a30f-dbb7-43f8-aeb5-fbd32b0bebb2', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bcdd59c6-9ec3-42b3-a196-6ccdda4f06cc', N'58f7fb5f-09dc-4bee-813f-4e47940650c4')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'16630510-68af-400b-918b-8d72e0a1fcab', N'93000374-46d9-44ef-ac96-6db416a28770')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'28138a9e-fefc-49da-93d2-d71e02216e8c', N'93000374-46d9-44ef-ac96-6db416a28770')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'364f540b-b357-4bbd-808f-7f429c76031f', N'93000374-46d9-44ef-ac96-6db416a28770')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'84c97761-e58a-4fdc-ab43-c2c3fd099066', N'93000374-46d9-44ef-ac96-6db416a28770')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b9eb173b-ab50-434e-88ac-75950696ecaf', N'93000374-46d9-44ef-ac96-6db416a28770')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e75222d6-a345-4d3f-a394-56fcab7a9eef', N'93000374-46d9-44ef-ac96-6db416a28770')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'78309e36-da31-41ee-b9a4-bfa8351ceac6', N'd20bd231-b1fe-477b-be40-e4e38be41762')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'dff84bd5-c5dc-4205-99f7-0fb4e174e2ab', N'd20bd231-b1fe-477b-be40-e4e38be41762')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ff7017b3-2807-4004-bf9d-8342ecd0c774', N'd20bd231-b1fe-477b-be40-e4e38be41762')
GO
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 

INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'bcdd59c6-9ec3-42b3-a196-6ccdda4f06cc', N'UserId', N'1')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (2, N'e75222d6-a345-4d3f-a394-56fcab7a9eef', N'UserId', N'2')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (3, N'364f540b-b357-4bbd-808f-7f429c76031f', N'UserId', N'3')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (4, N'84c97761-e58a-4fdc-ab43-c2c3fd099066', N'UserId', N'4')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (5, N'16630510-68af-400b-918b-8d72e0a1fcab', N'UserId', N'5')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (6, N'28138a9e-fefc-49da-93d2-d71e02216e8c', N'UserId', N'6')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (7, N'b9eb173b-ab50-434e-88ac-75950696ecaf', N'UserId', N'7')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (8, N'ff7017b3-2807-4004-bf9d-8342ecd0c774', N'UserId', N'8')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (9, N'dff84bd5-c5dc-4205-99f7-0fb4e174e2ab', N'UserId', N'9')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (10, N'78309e36-da31-41ee-b9a4-bfa8351ceac6', N'UserId', N'10')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221105154724_AddIdentity', N'5.0.17')
GO
SET IDENTITY_INSERT [dbo].[Rooms] ON 

INSERT [dbo].[Rooms] ([Id], [Number], [RoomType], [Floor], [Deleted]) VALUES (1, N'101A', 5,  1, 0)
INSERT [dbo].[Rooms] ([Id], [Number], [RoomType], [Floor], [Deleted]) VALUES (2, N'204', 5, 2, 0)
INSERT [dbo].[Rooms] ([Id], [Number], [RoomType], [Floor], [Deleted]) VALUES (3, N'305B', 5, 3, 0)
INSERT [dbo].[Rooms] ([Id], [Number], [RoomType], [Floor], [Deleted]) VALUES (4, N'STORAGE', 0, 3, 0)
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO
SET IDENTITY_INSERT [dbo].[WorkingDays] ON 

INSERT [dbo].[WorkingDays] ([Id], [DayOfWeek], [Day], [StartTime], [EndTime], [UserId], [Deleted]) VALUES (1, 10, 0, '2022-11-16 11:00:00', '2022-11-16 11:00:00', 3, 0)
SET IDENTITY_INSERT [dbo].[WorkingDays] OFF
GO
SET IDENTITY_INSERT [dbo].[Appointments] ON 

INSERT [dbo].[Appointments] ([Id], [PatientId], [DoctorId], [DateTime], [Deleted]) VALUES (1, 3, 2, '2022-11-16 11:30:00', 0)
SET IDENTITY_INSERT [dbo].[Appointments] OFF
GO
SET IDENTITY_INSERT [dbo].[Medicines] ON 

INSERT [dbo].[Medicines] ([Id], [Name], [Quantity], [RoomId], [Deleted]) VALUES (1, 'Brufen', 12, 4, 0)
INSERT [dbo].[Medicines] ([Id], [Name], [Quantity], [RoomId], [Deleted]) VALUES (2, 'Aspirin', 20, 4, 0)
INSERT [dbo].[Medicines] ([Id], [Name], [Quantity], [RoomId], [Deleted]) VALUES (3, 'Dexomen', 10, 4, 0)
INSERT [dbo].[Medicines] ([Id], [Name], [Quantity], [RoomId], [Deleted]) VALUES (4, 'Robenan', 5, 4, 0)
INSERT [dbo].[Medicines] ([Id], [Name], [Quantity], [RoomId], [Deleted]) VALUES (5, 'Fervex', 1, 4, 0)
SET IDENTITY_INSERT [dbo].[Medicines] OFF
GO
SET IDENTITY_INSERT [dbo].[Bloods] ON 


INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (1, 0, 11, 1, 0)
INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (2, 2, 3, 1, 0)
INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (3, 1, 10, 2, 0)
INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (4, 3, 5, 3, 0)

INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (5, 0, 1000, 4, 0)
INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (6, 1, 1000, 4, 0)
INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (7, 2, 1000, 4, 0)
INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (8, 3, 1000, 4, 0)
INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (9, 4, 1000, 4, 0)
INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (10, 5, 1000, 4, 0)
INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (11, 6, 1000, 4, 0)
INSERT [dbo].[Bloods] ([Id], [BloodType], [Quantity], [RoomId], [Deleted]) VALUES (12, 7, 1000, 4, 0)

SET IDENTITY_INSERT [dbo].[Bloods] OFF
GO
SET IDENTITY_INSERT [dbo].[Therapys] ON 

INSERT [dbo].[Therapys] ([Id], [MedicineId], [BloodId], [QuantitytMedicine], [QuantityBlood], [Deleted]) VALUES (1, 2, 3, 2, 1, 0)
SET IDENTITY_INSERT [dbo].[Therapys] OFF
GO
SET IDENTITY_INSERT [dbo].[Beds] ON 

INSERT [dbo].[Beds] ([Id], [Name], [BedState], [PatientId], [Quantity], [RoomId], [Deleted]) VALUES (1, 'Dekubitalni', 1, NULL, 10, 1, 0)
INSERT [dbo].[Beds] ([Id], [Name], [BedState], [PatientId], [Quantity], [RoomId], [Deleted]) VALUES (2, 'Dormeo', 0, NULL, 5, 1, 0)
INSERT [dbo].[Beds] ([Id], [Name], [BedState], [PatientId], [Quantity], [RoomId], [Deleted]) VALUES (3, 'Intenzivni', 1, NULL, 3, 1, 0)
INSERT [dbo].[Beds] ([Id], [Name], [BedState], [PatientId], [Quantity], [RoomId], [Deleted]) VALUES (4, 'Poluintenzivni', 0, 3, 6, 2, 0)
INSERT [dbo].[Beds] ([Id], [Name], [BedState], [PatientId], [Quantity], [RoomId], [Deleted]) VALUES (5, 'Invalidski', 1, NULL, 8, 2, 0)
INSERT [dbo].[Beds] ([Id], [Name], [BedState], [PatientId], [Quantity], [RoomId], [Deleted]) VALUES (6, 'Dekubitalni', 1, NULL, 10, 2, 0)
INSERT [dbo].[Beds] ([Id], [Name], [BedState], [PatientId], [Quantity], [RoomId], [Deleted]) VALUES (7, 'Dormeo', 0, 1, 4, 3, 0)
INSERT [dbo].[Beds] ([Id], [Name], [BedState], [PatientId], [Quantity], [RoomId], [Deleted]) VALUES (8, 'Intenzivni', 1, NULL, 3, 3, 0)
INSERT [dbo].[Beds] ([Id], [Name], [BedState], [PatientId], [Quantity], [RoomId], [Deleted]) VALUES (9, 'Poluintenzivni', 1, NULL, 6, 3, 0)
INSERT [dbo].[Beds] ([Id], [Name], [BedState], [PatientId], [Quantity], [RoomId], [Deleted]) VALUES (10, 'Invalidski', 1, NULL, 8, 3, 0)

SET IDENTITY_INSERT [dbo].[Beds] OFF
GO
SET IDENTITY_INSERT [dbo].[Treatments] ON 

INSERT [dbo].[Treatments] ([Id], [PatientId], [DateAdmission], [DateDischarge], [ReasonForAdmission], [ReasonForDischarge], [TreatmentState], [TherapyId], [RoomId], [Deleted]) VALUES (1, 2, '2022-11-16 11:30:00', '2022-11-16 13:00:00', 'Bol u grlu', 'Pacijent se oseca bolje', 1, 1, 1, 0)
SET IDENTITY_INSERT [dbo].[Treatments] OFF
GO