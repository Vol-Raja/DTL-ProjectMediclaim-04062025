USE [DTLPension]
GO

/****** Object:  Table [dbo].[ContactUs]    Script Date: 17-10-2022 5.35.21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContactUs](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[NameHindi] [nvarchar](250) NULL,
	[Designation] [nvarchar](250) NULL,
	[DesignationHindi] [nvarchar](250) NULL,
	[EmailAddress] [nvarchar](250) NULL,
	[Telephone] [nvarchar](250) NULL,
	[PhoneNumber] [nvarchar](250) NULL,
	[IsDeleted] [bit] NULL,
	 
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	 
 CONSTRAINT [PK_ContactUs] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ContactUs] ADD  CONSTRAINT [DF_ContactUs_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[ContactUs] ADD  CONSTRAINT [DF_ContactUs_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


