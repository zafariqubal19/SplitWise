USE [SplitWiseDB]
GO

/****** Object:  Table [dbo].[GROUPS]    Script Date: 1/2/2024 12:54:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GROUPS](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NOT NULL,
	[CreatorEmail] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GROUPS]  WITH CHECK ADD FOREIGN KEY([CreatorEmail])
REFERENCES [dbo].[USERS] ([Email])
GO
--=================================================================================---------------

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[USERS](
	[Email] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 1/2/2024 12:55:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetUserById]

@Email nvarchar(50)
AS
BEGIN 
SELECT * FROM Users WHERE Email=@Email
END

/****** Object:  StoredProcedure [dbo].[IdentifyUser]    Script Date: 1/2/2024 12:55:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[IdentifyUser]
@Email nvarchar(50),
@Password nvarchar(50)
AS 
BEGIN
SELECT * FROM USERS WHERE Email=@Email AND Password=@Password
END

/****** Object:  StoredProcedure [dbo].[SP_GetAllSplitWiseUsers]    Script Date: 1/2/2024 12:56:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_GetAllSplitWiseUsers]
AS 
BEGIN
SELECT * FROM Users
END

/****** Object:  StoredProcedure [dbo].[sp_InsertSplitwiseUser]    Script Date: 1/2/2024 12:56:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_InsertSplitwiseUser]        
        
@Email nvarchar(50),   
@Name nvarchar(50),  
@PhoneNUmber nvarchar(50),    
@Password nvarchar(50)    
AS        
BEGIN        
INSERT INTO Users (Email,Name,PhoneNumber,Password)   VALUES (@Email,@Name,@PhoneNUmber,@Password)        
END

